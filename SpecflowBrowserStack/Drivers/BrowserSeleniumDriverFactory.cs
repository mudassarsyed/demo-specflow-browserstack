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

		public IWebDriver GetForBrowser(string browserId)
		{
			DesiredCapabilities caps = new DesiredCapabilities();
			caps.SetCapability("name", _configurationDriver.BaseSessionName + " " + browserId);
			caps.SetCapability("project", _configurationDriver.ProjectName);
			caps.SetCapability("build", _configurationDriver.BuildName);
			string lowerBrowserId = browserId.ToUpper();
			switch (lowerBrowserId)
			{
				case "SAFARI":
					foreach (var tuple in _configurationDriver.Safari)
					{
						caps.SetCapability(tuple.Key, tuple.Value);
					}
					break;
				case "CHROME":
					foreach (var tuple in _configurationDriver.Chrome)
					{
						caps.SetCapability(tuple.Key, tuple.Value);
					}
					break;
				case "FIREFOX":
					foreach (var tuple in _configurationDriver.Firefox)
					{
						caps.SetCapability(tuple.Key, tuple.Value);
					}
					break;
				case string browser: throw new NotSupportedException($"{browser} is not a supported browser");
				default: throw new NotSupportedException("not supported browser: <null>");
			}
			string username = Environment.GetEnvironmentVariable("BROWSERSTACK_USER");
			Console.WriteLine(username);
			if (username == null || username == "")
			{
				username = _configurationDriver.BSUsername;
			}
			string access_key = Environment.GetEnvironmentVariable("BROWSERSTACK_ACCESS_KEY");
			Console.WriteLine(access_key);
			if (access_key == null || access_key == "")
			{
				access_key = _configurationDriver.BSAccessKey;
			}

			return new RemoteWebDriver(new Uri("https://" + username + ":" + access_key + "@" + _configurationDriver.SeleniumBaseUrl + "/wd/hub/"), caps);
		}

		public Local GetLocal(string browserId)
		{
			string lowerBrowserId = browserId.ToUpper();
			IEnumerable<IConfigurationSection> enumerator;
			switch (lowerBrowserId)
			{
				case "FIREFOX":
					enumerator = _configurationDriver.Firefox;
					break;
				case "SAFARI":
					enumerator = _configurationDriver.Safari;
					break;
				case "CHROME":
					enumerator = _configurationDriver.Chrome;
					break;
				default:
					return null;
			}
			foreach (var tuple in enumerator)
			{
				if (tuple.Key.ToString().Equals("browserstack.local") && tuple.Value.ToString().Equals("True") && !Process.GetProcessesByName("BrowserStackLocal").Any())
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
