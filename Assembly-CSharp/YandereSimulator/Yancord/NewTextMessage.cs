using System;
using UnityEngine;

namespace YandereSimulator.Yancord
{
	// Token: 0x020005C7 RID: 1479
	[Serializable]
	public class NewTextMessage
	{
		// Token: 0x04003D40 RID: 15680
		public string Message;

		// Token: 0x04003D41 RID: 15681
		public bool isQuestion;

		// Token: 0x04003D42 RID: 15682
		public bool sentByPlayer;

		// Token: 0x04003D43 RID: 15683
		public bool isSystemMessage;

		// Token: 0x04003D44 RID: 15684
		[Header("== Question Related ==")]
		public string OptionQ;

		// Token: 0x04003D45 RID: 15685
		public string OptionR;

		// Token: 0x04003D46 RID: 15686
		public string OptionF;

		// Token: 0x04003D47 RID: 15687
		[Space(20f)]
		public string ReactionQ;

		// Token: 0x04003D48 RID: 15688
		public string ReactionR;

		// Token: 0x04003D49 RID: 15689
		public string ReactionF;
	}
}
