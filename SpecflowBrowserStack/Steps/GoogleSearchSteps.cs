using TechTalk.SpecFlow;
using SpecflowBrowserStack.Drivers;

namespace SpecflowBrowserStack.Steps
{
	[Binding]
	public class GoogleSearchSteps
	{
		private readonly BrowserDriver _driver;

		public GoogleSearchSteps(BrowserDriver driver)
		{
			_driver = driver;
		}

		[Given(@"goto Google")]
		public void GivenINavigatedToGoogle()
		{
			_driver.GoToGoogle();
		}

		[Then(@"title should be '(.*)'")]
		public void ThenTheResultShouldBeOnTheScreen(string expectedTitle)
		{
			_driver.ValidateTitleShouldBe(expectedTitle);
		}
	}
}
