using System;
using UnityEngine;

// Token: 0x02000403 RID: 1027
public static class CounselorGlobals
{
	// Token: 0x17000458 RID: 1112
	// (get) Token: 0x06001C18 RID: 7192 RVA: 0x000FBF81 File Offset: 0x000FA381
	// (set) Token: 0x06001C19 RID: 7193 RVA: 0x000FBFA1 File Offset: 0x000FA3A1
	public static int DelinquentPunishments
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_DelinquentPunishments");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_DelinquentPunishments", value);
		}
	}

	// Token: 0x17000459 RID: 1113
	// (get) Token: 0x06001C1A RID: 7194 RVA: 0x000FBFC2 File Offset: 0x000FA3C2
	// (set) Token: 0x06001C1B RID: 7195 RVA: 0x000FBFE2 File Offset: 0x000FA3E2
	public static int CounselorPunishments
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_CounselorPunishments");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_CounselorPunishments", value);
		}
	}

	// Token: 0x1700045A RID: 1114
	// (get) Token: 0x06001C1C RID: 7196 RVA: 0x000FC003 File Offset: 0x000FA403
	// (set) Token: 0x06001C1D RID: 7197 RVA: 0x000FC023 File Offset: 0x000FA423
	public static int CounselorVisits
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_CounselorVisits");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_CounselorVisits", value);
		}
	}

	// Token: 0x1700045B RID: 1115
	// (get) Token: 0x06001C1E RID: 7198 RVA: 0x000FC044 File Offset: 0x000FA444
	// (set) Token: 0x06001C1F RID: 7199 RVA: 0x000FC064 File Offset: 0x000FA464
	public static int CounselorTape
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_CounselorTape");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_CounselorTape", value);
		}
	}

	// Token: 0x1700045C RID: 1116
	// (get) Token: 0x06001C20 RID: 7200 RVA: 0x000FC085 File Offset: 0x000FA485
	// (set) Token: 0x06001C21 RID: 7201 RVA: 0x000FC0A5 File Offset: 0x000FA4A5
	public static int ApologiesUsed
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_ApologiesUsed");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_ApologiesUsed", value);
		}
	}

	// Token: 0x1700045D RID: 1117
	// (get) Token: 0x06001C22 RID: 7202 RVA: 0x000FC0C6 File Offset: 0x000FA4C6
	// (set) Token: 0x06001C23 RID: 7203 RVA: 0x000FC0E6 File Offset: 0x000FA4E6
	public static int WeaponsBanned
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_WeaponsBanned");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_WeaponsBanned", value);
		}
	}

	// Token: 0x1700045E RID: 1118
	// (get) Token: 0x06001C24 RID: 7204 RVA: 0x000FC107 File Offset: 0x000FA507
	// (set) Token: 0x06001C25 RID: 7205 RVA: 0x000FC127 File Offset: 0x000FA527
	public static int BloodVisits
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_BloodVisits");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_BloodVisits", value);
		}
	}

	// Token: 0x1700045F RID: 1119
	// (get) Token: 0x06001C26 RID: 7206 RVA: 0x000FC148 File Offset: 0x000FA548
	// (set) Token: 0x06001C27 RID: 7207 RVA: 0x000FC168 File Offset: 0x000FA568
	public static int InsanityVisits
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_InsanityVisits");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_InsanityVisits", value);
		}
	}

	// Token: 0x17000460 RID: 1120
	// (get) Token: 0x06001C28 RID: 7208 RVA: 0x000FC189 File Offset: 0x000FA589
	// (set) Token: 0x06001C29 RID: 7209 RVA: 0x000FC1A9 File Offset: 0x000FA5A9
	public static int LewdVisits
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_LewdVisits");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_LewdVisits", value);
		}
	}

	// Token: 0x17000461 RID: 1121
	// (get) Token: 0x06001C2A RID: 7210 RVA: 0x000FC1CA File Offset: 0x000FA5CA
	// (set) Token: 0x06001C2B RID: 7211 RVA: 0x000FC1EA File Offset: 0x000FA5EA
	public static int TheftVisits
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_TheftVisits");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_TheftVisits", value);
		}
	}

	// Token: 0x17000462 RID: 1122
	// (get) Token: 0x06001C2C RID: 7212 RVA: 0x000FC20B File Offset: 0x000FA60B
	// (set) Token: 0x06001C2D RID: 7213 RVA: 0x000FC22B File Offset: 0x000FA62B
	public static int TrespassVisits
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_TrespassVisits");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_TrespassVisits", value);
		}
	}

	// Token: 0x17000463 RID: 1123
	// (get) Token: 0x06001C2E RID: 7214 RVA: 0x000FC24C File Offset: 0x000FA64C
	// (set) Token: 0x06001C2F RID: 7215 RVA: 0x000FC26C File Offset: 0x000FA66C
	public static int WeaponVisits
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_WeaponVisits");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_WeaponVisits", value);
		}
	}

	// Token: 0x17000464 RID: 1124
	// (get) Token: 0x06001C30 RID: 7216 RVA: 0x000FC28D File Offset: 0x000FA68D
	// (set) Token: 0x06001C31 RID: 7217 RVA: 0x000FC2AD File Offset: 0x000FA6AD
	public static int BloodExcuseUsed
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_BloodExcuseUsed");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_BloodExcuseUsed", value);
		}
	}

	// Token: 0x17000465 RID: 1125
	// (get) Token: 0x06001C32 RID: 7218 RVA: 0x000FC2CE File Offset: 0x000FA6CE
	// (set) Token: 0x06001C33 RID: 7219 RVA: 0x000FC2EE File Offset: 0x000FA6EE
	public static int InsanityExcuseUsed
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_InsanityExcuseUsed");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_InsanityExcuseUsed", value);
		}
	}

	// Token: 0x17000466 RID: 1126
	// (get) Token: 0x06001C34 RID: 7220 RVA: 0x000FC30F File Offset: 0x000FA70F
	// (set) Token: 0x06001C35 RID: 7221 RVA: 0x000FC32F File Offset: 0x000FA72F
	public static int LewdExcuseUsed
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_LewdExcuseUsed");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_LewdExcuseUsed", value);
		}
	}

	// Token: 0x17000467 RID: 1127
	// (get) Token: 0x06001C36 RID: 7222 RVA: 0x000FC350 File Offset: 0x000FA750
	// (set) Token: 0x06001C37 RID: 7223 RVA: 0x000FC370 File Offset: 0x000FA770
	public static int TheftExcuseUsed
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_TheftExcuseUsed");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_TheftExcuseUsed", value);
		}
	}

	// Token: 0x17000468 RID: 1128
	// (get) Token: 0x06001C38 RID: 7224 RVA: 0x000FC391 File Offset: 0x000FA791
	// (set) Token: 0x06001C39 RID: 7225 RVA: 0x000FC3B1 File Offset: 0x000FA7B1
	public static int TrespassExcuseUsed
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_TrespassExcuseUsed");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_TrespassExcuseUsed", value);
		}
	}

	// Token: 0x17000469 RID: 1129
	// (get) Token: 0x06001C3A RID: 7226 RVA: 0x000FC3D2 File Offset: 0x000FA7D2
	// (set) Token: 0x06001C3B RID: 7227 RVA: 0x000FC3F2 File Offset: 0x000FA7F2
	public static int WeaponExcuseUsed
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_WeaponExcuseUsed");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_WeaponExcuseUsed", value);
		}
	}

	// Token: 0x1700046A RID: 1130
	// (get) Token: 0x06001C3C RID: 7228 RVA: 0x000FC413 File Offset: 0x000FA813
	// (set) Token: 0x06001C3D RID: 7229 RVA: 0x000FC433 File Offset: 0x000FA833
	public static int BloodBlameUsed
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_BloodBlameUsed");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_BloodBlameUsed", value);
		}
	}

	// Token: 0x1700046B RID: 1131
	// (get) Token: 0x06001C3E RID: 7230 RVA: 0x000FC454 File Offset: 0x000FA854
	// (set) Token: 0x06001C3F RID: 7231 RVA: 0x000FC474 File Offset: 0x000FA874
	public static int InsanityBlameUsed
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_InsanityBlameUsed");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_InsanityBlameUsed", value);
		}
	}

	// Token: 0x1700046C RID: 1132
	// (get) Token: 0x06001C40 RID: 7232 RVA: 0x000FC495 File Offset: 0x000FA895
	// (set) Token: 0x06001C41 RID: 7233 RVA: 0x000FC4B5 File Offset: 0x000FA8B5
	public static int LewdBlameUsed
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_LewdBlameUsed");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_LewdBlameUsed", value);
		}
	}

	// Token: 0x1700046D RID: 1133
	// (get) Token: 0x06001C42 RID: 7234 RVA: 0x000FC4D6 File Offset: 0x000FA8D6
	// (set) Token: 0x06001C43 RID: 7235 RVA: 0x000FC4F6 File Offset: 0x000FA8F6
	public static int TheftBlameUsed
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_TheftBlameUsed");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_TheftBlameUsed", value);
		}
	}

	// Token: 0x1700046E RID: 1134
	// (get) Token: 0x06001C44 RID: 7236 RVA: 0x000FC517 File Offset: 0x000FA917
	// (set) Token: 0x06001C45 RID: 7237 RVA: 0x000FC537 File Offset: 0x000FA937
	public static int TrespassBlameUsed
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_TrespassBlameUsed");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_TrespassBlameUsed", value);
		}
	}

	// Token: 0x1700046F RID: 1135
	// (get) Token: 0x06001C46 RID: 7238 RVA: 0x000FC558 File Offset: 0x000FA958
	// (set) Token: 0x06001C47 RID: 7239 RVA: 0x000FC578 File Offset: 0x000FA978
	public static int WeaponBlameUsed
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_WeaponBlameUsed");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_WeaponBlameUsed", value);
		}
	}

	// Token: 0x06001C48 RID: 7240 RVA: 0x000FC59C File Offset: 0x000FA99C
	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_DelinquentPunishments");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_CounselorPunishments");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_CounselorVisits");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_CounselorTape");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_ApologiesUsed");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_WeaponsBanned");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_BloodVisits");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_InsanityVisits");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_LewdVisits");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_TheftVisits");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_TrespassVisits");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_WeaponVisits");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_BloodExcuseUsed");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_InsanityExcuseUsed");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_LewdExcuseUsed");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_TheftExcuseUsed");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_TrespassExcuseUsed");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_WeaponExcuseUsed");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_BloodBlameUsed");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_InsanityBlameUsed");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_LewdBlameUsed");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_TheftBlameUsed");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_TrespassBlameUsed");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_WeaponBlameUsed");
	}

	// Token: 0x0400204B RID: 8267
	private const string Str_DelinquentPunishments = "DelinquentPunishments";

	// Token: 0x0400204C RID: 8268
	private const string Str_CounselorPunishments = "CounselorPunishments";

	// Token: 0x0400204D RID: 8269
	private const string Str_CounselorVisits = "CounselorVisits";

	// Token: 0x0400204E RID: 8270
	private const string Str_CounselorTape = "CounselorTape";

	// Token: 0x0400204F RID: 8271
	private const string Str_ApologiesUsed = "ApologiesUsed";

	// Token: 0x04002050 RID: 8272
	private const string Str_WeaponsBanned = "WeaponsBanned";

	// Token: 0x04002051 RID: 8273
	private const string Str_BloodVisits = "BloodVisits";

	// Token: 0x04002052 RID: 8274
	private const string Str_InsanityVisits = "InsanityVisits";

	// Token: 0x04002053 RID: 8275
	private const string Str_LewdVisits = "LewdVisits";

	// Token: 0x04002054 RID: 8276
	private const string Str_TheftVisits = "TheftVisits";

	// Token: 0x04002055 RID: 8277
	private const string Str_TrespassVisits = "TrespassVisits";

	// Token: 0x04002056 RID: 8278
	private const string Str_WeaponVisits = "WeaponVisits";

	// Token: 0x04002057 RID: 8279
	private const string Str_BloodExcuseUsed = "BloodExcuseUsed";

	// Token: 0x04002058 RID: 8280
	private const string Str_InsanityExcuseUsed = "InsanityExcuseUsed";

	// Token: 0x04002059 RID: 8281
	private const string Str_LewdExcuseUsed = "LewdExcuseUsed";

	// Token: 0x0400205A RID: 8282
	private const string Str_TheftExcuseUsed = "TheftExcuseUsed";

	// Token: 0x0400205B RID: 8283
	private const string Str_TrespassExcuseUsed = "TrespassExcuseUsed";

	// Token: 0x0400205C RID: 8284
	private const string Str_WeaponExcuseUsed = "WeaponExcuseUsed";

	// Token: 0x0400205D RID: 8285
	private const string Str_BloodBlameUsed = "BloodBlameUsed";

	// Token: 0x0400205E RID: 8286
	private const string Str_InsanityBlameUsed = "InsanityBlameUsed";

	// Token: 0x0400205F RID: 8287
	private const string Str_LewdBlameUsed = "LewdBlameUsed";

	// Token: 0x04002060 RID: 8288
	private const string Str_TheftBlameUsed = "TheftBlameUsed";

	// Token: 0x04002061 RID: 8289
	private const string Str_TrespassBlameUsed = "TrespassBlameUsed";

	// Token: 0x04002062 RID: 8290
	private const string Str_WeaponBlameUsed = "WeaponBlameUsed";
}
