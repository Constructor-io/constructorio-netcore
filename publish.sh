#!/bin/bash

# Simple publish script for Constructor.io .NET SDK
set -e

# Configuration
NUGET_SOURCE=${1:-https://api.nuget.org/v3/index.json}
API_KEY=${2:-$NUGET_API_KEY}
ROOT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"

echo "Publishing Constructor.io .NET SDK..."

# Check if API key is provided
if [[ -z "$API_KEY" ]]; then
    echo "Error: NuGet API key not provided."
    echo "Usage: ./publish.sh [nuget-source] [api-key]"
    echo "Or set NUGET_API_KEY environment variable"
    exit 1
fi

# Find the latest package
PACKAGE=$(find ./artifacts -name "*.nupkg" | head -1)

if [[ -z "$PACKAGE" ]]; then
    echo "Error: No .nupkg file found in ./artifacts"
    echo "Run ./build.sh first to create the package"
    exit 1
fi

echo "Found package: $(basename "$PACKAGE")"
echo "Publishing to: $NUGET_SOURCE"

# Publish the package
dotnet nuget push "$PACKAGE" --source "$NUGET_SOURCE" --api-key "$API_KEY"

echo "Package published successfully!"