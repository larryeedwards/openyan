using System;
using System.Collections.Generic;

// Token: 0x0200012F RID: 303
[Serializable]
public class GameState
{
	// Token: 0x06000AA4 RID: 2724 RVA: 0x0005294C File Offset: 0x00050D4C
	public GameState()
	{
		this.Health = 100f;
		this.Ratings = new Dictionary<DDRRating, int>();
		this.Ratings.Add(DDRRating.Early, 0);
		this.Ratings.Add(DDRRating.Good, 0);
		this.Ratings.Add(DDRRating.Great, 0);
		this.Ratings.Add(DDRRating.Miss, 0);
		this.Ratings.Add(DDRRating.Ok, 0);
		this.Ratings.Add(DDRRating.Perfect, 0);
	}

	// Token: 0x04000781 RID: 1921
	public int Score;

	// Token: 0x04000782 RID: 1922
	public float Health;

	// Token: 0x04000783 RID: 1923
	public int LongestCombo;

	// Token: 0x04000784 RID: 1924
	public int Combo;

	// Token: 0x04000785 RID: 1925
	public Dictionary<DDRRating, int> Ratings;

	// Token: 0x04000786 RID: 1926
	public DDRFinishStatus FinishStatus;
}
