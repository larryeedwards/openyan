using System;
using UnityEngine;

namespace AmplifyMotion
{
	// Token: 0x02000298 RID: 664
	[Serializable]
	public class VersionInfo
	{
		// Token: 0x06001538 RID: 5432 RVA: 0x000A3FA3 File Offset: 0x000A23A3
		private VersionInfo()
		{
			this.m_major = 1;
			this.m_minor = 8;
			this.m_release = 3;
		}

		// Token: 0x06001539 RID: 5433 RVA: 0x000A3FC0 File Offset: 0x000A23C0
		private VersionInfo(byte major, byte minor, byte release)
		{
			this.m_major = (int)major;
			this.m_minor = (int)minor;
			this.m_release = (int)release;
		}

		// Token: 0x0600153A RID: 5434 RVA: 0x000A3FDD File Offset: 0x000A23DD
		public static string StaticToString()
		{
			return string.Format("{0}.{1}.{2}", 1, 8, 3) + VersionInfo.StageSuffix + VersionInfo.TrialSuffix;
		}

		// Token: 0x0600153B RID: 5435 RVA: 0x000A400A File Offset: 0x000A240A
		public override string ToString()
		{
			return string.Format("{0}.{1}.{2}", this.m_major, this.m_minor, this.m_release) + VersionInfo.StageSuffix + VersionInfo.TrialSuffix;
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x0600153C RID: 5436 RVA: 0x000A4046 File Offset: 0x000A2446
		public int Number
		{
			get
			{
				return this.m_major * 100 + this.m_minor * 10 + this.m_release;
			}
		}

		// Token: 0x0600153D RID: 5437 RVA: 0x000A4062 File Offset: 0x000A2462
		public static VersionInfo Current()
		{
			return new VersionInfo(1, 8, 3);
		}

		// Token: 0x0600153E RID: 5438 RVA: 0x000A406C File Offset: 0x000A246C
		public static bool Matches(VersionInfo version)
		{
			return version.m_major == 1 && version.m_minor == 8 && 3 == version.m_release;
		}

		// Token: 0x04001216 RID: 4630
		public const byte Major = 1;

		// Token: 0x04001217 RID: 4631
		public const byte Minor = 8;

		// Token: 0x04001218 RID: 4632
		public const byte Release = 3;

		// Token: 0x04001219 RID: 4633
		private static string StageSuffix = "_dev001";

		// Token: 0x0400121A RID: 4634
		private static string TrialSuffix = string.Empty;

		// Token: 0x0400121B RID: 4635
		[SerializeField]
		private int m_major;

		// Token: 0x0400121C RID: 4636
		[SerializeField]
		private int m_minor;

		// Token: 0x0400121D RID: 4637
		[SerializeField]
		private int m_release;
	}
}
