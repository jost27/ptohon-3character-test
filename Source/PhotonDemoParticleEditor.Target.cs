// Copyright 1998-2014 Epic Games, Inc. All Rights Reserved.

using UnrealBuildTool;
using System.Collections.Generic;

public class PhotonDemoParticleEditorTarget : TargetRules
{
	public PhotonDemoParticleEditorTarget(TargetInfo Target) : base(Target)
    {
		bOverrideBuildEnvironment = true;
		Type = TargetType.Editor;
		ExtraModuleNames.Add("PhotonDemoParticle");
	}
}
