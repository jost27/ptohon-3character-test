APP_MODULES := loadbalancing-cpp-static
APP_ABI := armeabi-v7a,arm64-v8a,x86,x86_64
APP_CFLAGS := -D_EG_ANDROID_PLATFORM
APP_CPPFLAGS := -frtti
APP_STL := c++_static
MY_APP_STDLIB_SUFFIX := _libc++