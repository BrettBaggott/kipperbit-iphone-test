#!/usr/bin/env bash
set -euo pipefail

src="personal/brett/log-summary.md"
destinations=(
  "personal/connor/log-summary.md"
  "personal/chandler/log-summary.md"
)

if [[ ! -f "$src" ]]; then
  echo "Source log summary not found: $src" >&2
  exit 1
fi

for dest in "${destinations[@]}"; do
  cp "$src" "$dest"
done

echo "Synced $src to ${destinations[*]}"
