using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using BrowserStack;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using TechTalk.SpecRun;

namespace SpecflowBrowserStack.Drivers
{
	public class BrowserSeleniumDriverFactory
	{
		private readonly ConfigurationDriver _configurationDriver;
		private readonly TestRunContext _testRunContext;

		public BrowserSeleniumDriverFactory(ConfigurationDriver configurationDriver, TestRunContext testRunContext)
		{
			_configurationDriver = configurationDriver;
			_testRunContext = testRunContext;
		}

		public IWebDriver GetForBrowser(int browserId)
		{
			DesiredCapabilities caps = new DesiredCapabilities();
			// Set common capabilities like "browserstack.local", project, name, session
			foreach (var tuple in _configurationDriver.CommonCapabilities)
			{
				if (tuple.Key.ToString() == "name")
				{
					caps.SetCapability(tuple.Key.ToString(), tuple.Value.ToString() + " " + browserId.ToString());
				}
				else
				{
					caps.SetCapability(tuple.Key.ToString(), tuple.Value.ToString());
				}
			}
			// Set session specific capability
			var specificCap = _configurationDriver.Capabilities.ToList<IConfigurationSection>()[browserId];
			foreach (var tuple in specificCap.GetChildren().AsEnumerable())
			{
				// Console.WriteLine("!!");
				// Console.WriteLine(tuple.Key.ToString());
				// Console.WriteLine("@@");
				// Console.WriteLine(tuple.Value);
				caps.SetCapability(tuple.Key.ToString(), tuple.Value.ToString());
			}

			// sets remote URL
			string username = Environment.GetEnvironmentVariable("BROWSERSTACK_USERNAME");
			if (username == null || username == "")
			{
				username = _configurationDriver.BSUsername;
			}
			string access_key = Environment.GetEnvironmentVariable("BROWSERSTACK_ACCESS_KEY");
			if (access_key == null || access_key == "")
			{
				access_key = _configurationDriver.BSAccessKey;
			}
			string remoteUrl = "https://";
			if (username != null && access_key != null)
			{
				remoteUrl += username + ":" + access_key + "@";
			}
			remoteUrl += _configurationDriver.SeleniumBaseUrl + "/wd/hub";

			return new RemoteWebDriver(new Uri(remoteUrl), caps);
			// return null;
		}

		public Local GetLocal(int browserIndex)
		{

			var specificCap = _configurationDriver.Capabilities.ToList<IConfigurationSection>()[browserIndex];
			foreach (var tuple in specificCap.GetChildren().AsEnumerable())
			{
				if (tuple.Key.ToString() == "browserstack.local")
				{
					Local _local = new Local();
					List<KeyValuePair<string, string>> bsLocalArgs = new List<KeyValuePair<string, string>>() {
						new KeyValuePair<string, string>("key", _configurationDriver.BSAccessKey)
					};
					_local.start(bsLocalArgs);
					return _local;

				}
			}
			return null;
		}
	}
}
