# Security Policy

## Supported versions

This is a sample / educational repository. Security fixes are applied on the latest `master` branch only.

## Reporting a vulnerability

Please **do not** open a public issue for security-sensitive reports.

Instead, email the maintainer via the contact details on the [GitHub profile](https://github.com/VAllens), or open a private [GitHub Security Advisory](https://docs.github.com/en/code-security/security-advisories/guidance-on-reporting-and-writing-information-about-vulnerabilities/privately-reporting-a-security-vulnerability) if available for this repository.

Include:

- A short description of the issue
- Steps to reproduce
- Impact assessment (if known)
- Any suggested fix

We aim to acknowledge reports within 7 days.

## Scope notes

This sample launches a headless browser and fetches public web content. When adapting it for production:

- Respect target site `robots.txt`, terms of service, and rate limits
- Do not store or log credentials in source control
- Pin package versions and keep dependencies updated (Dependabot is enabled)
- Treat HTML from the network as untrusted input
