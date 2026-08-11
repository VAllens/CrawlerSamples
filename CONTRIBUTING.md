# Contributing to Crawler Samples

Thanks for helping keep this sample useful for the .NET crawler / scraping community.

## Development setup

1. Install the [.NET 10 SDK](https://dotnet.microsoft.com/download)
2. Clone the repository
3. Restore and build:

```bash
dotnet restore
dotnet build CrawlerSamples.sln -c Release
```

4. Run the sample:

```bash
dotnet run --project src/CrawlerSamples.ConsoleApp
```

## Pull requests

- Keep changes focused and documented in the PR description
- Prefer small PRs that are easy to review
- Update README / comments when selectors, target pages, or runtime requirements change
- Make sure `dotnet build CrawlerSamples.sln -c Release` succeeds locally
- Avoid committing secrets, local Chromium caches, or build outputs

## Coding guidelines

- Target `net10.0` and keep nullable reference types enabled
- Prefer explicit resource cleanup for browser / page objects
- Keep HTML selectors as stable as possible; document fragile CSS-module selectors
- Do not scrape private or authenticated content in this sample

## Issues

When filing a bug, include:

- OS and .NET SDK version
- Package versions (`PuppeteerSharp`, `AngleSharp`)
- Exact exception / stack trace
- Whether Chromium downloaded successfully

## Code of conduct

This project follows the [Contributor Covenant](CODE_OF_CONDUCT.md).
