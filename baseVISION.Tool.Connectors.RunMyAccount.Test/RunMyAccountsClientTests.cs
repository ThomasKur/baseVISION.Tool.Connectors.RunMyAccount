using Microsoft.VisualStudio.TestTools.UnitTesting;
using baseVISION.Core.Connectors.RunMyAccount;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using baseVISION.Tool.Connectors.RunMyAccount.Test;
using baseVISION.Core.Connectors.RunMyAccount.Model;

namespace baseVISION.Tool.Connectors.RunMyAccount.Tests
{
    [TestClass()]
    public class RunMyAccountsClientTests
    {
        RunMyAccountsClient RmaCon = null;

        public void Initialize()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddUserSecrets<RunMyAccountsClientTests>()
                .Build();

            UserConfig uc = config.Get<UserConfig>();
            RmaCon = new RunMyAccountsClient(uc.RmaTenant, uc.RmaApi, uc.RmaUrl);
        }

        [TestMethod()]
        public void RunMyAccountsClientTest()
        {
            try
            {
                Initialize();
            }
            catch (Exception e)
            {
                Assert.Fail();
            }
            
        }

        [TestMethod()]
        public void ListCustomersTest()
        {

            try
            {
                Initialize();

                var cus = RmaCon.ListCustomers();
                Assert.IsTrue(cus.Count > 1);
                
            }
            catch (Exception e)
            {
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void ListPayableTest()
        {

            try
            {
                Initialize();

                var cus = RmaCon.ListAllPayables(DateTime.Now.AddYears(-1),null);
                Assert.IsTrue(cus.Count > 1);

            }
            catch (Exception e)
            {
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void ListMonthlyPayableTest()
        {

            try
            {
                Initialize();
                int i = 1;
                DateTime from = new DateTime(DateTime.Now.AddMonths(-i).Year, DateTime.Now.AddMonths(-i).Month, 1);
                DateTime to = from.AddMonths(1).AddDays(-1);
                var cus = RmaCon.ListAllSaldoV2("xxxx", from, to, null, "SOC", false);
                var cus2 = RmaCon.ListAllSaldoV2("xxxx", from, to, null, "", false);
                var cus3 = RmaCon.ListAllSaldoV2("xxxx", from, to, null, "General", false);
                Assert.IsTrue(cus.Count > 1);

            }
            catch (Exception e)
            {
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void ListAllInvoicesTest()
        {

            try
            {
                Initialize();

                var cus = RmaCon.ListAllInvoices2(DateTime.Now.AddYears(-1), null);
                Assert.IsTrue(cus.Count > 1);
             
            }
            catch (Exception e)
            {
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void ListAllArticlesTest()
        {

            try
            {
                Initialize();

                var cus = RmaCon.ListAllArticles();
                Assert.IsTrue(cus.Count > 1);

            }
            catch (Exception e)
            {
                Assert.Fail();
            }
        }
        [TestMethod()]
        public void CreateInvoice()
        {

            try
            {
                Initialize();
                RunMyAccountsInvoice i = new RunMyAccountsInvoice();
                i.ar_accno = 1100;
                i.currency = "CHF";
                i.customernumber = "HC-4141121";
                i.description = "TestKur";
                i.duedate = DateTime.Now.AddDays(39);
                i.invnumber = "R00003951";
                i.ordnumber = "";
                i.status = InvoiceStatus.OPEN;
                i.taxincluded = false;
                i.transdate = DateTime.Now;
                i.parts = new RunMyAccountsArticleList();
                i.parts.part.Add(new RunMyAccountsArticle() { description = "Test", discount = 0, expense_accno = "4999", income_accno = "3400", itemnote = "", partnumber = "Service", price_update = new DateTime(2014, 12, 24), quantity = 1, sellprice = 200, tax_accnos = "2201", unit = "h" });


// string result = RmaCon.CreateInvoiceWithStatus(i);
               // Assert.IsNotNull(result);

            }
            catch (Exception e)
            {
                Assert.Fail();
            }
        }


    }
}