


using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium;
//using OpenQA.Selenium.Android;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using Selenium;
using Assert = NUnit.Framework.Assert;

namespace SelGridTests
{
    internal static class Config
    {
        public static TimeSpan m_defaultTimeout;
        public static TimeSpan m_OperaTimeout;
        public static TimeSpan m_SafariTimeout;
        public static string m_AUTUrl;
        public static string m_hubURL;

        static Config()
        {
            m_defaultTimeout = new TimeSpan(0, 0, 10);
            m_SafariTimeout = new TimeSpan(0, 0, 10);
            m_OperaTimeout = new TimeSpan(0, 0, 10);
            m_AUTUrl = "http://www.issoft.by/";
            m_hubURL = "http://127.0.0.1:4444/wd/hub";
        }
    }

    [TestFixture]
    public class IsSoftTest
    {
        private RemoteWebDriver m_webDriver;

        internal enum IsSoftMenuEnum
        {
            ISsoft,
            Сервисы,
            Технологии,
            Работа,
            Вакансии,
            Новости,
            Галереи,
            Нас
        };


        [SetUp]
        public void TestSetup()
        {
        }

        [Test]
        public void IsSoftMenuFFTest()
        {
            // setup desired capabilities and firefox profile for Remote WebDriver
            FirefoxProfile profile = new FirefoxProfile();
            profile.AcceptUntrustedCertificates = true;
            DesiredCapabilities desiredCapabilities = new DesiredCapabilities();
            desiredCapabilities = DesiredCapabilities.Firefox();
            desiredCapabilities.SetCapability(CapabilityType.Platform, "WINDOWS");
            desiredCapabilities.SetCapability("firefox_profile", profile.ToBase64String());
            desiredCapabilities.SetCapability("UnhandledAlertException", "dismiss");
            m_webDriver = new RemoteWebDriver(new Uri(Config.m_hubURL), desiredCapabilities);

            // Notes:Setup WebDriver Timeouts
            m_webDriver.Manage().Timeouts().ImplicitlyWait(Config.m_defaultTimeout);
            m_webDriver.Manage().Timeouts().SetPageLoadTimeout(Config.m_defaultTimeout);
            m_webDriver.Manage().Timeouts().SetScriptTimeout(Config.m_defaultTimeout);

            // Notes:open ISSoft site main page
            m_webDriver.Navigate().GoToUrl(Config.m_AUTUrl);

            // Notes: TestMenuItems
            var currentMenuItem = m_webDriver.FindElement(By.PartialLinkText(IsSoftMenuEnum.ISsoft.ToString()));
            currentMenuItem.Click();
            Assert.IsTrue(string.Equals("http://www.issoft.by/", m_webDriver.Url));
            currentMenuItem = m_webDriver.FindElement(By.PartialLinkText(IsSoftMenuEnum.Сервисы.ToString("G")));
            currentMenuItem.Click();
            Assert.IsTrue(string.Equals("http://www.issoft.by/service-category/nashi-servisy/", m_webDriver.Url));
            currentMenuItem = m_webDriver.FindElement(By.PartialLinkText(IsSoftMenuEnum.Технологии.ToString("G")));
            currentMenuItem.Click();
            Assert.IsTrue(string.Equals("http://www.issoft.by/texnologii/", m_webDriver.Url));
            currentMenuItem = m_webDriver.FindElement(By.PartialLinkText(IsSoftMenuEnum.Работа.ToString("G"))); // TODO: Let's develop our own extension for "IsSoftMenuEnum.Работа.ToString("G")"
            currentMenuItem.Click();
            Assert.IsTrue(string.Equals("http://www.issoft.by/atmosfera-vnutri-kompanii-2/", m_webDriver.Url));
            currentMenuItem = m_webDriver.FindElement(By.PartialLinkText(IsSoftMenuEnum.Вакансии.ToString("G")));
            currentMenuItem.Click();
            Assert.IsTrue(string.Equals("http://www.issoft.by/vakansii-2/", m_webDriver.Url));
            currentMenuItem = m_webDriver.FindElement(By.PartialLinkText(IsSoftMenuEnum.Новости.ToString("G")));
            currentMenuItem = m_webDriver.FindElement(By.PartialLinkText(IsSoftMenuEnum.Галереи.ToString("G")));
            currentMenuItem.Click();
            Assert.IsTrue(CheckMenuItem(IsSoftMenuEnum.Галереи.ToString("G"), "http://www.issoft.by/category/galerei/"));
            currentMenuItem = m_webDriver.FindElement(By.PartialLinkText(IsSoftMenuEnum.Нас.ToString("G")));
            m_webDriver.Quit();

        }
        [Test]
        public void IsSoftMenuSafariTest()
        {
            //Setup desired capabilities for Safari http://code.google.com/p/selenium/wiki/DesiredCapabilities
            var desiredCapabilities = new DesiredCapabilities();
            desiredCapabilities.SetCapability(CapabilityType.BrowserName, "safari");
            desiredCapabilities.SetCapability(CapabilityType.Platform, "WINDOWS");
            desiredCapabilities.SetCapability("UnhandledAlertException", "dismiss");

            //Create WebDriver for Safari
            m_webDriver = new RemoteWebDriver(new Uri(Config.m_hubURL), desiredCapabilities);

            // Set Timeouts 
            Thread.Sleep(3000);
            // m_webDriver.Manage().Timeouts().SetPageLoadTimeout(Config.m_SafariTimeout);
            m_webDriver.Manage().Timeouts().ImplicitlyWait(Config.m_SafariTimeout);
            m_webDriver.Manage().Timeouts().SetScriptTimeout(Config.m_SafariTimeout);
            // Notes:open ISSoft site main page
            m_webDriver.Navigate().GoToUrl(Config.m_AUTUrl);

            Thread.Sleep(900);
            //TestMenuItems
            var currentMenuItem = m_webDriver.FindElement(By.PartialLinkText(IsSoftMenuEnum.ISsoft.ToString("G")));
            currentMenuItem.Click();
            Thread.Sleep(900);
            Assert.IsTrue(string.Equals("http://www.issoft.by/", m_webDriver.Url));
            currentMenuItem = m_webDriver.FindElement(By.PartialLinkText(IsSoftMenuEnum.Сервисы.ToString("G")));
            currentMenuItem.Click();
            Thread.Sleep(900);
            Assert.IsTrue(string.Equals("http://www.issoft.by/service-category/nashi-servisy/", m_webDriver.Url));
            currentMenuItem = m_webDriver.FindElement(By.PartialLinkText(IsSoftMenuEnum.Технологии.ToString("G")));
            currentMenuItem.Click();
            Thread.Sleep(900);
            Assert.IsTrue(string.Equals("http://www.issoft.by/texnologii/", m_webDriver.Url));
            currentMenuItem = m_webDriver.FindElement(By.PartialLinkText(IsSoftMenuEnum.Работа.ToString("G")));
            currentMenuItem.Click();
            Thread.Sleep(900);
            Assert.IsTrue(string.Equals("http://www.issoft.by/atmosfera-vnutri-kompanii-2/", m_webDriver.Url));
            currentMenuItem = m_webDriver.FindElement(By.PartialLinkText(IsSoftMenuEnum.Вакансии.ToString("G")));
            currentMenuItem.Click();
            Thread.Sleep(900);
            Assert.IsTrue(string.Equals("http://www.issoft.by/vakansii-2/", m_webDriver.Url));
            currentMenuItem = m_webDriver.FindElement(By.PartialLinkText(IsSoftMenuEnum.Новости.ToString("G")));
            currentMenuItem = m_webDriver.FindElement(By.PartialLinkText(IsSoftMenuEnum.Галереи.ToString("G")));
            currentMenuItem.Click();
            Assert.IsTrue(CheckMenuItem(IsSoftMenuEnum.Галереи.ToString("G"), "http://www.issoft.by/category/galerei/"));
            currentMenuItem = m_webDriver.FindElement(By.PartialLinkText(IsSoftMenuEnum.Нас.ToString("G")));

            //kill webDriver instance
            m_webDriver.Quit();
        }

        [Test]
        public void IsSoftMenuOperaTest()
        {
            //Setup desired capabilities for Opera http://code.google.com/p/selenium/wiki/DesiredCapabilities
            var desiredCapabilities = new DesiredCapabilities();
            desiredCapabilities.SetCapability(CapabilityType.BrowserName, "opera");
            desiredCapabilities.SetCapability(CapabilityType.Platform, "WINDOWS");
            desiredCapabilities.SetCapability("opera.binary", @"C:\Program Files\Opera x64\opera.exe");

            //Create RemoteWebDriver for Opera
            m_webDriver = new RemoteWebDriver(new Uri(Config.m_hubURL), desiredCapabilities);
            Thread.Sleep(3600);
            // Notes:open ISSoft site main page
            m_webDriver.Navigate().GoToUrl(Config.m_AUTUrl);

            //Test Menu Items
            var currentMenuItem = m_webDriver.FindElement(By.PartialLinkText(IsSoftMenuEnum.ISsoft.ToString("G")));
            currentMenuItem = m_webDriver.FindElement(By.PartialLinkText(IsSoftMenuEnum.ISsoft.ToString("G")));
            currentMenuItem.Click();
            Thread.Sleep(900);
            Assert.IsTrue(string.Equals("http://www.issoft.by/", m_webDriver.Url));
            currentMenuItem = m_webDriver.FindElement(By.PartialLinkText(IsSoftMenuEnum.Сервисы.ToString("G")));
            currentMenuItem.Click();
            Thread.Sleep(900);
            Assert.IsTrue(string.Equals("http://www.issoft.by/service-category/nashi-servisy/", m_webDriver.Url));
            currentMenuItem = m_webDriver.FindElement(By.PartialLinkText(IsSoftMenuEnum.Технологии.ToString("G")));
            currentMenuItem.Click();
            Thread.Sleep(900);
            Assert.IsTrue(string.Equals("http://www.issoft.by/texnologii/", m_webDriver.Url));
            currentMenuItem = m_webDriver.FindElement(By.PartialLinkText(IsSoftMenuEnum.Работа.ToString("G")));
            currentMenuItem.Click();
            Thread.Sleep(900);
            Assert.IsTrue(string.Equals("http://www.issoft.by/atmosfera-vnutri-kompanii-2/", m_webDriver.Url));
            currentMenuItem = m_webDriver.FindElement(By.PartialLinkText(IsSoftMenuEnum.Вакансии.ToString("G")));
            currentMenuItem.Click();
            Thread.Sleep(900);
            Assert.IsTrue(string.Equals("http://www.issoft.by/vakansii-2/", m_webDriver.Url));
            currentMenuItem = m_webDriver.FindElement(By.PartialLinkText(IsSoftMenuEnum.Новости.ToString("G")));
            currentMenuItem = m_webDriver.FindElement(By.PartialLinkText(IsSoftMenuEnum.Галереи.ToString("G")));
            currentMenuItem.Click();
            Assert.IsTrue(CheckMenuItem(IsSoftMenuEnum.Галереи.ToString("G"), "http://www.issoft.by/category/galerei/"));
            currentMenuItem = m_webDriver.FindElement(By.PartialLinkText(IsSoftMenuEnum.Нас.ToString("G")));

            // kill WebDriver instance
            m_webDriver.Quit();
        }

        public Boolean CheckMenuItem(string menuItemName, string pageUrl)
        {
            var currentMenuItem = m_webDriver.FindElement(By.PartialLinkText(menuItemName));
            currentMenuItem.Click();
            Thread.Sleep(900);
            return string.Equals(pageUrl, m_webDriver.Url);
        }


        [Test]
        public void IsSoftMenuSlenium1Test()
        {   //verify ISSoft site Menu Items using SeleniumRC and Firefox
            var selenium = new DefaultSelenium("localhost", 4444, "*firefox", Config.m_AUTUrl);
            selenium.Start();
            selenium.Open(Config.m_AUTUrl);
            selenium.WaitForPageToLoad("30000");
            selenium.Click("//a[contains(text(),'Технологии')]");
            selenium.WaitForPageToLoad("300");
            selenium.Click("//a[contains(text(),'Вакансии')]");
            selenium.WaitForPageToLoad("300");
            selenium.Click("//a[contains(text(),'Сервисы')]");
            selenium.Click("//a[contains(text(),'О Нас')]");

            selenium.Stop();
        }



        [TearDown]
        public void TestCleanup()
        {

        }

    }
}
