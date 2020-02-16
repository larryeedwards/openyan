using System;
using UnityEngine;

// Token: 0x0200055D RID: 1373
public class TrespassScript : MonoBehaviour
{
	// Token: 0x060021CA RID: 8650 RVA: 0x001996DC File Offset: 0x00197ADC
	private void OnTriggerEnter(Collider other)
	{
		if (base.enabled && other.gameObject.layer == 13)
		{
			this.YandereObject = other.gameObject;
			this.Yandere = other.gameObject.GetComponent<YandereScript>();
			if (this.Yandere != null)
			{
				if (!this.Yandere.Trespassing)
				{
					this.Yandere.NotificationManager.DisplayNotification(NotificationType.Intrude);
				}
				this.Yandere.Trespassing = true;
			}
		}
	}

	// Token: 0x060021CB RID: 8651 RVA: 0x00199761 File Offset: 0x00197B61
	private void OnTriggerExit(Collider other)
	{
		if (this.Yandere != null && other.gameObject == this.YandereObject)
		{
			this.Yandere.Trespassing = false;
		}
	}

	// Token: 0x040036D8 RID: 14040
	public GameObject YandereObject;

	// Token: 0x040036D9 RID: 14041
	public YandereScript Yandere;

	// Token: 0x040036DA RID: 14042
	public bool HideNotification;

	// Token: 0x040036DB RID: 14043
	public bool OffLimits;
}
