nuget restore BandyerSdk.iOS/BandyerSdk.iOS.csproj
msbuild /p:Configuration=Release /t:Clean /target:Build BandyerSdk.iOS/BandyerSdk.iOS.csproj
nuget pack BandyerSdk.iOS/BandyerSdk.iOS.nuspec -Verbosity detailed
