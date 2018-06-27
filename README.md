# Crawler Samples
This is a `PuppeteerSharp` + `AngleSharp` crawler console app samples, used `C# 7.1` coding and dotnet core build.

# Useful links
* [Puppeteer Sharp](https://github.com/kblok/puppeteer-sharp)
* [AngleSharp](https://github.com/AngleSharp/AngleSharp)


# Usage
Open this sample project in the `Visual Studio 2017` and press **`F5`** key run it.
### Or
Open this sample project in the `Visual Studio 2017` and enter **`Build(B) --> Publish(H)`** in the top toolbar.

Go to `build\win\netcoreapp2.0\x64` or `build\win\netcoreapp2.0\x86` folder, run `CrawlerSamples.ConsoleApp.exe` here.


# Result picture
<img src="https://github.com/VAllens/CrawlerSamples/raw/master/SampleSnapshoot.png" width="859" height="453" alt="Sample Snapshoot" title="SampleSnapshoot">

# Note
For the first time, it will download the `download-Win64-536395.zip` portable installation package from the network to the .local-chromium directory of the current program.

It will some time to wait here.

If a network problem occurs during downloading, which results in download failure, it throws an exception and exits the program.


# Author
Author: [Allen](http://vallen.cnblogs.com)

# License
MIT
