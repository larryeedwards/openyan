using System;
using UnityEngine;

// Token: 0x02000431 RID: 1073
public class IfElseScript : MonoBehaviour
{
	// Token: 0x06001CED RID: 7405 RVA: 0x0010ABD2 File Offset: 0x00108FD2
	private void Start()
	{
		this.SwitchCase();
	}

	// Token: 0x06001CEE RID: 7406 RVA: 0x0010ABDC File Offset: 0x00108FDC
	private void IfElse()
	{
		if (this.ID == 1)
		{
			this.Day = "Monday";
		}
		else if (this.ID == 2)
		{
			this.Day = "Tuesday";
		}
		else if (this.ID == 3)
		{
			this.Day = "Wednesday";
		}
		else if (this.ID == 4)
		{
			this.Day = "Thursday";
		}
		else if (this.ID == 5)
		{
			this.Day = "Friday";
		}
		else if (this.ID == 6)
		{
			this.Day = "Saturday";
		}
		else if (this.ID == 7)
		{
			this.Day = "Sunday";
		}
	}

	// Token: 0x06001CEF RID: 7407 RVA: 0x0010ACA8 File Offset: 0x001090A8
	private void SwitchCase()
	{
		switch (this.ID)
		{
		case 1:
			this.Day = "Monday";
			break;
		case 2:
			this.Day = "Tuesday";
			break;
		case 3:
			this.Day = "Wednesday";
			break;
		case 4:
			this.Day = "Thursday";
			break;
		case 5:
			this.Day = "Friday";
			break;
		case 6:
			this.Day = "Saturday";
			break;
		case 7:
			this.Day = "Sunday";
			break;
		}
	}

	// Token: 0x040022C0 RID: 8896
	public int ID;

	// Token: 0x040022C1 RID: 8897
	public string Day;
}
