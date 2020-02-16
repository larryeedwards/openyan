using System;

// Token: 0x0200032C RID: 812
[Serializable]
public class ArrayLayout
{
	// Token: 0x04001676 RID: 5750
	public ArrayLayout.rowData[] rows = new ArrayLayout.rowData[6];

	// Token: 0x0200032D RID: 813
	[Serializable]
	public struct rowData
	{
		// Token: 0x04001677 RID: 5751
		public bool[] row;
	}
}
