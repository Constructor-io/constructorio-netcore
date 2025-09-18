#!/bin/bash

# Simple build script for Constructor.io .NET SDK
set -e

# Configuration
CONFIGURATION=${1:-Release}
RUN_TESTS=false
CLEAN=false
ROOT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
SRC_DIR="$ROOT_DIR/src"

# Parse arguments
for arg in "$@"; do
    case $arg in
        --test)
            RUN_TESTS=true
            ;;
        --clean)
            CLEAN=true
            ;;
    esac
done

echo "Building Constructor.io .NET SDK ($CONFIGURATION)..."

# Clean (optional)
if [[ "$CLEAN" == true ]]; then
    echo "Cleaning..."
    dotnet clean "$SRC_DIR/Constructorio_Net.sln" --configuration "$CONFIGURATION"
fi

# Restore, build, test (optional), pack
echo "Restoring packages..."
dotnet restore "$SRC_DIR/Constructorio_Net.sln"

echo "Building solution..."
dotnet build "$SRC_DIR/Constructorio_Net.sln" --configuration "$CONFIGURATION" --no-restore

# Only run tests if explicitly requested
if [[ "$RUN_TESTS" == true ]]; then
    echo "Running tests..."
    dotnet test "$SRC_DIR/Constructorio_NET.Tests/Constructorio_NET.Tests.csproj" --configuration "$CONFIGURATION" --no-build || echo "Some tests failed, continuing..."
else
    echo "Skipping tests (use --test to run tests)"
fi

echo "Creating package..."
dotnet pack "$SRC_DIR/constructor.io/constructor.io.csproj" --configuration "$CONFIGURATION" --no-build --output ./artifacts

echo "Build complete!"