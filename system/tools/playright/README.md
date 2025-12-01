# Playright (Playwright) Fetch Tool

Minimal Playwright utility to fetch a target site, capture HTML, and take a screenshot. Defaults to `https://www.nashvillewraps.com` and attempts to dodge bot challenges with a normal Chrome UA and headless Chromium.

## Quick start
- Install deps: `cd system/tools/playright && npm install`
- Install browsers (first run): `npx playwright install chromium`
- Fetch default (wraps): `npm run fetch`
- Headful (for interactive/debug): `npm run fetch:wraps:show`

## Options
- `--target=<name>`: uses a preset (defaults to `wraps` → `https://www.nashvillewraps.com`).
- `--url=<custom>`: fetch an arbitrary URL instead of a preset.
- `HEADLESS=false` or `--headless=false`: run with a visible browser window.
- `--storage-state=path` or `STORAGE_STATE=path`: use a Playwright storage state (cookies/session) for authenticated targets.
- `--save-storage-state=path` or `SAVE_STORAGE_STATE=path`: after fetching, write the current storage state to a file (useful after an interactive login).
- `OUTPUT_DIR=...` or `--output=...`: change where HTML/screenshot land (default `./output/<target>`).

Outputs (HTML + PNG) are written under `system/tools/playright/output/<target>/`.
