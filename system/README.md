# System (planned)

Intent: centralize “live” workflow controls here while current hacked-together system remains as-is until we switch over. Use this folder to design the replacement structure; we’ll transition when agreed ready.

Initial structure ideas:
- `rules/` – rules currently in `docs/rules.md` (could migrate here).
- `startup/` – per-user startup flows.
- `out-of-box/` – snapshot of current system and changelog.
- `profiles/` – personal profiles/current snapshots.
- `projects/` – pointers to external repos (movie-tinder, voice-workflow, gmail-ingestor, etc.).
- `logging/` – conventions for agents/daily summaries/private logs (prompts/responses).
- `security/` – boss token/secret guidance and role/access model (`security/roles.md`).

Next steps:
- Draft the folder structure and migrate references (without changing current behavior).
- Define the “switch over” plan once the new layout is stable.

Per-user systems:
- Each `/personal/<name>/system` can hold overrides/customizations for that person’s workflow while `/system` is the default/global. Brett’s `/system` effectively is the default; others inherit and extend.
