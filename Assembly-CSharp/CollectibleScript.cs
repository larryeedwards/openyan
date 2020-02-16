using System;
using UnityEngine;

// Token: 0x0200036F RID: 879
public class CollectibleScript : MonoBehaviour
{
	// Token: 0x06001803 RID: 6147 RVA: 0x000C43A8 File Offset: 0x000C27A8
	private void Start()
	{
		bool flag = (this.CollectibleType == CollectibleType.BasementTape && CollectibleGlobals.GetBasementTapeCollected(this.ID)) || (this.CollectibleType == CollectibleType.Manga && CollectibleGlobals.GetMangaCollected(this.ID)) || (this.CollectibleType == CollectibleType.Tape && CollectibleGlobals.GetTapeCollected(this.ID));
		if (flag)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
		if (GameGlobals.LoveSick || MissionModeGlobals.MissionMode)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x17000380 RID: 896
	// (get) Token: 0x06001804 RID: 6148 RVA: 0x000C443C File Offset: 0x000C283C
	public CollectibleType CollectibleType
	{
		get
		{
			if (this.Name == "HeadmasterTape")
			{
				return CollectibleType.HeadmasterTape;
			}
			if (this.Name == "BasementTape")
			{
				return CollectibleType.BasementTape;
			}
			if (this.Name == "Manga")
			{
				return CollectibleType.Manga;
			}
			if (this.Name == "Tape")
			{
				return CollectibleType.Tape;
			}
			if (this.Type == 5)
			{
				return CollectibleType.Key;
			}
			Debug.LogError("Unrecognized collectible \"" + this.Name + "\".", base.gameObject);
			return CollectibleType.Tape;
		}
	}

	// Token: 0x06001805 RID: 6149 RVA: 0x000C44D4 File Offset: 0x000C28D4
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			if (this.CollectibleType == CollectibleType.HeadmasterTape)
			{
				CollectibleGlobals.SetHeadmasterTapeCollected(this.ID, true);
			}
			else if (this.CollectibleType == CollectibleType.BasementTape)
			{
				CollectibleGlobals.SetBasementTapeCollected(this.ID, true);
			}
			else if (this.CollectibleType == CollectibleType.Manga)
			{
				CollectibleGlobals.SetMangaCollected(this.ID, true);
			}
			else if (this.CollectibleType == CollectibleType.Tape)
			{
				CollectibleGlobals.SetTapeCollected(this.ID, true);
			}
			else if (this.CollectibleType == CollectibleType.Key)
			{
				this.Prompt.Yandere.Inventory.MysteriousKeys++;
			}
			else
			{
				Debug.LogError("Collectible \"" + this.Name + "\" not implemented.", base.gameObject);
			}
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x040018C8 RID: 6344
	public PromptScript Prompt;

	// Token: 0x040018C9 RID: 6345
	public string Name = string.Empty;

	// Token: 0x040018CA RID: 6346
	public int Type;

	// Token: 0x040018CB RID: 6347
	public int ID;
}
