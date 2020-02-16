using System;
using UnityEngine;

// Token: 0x0200058C RID: 1420
public class WeaponManagerScript : MonoBehaviour
{
	// Token: 0x0600225D RID: 8797 RVA: 0x0019E244 File Offset: 0x0019C644
	public void Start()
	{
		for (int i = 0; i < this.Weapons.Length; i++)
		{
			this.Weapons[i].GlobalID = i;
			if (WeaponGlobals.GetWeaponStatus(i) == 1)
			{
				this.Weapons[i].gameObject.SetActive(false);
			}
		}
		this.ChangeBloodTexture();
	}

	// Token: 0x0600225E RID: 8798 RVA: 0x0019E2A0 File Offset: 0x0019C6A0
	public void UpdateLabels()
	{
		foreach (WeaponScript weaponScript in this.Weapons)
		{
			weaponScript.UpdateLabel();
		}
	}

	// Token: 0x0600225F RID: 8799 RVA: 0x0019E2D4 File Offset: 0x0019C6D4
	public void CheckWeapons()
	{
		this.MurderWeapons = 0;
		this.Fingerprints = 0;
		for (int i = 0; i < this.Victims.Length; i++)
		{
			this.Victims[i] = 0;
		}
		foreach (WeaponScript weaponScript in this.Weapons)
		{
			if (weaponScript != null && weaponScript.Blood.enabled && !weaponScript.AlreadyExamined)
			{
				this.MurderWeapons++;
				if (weaponScript.FingerprintID > 0)
				{
					this.Fingerprints++;
					for (int k = 0; k < weaponScript.Victims.Length; k++)
					{
						if (weaponScript.Victims[k])
						{
							this.Victims[k] = weaponScript.FingerprintID;
						}
					}
				}
			}
		}
	}

	// Token: 0x06002260 RID: 8800 RVA: 0x0019E3BC File Offset: 0x0019C7BC
	public void CleanWeapons()
	{
		foreach (WeaponScript weaponScript in this.Weapons)
		{
			if (weaponScript != null)
			{
				weaponScript.Blood.enabled = false;
				weaponScript.FingerprintID = 0;
			}
		}
	}

	// Token: 0x06002261 RID: 8801 RVA: 0x0019E408 File Offset: 0x0019C808
	public void ChangeBloodTexture()
	{
		foreach (WeaponScript weaponScript in this.Weapons)
		{
			if (weaponScript != null)
			{
				if (!GameGlobals.CensorBlood)
				{
					weaponScript.Blood.material.mainTexture = this.Blood;
					weaponScript.Blood.material.SetColor("_TintColor", new Color(0.25f, 0.25f, 0.25f, 0.5f));
				}
				else
				{
					weaponScript.Blood.material.mainTexture = this.Flower;
					weaponScript.Blood.material.SetColor("_TintColor", new Color(0.5f, 0.5f, 0.5f, 0.5f));
				}
			}
		}
	}

	// Token: 0x06002262 RID: 8802 RVA: 0x0019E4D8 File Offset: 0x0019C8D8
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Z))
		{
			this.CheckWeapons();
			for (int i = 0; i < this.Victims.Length; i++)
			{
				if (this.Victims[i] != 0)
				{
					if (this.Victims[i] == 100)
					{
						Debug.Log("The student named " + this.JSON.Students[i].Name + " was killed by Yandere-chan!");
					}
					else
					{
						Debug.Log(string.Concat(new string[]
						{
							"The student named ",
							this.JSON.Students[i].Name,
							" was killed by ",
							this.JSON.Students[this.Victims[i]].Name,
							"!"
						}));
					}
				}
			}
		}
	}

	// Token: 0x06002263 RID: 8803 RVA: 0x0019E5B0 File Offset: 0x0019C9B0
	public void TrackDumpedWeapons()
	{
		for (int i = 0; i < this.Weapons.Length; i++)
		{
			if (this.Weapons[i] == null)
			{
				Debug.Log("Weapon #" + i + " was destroyed! Setting status to 1!");
			}
		}
	}

	// Token: 0x040037DF RID: 14303
	public WeaponScript[] DelinquentWeapons;

	// Token: 0x040037E0 RID: 14304
	public WeaponScript[] Weapons;

	// Token: 0x040037E1 RID: 14305
	public JsonScript JSON;

	// Token: 0x040037E2 RID: 14306
	public int[] Victims;

	// Token: 0x040037E3 RID: 14307
	public int MisplacedWeapons;

	// Token: 0x040037E4 RID: 14308
	public int MurderWeapons;

	// Token: 0x040037E5 RID: 14309
	public int Fingerprints;

	// Token: 0x040037E6 RID: 14310
	public Texture Flower;

	// Token: 0x040037E7 RID: 14311
	public Texture Blood;

	// Token: 0x040037E8 RID: 14312
	public bool YandereGuilty;
}
