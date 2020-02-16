using System;

// Token: 0x020003C2 RID: 962
[Serializable]
public class ScheduleBlock
{
	// Token: 0x0600196A RID: 6506 RVA: 0x000ED6E0 File Offset: 0x000EBAE0
	public ScheduleBlock(float time, string destination, string action)
	{
		this.time = time;
		this.destination = destination;
		this.action = action;
	}

	// Token: 0x04001E2A RID: 7722
	public float time;

	// Token: 0x04001E2B RID: 7723
	public string destination;

	// Token: 0x04001E2C RID: 7724
	public string action;
}
