# LoadBalancing-cpp

LOCAL_PATH := $(call my-dir)

all_static_libraries = photon-cpp-static-prebuilt

include $(CLEAR_VARS)
LOCAL_MODULE            := loadbalancing-cpp-static-prebuilt
LOCAL_SRC_FILES         := libloadbalancing-cpp-static_${APP_OPTIM}_android_$(TARGET_ARCH_ABI)$(MY_APP_STDLIB_SUFFIX)$(MY_APP_NORTTI_SUFFIX).a
LOCAL_STATIC_LIBRARIES  := $(all_static_libraries)
include $(PREBUILT_STATIC_LIBRARY)

$(call import-add-path, $(LOCAL_PATH)/../../../Photon-cpp/lib)

$(call import-module,photon-cpp-prebuilt)


