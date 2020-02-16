using System;

// Token: 0x020004FA RID: 1274
public static class SchoolAtmosphere
{
	// Token: 0x1700049A RID: 1178
	// (get) Token: 0x06001FC6 RID: 8134 RVA: 0x00146018 File Offset: 0x00144418
	public static SchoolAtmosphereType Type
	{
		get
		{
			float schoolAtmosphere = SchoolGlobals.SchoolAtmosphere;
			if (schoolAtmosphere > 0.6666667f)
			{
				return SchoolAtmosphereType.High;
			}
			if (schoolAtmosphere > 0.333333343f)
			{
				return SchoolAtmosphereType.Medium;
			}
			return SchoolAtmosphereType.Low;
		}
	}
}
