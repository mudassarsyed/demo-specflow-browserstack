## Demo-Specflow+-BrowserStack <img src="https://visualstudio.microsoft.com/wp-content/uploads/2019/06/BrandVisualStudioWin2019-3.svg" height="22"> <img src="https://camo.githubusercontent.com/799a5c97a4d00394703cf20a5de308784c5454c05726b4c6ba559397644e58d2/68747470733a2f2f643938623874316e6e756c6b352e636c6f756466726f6e742e6e65742f70726f64756374696f6e2f696d616765732f6c61796f75742f6c6f676f2d6865616465722e706e673f31343639303034373830" height="22">

---

<!-- [![BrowserStack Status](https://automate.browserstack.com/badge.svg?badge_key=cTY2c2NMN2tPSzRJUUZNbFpXQ1doRGlhRVFrWG5KOXkzbmN1RTFMdjZNbz0tLUd6L1NDRkp4NHlhZ2UwYWphTytQWHc9PQ==--76e8689d99c05a6556cfaf5b48a2759865cfebd3)](https://automate.browserstack.com/public-build/cTY2c2NMN2tPSzRJUUZNbFpXQ1doRGlhRVFrWG5KOXkzbmN1RTFMdjZNbz0tLUd6L1NDRkp4NHlhZ2UwYWphTytQWHc9PQ==--76e8689d99c05a6556cfaf5b48a2759865cfebd3) -->

This repository contains sample tests to run on the BrowserStack Infrastructure using Specflow+.

### Setup

- Install the following necessary packages using command line (refer below for performing local testing):

  ```powershell
  dotnet restore
  ```

- Export the environment variables for the Username and Access Key of your BrowserStack account

  ```powershell
  set BROWSERSTACK_USERNAME=<browserstack-username> &&
  set BROWSERSTACK_ACCESS_KEY=<browserstack-access-key>
  ```

### Web

To run tests on a website run anyone of the following commands:

```powershell
# C# and dotnet will automatically detect all your tests and trigger them in Parallel
dotnet test
```

OR

Use Visual Studio Test Explorer and manually select test file and test case you want to run
