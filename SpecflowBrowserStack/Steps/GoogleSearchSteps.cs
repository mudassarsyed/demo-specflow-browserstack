using TechTalk.SpecFlow;
using FluentAssertions;
using SpecflowBrowserStack.Drivers;
using OpenQA.Selenium;

namespace SpecflowBrowserStack.Steps
{
	[Binding]
	public class GoogleSearchSteps
	{
		private readonly WebDriver _webDriver;
		private static bool passed = true;

		private static string message = "";

		public GoogleSearchSteps(WebDriver driver)
		{
			_webDriver = driver;
		}

		[Given(@"goto Google")]
		public void GivenINavigatedToGoogle()
		{
			_webDriver.Current.Navigate().GoToUrl("https://www.google.com/ncr");
		}

		[Then(@"title should be '(.*)'")]
		public void ThenTheResultShouldBeOnTheScreen(string expectedTitle)
		{
			string result = _webDriver.Wait.Until(d => d.Title.ToString());
			passed &= result.ToString().Equals(expectedTitle);
			if (!passed)
			{
				message = result.ToString() + " is not equal to " + expectedTitle;
			}
		}

		[AfterScenario]
		public void MarkTestAsPassOrFail()
		{
			if (passed)
			{
				((IJavaScriptExecutor)_webDriver.Current).ExecuteScript("browserstack_executor: {\"action\": \"setSessionStatus\", \"arguments\": {\"status\":\"passed\", \"reason\": \"Tests function correctly\"}}");
			}
			else
			{
				((IJavaScriptExecutor)_webDriver.Current).ExecuteScript("browserstack_executor: {\"action\": \"setSessionStatus\", \"arguments\": {\"status\":\"failed\", \"reason\": \"" + message + "\"}}");
			}
		}
	}
}
