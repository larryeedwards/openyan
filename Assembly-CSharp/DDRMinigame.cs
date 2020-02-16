using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200012B RID: 299
public class DDRMinigame : MonoBehaviour
{
	// Token: 0x06000A8D RID: 2701 RVA: 0x00051C00 File Offset: 0x00050000
	public void LoadLevel(DDRLevel level)
	{
		this.gameplayUiParent.anchoredPosition = Vector2.zero;
		this.gameplayUiParent.rotation = Quaternion.identity;
		this.trackCache = new Dictionary<float, RectTransform>[4];
		for (int i = 0; i < this.trackCache.Length; i++)
		{
			this.trackCache[i] = new Dictionary<float, RectTransform>();
			foreach (float num in level.Tracks[i].Nodes)
			{
				float key = num;
				RectTransform component = UnityEngine.Object.Instantiate<GameObject>(this.arrowPrefab, this.uiTracks[i]).GetComponent<RectTransform>();
				switch (i)
				{
				case 0:
					component.rotation = Quaternion.Euler(0f, 0f, 90f);
					break;
				case 1:
					component.rotation = Quaternion.Euler(0f, 0f, 180f);
					break;
				case 2:
					component.rotation = Quaternion.Euler(0f, 0f, 0f);
					break;
				case 3:
					component.rotation = Quaternion.Euler(0f, 0f, -90f);
					break;
				}
				this.trackCache[i].Add(key, component);
			}
		}
	}

	// Token: 0x06000A8E RID: 2702 RVA: 0x00051D70 File Offset: 0x00050170
	public void LoadLevelSelect(DDRLevel[] levels)
	{
		this.levelSelectCache = new Dictionary<RectTransform, DDRLevel>();
		this.levels = levels;
		for (int i = 0; i < levels.Length; i++)
		{
			RectTransform component = UnityEngine.Object.Instantiate<GameObject>(this.levelIconPrefab, this.levelSelectParent).GetComponent<RectTransform>();
			component.GetComponent<Image>().sprite = levels[i].LevelIcon;
			this.levelSelectCache.Add(component, levels[i]);
		}
		this.positionLevels(true);
	}

	// Token: 0x06000A8F RID: 2703 RVA: 0x00051DE4 File Offset: 0x000501E4
	public void UnloadLevelSelect()
	{
		foreach (KeyValuePair<RectTransform, DDRLevel> keyValuePair in this.levelSelectCache)
		{
			UnityEngine.Object.Destroy(keyValuePair.Key.gameObject);
		}
		this.levelSelectCache = new Dictionary<RectTransform, DDRLevel>();
	}

	// Token: 0x06000A90 RID: 2704 RVA: 0x00051E58 File Offset: 0x00050258
	public void UpdateLevelSelect()
	{
		if (this.inputManager.TappedLeft)
		{
			this.levelSelectScroll -= 1f;
		}
		else if (this.inputManager.TappedRight)
		{
			this.levelSelectScroll += 1f;
		}
		this.levelSelectScroll = Mathf.Clamp(this.levelSelectScroll, 0f, (float)(this.levels.Length - 1));
		this.selectedLevel = (int)Mathf.Round(this.levelSelectScroll);
		this.positionLevels(false);
		if (Input.GetButtonDown("A"))
		{
			this.manager.LoadedLevel = this.levels[this.selectedLevel];
		}
		if (Input.GetButtonDown("B"))
		{
			this.manager.BootOut();
		}
	}

	// Token: 0x06000A91 RID: 2705 RVA: 0x00051F2C File Offset: 0x0005032C
	private void positionLevels(bool instant = false)
	{
		for (int i = 0; i < this.levelSelectCache.Keys.Count; i++)
		{
			RectTransform key = this.levelSelectCache.ElementAt(i).Key;
			Vector2 vector = new Vector2((float)(-(float)this.selectedLevel * 400 + i * 400), 0f);
			key.anchoredPosition = ((!instant) ? Vector2.Lerp(key.anchoredPosition, vector, 10f * Time.deltaTime) : vector);
			this.levelNameLabel.text = this.levels[this.selectedLevel].LevelName;
		}
	}

	// Token: 0x06000A92 RID: 2706 RVA: 0x00051FD8 File Offset: 0x000503D8
	public void UpdateGame(float time)
	{
		if (this.trackCache == null)
		{
			return;
		}
		bool flag = this.manager.GameState.FinishStatus == DDRFinishStatus.Failed;
		if (!flag)
		{
			this.pollInput(time);
			this.gameplayUiParent.anchoredPosition = Vector2.Lerp(this.gameplayUiParent.anchoredPosition, Vector2.zero, 10f * Time.deltaTime);
			this.gameplayUiParent.rotation = Quaternion.identity;
		}
		else
		{
			this.gameplayUiParent.anchoredPosition += Vector2.down * 50f * Time.deltaTime;
			this.gameplayUiParent.Rotate(Vector3.forward * -2f * Time.deltaTime);
			this.shakeUi(0.5f);
		}
		this.healthImage.fillAmount = Mathf.Lerp(this.healthImage.fillAmount, this.manager.GameState.Health / 100f, 10f * Time.deltaTime);
		for (int i = 0; i < this.trackCache.Length; i++)
		{
			Dictionary<float, RectTransform> dictionary = this.trackCache[i];
			foreach (float num in dictionary.Keys)
			{
				float num2 = num;
				float num3 = num2 - time;
				if (num3 < -0.05f)
				{
					if (!flag)
					{
						this.displayHitRating(i, DDRRating.Miss);
					}
					this.assignPoints(DDRRating.Miss);
					this.updateCombo(DDRRating.Miss);
					this.removeNodeAt(this.trackCache.ToList<Dictionary<float, RectTransform>>().IndexOf(dictionary), 0f);
					return;
				}
				dictionary[num2].anchoredPosition = new Vector2(0f, -num3 * this.speed) + this.offset;
			}
		}
	}

	// Token: 0x06000A93 RID: 2707 RVA: 0x000521D4 File Offset: 0x000505D4
	public void UpdateEndcard(GameState state)
	{
		this.scoreText.text = string.Format("Score: {0}", state.Score);
		Color color;
		this.rankText.text = this.getRank(state, out color);
		this.rankText.color = color;
		this.longestComboText.text = string.Format("Biggest combo: {0}", state.LongestCombo.ToString());
	}

	// Token: 0x06000A94 RID: 2708 RVA: 0x00052248 File Offset: 0x00050648
	private DDRRating getRating(int track, float time)
	{
		float num;
		RectTransform rectTransform;
		this.getFirstNodeOn(track, out num, out rectTransform);
		DDRRating result = DDRRating.Miss;
		float num2 = this.offset.y - rectTransform.localPosition.y;
		if (num2 < 130f)
		{
			result = DDRRating.Early;
			if (num2 < 75f)
			{
				result = DDRRating.Ok;
			}
			if (num2 < 65f)
			{
				result = DDRRating.Good;
			}
			if (num2 < 50f)
			{
				result = DDRRating.Great;
			}
			if (num2 < 30f)
			{
				result = DDRRating.Perfect;
			}
			if (num2 < -30f)
			{
				result = DDRRating.Ok;
			}
			if (num2 < -130f)
			{
				result = DDRRating.Miss;
			}
		}
		return result;
	}

	// Token: 0x06000A95 RID: 2709 RVA: 0x000522DC File Offset: 0x000506DC
	private string getRank(GameState state, out Color resultColor)
	{
		string result = "F";
		int num = 0;
		int perfectScorePoints = this.manager.LoadedLevel.PerfectScorePoints;
		foreach (DDRTrack ddrtrack in this.manager.LoadedLevel.Tracks)
		{
			num += ddrtrack.Nodes.Count * perfectScorePoints;
		}
		float num2 = (float)state.Score / (float)num * 100f;
		if (num2 >= 30f)
		{
			result = "D";
		}
		if (num2 >= 50f)
		{
			result = "C";
		}
		if (num2 >= 75f)
		{
			result = "B";
		}
		if (num2 >= 80f)
		{
			result = "A";
		}
		if (num2 >= 95f)
		{
			result = "S";
		}
		if (num2 >= 100f)
		{
			result = "S+";
		}
		resultColor = Color.Lerp(Color.red, Color.green, num2 / 100f);
		return result;
	}

	// Token: 0x06000A96 RID: 2710 RVA: 0x000523E0 File Offset: 0x000507E0
	private void pollInput(float time)
	{
		if (this.inputManager.TappedLeft)
		{
			this.registerKeypress(0, time);
		}
		if (this.inputManager.TappedDown)
		{
			this.registerKeypress(1, time);
		}
		if (this.inputManager.TappedUp)
		{
			this.registerKeypress(2, time);
		}
		if (this.inputManager.TappedRight)
		{
			this.registerKeypress(3, time);
		}
	}

	// Token: 0x06000A97 RID: 2711 RVA: 0x00052450 File Offset: 0x00050850
	private void registerKeypress(int track, float time)
	{
		DDRRating rating = this.getRating(track, time);
		this.displayHitRating(track, rating);
		this.assignPoints(rating);
		this.registerRating(rating);
		this.updateCombo(rating);
		if (rating != DDRRating.Miss)
		{
			this.removeNodeAt(track, 0f);
		}
	}

	// Token: 0x06000A98 RID: 2712 RVA: 0x00052498 File Offset: 0x00050898
	private void registerRating(DDRRating rating)
	{
		Dictionary<DDRRating, int> ratings;
		(ratings = this.manager.GameState.Ratings)[rating] = ratings[rating] + 1;
		from x in this.manager.GameState.Ratings
		orderby x.Value
		select x;
	}

	// Token: 0x06000A99 RID: 2713 RVA: 0x000524FC File Offset: 0x000508FC
	private void updateCombo(DDRRating rating)
	{
		this.comboText.text = string.Empty;
		this.comboText.color = Color.white;
		this.comboText.GetComponent<Animation>().Play();
		if (rating != DDRRating.Miss && rating != DDRRating.Early)
		{
			this.manager.GameState.Combo++;
			if (this.manager.GameState.Combo > this.manager.GameState.LongestCombo)
			{
				this.manager.GameState.LongestCombo = this.manager.GameState.Combo;
				this.comboText.color = Color.yellow;
			}
			if (this.manager.GameState.Combo >= 2)
			{
				this.comboText.text = string.Format("x{0} combo", this.manager.GameState.Combo);
				this.comboText.color = Color.white;
			}
		}
		else
		{
			this.manager.GameState.Combo = 0;
		}
	}

	// Token: 0x06000A9A RID: 2714 RVA: 0x0005261C File Offset: 0x00050A1C
	private void removeNodeAt(int trackId, float delay = 0f)
	{
		Dictionary<float, RectTransform> dictionary = this.trackCache[trackId];
		float[] array = dictionary.Keys.ToArray<float>();
		Array.Sort<float>(array, (float a, float b) => a.CompareTo(b));
		UnityEngine.Object.Destroy(dictionary[array[0]].gameObject, delay);
		dictionary.Remove(array[0]);
	}

	// Token: 0x06000A9B RID: 2715 RVA: 0x00052680 File Offset: 0x00050A80
	private void getFirstNodeOn(int track, out float time, out RectTransform rect)
	{
		Dictionary<float, RectTransform> dictionary = this.trackCache[track];
		float[] array = dictionary.Keys.ToArray<float>();
		Array.Sort<float>(array, (float a, float b) => a.CompareTo(b));
		time = array[0];
		rect = dictionary[time];
	}

	// Token: 0x06000A9C RID: 2716 RVA: 0x000526D4 File Offset: 0x00050AD4
	private void displayHitRating(int track, DDRRating rating)
	{
		Text component = UnityEngine.Object.Instantiate<GameObject>(this.ratingTextPrefab, this.uiTracks[track]).GetComponent<Text>();
		component.rectTransform.anchoredPosition = new Vector2(0f, 280f);
		switch (rating)
		{
		case DDRRating.Perfect:
			component.text = "Perfect";
			component.color = this.perfectColor;
			break;
		case DDRRating.Great:
			component.text = "Great";
			component.color = this.greatColor;
			break;
		case DDRRating.Good:
			component.text = "Good";
			component.color = this.goodColor;
			break;
		case DDRRating.Ok:
			component.text = "Ok";
			component.color = this.okColor;
			break;
		case DDRRating.Miss:
			component.text = "Miss";
			component.color = Color.red;
			this.shakeUi(5f);
			break;
		case DDRRating.Early:
			component.text = "Early";
			component.color = this.earlyColor;
			break;
		}
		UnityEngine.Object.Destroy(component, 1f);
	}

	// Token: 0x06000A9D RID: 2717 RVA: 0x000527F4 File Offset: 0x00050BF4
	private void assignPoints(DDRRating rating)
	{
		int num = 0;
		switch (rating)
		{
		case DDRRating.Perfect:
			num = this.manager.LoadedLevel.PerfectScorePoints;
			break;
		case DDRRating.Great:
			num = this.manager.LoadedLevel.GreatScorePoints;
			break;
		case DDRRating.Good:
			num = this.manager.LoadedLevel.GoodScorePoints;
			break;
		case DDRRating.Ok:
			num = this.manager.LoadedLevel.OkScorePoints;
			break;
		case DDRRating.Miss:
			num = this.manager.LoadedLevel.MissScorePoints;
			break;
		case DDRRating.Early:
			num = this.manager.LoadedLevel.EarlyScorePoints;
			break;
		}
		if (rating != DDRRating.Miss)
		{
			this.manager.GameState.Score += num;
		}
		this.manager.GameState.Health += (float)num;
	}

	// Token: 0x06000A9E RID: 2718 RVA: 0x000528E4 File Offset: 0x00050CE4
	private void shakeUi(float factor)
	{
		Vector2 b = new Vector2(UnityEngine.Random.Range(-factor, factor), UnityEngine.Random.Range(-factor, factor));
		this.gameplayUiParent.anchoredPosition += b;
	}

	// Token: 0x06000A9F RID: 2719 RVA: 0x0005291F File Offset: 0x00050D1F
	public void Unload()
	{
		this.UnloadLevelSelect();
	}

	// Token: 0x04000759 RID: 1881
	[Header("General")]
	[SerializeField]
	private DDRManager manager;

	// Token: 0x0400075A RID: 1882
	[SerializeField]
	private InputManagerScript inputManager;

	// Token: 0x0400075B RID: 1883
	[Header("Level select")]
	[SerializeField]
	private GameObject levelIconPrefab;

	// Token: 0x0400075C RID: 1884
	[SerializeField]
	private RectTransform levelSelectParent;

	// Token: 0x0400075D RID: 1885
	[SerializeField]
	private Text levelNameLabel;

	// Token: 0x0400075E RID: 1886
	private DDRLevel[] levels;

	// Token: 0x0400075F RID: 1887
	[Header("Gameplay")]
	[SerializeField]
	private Text comboText;

	// Token: 0x04000760 RID: 1888
	[SerializeField]
	private Text longestComboText;

	// Token: 0x04000761 RID: 1889
	[SerializeField]
	private Text rankText;

	// Token: 0x04000762 RID: 1890
	[SerializeField]
	private Text scoreText;

	// Token: 0x04000763 RID: 1891
	[SerializeField]
	private Image healthImage;

	// Token: 0x04000764 RID: 1892
	[SerializeField]
	private GameObject arrowPrefab;

	// Token: 0x04000765 RID: 1893
	[SerializeField]
	private GameObject ratingTextPrefab;

	// Token: 0x04000766 RID: 1894
	[SerializeField]
	private RectTransform gameplayUiParent;

	// Token: 0x04000767 RID: 1895
	[SerializeField]
	private RectTransform[] uiTracks;

	// Token: 0x04000768 RID: 1896
	[SerializeField]
	private Vector2 offset;

	// Token: 0x04000769 RID: 1897
	[SerializeField]
	private float speed;

	// Token: 0x0400076A RID: 1898
	[Header("Colors")]
	[SerializeField]
	private Color perfectColor;

	// Token: 0x0400076B RID: 1899
	[SerializeField]
	private Color greatColor;

	// Token: 0x0400076C RID: 1900
	[SerializeField]
	private Color goodColor;

	// Token: 0x0400076D RID: 1901
	[SerializeField]
	private Color okColor;

	// Token: 0x0400076E RID: 1902
	[SerializeField]
	private Color earlyColor;

	// Token: 0x0400076F RID: 1903
	private float levelSelectScroll;

	// Token: 0x04000770 RID: 1904
	private int selectedLevel;

	// Token: 0x04000771 RID: 1905
	private Dictionary<RectTransform, DDRLevel> levelSelectCache;

	// Token: 0x04000772 RID: 1906
	private Dictionary<float, RectTransform>[] trackCache;
}
