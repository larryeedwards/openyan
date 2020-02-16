using System;
using UnityEngine;

// Token: 0x0200043D RID: 1085
public class InventoryScript : MonoBehaviour
{
	// Token: 0x06001D0E RID: 7438 RVA: 0x001102CC File Offset: 0x0010E6CC
	private void Start()
	{
		this.PantyShots = PlayerGlobals.PantyShots;
		this.Money = PlayerGlobals.Money;
		this.UpdateMoney();
	}

	// Token: 0x06001D0F RID: 7439 RVA: 0x001102EA File Offset: 0x0010E6EA
	public void UpdateMoney()
	{
		this.MoneyLabel.text = "$" + this.Money.ToString("F2");
	}

	// Token: 0x04002394 RID: 9108
	public SchemesScript Schemes;

	// Token: 0x04002395 RID: 9109
	public bool ModifiedUniform;

	// Token: 0x04002396 RID: 9110
	public bool DirectionalMic;

	// Token: 0x04002397 RID: 9111
	public bool DuplicateSheet;

	// Token: 0x04002398 RID: 9112
	public bool AnswerSheet;

	// Token: 0x04002399 RID: 9113
	public bool MaskingTape;

	// Token: 0x0400239A RID: 9114
	public bool RivalPhone;

	// Token: 0x0400239B RID: 9115
	public bool LockPick;

	// Token: 0x0400239C RID: 9116
	public bool Headset;

	// Token: 0x0400239D RID: 9117
	public bool FakeID;

	// Token: 0x0400239E RID: 9118
	public bool IDCard;

	// Token: 0x0400239F RID: 9119
	public bool Book;

	// Token: 0x040023A0 RID: 9120
	public bool LethalPoison;

	// Token: 0x040023A1 RID: 9121
	public bool ChemicalPoison;

	// Token: 0x040023A2 RID: 9122
	public bool EmeticPoison;

	// Token: 0x040023A3 RID: 9123
	public bool RatPoison;

	// Token: 0x040023A4 RID: 9124
	public bool HeadachePoison;

	// Token: 0x040023A5 RID: 9125
	public bool Tranquilizer;

	// Token: 0x040023A6 RID: 9126
	public bool Sedative;

	// Token: 0x040023A7 RID: 9127
	public bool Cigs;

	// Token: 0x040023A8 RID: 9128
	public bool Ring;

	// Token: 0x040023A9 RID: 9129
	public bool Rose;

	// Token: 0x040023AA RID: 9130
	public bool Sake;

	// Token: 0x040023AB RID: 9131
	public bool Soda;

	// Token: 0x040023AC RID: 9132
	public bool Bra;

	// Token: 0x040023AD RID: 9133
	public bool CabinetKey;

	// Token: 0x040023AE RID: 9134
	public bool CaseKey;

	// Token: 0x040023AF RID: 9135
	public bool SafeKey;

	// Token: 0x040023B0 RID: 9136
	public bool ShedKey;

	// Token: 0x040023B1 RID: 9137
	public int MysteriousKeys;

	// Token: 0x040023B2 RID: 9138
	public int PantyShots;

	// Token: 0x040023B3 RID: 9139
	public float Money;

	// Token: 0x040023B4 RID: 9140
	public bool[] ShrineCollectibles;

	// Token: 0x040023B5 RID: 9141
	public UILabel MoneyLabel;
}
