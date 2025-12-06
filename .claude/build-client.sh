#!/bin/bash
# Fast client-only build script

set -e

echo "🏗️  Building client components only..."

# Build using solution filter (much faster than full solution)
dotnet build Intersect.Client-only.slnf \
  -p:Configuration=Debug \
  -p:PackageVersion=0.8.0-dev \
  -p:Version=0.8.0

echo "✅ Client build complete!"
echo ""
echo "Run client with:"
echo "  dotnet run --project Intersect.Client/Intersect.Client.csproj"
echo ""
echo "Or watch mode:"
echo "  dotnet watch --project Intersect.Client/Intersect.Client.csproj"
