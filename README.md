# kipperbit-iphone-test

MVP: iPhone tracker proof-of-concept using the Aspire-style baseline (API + worker + shared core). Worker reads mock events (device id, battery, coarse location) and appends them to storage; API exposes health/status/start/stop.

## Quick start
- Prereq: .NET 8 SDK. If not installed system-wide, use the local one at `~/.dotnet/dotnet` (installed via dotnet-install).  
- Install/restore: `~/.dotnet/dotnet restore`
- Run worker (mock ingestion): `cd IphoneTest.Worker && ~/.dotnet/dotnet run`
- Run API: `cd IphoneTest.Api && ~/.dotnet/dotnet run`
- API endpoints:
  - `GET /health` — liveness
  - `GET /status` — enabled flag, last poll/success/error, last event
  - `POST /start` — enable ingestion
  - `POST /stop` — disable ingestion

## How it works (MVP)
- Worker:
  - Reads `data/mock-events.json` (battery + location samples).
  - Respects control state in `storage/control.json` (enabled flag, cursors, timestamps).
  - Appends events to `storage/events.jsonl`.
- API:
  - Shares the same storage folder (`/storage` at repo root).
  - Surfaces current control state and last event; start/stop toggles ingestion.

## Next steps
- Replace mock events with real iPhone data source/ingestion path.
- Add auth/IAM once the system-level users are ready.
- Add metrics/log shipping and a small UI/dashboard to view recent events.
