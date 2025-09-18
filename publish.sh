#!/bin/bash

# Simple publish script for Constructor.io .NET SDK
set -e

# Configuration
DRY_RUN=false
API_KEY="$NUGET_API_KEY"

# Parse arguments
for arg in "$@"; do
    case $arg in
        --dry-run)
            DRY_RUN=true
            ;;
    esac
done

if [[ "$DRY_RUN" == true ]]; then
    echo "Constructor.io .NET SDK Publish Script (DRY RUN)"
else
    echo "Publishing Constructor.io .NET SDK..."
fi

# Check if API key is provided (not needed for dry run)
if [[ "$DRY_RUN" == false && -z "$API_KEY" ]]; then
    echo "Error: NuGet API key not provided."
    echo "Set NUGET_API_KEY environment variable"
    exit 1
fi

# Find the latest package
PACKAGE=$(find ./artifacts -name "*.nupkg" 2>/dev/null | head -1)

if [[ -z "$PACKAGE" ]]; then
    echo "Error: No .nupkg file found in ./artifacts"
    echo "Run ./build.sh first to create the package"
    exit 1
fi

echo "Found package: $(basename "$PACKAGE")"
echo "Publishing to: https://api.nuget.org/v3/index.json"

if [[ "$DRY_RUN" == true ]]; then
    echo ""
    echo "DRY RUN: Would execute the following command:"
    echo "dotnet nuget push \"$PACKAGE\" --source \"https://api.nuget.org/v3/index.json\" --api-key \"[HIDDEN]\""
    echo ""
    echo "Package details:"
    echo "  File: $PACKAGE"
    echo "  Size: $(du -h "$PACKAGE" | cut -f1)"
    if [[ -n "$API_KEY" ]]; then
        echo "  API Key: [PROVIDED]"
    else
        echo "  API Key: [NOT PROVIDED - would fail]"
    fi
else
    # Publish the package
    dotnet nuget push "$PACKAGE" --source "https://api.nuget.org/v3/index.json" --api-key "$API_KEY"
    echo "Package published successfully!"
fi
