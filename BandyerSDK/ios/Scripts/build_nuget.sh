#!/usr/bin/env sh
##########################################################################################
# Copyright © 2020 Bandyer S.r.l. All Rights Reserved
# See LICENSE.txt for licensing information
#
# Usage: build_nuget.sh 
#
# This script creates the NuGet packet for the BandyerSDK iOS binding
#
###########################################################################################

command -v nuget >/dev/null 2>&1 || { echo >&2 "error: Could not find nuget. Aborting."; exit 1; }
command -v msbuild >/dev/null 2>&1 || { echo >&2 "error: Could not find msbuild. Aborting."; exit 1; }

test -d "Bandyer.SDK.iOS" || { echo >&2 "error: This script must be run under iOS project root folder. Aborting."; exit 1;  }

nuget restore "Bandyer.SDK.iOS/Bandyer.SDK.iOS.csproj"
msbuild /p:Configuration=Release /t:Clean /target:Build "Bandyer.SDK.iOS/Bandyer.SDK.iOS.csproj"
nuget pack "Bandyer.SDK.iOS/Bandyer.SDK.iOS.nuspec" -Verbosity detailed
