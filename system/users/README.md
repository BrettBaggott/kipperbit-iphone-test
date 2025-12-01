# System Users (IAM Stub)

Purpose: track system-level identities separate from personal profile docs. This will house internal users/roles when we’re ready to switch from personal-doc-based identification to a proper IAM layer.

Notes:
- Do not confuse with `/personal/<name>` (human-facing profiles/changelogs). This folder is for system identities/roles and future auth metadata.
- Dev copy lives at `dev/system/users/` (mirrors prod for safe iteration).
- Pending design: mapping for roles/groups, allowed scopes, and integration points with prompts/agents.
