/*
 * This is a Puppeteer+AngleSharp crawler console app samples
 */
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using PuppeteerSharp;
using PuppeteerSharp.BrowserData;
using System;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace CrawlerSamples
{
    internal static class Program
    {
        private const string Url = "https://github.com/orgs/dotnet/repositories";

        private static async Task Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            // Download chromium browser revision package
            await new BrowserFetcher().DownloadAsync(Chrome.DefaultBuildId);

            await TestAngleSharp();

            if (ShouldWaitForExit())
            {
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey(intercept: true);
            }
        }

        private static bool ShouldWaitForExit()
        {
            return Environment.UserInteractive
                && !Console.IsInputRedirected
                && !Console.IsOutputRedirected;
        }

        private static async Task TestAngleSharp()
        {
            /*
             * Used AngleSharp loading of HTML document
             * TODO: To enable JS evaluation, install the AngleSharp.Js nuget package
             *       (AngleSharp.Scripting.Javascript is deprecated and replaced by AngleSharp.Js)
             *       and call WithJs() on the configuration.
             * Note: AngleSharp.Js (via Jint) now supports modern ECMAScript (ES2015–ES2025) and can run
             *       libraries such as jQuery / React 16 / Bootstrap 5 in its test suite. For many
             *       real-world pages you typically also need AngleSharp.Io, AngleSharp.Css, and WithEventLoop().
             *       It is still not a full browser: DOM/browser API coverage and heavy SPA apps can fail,
             *       and Jint is an interpreter (slower than Chromium). Prefer PuppeteerSharp for complex sites.
             */
            //IConfiguration config = Configuration.Default
            //    .WithDefaultLoader(new LoaderOptions { IsResourceLoadingEnabled = true })
            //    //.WithCss()
            //    .WithJs()
            //    .WithEventLoop();
            //IBrowsingContext context = BrowsingContext.New(config);
            //IDocument document = await context.OpenAsync(Url);

            // Used PuppeteerSharp loading of HTML document
            string htmlString = await TestPuppeteerSharp();

            /*
             * Parsing of HTML document string
             */
            IBrowsingContext context = BrowsingContext.New(Configuration.Default);
            IHtmlParser? parser = context.GetService<IHtmlParser>();
            if (parser == null)
            {
                throw new InvalidOperationException($"Failed to get {nameof(IHtmlParser)} service.");
            }

            IHtmlDocument document = parser.ParseDocument(htmlString);

            // Selector repository element list
            // Note: GitHub UI markup changes over time; keep selectors under review when scraping breaks.
            Type listItemType = typeof(IHtmlListItemElement);
            IHtmlCollection<IHtmlListItemElement> repoElementList = document.QuerySelectorAll("ul[data-listview-component='items-list'] > li")
                .Where(x => listItemType.IsInstanceOfType(x))
                .Cast<IHtmlListItemElement>()
                .ToCollection();

            RepoModel[] repoModels = new RepoModel[repoElementList.Length];
            for (int i = 0; i < repoElementList.Length; i++)
            {
                // Parsing and converting to the repository model object.
                repoModels[i] = CreateModelWithAngleSharp(repoElementList[i]);
            }

            // Printing to console windows
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                TypeInfoResolver = CustomJsonSerializerContext.Default,
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            string jsonString = JsonSerializer.Serialize(repoModels, options);
            Console.WriteLine(jsonString);
            Console.WriteLine();
            Console.WriteLine("Total count: " + repoModels.Length);
        }

        private static async Task<string> TestPuppeteerSharp()
        {
            // Enabled headless option
            LaunchOptions launchOptions = new LaunchOptions { Headless = true };
            // Starting headless browser
            await using IBrowser browser = await Puppeteer.LaunchAsync(launchOptions).ConfigureAwait(false);

            // Get all(default) pages
            IPage[] pages = await browser.PagesAsync().ConfigureAwait(false);
            // Get first page or new tab page
            IPage firstPage = pages.Length > 0 ? pages[0] : await browser.NewPageAsync().ConfigureAwait(false);

            try
            {
                // Request URL to get the page
                await firstPage.GoToAsync(Url, WaitUntilNavigation.Networkidle2).ConfigureAwait(false);
                // Get and return the HTML content of the page
                return await firstPage.GetContentAsync().ConfigureAwait(false);
            }
            finally
            {
                if (!firstPage.IsClosed)
                {
                    await firstPage.CloseAsync().ConfigureAwait(false);
                }
            }
        }

        private static RepoModel CreateModelWithAngleSharp(IHtmlListItemElement repoItem)
        {
            return new RepoModel
            {
                Url = NormalizeText(repoItem.QuerySelector("div[data-listview-item-title-container] > h4 > a")?.GetAttribute("href")),
                Title = NormalizeText(repoItem.QuerySelector("div[data-listview-item-title-container] > h4 > a > span")?.TextContent),
                Description = NormalizeText(repoItem.QuerySelector("div.repos-list-description > div")?.TextContent),
                Visibility = NormalizeText(repoItem.QuerySelector("span[data-listview-item-visibility-label]")?.TextContent),
                // CSS-module class prefixes are fragile; prefer data-* attributes when available.
                Language = NormalizeText(repoItem.QuerySelector("span[class^='ReposListItem-module__PrimaryLanguageName']")?.TextContent),
                License = NormalizeText(repoItem.QuerySelector("div[class^='ReposListItem-module__IconLabel']")?.TextContent)
            };
        }

        private static string? NormalizeText(string? value) => string.IsNullOrWhiteSpace(value) ? null : value.Trim();
    }
}
