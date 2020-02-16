using System;

// Token: 0x020004DC RID: 1244
[Serializable]
public class ApplicationSaveData
{
	// Token: 0x06001F62 RID: 8034 RVA: 0x00140A64 File Offset: 0x0013EE64
	public static ApplicationSaveData ReadFromGlobals()
	{
		return new ApplicationSaveData
		{
			versionNumber = ApplicationGlobals.VersionNumber
		};
	}

	// Token: 0x06001F63 RID: 8035 RVA: 0x00140A83 File Offset: 0x0013EE83
	public static void WriteToGlobals(ApplicationSaveData data)
	{
		ApplicationGlobals.VersionNumber = data.versionNumber;
	}

	// Token: 0x04002A8C RID: 10892
	public float versionNumber;
}
