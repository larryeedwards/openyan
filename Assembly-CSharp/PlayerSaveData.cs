using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004E8 RID: 1256
[Serializable]
public class PlayerSaveData
{
	// Token: 0x06001F86 RID: 8070 RVA: 0x00141AB4 File Offset: 0x0013FEB4
	public static PlayerSaveData ReadFromGlobals()
	{
		PlayerSaveData playerSaveData = new PlayerSaveData();
		playerSaveData.alerts = PlayerGlobals.Alerts;
		playerSaveData.enlightenment = PlayerGlobals.Enlightenment;
		playerSaveData.enlightenmentBonus = PlayerGlobals.EnlightenmentBonus;
		playerSaveData.headset = PlayerGlobals.Headset;
		playerSaveData.kills = PlayerGlobals.Kills;
		playerSaveData.numbness = PlayerGlobals.Numbness;
		playerSaveData.numbnessBonus = PlayerGlobals.NumbnessBonus;
		playerSaveData.pantiesEquipped = PlayerGlobals.PantiesEquipped;
		playerSaveData.pantyShots = PlayerGlobals.PantyShots;
		foreach (int num in PlayerGlobals.KeysOfPhoto())
		{
			if (PlayerGlobals.GetPhoto(num))
			{
				playerSaveData.photo.Add(num);
			}
		}
		foreach (int num2 in PlayerGlobals.KeysOfPhotoOnCorkboard())
		{
			if (PlayerGlobals.GetPhotoOnCorkboard(num2))
			{
				playerSaveData.photoOnCorkboard.Add(num2);
			}
		}
		foreach (int num3 in PlayerGlobals.KeysOfPhotoPosition())
		{
			playerSaveData.photoPosition.Add(num3, PlayerGlobals.GetPhotoPosition(num3));
		}
		foreach (int num4 in PlayerGlobals.KeysOfPhotoRotation())
		{
			playerSaveData.photoRotation.Add(num4, PlayerGlobals.GetPhotoRotation(num4));
		}
		playerSaveData.reputation = PlayerGlobals.Reputation;
		playerSaveData.seduction = PlayerGlobals.Seduction;
		playerSaveData.seductionBonus = PlayerGlobals.SeductionBonus;
		foreach (int num5 in PlayerGlobals.KeysOfSenpaiPhoto())
		{
			if (PlayerGlobals.GetSenpaiPhoto(num5))
			{
				playerSaveData.senpaiPhoto.Add(num5);
			}
		}
		playerSaveData.senpaiShots = PlayerGlobals.SenpaiShots;
		playerSaveData.socialBonus = PlayerGlobals.SocialBonus;
		playerSaveData.speedBonus = PlayerGlobals.SpeedBonus;
		playerSaveData.stealthBonus = PlayerGlobals.StealthBonus;
		foreach (int num6 in PlayerGlobals.KeysOfStudentFriend())
		{
			if (PlayerGlobals.GetStudentFriend(num6))
			{
				playerSaveData.studentFriend.Add(num6);
			}
		}
		foreach (string text in PlayerGlobals.KeysOfStudentPantyShot())
		{
			if (PlayerGlobals.GetStudentPantyShot(text))
			{
				playerSaveData.studentPantyShot.Add(text);
			}
		}
		return playerSaveData;
	}

	// Token: 0x06001F87 RID: 8071 RVA: 0x00141D28 File Offset: 0x00140128
	public static void WriteToGlobals(PlayerSaveData data)
	{
		PlayerGlobals.Alerts = data.alerts;
		PlayerGlobals.Enlightenment = data.enlightenment;
		PlayerGlobals.EnlightenmentBonus = data.enlightenmentBonus;
		PlayerGlobals.Headset = data.headset;
		PlayerGlobals.Kills = data.kills;
		PlayerGlobals.Numbness = data.numbness;
		PlayerGlobals.NumbnessBonus = data.numbnessBonus;
		PlayerGlobals.PantiesEquipped = data.pantiesEquipped;
		PlayerGlobals.PantyShots = data.pantyShots;
		Debug.Log("Is this being called anywhere?");
		foreach (int photoID in data.photo)
		{
			PlayerGlobals.SetPhoto(photoID, true);
		}
		foreach (int photoID2 in data.photoOnCorkboard)
		{
			PlayerGlobals.SetPhotoOnCorkboard(photoID2, true);
		}
		foreach (KeyValuePair<int, Vector2> keyValuePair in data.photoPosition)
		{
			PlayerGlobals.SetPhotoPosition(keyValuePair.Key, keyValuePair.Value);
		}
		foreach (KeyValuePair<int, float> keyValuePair2 in data.photoRotation)
		{
			PlayerGlobals.SetPhotoRotation(keyValuePair2.Key, keyValuePair2.Value);
		}
		PlayerGlobals.Reputation = data.reputation;
		PlayerGlobals.Seduction = data.seduction;
		PlayerGlobals.SeductionBonus = data.seductionBonus;
		foreach (int photoID3 in data.senpaiPhoto)
		{
			PlayerGlobals.SetSenpaiPhoto(photoID3, true);
		}
		PlayerGlobals.SenpaiShots = data.senpaiShots;
		PlayerGlobals.SocialBonus = data.socialBonus;
		PlayerGlobals.SpeedBonus = data.speedBonus;
		PlayerGlobals.StealthBonus = data.stealthBonus;
		foreach (int studentID in data.studentFriend)
		{
			PlayerGlobals.SetStudentFriend(studentID, true);
		}
		foreach (string studentName in data.studentPantyShot)
		{
			PlayerGlobals.SetStudentPantyShot(studentName, true);
		}
	}

	// Token: 0x04002AD1 RID: 10961
	public int alerts;

	// Token: 0x04002AD2 RID: 10962
	public int enlightenment;

	// Token: 0x04002AD3 RID: 10963
	public int enlightenmentBonus;

	// Token: 0x04002AD4 RID: 10964
	public bool headset;

	// Token: 0x04002AD5 RID: 10965
	public int kills;

	// Token: 0x04002AD6 RID: 10966
	public int numbness;

	// Token: 0x04002AD7 RID: 10967
	public int numbnessBonus;

	// Token: 0x04002AD8 RID: 10968
	public int pantiesEquipped;

	// Token: 0x04002AD9 RID: 10969
	public int pantyShots;

	// Token: 0x04002ADA RID: 10970
	public IntHashSet photo = new IntHashSet();

	// Token: 0x04002ADB RID: 10971
	public IntHashSet photoOnCorkboard = new IntHashSet();

	// Token: 0x04002ADC RID: 10972
	public IntAndVector2Dictionary photoPosition = new IntAndVector2Dictionary();

	// Token: 0x04002ADD RID: 10973
	public IntAndFloatDictionary photoRotation = new IntAndFloatDictionary();

	// Token: 0x04002ADE RID: 10974
	public float reputation;

	// Token: 0x04002ADF RID: 10975
	public int seduction;

	// Token: 0x04002AE0 RID: 10976
	public int seductionBonus;

	// Token: 0x04002AE1 RID: 10977
	public IntHashSet senpaiPhoto = new IntHashSet();

	// Token: 0x04002AE2 RID: 10978
	public int senpaiShots;

	// Token: 0x04002AE3 RID: 10979
	public int socialBonus;

	// Token: 0x04002AE4 RID: 10980
	public int speedBonus;

	// Token: 0x04002AE5 RID: 10981
	public int stealthBonus;

	// Token: 0x04002AE6 RID: 10982
	public IntHashSet studentFriend = new IntHashSet();

	// Token: 0x04002AE7 RID: 10983
	public StringHashSet studentPantyShot = new StringHashSet();
}
