using Microsoft.VisualStudio.TestTools.UnitTesting;
using baseVISION.Tool.Connectors.RunMyAccount;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using baseVISION.Tool.Connectors.RunMyAccount.Test;

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
        public void ListAllInvoicesTest()
        {

            try
            {
                Initialize();

                var cus = RmaCon.ListAllInvoices();
                Assert.IsTrue(cus.Count > 1);
                cus = RmaCon.ListAllInvoices();
                Assert.IsTrue(cus.Count > 1);
                cus = RmaCon.ListAllInvoices();
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
                Model.RunMyAccountsInvoice i = new Model.RunMyAccountsInvoice();
                i.ar_accno = 1100;
                i.currency = "CHF";
                i.customernumber = "HC-4141121";
                i.description = "TestKur";
                i.duedate = DateTime.Now.AddDays(39);
                i.invnumber = "R00003951";
                i.ordnumber = "";
                i.status = Model.InvoiceStatus.OPEN;
                i.taxincluded = false;
                i.transdate = DateTime.Now;
                i.parts = new Model.RunMyAccountsArticleList();
                i.parts.part.Add(new Model.RunMyAccountsArticle() { description = "Test", discount = 0, expense_accno = "4999", income_accno = "3400", itemnote = "", partnumber = "Service", price_update = new DateTime(2014, 12, 24), quantity = 1, sellprice = 200, tax_accnos = "2201", unit = "h" });


                string result = RmaCon.CreateInvoice(i);
                Assert.IsNotNull(result);

            }
            catch (Exception e)
            {
                Assert.Fail();
            }
        }


    }
}