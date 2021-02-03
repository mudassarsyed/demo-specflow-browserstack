using TechTalk.SpecFlow;
using FluentAssertions;
using SpecflowBrowserStack.Drivers;

namespace SpecflowBrowserStack.Steps
{
	[Binding]
	public class GoogleSearchSteps
	{
		private readonly WebDriver _webDriver;

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
			string result = _webDriver.Wait.Until(d => d.Title);
			result.Should().Be(expectedTitle);
		}
	}
}
