# Photon-cpp

LOCAL_PATH := $(call my-dir)

all_static_libraries = common-cpp-static-prebuilt crypto websockets

include $(CLEAR_VARS)
LOCAL_MODULE            := photon-cpp-static-prebuilt
LOCAL_SRC_FILES         := libphoton-cpp-static_${APP_OPTIM}_android_$(TARGET_ARCH_ABI)$(MY_APP_STDLIB_SUFFIX)$(MY_APP_NORTTI_SUFFIX).a
LOCAL_STATIC_LIBRARIES  := $(all_static_libraries)
include $(PREBUILT_STATIC_LIBRARY)

$(call import-add-path, $(LOCAL_PATH)/../../../Common-cpp/lib)
$(call import-add-path, $(LOCAL_PATH)/../../../3rdparty/lib/android)

$(call import-module,common-cpp-prebuilt)
$(call import-module,crypto)
$(call import-module,websockets)
