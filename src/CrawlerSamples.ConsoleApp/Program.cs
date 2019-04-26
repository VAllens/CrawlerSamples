/*
 * This is a Puppeteer+AngleSharp crawler console app samples
 */
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using Newtonsoft.Json;
using PuppeteerSharp;

namespace CrawlerSamples
{
    internal class Program
    {
        private const string Url = "https://store.mall.autohome.com.cn/83106681.html";
        private const int ChromiumRevision = BrowserFetcher.DefaultRevision;

        private static async Task Main(string[] args)
        {
            //Download chromium browser revision package
            await new BrowserFetcher().DownloadAsync(ChromiumRevision);

            //Test AngleSharp
            await TestAngleSharp();

            Console.ReadKey();
        }

        private static async Task TestAngleSharp()
        {
            /*
             * Used AngleSharp loading of HTML document
             * TODO: Used WithJavaScript function need install AngleSharp.Scripting.Javascript nuget package
             * Note: that JavaScripts support is an experimental and does not support complex JavaScripts code.
             */
            //IConfiguration config = Configuration.Default.WithDefaultLoader().WithCss().WithCookies().WithJavaScript();
            //IBrowsingContext context = BrowsingContext.New(config);
            //IDocument document = await context.OpenAsync(url);

            //Used PuppeteerSharp loading of HTML document
            var htmlString = await TestPuppeteerSharp();

            /*
             * Parsing of HTML document string
             */
            var context = BrowsingContext.New(Configuration.Default);
            var parser = context.GetService<IHtmlParser>();
            var document = parser.ParseDocument(htmlString);

            //Selector carbox element list
            var carboxList = document.QuerySelectorAll("div.shop-content div.content div.list li.carbox");

            var carModelList = new List<CarModel>();
            foreach (var carbox in carboxList)
            {
                //Parsing and converting to the car model object.
                var model = CreateModelWithAngleSharp(carbox);
                carModelList.Add(model);

                //Printing to console windows
                var jsonString = JsonConvert.SerializeObject(model);
                Console.WriteLine(jsonString);
                Console.WriteLine();
            }

            Console.WriteLine("Total count:" + carModelList.Count);
        }

        private static async Task<string> TestPuppeteerSharp()
        {
            //Enabled headless option
            var launchOptions = new LaunchOptions { Headless = true };
            //Starting headless browser
            var browser = await Puppeteer.LaunchAsync(launchOptions);

            //New tab page
            var page = await browser.NewPageAsync();
            //Request URL to get the page
            await page.GoToAsync(Url);

            //Get and return the HTML content of the page
            var htmlString = await page.GetContentAsync();

            #region Dispose resources
            //Close tab page
            await page.CloseAsync();

            //Close headless browser, all pages will be closed here.
            await browser.CloseAsync();
            #endregion

            return htmlString;
        }

        private static CarModel CreateModelWithAngleSharp(IParentNode node)
        {
            var model = new CarModel
            {
                Title = node.QuerySelector("a div.carbox-title").TextContent,
                ImageUrl = node.QuerySelector("a div.carbox-carimg img").GetAttribute("src"),
                ProductUrl = node.QuerySelector("a").GetAttribute("href"),
                Tip = node.QuerySelector("a div.carbox-tip").TextContent,
                OrdersNumber = node.QuerySelector("a div.carbox-number span").TextContent
            };

            return model;
        }
    }
}
