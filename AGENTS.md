# Repository guidance for AI coding agents

This repository is a .NET sample demonstrating Puppeteer Sharp + AngleSharp crawling.

## Goals

- Keep the sample small, readable, and runnable with `dotnet run`
- Prefer correctness and resource safety (browser/page disposal) over clever abstractions
- Preserve MIT licensing and public-sample intent

## When changing code

1. Run `dotnet build CrawlerSamples.sln -c Release` after edits
2. Do not add secrets, credentials, or private scrape targets
3. If HTML selectors change, update `CreateModelWithAngleSharp` and mention it in the PR
4. Keep README/CI docs in sync with target framework and package versions
5. Avoid expanding scope into a full crawler framework unless explicitly requested

## CI expectations

- GitHub Actions builds on push/PR to `master`
- Optional Codex PR review workflow runs only when `CODEX_REVIEW_ENABLED=true`
