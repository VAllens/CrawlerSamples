# Crawler Samples

[![CI](https://github.com/VAllens/CrawlerSamples/actions/workflows/ci.yml/badge.svg)](https://github.com/VAllens/CrawlerSamples/actions/workflows/ci.yml)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)
[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)](https://dotnet.microsoft.com/)

A small, practical **C#** sample that crawls a modern JavaScript-rendered page with [Puppeteer Sharp](https://github.com/hardkoded/puppeteer-sharp), then parses and extracts structured data with [AngleSharp](https://github.com/AngleSharp/AngleSharp).

The demo targets the public [.NET organization repositories](https://github.com/orgs/dotnet/repositories) page on GitHub and prints the extracted repository list as JSON.

## Why this sample exists

Many real-world pages are no longer static HTML. This repository shows a maintainable pattern for:

1. Rendering dynamic pages in a headless Chromium browser (`PuppeteerSharp`)
2. Parsing the resulting DOM with a standards-based HTML parser (`AngleSharp`)
3. Mapping DOM nodes into typed models and serializing them with `System.Text.Json`

It is intended as a learning / reference project for .NET developers building crawlers, scrapers, or HTML extraction tools.

## Requirements

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- Network access on first run (downloads a Chromium revision used by Puppeteer Sharp)

## Quick start

```bash
dotnet restore
dotnet run --project src/CrawlerSamples.ConsoleApp
```

Or open `CrawlerSamples.sln` in Visual Studio / VS Code / Rider and run the console app.

### Publish (optional)

```bash
dotnet publish src/CrawlerSamples.ConsoleApp -c Release -r win-x64 --self-contained true -o build/win/net10.0/x64
```

You can also use the included publish profiles under `src/CrawlerSamples.ConsoleApp/Properties/PublishProfiles/`.

## Sample output

The app prints a JSON array of repository models, for example:

```json
[
  {
    "url": "/dotnet/runtime",
    "visibility": "Public",
    "title": "runtime",
    "description": ".NET is a cross-platform runtime...",
    "language": "C#",
    "license": "MIT"
  }
]
```

Screenshot:

<img src="https://github.com/VAllens/CrawlerSamples/raw/master/SampleSnapshoot.png" width="859" height="453" alt="Sample snapshot" title="SampleSnapshoot">

## Project layout

```text
CrawlerSamples.sln
src/CrawlerSamples.ConsoleApp/
  Program.cs          # Crawl + parse flow
  RepoModel.cs        # Extracted model + JSON source generation
```

## Notes

- On first run, Puppeteer Sharp downloads a Chromium package into a local cache. This can take a while depending on your network.
- If the Chromium download fails, the process exits with an exception.
- GitHub's UI markup can change. If selectors stop matching, update them in `CreateModelWithAngleSharp`.
- The sample waits for a key press only in interactive terminals; redirected / CI environments exit immediately after printing results.

## Useful links

- [Puppeteer Sharp](https://github.com/hardkoded/puppeteer-sharp)
- [AngleSharp](https://github.com/AngleSharp/AngleSharp)
- [Codex for Open Source](https://developers.openai.com/community/codex-for-oss)

## Contributing

See [CONTRIBUTING.md](CONTRIBUTING.md). Please open issues and pull requests — CI builds on every PR.

## Security

See [SECURITY.md](SECURITY.md).

## Author

[Allen (VAllens)](https://github.com/VAllens)

## License

[MIT](LICENSE)
