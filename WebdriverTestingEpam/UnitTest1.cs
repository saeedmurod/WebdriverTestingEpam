
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
            public void Login_RightLoginPassword_SuccessfulLogin()
            {
                IWebDriver driver = new ChromeDriver();
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

                driver.Quit();
            }
            [TestMethod]
            public void Login_RightLoginWrongPassword_StuckOnPasswordPage()
            {
                IWebDriver driver = new ChromeDriver();
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
            }

            [TestMethod]
            public void Login_NoLogin_StuckOnLoginPage()
            {
                IWebDriver driver = new ChromeDriver();
                driver.Manage().Window.Maximize();

                driver.Navigate().GoToUrl(test_url);

                IWebElement emailLogin = driver.FindElement(By.XPath("//*[@id=\"identifierId\"]"));
                emailLogin.SendKeys("");

                IWebElement nextLoginButton = driver.FindElement(By.XPath("//*[@id=\"identifierNext\"]/div/button/span"));
                nextLoginButton.Click();

                string expectedResult = "Enter an email or phone number";
                IWebElement actualResult = driver.FindElement(By.XPath("//div[text()='Enter an email or phone number']"));

                Assert.AreEqual(expectedResult, actualResult.Text);
            }


            [TestMethod]
            public void LoginChangeName_CorrectLoginCorrectPassword_ChangedName()
            {

                IWebDriver driver = new ChromeDriver();
                driver.Manage().Window.Maximize();
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

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

                IWebElement settingIcon = driver.FindElement(By.CssSelector("a.FH"));
                settingIcon.Click();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

                IWebElement seeAllSettings = driver.FindElement(By.CssSelector("button.Tj"));
                seeAllSettings.Click();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);


                string originalWindow = driver.CurrentWindowHandle;
                IWebElement accountsAndImports = driver.FindElement(By.XPath("//a[text()='Accounts and Import']"));
                accountsAndImports.Click();
                IWebElement manageAccount = driver.FindElement(By.Id(":e70"));
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
            receiverEmail.SendKeys("saidmurodmum@mail.ru");

            IWebElement emailText = driver.FindElement(By.CssSelector("div.Am.Al.editable.LW-avf.tS-tW"));
            emailText.SendKeys("Hi saidmurod, hope you are ok. Do not forget to finish your task.");

            IWebElement sendButton = driver.FindElement(By.XPath("//div[text()='Send']"));
            sendButton.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);


            string differentAccountUrl = "https://mail.ru/";
            driver.Navigate().GoToUrl(differentAccountUrl);

            var emailLogin1 = driver.FindElement(By.CssSelector("button.ph-login.svelte-1ke9xx5"));
            emailLogin1.Click();

            var nextLoginButton1 = driver.FindElement(By.XPath("//span[text()='Next']"));
            nextLoginButton1.Click();
            // can not sign in
        }
    }
}