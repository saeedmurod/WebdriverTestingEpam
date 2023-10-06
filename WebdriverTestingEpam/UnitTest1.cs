
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System.Threading.Tasks;

namespace WebdriverTestingEpam
{
    [TestClass]
    public class UnitTest1
    {
        IWebDriver? driver;
        IWebDriver? driver1;
        readonly string test_url = "https://accounts.google.com/v3/signin/identifier?authuser=0&continue=https%3A%2F%2Fmail.google.com&ec=GAlAFw&hl=en&service=mail&flowName=GlifWebSignIn&flowEntry=AddSession&dsh=S1678429287%3A1696101771657405&theme=glif";
        readonly string expected = "Inbox";


        [TestMethod]
        public void Login_RightLoginPassword_SuccessfulLogin()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();

            driver.Navigate().GoToUrl(test_url);

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

            Cleanup();
        }
        [TestMethod]
        public void Login_RightLoginWrongPassword_StuckOnPasswordPage()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();

            driver.Navigate().GoToUrl(test_url);

            IWebElement emailLogin = driver.FindElement(By.XPath("//*[@id=\"identifierId\"]"));
            emailLogin.SendKeys("saidmurodtestepam@gmail.com");

            IWebElement nextLoginButton = driver.FindElement(By.XPath("//*[@id=\"identifierNext\"]/div/button/span"));
            nextLoginButton.Click();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);


            IWebElement password = driver.FindElement(By.XPath("//*[@id=\"password\"]/div[1]/div/div[1]/input"));
            password.SendKeys("x@iysu11");

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
            IWebElement nextPasswordButton = driver.FindElement(By.XPath("//*[@id=\"passwordNext\"]/div/button/span"));
            nextPasswordButton.Click();

            string expectedResult = "Wrong password. Try again or click Forgot password to reset it.";
            IWebElement actualResult = driver.FindElement(By.XPath("//span[contains(text(),'Wrong password.')]"));

            Assert.AreEqual(expectedResult, actualResult.Text);

            Cleanup();
        }

        [TestMethod]
        public void Login_NoLogin_StuckOnLoginPage()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();

            driver.Navigate().GoToUrl(test_url);

            IWebElement emailLogin = driver.FindElement(By.XPath("//*[@id=\"identifierId\"]"));
            emailLogin.SendKeys("");

            IWebElement nextLoginButton = driver.FindElement(By.XPath("//*[@id=\"identifierNext\"]/div/button/span"));
            nextLoginButton.Click();

            string expectedResult = "Enter an email or phone number";
            IWebElement actualResult = driver.FindElement(By.XPath("//div[text()='Enter an email or phone number']"));

            Assert.AreEqual(expectedResult, actualResult.Text);

            Cleanup();
        }


        [TestMethod]
        public void LoginChangeName_CorrectLoginCorrectPassword_ChangedName()
        {

            driver = new ChromeDriver();
            Actions actions = new Actions(driver);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(test_url);

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

            IWebElement settingIcon = driver.FindElement(By.CssSelector("a.FH"));
            settingIcon.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            IWebElement seeAllSettings = driver.FindElement(By.CssSelector("button.Tj"));
            seeAllSettings.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            string originalWindow = driver.CurrentWindowHandle;
            IWebElement accountsAndImports = driver.FindElement(By.XPath("//a[text()='Accounts and Import']"));
            accountsAndImports.Click();

            IWebElement manageAccount = driver.FindElement(By.XPath("//span[text()='edit info']"));
            manageAccount.Click();
            wait.Until(wd => wd.WindowHandles.Count == 2);
            foreach (string window in driver.WindowHandles)
            {
                if (originalWindow != window)
                {
                    driver.SwitchTo().Window(window);
                    break;
                }
            }
            //Wait for the new tab to finish loading content
            wait.Until(wd => wd.Title == "Gmail - Edit email address");

            string expectedName = "Saidmurod Muhitdinov";
            IWebElement radioButton = driver.FindElement(By.Id("cfn"));
            actions.DoubleClick(radioButton).Perform();
            radioButton.Clear();
            radioButton.SendKeys("Saidmurod Muhitdinov");
            string secondWindow = driver.CurrentWindowHandle;

            IWebElement submitButton = driver.FindElement(By.Id("bttn_sub"));
            submitButton.Click();

            wait.Until(wd => wd.WindowHandles.Count == 1);
            foreach (string window in driver.WindowHandles)
            {
                if (secondWindow != window)
                {
                    driver.SwitchTo().Window(window);
                    break;
                }
            }
            //Wait for the new tab to finish loading content
            wait.Until(wd => wd.Title == "Settings - saidmurodtestepam@gmail.com - Gmail");


            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            IWebElement nameChangedChecker = driver.FindElement(By.XPath("//div[@class='rc']//following::td[@class='CY']"));
            Assert.AreEqual(expectedName + " <saidmurodtestepam@gmail.com>", nameChangedChecker.Text);

            Cleanup();
        }

        [TestMethod]

        public void EmailSender_LoginSendEmailReadReply_SuccessfulReply()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(test_url);

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
            receiverEmail.SendKeys("steve@uzautotransinc.com");

            IWebElement emailText = driver.FindElement(By.CssSelector("div.Am.Al.editable.LW-avf.tS-tW"));
            emailText.SendKeys("Hi saidmurod, hope you are ok. Do not forget to finish your task.");

            try
            {
                IWebElement sendButton = driver.FindElement(By.XPath("//div[text()='Send']"));
                sendButton.Click();
            }
            catch (OpenQA.Selenium.UnhandledAlertException)
            {
                driver.SwitchTo().Alert().Accept();
            }

            Thread.Sleep(10000);
            driver.Quit();


            driver1 = new ChromeDriver();
            driver1.Manage().Window.Maximize();
            driver1.Navigate().GoToUrl(test_url);


            IWebElement firstCheckBox1 = driver1.FindElement(By.XPath("//*[@id=\"identifierId\"]"));
            firstCheckBox1.SendKeys("steve@uzautotransinc.com");


            IWebElement nextLoginButton1 = driver1.FindElement(By.XPath("//*[@id=\"identifierNext\"]/div/button/span"));
            nextLoginButton1.Click();

            driver1.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);


            IWebElement pass1 = driver1.FindElement(By.XPath("//*[@id=\"password\"]/div[1]/div/div[1]/input"));
            pass1.SendKeys("xzsaiu77");


            IWebElement passButton1 = driver1.FindElement(By.XPath("//*[@id=\"passwordNext\"]/div/button/span"));
            passButton1.Click();

            Thread.Sleep(3000);
            IWebElement notRead = driver1.FindElement(By.XPath("//span[text()='Hi saidmurod, hope you are ok. Do not forget to finish your task.']"));
            notRead.Click();

            IWebElement reply = driver1.FindElement(By.CssSelector("span.ams.bkH"));
            reply.Click();

            Thread.Sleep(3000);
            IWebElement textboxReply = driver1.FindElement(By.CssSelector("div.Am.aO9.Al.editable.LW-avf.tS-tW"));
            string textToMatch = "Hello, thank you for your message. Will work on your proposal";
            textboxReply.SendKeys("Hello, thank you for your message. Will work on your proposal");

            IWebElement replyButton = driver1.FindElement(By.CssSelector("div.T-I.J-J5-Ji.aoO.v7.T-I-atl.L3"));
            replyButton.Click();
            Thread.Sleep(3000);

            IWebElement expectedText = driver1.FindElement(By.XPath("//div[text()='Hello, thank you for your message. Will work on your proposal']"));

            Assert.AreEqual(textToMatch, expectedText.Text);

            Cleanup();
        }
   


        [TestMethod]

        public void EmailSenderChecker_LoginSendEmailReadReply_SuccesfulVerification()
        {
            driver = new ChromeDriver();
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
            receiverEmail.SendKeys("steve@uzautotransinc.com");

            IWebElement emailText = driver.FindElement(By.CssSelector("div.Am.Al.editable.LW-avf.tS-tW"));
            emailText.SendKeys("Hi saidmurod, hope you are ok. Do not forget to finish your task.");

            try
            {
                IWebElement sendButton = driver.FindElement(By.XPath("//div[text()='Send']"));
                sendButton.Click();
            }
            catch (OpenQA.Selenium.UnhandledAlertException)
            {
                driver.SwitchTo().Alert().Accept();
            }

            Thread.Sleep(10000);
            driver.Quit();


            driver1 = new ChromeDriver();

            driver1.Manage().Window.Maximize();

            driver1.Navigate().GoToUrl(test_url);


            IWebElement firstCheckBox1 = driver1.FindElement(By.XPath("//*[@id=\"identifierId\"]"));
            firstCheckBox1.SendKeys("steve@uzautotransinc.com");


            IWebElement nextLoginButton1 = driver1.FindElement(By.XPath("//*[@id=\"identifierNext\"]/div/button/span"));
            nextLoginButton1.Click();

            driver1.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);


            IWebElement pass1 = driver1.FindElement(By.XPath("//*[@id=\"password\"]/div[1]/div/div[1]/input"));
            pass1.SendKeys("xzsaiu77");


            IWebElement passButton1 = driver1.FindElement(By.XPath("//*[@id=\"passwordNext\"]/div/button/span"));
            passButton1.Click();
            Thread.Sleep(3000);

            IWebElement notRead = driver1.FindElement(By.XPath("//span[text()='Hi saidmurod, hope you are ok. Do not forget to finish your task.']"));
            notRead.Click();

            Thread.Sleep(3000);
            IWebElement nameActual = driver1.FindElement(By.XPath("//h3/span/span[1]/span"));

            string expectedName = "Saidmurod Muhitdinov";
            Assert.AreEqual(expectedName, nameActual.Text);

        }

        [TestCleanup]
        public void Cleanup()
        {
            if (driver != null)
            {
                driver.Quit();
            }
            
            else if (driver1 != null)
            {
                driver1.Quit();
            }
        }
    }
}