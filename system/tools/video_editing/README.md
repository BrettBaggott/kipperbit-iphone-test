# Video Editing Tools (System)

Purpose: central index for video editing capabilities and scripts.

- Project: `projects/video-editor/` (MVP: prompt-driven subtitling, evolving to YouTube-style motion titling; see `README.md` and `video_editor_gameplan.md`).
- CLI: `python projects/video-editor/video_edit.py --input <file> --target <lang> --out-name <name> [--out-dir <dir>] [--force] [--no-translate]`. Requires `ffmpeg`, `pip install -r projects/video-editor/requirements.txt`.
- Existing helper: `projects/bear-twin/tools/batch_resize.sh` for aspect/resize; keep referenced here until migrated.
- Future automation: wrappers to invoke titling/subs, styling hooks (beat-aware), and safe defaults (no overwrite without `--force`).
- Integration: expose commands here for use from prompts; keep dependencies noted (ffmpeg, STT/translate engine).
- When `system` moves private, mirror this folder there and leave a symlink in the public tree.
