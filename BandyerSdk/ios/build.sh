# download sharpie from
# https://download.visualstudio.microsoft.com/download/pr/693cb4a2-5455-4841-904f-a2936d2781f4/3890cb5f74ea75ff4474152f1f2b8008/objectivesharpie-3.5.22.pkg
# url taken from
# https://raw.githubusercontent.com/xamarin/xamarin-macios/master/Make.config

rm -rf Pods
rm -rf Binding
rm -rf bin
pod repo update
pod install
sharpie pod bind
