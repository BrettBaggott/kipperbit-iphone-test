# Roles and Access Model (kipperbit)

Purpose: capture role intent for kipperbit itself (paths/actions) in addition to GitHub permissions.

## Roles
- **Owner** (Brett): full control. Can edit `system/**`, rules, version/active flags, boss token, lexicon, core docs, CODEOWNERS, branch protections.
- **Assistant Admin** (Connor): can modify shared docs, personal folders (except boss secrets), projects, onboarding materials; can review/merge most changes; cannot change boss token, active flag, or semver major without Owner approval.
- **Contributor** (Matt, Chandler, others): can work in their personal folder and project code; propose changes to shared/system via PR but require Owner/Assistant Admin approval; no direct edits to boss-sensitive areas.
- **Limited Collaborator** (Beth): limited to `personal/beth/**`; can log, read docs, request changes via PR/assistant; no system/shared edits.
- Personas (`*-persona`): not real users; used for voice/modeling only and hold no permissions.

## Enforcement plan
- **CODEOWNERS**: align protected paths to owners (e.g., `system/**` -> Brett; `personal/beth/**` -> Beth; shared/docs -> Brett+Assistant Admin). PRs touching those paths require matching approvals.
- **Branch protection**: require PRs to `main`, required reviews, and required checks (tests/lint/security) before merge.
- **Policy check (optional)**: add a GH Action that reads a simple author-to-role map and fails PRs when authors touch disallowed paths for their role.
- **GitHub permissions**: map users to matching repo permissions (Owner vs Maintainer vs Triage) consistent with roles.

## Next steps
1) Add `CODEOWNERS` implementing the mapping above.
2) Set branch protection on `main` to require PRs, reviews, and required checks.
3) Add/enable a required status check (tests/lint/security scan) and optional role-policy action.
4) Keep this roles doc updated as people/permissions change.***
