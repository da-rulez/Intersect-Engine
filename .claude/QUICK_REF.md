# Quick Reference Card (30 seconds)

## Project Layout
```
Intersect (Core)/          → Core infrastructure (note the space!)
Framework/Framework.Core/  → Game objects, packets
Intersect.Server.Core/     → Server logic, database
Intersect.Client.Core/     → Client logic, rendering
Intersect.Network/         → Network layer
```

## Fast Build Commands
```bash
.claude/build-server.sh           # Server only (20s)
.claude/build-client.sh           # Client only (18s)
dotnet build ProjectName.csproj   # Single project (5s)
dotnet watch --project Path       # Auto-rebuild (1s)
```

## Key Locations
```
Packets:   Framework/Intersect.Framework.Core/Network/Packets/
Database:  Intersect.Server.Core/Database/
Entities:  Intersect.Server.Core/Entities/
UI:        Intersect.Client.Framework/Gwen/
Plugins:   Examples/Intersect.Examples.Plugin.*/
```

## Code Style (from .editorconfig)
- 4 spaces (C#), 2 spaces (XML/JSON)
- LF line endings
- File-scoped namespaces
- Nullable enabled
- Allman braces `{` on new line

## Commit Format
```
feat: add new feature
fix: fix bug
chore: documentation/tests/formatting
```

## Branch Strategy
- `main` - Stable (0.8.0.x) - Bug fixes only
- `prerelease` - RC (0.7.x.y) - Non-breaking
- `development` - Active (0.8.x.y) - Breaking OK

## Build Properties
```bash
-p:Configuration=Debug              # or Release
-p:PackageVersion=0.8.0-beta
-p:Version=0.8.0
-r linux-x64                        # or osx-x64, win-x64
```

## Test Commands
```bash
dotnet test --filter "Name~Player"  # Specific tests
dotnet watch test                   # Auto-run tests
```

## Common Namespaces
- `Intersect.Core` - Runtime
- `Intersect.Framework.Core` - Game objects
- `Intersect.Server.Core.Entities` - Server entities
- `Intersect.Client.Framework.Gwen` - UI

## Design Patterns
- Service lifecycle: Bootstrap → Start → Run → Stop
- Factory: `FactoryRegistry<T>`
- Packets: MessagePack serialization
- Plugins: Dynamic loading with contexts
- Database: EF Core with thread pool

## What NOT to Read (Save Tokens!)
- `bin/`, `obj/`, `vendor/`, `assets/`
- `.dll`, `.exe`, `.db`, `.png`, `.jpg`
- Examples (unless learning plugins)
- Migrations (unless DB work)

See `.claudeignore` for complete list.

## Performance Tips
✅ Use solution filters (`.slnf`)
✅ Build specific projects only
✅ Use `dotnet watch` for iterations
✅ Filter tests by name
❌ Don't build full `Intersect.sln`
❌ Don't clean unless necessary
❌ Don't read binary/generated files

## Full Documentation
- **Comprehensive Guide**: `CLAUDE.md` (885 lines)
- **Speed Optimization**: `.claude/README.md` (this directory)
- **Quick Reference**: `.claude/QUICK_REF.md` (this file)
