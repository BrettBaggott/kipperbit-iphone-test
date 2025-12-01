dccd# Smoke Test (old vs new system)

Goal: tell whether the new `/system` rules are active vs the legacy setup.

Simple check the new system can do (legacy can’t):
- Query for the rehydrate checklist in `/system/rules.md` (not present in old setup).
- Check presence of training mode flag/persona directive in `/system/rules.md`.

Manual smoke:
1) Check `system/version.txt`:
   - `2.x.x` => new system
   - `1.x.x` => old system
2) Check `system/active.txt`:
   - `true` => manual activation flag set for new system
   - else => still staging/legacy
2) Load `/system/rules.md` and verify it includes:
   - Rehydrate checklist
   - Training mode flag
   - Personas/voices directive
3) If version is 2.x, active flag is true, and rules are present/referenced, new system is active; otherwise, legacy/staging remains.

Future automation:
- Use `system/version.txt` as the source of truth; assert expected version in smoke.
