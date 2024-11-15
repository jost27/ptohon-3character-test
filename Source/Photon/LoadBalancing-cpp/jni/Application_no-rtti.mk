APP_MODULES := loadbalancing-cpp-static
APP_ABI := armeabi-v7a,arm64-v8a,x86,x86_64
APP_CFLAGS := -D_EG_ANDROID_PLATFORM
APP_CPPFLAGS := -fno-rtti -std=c++11
MY_APP_NORTTI_SUFFIX := _no-rtti
APP_STL := gnustl_static
