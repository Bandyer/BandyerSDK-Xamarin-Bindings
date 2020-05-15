pod repo update
pod install

cd Pods/Bandyer/Bandyer.framework/
lipo -remove i386 Bandyer -o Bandyer
lipo -remove x86_64 Bandyer -o Bandyer
cd -
cd Pods/Bandyer/WebRTC.framework/
lipo -remove i386 WebRTC -o WebRTC
lipo -remove x86_64 WebRTC -o WebRTC
cd -
cd Pods/TwilioChatClient/TwilioChatClient.framework/
lipo -remove i386 TwilioChatClient -o TwilioChatClient
lipo -remove x86_64 TwilioChatClient -o TwilioChatClient
cd -

xcodebuild -quiet -workspace Pods/Pods.xcodeproj -scheme Starscream-framework -configuration Release -sdk iphoneos clean build
xcodebuild -quiet -workspace Pods/Pods.xcodeproj -scheme Socket.IO-Client-Swift -configuration Release -sdk iphoneos clean build

mkdir -p frameworks/
cp -r Pods/Bandyer/Bandyer.framework frameworks/
cp -r Pods/Bandyer/WebRTC.framework frameworks/
cp -r Pods/TwilioChatClient/TwilioChatClient.framework frameworks/
cp -r build/Release-iphoneos/Starscream-framework/Starscream.framework frameworks/
cp -r build/Release-iphoneos/Socket.IO-Client-Swift-framework/SocketIO.framework frameworks/

lipo -archs frameworks/Bandyer.framework/Bandyer
lipo -archs frameworks/WebRTC.framework/WebRTC
lipo -archs frameworks/TwilioChatClient.framework/TwilioChatClient
lipo -archs frameworks/Starscream.framework/Starscream
lipo -archs frameworks/SocketIO.framework/SocketIO
