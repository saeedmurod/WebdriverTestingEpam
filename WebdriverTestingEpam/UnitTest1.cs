
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using static System.Net.WebRequestMethods;

namespace WebdriverTestingEpam
{
    [TestClass]
    public class UnitTest1
    {
        readonly string test_url = "https://accounts.google.com/v3/signin/identifier?authuser=0&continue=https%3A%2F%2Fmail.google.com&ec=GAlAFw&hl=en&service=mail&flowName=GlifWebSignIn&flowEntry=AddSession&dsh=S1678429287%3A1696101771657405&theme=glif";
        readonly string expected = "Inbox";


        [TestMethod]
        public void Login()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();

            driver.Navigate().GoToUrl(test_url);

            driver.Manage().Window.Maximize();

            IWebElement emailLogin = driver.FindElement(By.XPath("//*[@id=\"identifierId\"]"));
            emailLogin.SendKeys("saidmurodtestepam@gmail.com");

            IWebElement nextLoginButton = driver.FindElement(By.XPath("//*[@id=\"identifierNext\"]/div/button/span"));
            nextLoginButton.Click();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);


            IWebElement password = driver.FindElement(By.XPath("//*[@id=\"password\"]/div[1]/div/div[1]/input"));
            password.SendKeys("x@iysu27");

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
            IWebElement nextPasswordButton = driver.FindElement(By.XPath("//*[@id=\"passwordNext\"]/div/button/span"));
            nextPasswordButton.Click();

            IWebElement inboxActual = driver.FindElement(By.XPath("//a[text()='Inbox']"));


            Assert.AreEqual(expected, inboxActual.Text); 

            driver.Quit();
        }


        [TestMethod]
        public void LoginChangeName()
        {

            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();

            driver.Navigate().GoToUrl(test_url);

            Actions actions = new Actions(driver);


            IWebElement emailLogin = driver.FindElement(By.XPath("//*[@id=\"identifierId\"]"));
            emailLogin.SendKeys("saidmurodtestepam@gmail.com");

            IWebElement nextLoginButton = driver.FindElement(By.XPath("//*[@id=\"identifierNext\"]/div/button/span"));
            nextLoginButton.Click();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);


            IWebElement pass = driver.FindElement(By.XPath("//*[@id=\"password\"]/div[1]/div/div[1]/input"));
            pass.SendKeys("x@iysu27");

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
            IWebElement passButton = driver.FindElement(By.XPath("//*[@id=\"passwordNext\"]/div/button/span"));
            passButton.Click();

            IWebElement roundButton = driver.FindElement(By.CssSelector("a.gb_d.gb_Da.gb_H"));
            roundButton.Click();
            driver.Navigate().GoToUrl("https://myaccount.google.com/?hl=en&utm_source=OGB&utm_medium=act&pli=1"); //can not find the element and click
            driver.Navigate().GoToUrl("https://myaccount.google.com/personal-info"); // can not find the element and click

            IWebElement namechangePen = driver.FindElement(By.CssSelector("a.RlFDUe.I6g62c.N5YmOc.kJXJmd"));
            namechangePen.Click();
            // could not find button
            driver.Navigate().GoToUrl("https://myaccount.google.com/profile/name/edit?continue=https://myaccount.google.com/personal-info&pli=1&rapt=AEjHL4Me6Rg3lVZv73Cqn-J7rLF2d2N_skP5XKlu48UiinQqJ2jKnYsrPEmiiDMAxnlUPhHeeNJC7zUblz6iKBOHWBn2BVxtCQ");
            
            IWebElement namechangerButton = driver.FindElement(By.Id("i6"));
            actions.DoubleClick(namechangerButton).Perform();
            namechangerButton.SendKeys("Saidmurod");

            IWebElement save = driver.FindElement(By.CssSelector("button.UywwFc-LgbsSe.UywwFc-LgbsSe-OWXEXe-dgl2Hf.wMI9H"));
            save.Click();

            IWebElement changedName = driver.FindElement(By.XPath("//div[text()='Saidmurod mukhitdinov']"));
            string expectedName = "Saidmurod mukhitdinov";
            Assert.AreEqual(expectedName, changedName.Text);

            driver.Quit();
        }

        [TestMethod]

        public void EmailSender()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();

            driver.Navigate().GoToUrl(test_url);

            driver.Manage().Window.Maximize();

            IWebElement firstCheckBox = driver.FindElement(By.XPath("//*[@id=\"identifierId\"]"));
            firstCheckBox.SendKeys("saidmurodtestepam@gmail.com");

            IWebElement nextLoginButton = driver.FindElement(By.XPath("//*[@id=\"identifierNext\"]/div/button/span"));
            nextLoginButton.Click();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);


            IWebElement pass = driver.FindElement(By.XPath("//*[@id=\"password\"]/div[1]/div/div[1]/input"));
            pass.SendKeys("x@iysu27");

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
            IWebElement passButton = driver.FindElement(By.XPath("//*[@id=\"passwordNext\"]/div/button/span"));
            passButton.Click();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            IWebElement composeButton = driver.FindElement(By.XPath("//div[text()='Compose']"));
            composeButton.Click();

            IWebElement receiverEmail = driver.FindElement(By.CssSelector("input.agP.aFw"));
            receiverEmail.SendKeys("saidmurod11@gmail.com");

            IWebElement emailText = driver.FindElement(By.CssSelector("div.Am.Al.editable.LW-avf.tS-tW"));
            emailText.SendKeys("Hello, Mr. Saidmurod. Hope you are doing well. Just wanted to ask you for invoice.");

            IWebElement sendButton = driver.FindElement(By.XPath("//div[text()='Send']"));
            sendButton.Click();

            string differentAccountUrl = "https://accounts.google.com/v3/signin/identifier?authuser=0&continue=https%3A%2F%2Fmail.google.com&ec=GAlAFw&hl=en&service=mail&flowName=GlifWebSignIn&flowEntry=AddSession&dsh=S-1274899217%3A1696355119845412&theme=glif";
            driver.Navigate().GoToUrl(differentAccountUrl);

            var emailLogin1 = driver.FindElement(By.XPath("//input[@type='email']"));
            emailLogin1.SendKeys("saidmurod11@gmail.com");

            var nextLoginButton1 = driver.FindElement(By.XPath("//span[text()='Next']"));
            nextLoginButton1.Click();
            // can not sign in
        }
    }
}