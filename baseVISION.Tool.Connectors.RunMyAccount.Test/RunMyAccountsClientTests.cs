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
    }
}