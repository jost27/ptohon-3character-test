// Copyright 1998-2014 Epic Games, Inc. All Rights Reserved.

// https://wiki.unrealengine.com/Linking_Static_Libraries_Using_The_Build_System

using System;
using System.IO;
using UnrealBuildTool;

public class PhotonDemoParticle : ModuleRules
{
	public PhotonDemoParticle(ReadOnlyTargetRules Target) : base(Target)
	{
		PCHUsage = PCHUsageMode.UseExplicitOrSharedPCHs;
		MinFilesUsingPrecompiledHeaderOverride = 1;

		PublicDependencyModuleNames.AddRange(new string[] { "Core", "CoreUObject", "Engine", "InputCore" });

		PrivateDependencyModuleNames.AddRange(new string[] { });

		// Uncomment if you are using Slate UI
		// PrivateDependencyModuleNames.AddRange(new string[] { "Slate", "SlateCore" });

		// Uncomment if you are using online features
		// PrivateDependencyModuleNames.Add("OnlineSubsystem");
		// if ((Target.Platform == UnrealTargetPlatform.Win32) || (Target.Platform == UnrealTargetPlatform.Win64))
		// {
		//		if (UEBuildConfiguration.bCompileSteamOSS == true)
		//		{
		//			DynamicallyLoadedModuleNames.Add("OnlineSubsystemSteam");
		//		}
		// }

		LoadPhoton(Target);
	}

	private string ModulePath
	{
		get { return ModuleDirectory; }
	}

	private string PhotonPath
	{
		get { return Path.GetFullPath(Path.Combine(ModulePath, "..", "Photon")); }
	}

	private string PlatformString
	{
		get { return Target.Platform == UnrealTargetPlatform.Win64 ? "x64" : "Win32"; }
	}

	private void AddPhotonLibPathWin(ReadOnlyTargetRules Target, string name)
	{
		
		#if UE_5_4_OR_LATER
			PublicAdditionalLibraries.Add(Path.Combine(PhotonPath, "lib", "Windows", name + "-cpp_vc17_release_windows_md_" + PlatformString + ".lib"));
		#elif UE_4_25_OR_LATER
			PublicAdditionalLibraries.Add(Path.Combine(PhotonPath, "lib", "Windows", name + "-cpp_vc16_release_windows_md_" + PlatformString + ".lib"));
		#else
			PublicAdditionalLibraries.Add(Path.Combine(PhotonPath, "lib", "Windows", name + "-cpp_vc15_release_windows_md_" + PlatformString + ".lib"));
		#endif
	}

	private void AddPhotonLibPathAndroid(ReadOnlyTargetRules Target, string name)
	{
		#if UE_4_25_OR_LATER
			PublicAdditionalLibraries.Add(Path.Combine(PhotonPath, "lib", "Android", "lib" + name + "_debug_android_armeabi-v7a_libc++_no-rtti.a"));
		#else
			PublicAdditionalLibraries.Add(Path.Combine(PhotonPath, "lib", "Android", "lib" + name + "_debug_android_armeabi-v7a_no-rtti.a"));
		#endif
	}

	private void AddPhotonLibPathIOS(ReadOnlyTargetRules Target, string name)
	{
		#if UE_5_2_OR_LATER
			string archStr = (Target.Architecture == UnrealArch.IOSSimulator) ? "iphonesimulator" : "iphoneos";
		#else
			string archStr = (Target.Architecture == "-simulator") ? "iphonesimulator" : "iphoneos";
		#endif
		PublicAdditionalLibraries.Add(Path.Combine(PhotonPath, "lib", "iOS", "lib" + name + "_debug_" + archStr + ".a"));
	}

	private void AddPhotonLibPathMac(ReadOnlyTargetRules Target, string name)
	{
		PublicAdditionalLibraries.Add(Path.Combine(PhotonPath, "lib", "Mac", "lib" + name + "_debug_macosx.a"));
	}

	private void AddPhotonLibPathHTML5(ReadOnlyTargetRules Target, string name)
	{
		PublicAdditionalLibraries.Add(Path.Combine(PhotonPath, "lib", "HTML5", name + "-cpp_release_emscripten_Emscripten_unreal.bc"));
	}

	public bool LoadPhoton(ReadOnlyTargetRules Target)
	{
	#if UE_5_0_OR_LATER
		if (Target.Platform == UnrealTargetPlatform.Win64)
	#else
		if ((Target.Platform == UnrealTargetPlatform.Win64) || (Target.Platform == UnrealTargetPlatform.Win32))
	#endif
		{
			PublicDefinitions.Add("_EG_WINDOWS_PLATFORM");
			AddPhotonLibPathWin(Target, "Common");
			AddPhotonLibPathWin(Target, "Photon");
			AddPhotonLibPathWin(Target, "LoadBalancing");
		}
		else if (Target.Platform == UnrealTargetPlatform.Android)
		{
			PublicDefinitions.Add("_EG_ANDROID_PLATFORM");
			AddPhotonLibPathAndroid(Target, "common-cpp-static");
			AddPhotonLibPathAndroid(Target, "photon-cpp-static");
			AddPhotonLibPathAndroid(Target, "loadbalancing-cpp-static");
			AddPhotonLibPathAndroid(Target, "ssl");
			AddPhotonLibPathAndroid(Target, "websockets");
			AddPhotonLibPathAndroid(Target, "crypto");
    //
			PublicAdditionalLibraries.Add(Path.Combine(PhotonPath, "lib", "Android", "libcommon-cpp-static_debug_android_armeabi_no-rtti.a"));
			PublicAdditionalLibraries.Add(Path.Combine(PhotonPath, "lib", "Android", "libphoton-cpp-static_debug_android_armeabi_no-rtti.a"));
			PublicAdditionalLibraries.Add(Path.Combine(PhotonPath, "lib", "Android", "libloadbalancing-cpp-static_debug_android_armeabi_no-rtti.a"));

		}
		else if (Target.Platform == UnrealTargetPlatform.IOS)
		{
			PublicDefinitions.Add("_EG_IPHONE_PLATFORM");
			AddPhotonLibPathIOS(Target, "Common-cpp");
			AddPhotonLibPathIOS(Target, "Photon-cpp");
			AddPhotonLibPathIOS(Target, "LoadBalancing-cpp");
			AddPhotonLibPathIOS(Target, "crypto");
		}
		else if (Target.Platform == UnrealTargetPlatform.Mac)
		{
			PublicDefinitions.Add("_EG_IMAC_PLATFORM");
			AddPhotonLibPathMac(Target, "Common-cpp");
			AddPhotonLibPathMac(Target, "Photon-cpp");
			AddPhotonLibPathMac(Target, "LoadBalancing-cpp");
			AddPhotonLibPathMac(Target, "crypto");
		}
	#if !UE_4_24_OR_LATER
		else if (Target.Platform == UnrealTargetPlatform.HTML5)
		{
			PublicDefinitions.Add("_EG_EMSCRIPTEN_PLATFORM");
			AddPhotonLibPathHTML5(Target, "Common");
			AddPhotonLibPathHTML5(Target, "Photon");
			AddPhotonLibPathHTML5(Target, "LoadBalancing");
		}
	#endif
		else
		{
			throw new Exception("\nTarget platform not supported: " + Target.Platform);
		}

		// Include path
		PublicIncludePaths.Add(Path.Combine(PhotonPath, "."));

		return true;
	}
}

