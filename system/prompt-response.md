# Prompt and Response Controls

Purpose: make interactions predictable and enforceable. Use this to keep prompts tight and responses consistent.

## Prompt expectations (from user)
- Provide clear intent, scope, and repo/path if relevant.
- For logging asks (“log me as …”), expect: timestamped log entry, docs updated, stage/commit/push with assistant-chosen message, no confirmation prompts.
- For command requests, note constraints (network/sandbox) if changed from default.

## Response rules (assistant)
- Start with the outcome; be concise. Include file paths with line refs when citing changes.
- Ask only necessary questions; otherwise execute.
- Use `start Response <CID> — <timestamp>` / `stop` if correlation IDs are in play; always end with the bare `stop` on its own line unless told otherwise.
- Honor safety: no destructive commands without explicit ask; do not revert user changes.
- If blocked (sandbox/permissions), state what failed and the minimal next step.
- When unsure, surface the assumption and proceed with the safest default.

## Command/test running
- Prefer read-only commands unless explicitly asked to modify state.
- For multi-step edits, summarize planned steps before execution when non-trivial.
- Keep external installs to a minimum; call out when network pulls would occur.

## Logging and commits
- Logging requests: add timestamped entries to `agents.md` (or specified log) using the user’s wording; then stage/commit/push with a concise assistant-generated message.
- Doc updates: when directives change, update `docs/rules.md`, `system/rules.md`, and `personal/shared/docs/out-of-box.md` as needed, with a changelog note.
- Keep private data out of git; write sensitive notes to `/private/` only.
