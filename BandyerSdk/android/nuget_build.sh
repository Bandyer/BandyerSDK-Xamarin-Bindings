# Bandyer.Common.Android
nuget restore Bandyer.Common.Android/Bandyer.Common.Android.csproj
msbuild /p:Configuration=Release /t:Clean /target:Build Bandyer.Common.Android/Bandyer.Common.Android.csproj
nuget pack Bandyer.Common.Android/Bandyer.Common.Android.nuspec -Verbosity detailed

# Bandyer.AudioSession.Android
nuget restore Bandyer.AudioSession.Android/Bandyer.AudioSession.Android.csproj
msbuild /p:Configuration=Release /t:Clean /target:Build Bandyer.AudioSession.Android/Bandyer.AudioSession.Android.csproj
nuget pack Bandyer.AudioSession.Android/Bandyer.AudioSession.Android.nuspec -Verbosity detailed

# Bandyer.CoreAV.Android
nuget restore Bandyer.CoreAV.Android/Bandyer.CoreAV.Android.csproj
msbuild /p:Configuration=Release /t:Clean /target:Build Bandyer.CoreAV.Android/Bandyer.CoreAV.Android.csproj
nuget pack Bandyer.CoreAV.Android/Bandyer.CoreAV.Android.nuspec -Verbosity detailed

# Bandyer.CommunicationCenter.Android
nuget restore Bandyer.CommunicationCenter.Android/Bandyer.CommunicationCenter.Android.csproj
msbuild /p:Configuration=Release /t:Clean /target:Build Bandyer.CommunicationCenter.Android/Bandyer.CommunicationCenter.Android.csproj
nuget pack Bandyer.CommunicationCenter.Android/Bandyer.CommunicationCenter.Android.nuspec -Verbosity detailed

# Bandyer.Design.Android
nuget restore Bandyer.Design.Android/Bandyer.Design.Android.csproj
msbuild /p:Configuration=Release /t:Clean /target:Build Bandyer.Design.Android/Bandyer.Design.Android.csproj
nuget pack Bandyer.Design.Android/Bandyer.Design.Android.nuspec -Verbosity detailed

# Bandyer.Chat.Android
nuget restore Bandyer.Chat.Android/Bandyer.Chat.Android.csproj
msbuild /p:Configuration=Release /t:Clean /target:Build Bandyer.Chat.Android/Bandyer.Chat.Android.csproj
nuget pack Bandyer.Chat.Android/Bandyer.Chat.Android.nuspec -Verbosity detailed

# Bandyer.Android
nuget restore Bandyer.SDK.Android/Bandyer.SDK.Android.csproj
msbuild /p:Configuration=Release /t:Clean /target:Build Bandyer.SDK.Android/Bandyer.SDK.Android.csproj
nuget pack Bandyer.SDK.Android/Bandyer.SDK.Android.nuspec -Verbosity detailed