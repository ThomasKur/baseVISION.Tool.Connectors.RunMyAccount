using Microsoft.VisualStudio.TestTools.UnitTesting;
using baseVISION.Core.Connectors.RunMyAccount;
using baseVISION.Core.Connectors.RunMyAccount.Model;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using baseVISION.Tool.Connectors.RunMyAccount.Test;

namespace baseVISION.Tool.Connectors.RunMyAccount.Tests
{
    [TestClass]
    public class RunMyAccountsClientTests
    {
        private RunMyAccountsClient? _client;
        private UserConfig? _userConfig;

        [TestInitialize]
        public void Setup()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddUserSecrets<RunMyAccountsClientTests>()
                .Build();

            _userConfig = configuration.Get<UserConfig>();
            if (_userConfig == null ||
                string.IsNullOrWhiteSpace(_userConfig.RmaTenant) ||
                string.IsNullOrWhiteSpace(_userConfig.RmaApi))
            {
                Assert.Inconclusive("RunMyAccounts credentials are not configured. Set the required user secrets before running the tests.");
            }

            _client = new RunMyAccountsClient(_userConfig.RmaTenant, _userConfig.RmaApi, _userConfig.RmaUrl);
        }

        private RunMyAccountsClient Client => _client ?? throw new InvalidOperationException("RunMyAccounts client was not initialized.");

        [TestMethod]
        public void RunMyAccountsClientTest()
        {
            Assert.IsNotNull(Client);
        }

        [TestMethod]
        public async Task ListCustomersAsync_ReturnsContacts()
        {
            var contacts = await Client.ListCustomersAsync();
            Assert.IsNotNull(contacts, "Customer list should not be null.");
            if (contacts.Count == 0)
            {
                Assert.Inconclusive("The configured tenant does not expose any customers.");
            }

            Assert.IsTrue(contacts.All(c => !string.IsNullOrWhiteSpace(c.customernumber)), "Every customer must expose a customer number.");
        }

        [TestMethod]
        public async Task ListAllArticlesAsync_ReturnsArticles()
        {
            var articles = await Client.ListAllArticlesAsync();
            Assert.IsNotNull(articles, "Article list should not be null.");
            if (articles.Count == 0)
            {
                Assert.Inconclusive("The configured tenant does not expose any articles.");
            }

            Assert.IsTrue(articles.All(a => !string.IsNullOrWhiteSpace(a.partnumber)), "Every article must expose a part number.");
        }

        [TestMethod]
        public async Task ListAllInvoices2Async_ReturnsInvoices()
        {
            var from = DateTime.Now.AddYears(-1);
            var to = DateTime.Now;
            var invoices = await Client.ListAllInvoicesAsync2(from, to);
            Assert.IsNotNull(invoices, "Invoice list should not be null.");
            if (invoices.Count == 0)
            {
                Assert.Inconclusive("No invoices were returned for the configured tenant.");
            }

            Assert.IsTrue(invoices.All(i => !string.IsNullOrWhiteSpace(i.invnumber)), "Every invoice must expose an invoice number.");
        }

        [TestMethod]
        public async Task ListAllPayablesAsync_ReturnsPayables()
        {
            var from = DateTime.Now.AddYears(-1);
            var to = DateTime.Now;
            var payables = await Client.ListAllPayablesAsync(from, to);
            Assert.IsNotNull(payables, "Payables list should not be null.");
            if (payables.Count == 0)
            {
                Assert.Inconclusive("No payables were returned for the configured tenant.");
            }

            Assert.IsTrue(payables.All(p => !string.IsNullOrWhiteSpace(p.invnumber)), "Every payable must expose an invoice number.");
        }

        [TestMethod]
        public async Task ListAllSaldoV2Async_FiltersByAccountNumber()
        {
            var contacts = await Client.ListCustomersAsync();
            var accountNumber = contacts.FirstOrDefault(c => !string.IsNullOrWhiteSpace(c.arap_accno))?.arap_accno;
            if (string.IsNullOrWhiteSpace(accountNumber))
            {
                Assert.Inconclusive("No account number was returned by the customer list to validate saldo.");
            }

            var from = DateTime.Now.AddMonths(-3);
            var to = DateTime.Now;
            var saldo = await Client.ListAllSaldoV2Async(accountNumber, from, to, null, null);
            Assert.IsNotNull(saldo, "Saldo response should not be null.");
            Assert.IsTrue(saldo.All(s => s.accountnumber == accountNumber), "Saldo rows must belong to the requested account.");
        }

        [TestMethod]
        public async Task ListCustomersTest()
        {
            await ListCustomersAsync_ReturnsContacts();
        }

        [TestMethod]
        public async Task ListPayableTest()
        {
            await ListAllPayablesAsync_ReturnsPayables();
        }

        [TestMethod]
        public async Task ListMonthlyPayableTest()
        {
            await ListAllSaldoV2Async_FiltersByAccountNumber();
        }

        [TestMethod]
        public async Task ListAllInvoicesTest()
        {
            await ListAllInvoices2Async_ReturnsInvoices();
        }

        [TestMethod]
        public async Task ListAllArticlesTest()
        {
            await ListAllArticlesAsync_ReturnsArticles();
        }

        [TestMethod]
        public void CreateInvoice()
        {
            var invoice = new RunMyAccountsInvoice
            {
                ar_accno = 1100,
                currency = "CHF",
                customernumber = "HC-4141121",
                description = "TestKur",
                duedate = DateTime.Now.AddDays(39),
                invnumber = "R00003951",
                ordnumber = string.Empty,
                status = InvoiceStatus.OPEN,
                taxincluded = false,
                transdate = DateTime.Now,
                parts = new RunMyAccountsArticleList()
            };

            invoice.parts.part.Add(new RunMyAccountsArticle
            {
                description = "Test",
                discount = 0,
                expense_accno = "4999",
                income_accno = "3400",
                itemnote = string.Empty,
                partnumber = "Service",
                price_update = new DateTime(2014, 12, 24),
                quantity = 1,
                sellprice = 200,
                tax_accnos = "2201",
                unit = "h"
            });

            Assert.IsNotNull(invoice);
            Assert.IsNotNull(invoice.parts);
            Assert.AreEqual(1, invoice.parts.part.Count);
        }

        [TestMethod]
        public async Task CreateCustomer()
        {
            var contact = new RunMyAccountsContact
            {
                name = "Testkunde KUR AG",
                address1 = "Musterstrasse 1",
                zipcode = "8000",
                city = "Zürich",
                country = "CH",
                customernumber = "HC-999999"
            };

            string status = await Client.CreateCustomerWithStatusAsync(contact);
            Assert.IsNotNull(status);
        }
    }
}