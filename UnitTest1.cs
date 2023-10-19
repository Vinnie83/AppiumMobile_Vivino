using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.PageObjects;

namespace AppiumVivinoApp
{
    public class VivinoTests
    {
        private const string UriString = "http://127.0.0.1:4723/wd/hub";
        private const string VivinoAppLocation = @"C:\vivino_8.18.11-8181203.apk";
        private AndroidDriver<AndroidElement> driver;
        private AppiumOptions options;

        [SetUp]
        public void Setup()
        {
            this.options = new AppiumOptions() { PlatformName = "Android" };
            options.AddAdditionalCapability("app", VivinoAppLocation);
            options.AddAdditionalCapability("appPackage", "vivino.web.app");
            options.AddAdditionalCapability("appActivity", "com.sphinx_solution.activities.SplashActivity");
            this.driver = new AndroidDriver<AndroidElement> (new Uri(UriString), options); 
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30); 
        }

        [TearDown]  

        public void CloseApp()
        {
            driver.Quit();
        }

        [Test]
        public void Test_SearchWine_VerifyNameAndRating()
        {
            var linkAccount = driver.FindElementById("vivino.web.app:id/txthaveaccount");
            linkAccount.Click();

            var inputUsername = driver.FindElementById("vivino.web.app:id/edtEmail");
            inputUsername.SendKeys("pesho@abv.bg");

            var inputPassword = driver.FindElementById("vivino.web.app:id/edtPassword");
            inputPassword.SendKeys("parola1parola1");

            var linkLogin = driver.FindElementById("vivino.web.app:id/action_signin");
            linkLogin.Click();

            var buttonSearch = driver.FindElementById("vivino.web.app:id/wine_explorer_tab");
            buttonSearch.Click();

            var searchArea = driver.FindElementById("vivino.web.app:id/search_header_text");
            searchArea.Click();

            var inputSearchField = driver.FindElementById("vivino.web.app:id/editText_input");
            inputSearchField.SendKeys("Katarzyna Reserve Red 2006");

            var listWineResultElement = driver.FindElementById("vivino.web.app:id/winename_textView");
            listWineResultElement.Click();

            var labelName = driver.FindElementById("vivino.web.app:id/wine_name");

            var rating = driver.FindElementById("vivino.web.app:id/rating");


            

            var sectionSummary = driver.FindElementByAndroidUIAutomator(
                "new UiScrollable(new UiSelector().scrollable(true))" +
                ".scrollIntoView(new UiSelector().resourceIdMatches(" +
                "\"vivino.web.app:id/header\"))"
                );

            Assert.That(labelName.Text, Is.EqualTo("Reserve Red 2006"));
            Assert.That(rating.Text, Is.EqualTo("4.2"));

            var linkFacts = driver.FindElementById("vivino.web.app:id/tabs");
            linkFacts.Click();  

            var labelFactTitle = driver.FindElementById("vivino.web.app:id/wine_fact_title");

            Assert.That(labelFactTitle.Text, Is.EqualTo("Grapes"));

            var labelFactText = driver.FindElementById("vivino.web.app:id/wine_fact_text");

            Assert.That(labelFactText.Text, Is.EqualTo("Cabernet Sauvignon,Merlot"));

        }
    }
}