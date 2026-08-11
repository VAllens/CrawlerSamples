# Codex PR review rubric

When reviewing a pull request in this repository, focus on:

- Correctness bugs and exception paths
- Browser / page resource leaks
- Fragile HTML selectors and scraping breakage risks
- Security issues (secrets, unsafe deserialization, SSRF-like fetches)
- Missing docs / CI updates when runtime or packages change

Be concise and specific. Prefer actionable findings with file paths.
Keep feedback proportional to the size of the PR.
