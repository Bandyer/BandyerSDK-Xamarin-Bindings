#!/usr/bin/env sh
##########################################################################################
# Copyright © 2020 Bandyer S.r.l. All Rights Reserved
# See LICENSE.txt for licensing information
#
# Usage: generate_binding.sh 
#
# This script generates the code binding using the sharpie tool
#
###########################################################################################

set -e

command -v sharpie >/dev/null 2>&1 || { echo >&2 "error: Could not find sharpie. Please take a look at https://docs.microsoft.com/en-us/xamarin/cross-platform/macios/binding/objective-sharpie/ for further information. Aborting."; exit 1; }

test -d "Bandyer.SDK.iOS" || { echo >&2 "error: This script must be run in ios root folder. Aborting "; exit 1; }

test -d "Binding" && rm -rf "Binding"

sharpie bind -o "Binding/Bandyer" -n "Bandyer" -sdk iphoneos13.6 -f "Pods/Bandyer/Bandyer.framework"

cp -f "Binding/Bandyer/ApiDefinitions.cs" "Bandyer.SDK.iOS/ApiDefinition.cs"
cp -f "Binding/Bandyer/StructsAndEnums.cs" "Bandyer.SDK.iOS/Structs.cs" 