# Claude AI Assistant Optimization Guide

## Quick Start (30 seconds)

```bash
# Build only what you need (much faster!)
dotnet build Intersect.Client-only.slnf  # Client development
dotnet build Intersect.Server-only.slnf  # Server development

# Run with hot reload
dotnet watch --project Intersect.Server/Intersect.Server.csproj

# Run specific tests only
dotnet test --filter "FullyQualifiedName~Player"
```

## Speed Optimizations

### 1. Incremental Builds
**Always** build specific projects instead of full solution:
```bash
# Instead of: dotnet build Intersect.sln (slow)
dotnet build Intersect.Server.Core/Intersect.Server.Core.csproj  # Fast!
```

### 2. Solution Filters
Use `.slnf` files for focused work:
- `Intersect.Client-only.slnf` - Client development only
- `Intersect.Server-only.slnf` - Server development only

Open in VS Code:
```bash
code Intersect.Client-only.slnf
```

### 3. Parallel Builds
Already enabled by default in .NET, but explicitly:
```bash
dotnet build -m:4  # Use 4 parallel processes
```

### 4. Skip Unnecessary Projects
When working on server, skip client builds:
```bash
dotnet build --no-dependencies Intersect.Server.Core/Intersect.Server.Core.csproj
```

### 5. Watch Mode for Development
Auto-rebuild on file changes:
```bash
dotnet watch --project Intersect.Server/Intersect.Server.csproj
dotnet watch test --project Intersect.Tests.Server/Intersect.Tests.Server.csproj
```

## Resource Usage Optimizations

### 1. Build Caching
Network keys are cached automatically in:
```
Intersect.Network/bin/Release/keys/
```
Don't delete this directory between builds!

### 2. NuGet Package Cache
Packages cached in `~/.nuget/packages/` - keep this directory.

### 3. Reduce Obj/Bin Bloat
Clean only when necessary:
```bash
# Full clean (slow)
dotnet clean

# Selective clean
rm -rf Intersect.Server.Core/bin/Debug
rm -rf Intersect.Server.Core/obj/Debug
```

### 4. Skip Tests During Development
```bash
dotnet build --no-test
```

## AI Assistant Workflow Optimizations

### 1. Read Only What You Need
Instead of reading entire CLAUDE.md (885 lines), use these quick references:

**For architecture questions**: Read lines 15-85 of CLAUDE.md
**For build commands**: Read lines 180-240 of CLAUDE.md
**For git workflow**: Read lines 270-340 of CLAUDE.md
**For code style**: Read lines 350-420 of CLAUDE.md

### 2. Use Glob Patterns Efficiently
```bash
# Find specific file types quickly
**/*.Server.*.cs      # Server-only C# files
**/Entities/*.cs      # Entity files only
**/Database/**/*.cs   # Database models only
```

### 3. Focused File Reading
Read specific sections:
```bash
# Read only entity definitions (lines 50-100)
Read with offset and limit parameters
```

### 4. Leverage Solution Filters
Tell your AI assistant to:
1. Use solution filters for focused builds
2. Read only relevant project files
3. Skip Examples/ and vendor/ directories

## Common Fast Workflows

### Quick Server Change
```bash
# 1. Edit server code
# 2. Build only server
dotnet build Intersect.Server.Core/Intersect.Server.Core.csproj
# 3. Run server
dotnet run --project Intersect.Server/Intersect.Server.csproj
```

### Quick Client Change
```bash
# 1. Edit client code
# 2. Build only client
dotnet build Intersect.Client.Core/Intersect.Client.Core.csproj
# 3. Run client
dotnet run --project Intersect.Client/Intersect.Client.csproj
```

### Quick Plugin Development
```bash
# Build plugin only
dotnet build Examples/Intersect.Examples.Plugin.Server/

# Copy to server plugins directory
cp -r Examples/Intersect.Examples.Plugin.Server/bin/Debug/net8.0/* \
      Intersect.Server/bin/Debug/net8.0/plugins/
```

### Quick Test Run
```bash
# Run only affected tests
dotnet test --filter "FullyQualifiedName~YourClassName"

# Run in watch mode
dotnet watch test --project Intersect.Tests.Server/
```

## Directory Shortcuts

Key directories for quick navigation:

```
Core/           → Intersect (Core)/           # Application runtime
Network/        → Intersect.Network/          # Network layer
ServerCore/     → Intersect.Server.Core/      # Server logic
ClientCore/     → Intersect.Client.Core/      # Client logic
Framework/      → Framework/Intersect.Framework.Core/  # Game objects
Packets/        → Framework/Intersect.Framework.Core/Network/Packets/
Database/       → Intersect.Server.Core/Database/
UI/             → Intersect.Client.Framework/Gwen/
```

## What NOT to Do (Slow)

❌ `dotnet build Intersect.sln` (unless you really need everything)
❌ `dotnet clean` before every build
❌ Reading entire CLAUDE.md for every question
❌ Building all test projects when testing one feature
❌ Rebuilding `Intersect.Network` (keys are cached)
❌ Using Glob on entire repository (`**/*`)

## What TO Do (Fast)

✅ Build specific projects: `dotnet build ProjectName/ProjectName.csproj`
✅ Use solution filters: `dotnet build Intersect.Server-only.slnf`
✅ Use watch mode: `dotnet watch`
✅ Filter tests: `dotnet test --filter`
✅ Read specific CLAUDE.md sections by line number
✅ Use Glob with specific patterns: `**/Entities/*.cs`
✅ Cache network keys (never delete)

## Build Time Benchmarks

Typical build times on modern hardware:

| Command | Time | Use Case |
|---------|------|----------|
| Full solution build | ~60s | Initial setup only |
| Client-only filter | ~20s | Client development |
| Server-only filter | ~25s | Server development |
| Single project | ~5s | Quick iterations |
| Incremental rebuild | ~2s | Minor changes |
| Watch mode rebuild | ~1s | Auto-rebuild |

## Memory Usage

| Task | Memory | Notes |
|------|--------|-------|
| Full solution build | ~4GB | Rare |
| Filtered build | ~2GB | Recommended |
| Single project | ~1GB | Fastest |
| Running server | ~500MB | Normal |
| Running client | ~800MB | Graphics |
| Tests | ~1GB | Per test project |

## Tips for AI Assistants

1. **Always ask which subsystem** before building (client/server/both)
2. **Use solution filters** by default
3. **Read CLAUDE.md sections** instead of whole file
4. **Build incrementally** - single projects when possible
5. **Cache results** - don't re-read unchanged files
6. **Use Glob efficiently** - specific patterns only
7. **Filter tests** - run only relevant tests
8. **Leverage watch mode** - for iterative development

## Advanced: Build Profiling

Find slow builds:
```bash
dotnet build -bl:build.binlog
# Analyze with MSBuild Binary Log Viewer
```

## Questions?

Refer to main CLAUDE.md for comprehensive documentation.
This guide focuses on **speed and efficiency** only.
