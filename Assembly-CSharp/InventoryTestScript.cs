using System;
using UnityEngine;

// Token: 0x0200043E RID: 1086
public class InventoryTestScript : MonoBehaviour
{
	// Token: 0x06001D11 RID: 7441 RVA: 0x0011033C File Offset: 0x0010E73C
	private void Start()
	{
		this.RightGrid.localScale = new Vector3(0f, 0f, 0f);
		this.LeftGrid.localScale = new Vector3(0f, 0f, 0f);
		Time.timeScale = 1f;
	}

	// Token: 0x06001D12 RID: 7442 RVA: 0x00110394 File Offset: 0x0010E794
	private void Update()
	{
		if (Input.GetKeyDown("space"))
		{
			this.Open = !this.Open;
		}
		AnimationState animationState = this.SkirtAnimation["InverseSkirtOpen"];
		AnimationState animationState2 = this.GirlAnimation["f02_inventory_00"];
		if (this.Open)
		{
			this.RightGrid.localScale = Vector3.MoveTowards(this.RightGrid.localScale, new Vector3(0.9f, 0.9f, 0.9f), Time.deltaTime * 10f);
			this.LeftGrid.localScale = Vector3.MoveTowards(this.LeftGrid.localScale, new Vector3(0.9f, 0.9f, 0.9f), Time.deltaTime * 10f);
			base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y, Mathf.Lerp(base.transform.position.z, 0.37f, Time.deltaTime * 10f));
			animationState.time = Mathf.Lerp(animationState2.time, 1f, Time.deltaTime * 10f);
			animationState2.time = animationState.time;
			this.Alpha = Mathf.Lerp(this.Alpha, 1f, Time.deltaTime * 10f);
			this.SkirtRenderer.material.color = new Color(1f, 1f, 1f, this.Alpha);
			this.GirlRenderer.materials[0].color = new Color(0f, 0f, 0f, this.Alpha);
			this.GirlRenderer.materials[1].color = new Color(0f, 0f, 0f, this.Alpha);
			if (Input.GetKeyDown("right"))
			{
				this.Column++;
				this.UpdateHighlight();
			}
			if (Input.GetKeyDown("left"))
			{
				this.Column--;
				this.UpdateHighlight();
			}
			if (Input.GetKeyDown("up"))
			{
				this.Row--;
				this.UpdateHighlight();
			}
			if (Input.GetKeyDown("down"))
			{
				this.Row++;
				this.UpdateHighlight();
			}
		}
		else
		{
			this.RightGrid.localScale = Vector3.MoveTowards(this.RightGrid.localScale, new Vector3(0f, 0f, 0f), Time.deltaTime * 10f);
			this.LeftGrid.localScale = Vector3.MoveTowards(this.LeftGrid.localScale, new Vector3(0f, 0f, 0f), Time.deltaTime * 10f);
			base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y, Mathf.Lerp(base.transform.position.z, 1f, Time.deltaTime * 10f));
			animationState.time = Mathf.Lerp(animationState2.time, 0f, Time.deltaTime * 10f);
			animationState2.time = animationState.time;
			this.Alpha = Mathf.Lerp(this.Alpha, 0f, Time.deltaTime * 10f);
			this.SkirtRenderer.material.color = new Color(1f, 1f, 1f, this.Alpha);
			this.GirlRenderer.materials[0].color = new Color(0f, 0f, 0f, this.Alpha);
			this.GirlRenderer.materials[1].color = new Color(0f, 0f, 0f, this.Alpha);
		}
		for (int i = 0; i < this.Items.Length; i++)
		{
			if (this.Items[i].Clicked)
			{
				Debug.Log(string.Concat(new object[]
				{
					"Item width is ",
					this.Items[i].InventoryItem.Width,
					" and item height is ",
					this.Items[i].InventoryItem.Height,
					". Open space is: ",
					this.OpenSpace
				}));
				if (this.Items[i].InventoryItem.Height * this.Items[i].InventoryItem.Width < this.OpenSpace)
				{
					Debug.Log("We might have enough open space to add the item to the inventory.");
					this.CheckOpenSpace();
					if (this.UseGrid == 1)
					{
						this.Items[i].transform.parent = this.LeftGridItemParent;
						float inventorySize = this.Items[i].InventoryItem.InventorySize;
						this.Items[i].transform.localScale = new Vector3(inventorySize, inventorySize, inventorySize);
						this.Items[i].transform.localEulerAngles = new Vector3(90f, 180f, 0f);
						this.Items[i].transform.localPosition = this.Items[i].InventoryItem.InventoryPosition;
						int j = 1;
						if (this.UseColumn == 1)
						{
							while (j < this.Items[i].InventoryItem.Height + 1)
							{
								this.LeftSpaces1[j] = true;
								j++;
							}
						}
						else if (this.UseColumn == 2)
						{
							while (j < this.Items[i].InventoryItem.Height + 1)
							{
								this.LeftSpaces2[j] = true;
								j++;
							}
						}
						if (this.UseColumn > 1)
						{
							this.Items[i].transform.localPosition -= new Vector3(0.05f * (float)(this.UseColumn - 1), 0f, 0f);
						}
					}
				}
				this.Items[i].Clicked = false;
			}
		}
	}

	// Token: 0x06001D13 RID: 7443 RVA: 0x00110A24 File Offset: 0x0010EE24
	private void CheckOpenSpace()
	{
		this.UseColumn = 0;
		this.UseGrid = 0;
		int i;
		for (i = 1; i < this.LeftSpaces1.Length; i++)
		{
			if (this.UseGrid == 0 && !this.LeftSpaces1[i])
			{
				this.UseColumn = 1;
				this.UseGrid = 1;
			}
		}
		i = 1;
		if (this.UseGrid == 0)
		{
			while (i < this.LeftSpaces2.Length)
			{
				if (this.UseGrid == 0 && !this.LeftSpaces2[i])
				{
					this.UseColumn = 2;
					this.UseGrid = 1;
				}
				i++;
			}
		}
	}

	// Token: 0x06001D14 RID: 7444 RVA: 0x00110AC8 File Offset: 0x0010EEC8
	private void UpdateHighlight()
	{
		if (this.Column == 5)
		{
			if (this.Grid == 1)
			{
				this.Grid = 2;
			}
			else
			{
				this.Grid = 1;
			}
			this.Column = 1;
		}
		else if (this.Column == 0)
		{
			if (this.Grid == 1)
			{
				this.Grid = 2;
			}
			else
			{
				this.Grid = 1;
			}
			this.Column = 4;
		}
		if (this.Row == 6)
		{
			this.Row = 1;
		}
		else if (this.Row == 0)
		{
			this.Row = 5;
		}
		if (this.Grid == 1)
		{
			this.Highlight.transform.parent = this.LeftGridHighlightParent;
		}
		else
		{
			this.Highlight.transform.parent = this.RightGridHighlightParent;
		}
		this.Highlight.localPosition = new Vector3((float)this.Column, (float)(this.Row * -1), 0f);
	}

	// Token: 0x040023B6 RID: 9142
	public SimpleDetectClickScript[] Items;

	// Token: 0x040023B7 RID: 9143
	public Animation SkirtAnimation;

	// Token: 0x040023B8 RID: 9144
	public Animation GirlAnimation;

	// Token: 0x040023B9 RID: 9145
	public GameObject Skirt;

	// Token: 0x040023BA RID: 9146
	public GameObject Girl;

	// Token: 0x040023BB RID: 9147
	public Renderer SkirtRenderer;

	// Token: 0x040023BC RID: 9148
	public Renderer GirlRenderer;

	// Token: 0x040023BD RID: 9149
	public Transform RightGridHighlightParent;

	// Token: 0x040023BE RID: 9150
	public Transform LeftGridHighlightParent;

	// Token: 0x040023BF RID: 9151
	public Transform RightGridItemParent;

	// Token: 0x040023C0 RID: 9152
	public Transform LeftGridItemParent;

	// Token: 0x040023C1 RID: 9153
	public Transform Highlight;

	// Token: 0x040023C2 RID: 9154
	public Transform RightGrid;

	// Token: 0x040023C3 RID: 9155
	public Transform LeftGrid;

	// Token: 0x040023C4 RID: 9156
	public float Alpha;

	// Token: 0x040023C5 RID: 9157
	public bool Open = true;

	// Token: 0x040023C6 RID: 9158
	public int OpenSpace = 1;

	// Token: 0x040023C7 RID: 9159
	public int UseColumn;

	// Token: 0x040023C8 RID: 9160
	public int UseGrid;

	// Token: 0x040023C9 RID: 9161
	public int Column = 1;

	// Token: 0x040023CA RID: 9162
	public int Grid = 1;

	// Token: 0x040023CB RID: 9163
	public int Row = 1;

	// Token: 0x040023CC RID: 9164
	public bool[] LeftSpaces1;

	// Token: 0x040023CD RID: 9165
	public bool[] LeftSpaces2;

	// Token: 0x040023CE RID: 9166
	public bool[] LeftSpaces3;

	// Token: 0x040023CF RID: 9167
	public bool[] LeftSpaces4;

	// Token: 0x040023D0 RID: 9168
	public bool[] RightSpaces1;

	// Token: 0x040023D1 RID: 9169
	public bool[] RightSpaces2;

	// Token: 0x040023D2 RID: 9170
	public bool[] RightSpaces3;

	// Token: 0x040023D3 RID: 9171
	public bool[] RightSpaces4;
}
