using System;

// Token: 0x020004E7 RID: 1255
[Serializable]
public class OptionSaveData
{
	// Token: 0x06001F83 RID: 8067 RVA: 0x00141928 File Offset: 0x0013FD28
	public static OptionSaveData ReadFromGlobals()
	{
		return new OptionSaveData
		{
			disableBloom = OptionGlobals.DisableBloom,
			disableFarAnimations = OptionGlobals.DisableFarAnimations,
			disableOutlines = OptionGlobals.DisableOutlines,
			disablePostAliasing = OptionGlobals.DisablePostAliasing,
			enableShadows = OptionGlobals.EnableShadows,
			drawDistance = OptionGlobals.DrawDistance,
			drawDistanceLimit = OptionGlobals.DrawDistanceLimit,
			fog = OptionGlobals.Fog,
			fpsIndex = OptionGlobals.FPSIndex,
			highPopulation = OptionGlobals.HighPopulation,
			lowDetailStudents = OptionGlobals.LowDetailStudents,
			particleCount = OptionGlobals.ParticleCount
		};
	}

	// Token: 0x06001F84 RID: 8068 RVA: 0x001419C0 File Offset: 0x0013FDC0
	public static void WriteToGlobals(OptionSaveData data)
	{
		OptionGlobals.DisableBloom = data.disableBloom;
		OptionGlobals.DisableFarAnimations = data.disableFarAnimations;
		OptionGlobals.DisableOutlines = data.disableOutlines;
		OptionGlobals.DisablePostAliasing = data.disablePostAliasing;
		OptionGlobals.EnableShadows = data.enableShadows;
		OptionGlobals.DrawDistance = data.drawDistance;
		OptionGlobals.DrawDistanceLimit = data.drawDistanceLimit;
		OptionGlobals.Fog = data.fog;
		OptionGlobals.FPSIndex = data.fpsIndex;
		OptionGlobals.HighPopulation = data.highPopulation;
		OptionGlobals.LowDetailStudents = data.lowDetailStudents;
		OptionGlobals.ParticleCount = data.particleCount;
	}

	// Token: 0x04002AC5 RID: 10949
	public bool disableBloom;

	// Token: 0x04002AC6 RID: 10950
	public int disableFarAnimations = 5;

	// Token: 0x04002AC7 RID: 10951
	public bool disableOutlines;

	// Token: 0x04002AC8 RID: 10952
	public bool disablePostAliasing;

	// Token: 0x04002AC9 RID: 10953
	public bool enableShadows;

	// Token: 0x04002ACA RID: 10954
	public int drawDistance;

	// Token: 0x04002ACB RID: 10955
	public int drawDistanceLimit;

	// Token: 0x04002ACC RID: 10956
	public bool fog;

	// Token: 0x04002ACD RID: 10957
	public int fpsIndex;

	// Token: 0x04002ACE RID: 10958
	public bool highPopulation;

	// Token: 0x04002ACF RID: 10959
	public int lowDetailStudents;

	// Token: 0x04002AD0 RID: 10960
	public int particleCount;
}
