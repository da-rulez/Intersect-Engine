#!/bin/bash
# Fast server-only build script

set -e

echo "🏗️  Building server components only..."

# Build using solution filter (much faster than full solution)
dotnet build Intersect.Server-only.slnf \
  -p:Configuration=Debug \
  -p:PackageVersion=0.8.0-dev \
  -p:Version=0.8.0

echo "✅ Server build complete!"
echo ""
echo "Run server with:"
echo "  dotnet run --project Intersect.Server/Intersect.Server.csproj"
echo ""
echo "Or watch mode:"
echo "  dotnet watch --project Intersect.Server/Intersect.Server.csproj"
