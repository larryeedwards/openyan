using System;

// Token: 0x020004E4 RID: 1252
[Serializable]
public class GameSaveData
{
	// Token: 0x06001F7A RID: 8058 RVA: 0x001416E4 File Offset: 0x0013FAE4
	public static GameSaveData ReadFromGlobals()
	{
		return new GameSaveData
		{
			loveSick = GameGlobals.LoveSick,
			masksBanned = GameGlobals.MasksBanned,
			paranormal = GameGlobals.Paranormal
		};
	}

	// Token: 0x06001F7B RID: 8059 RVA: 0x00141719 File Offset: 0x0013FB19
	public static void WriteToGlobals(GameSaveData data)
	{
		GameGlobals.LoveSick = data.loveSick;
		GameGlobals.MasksBanned = data.masksBanned;
		GameGlobals.Paranormal = data.paranormal;
	}

	// Token: 0x04002AB6 RID: 10934
	public bool loveSick;

	// Token: 0x04002AB7 RID: 10935
	public bool masksBanned;

	// Token: 0x04002AB8 RID: 10936
	public bool paranormal;
}
