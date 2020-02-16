using System;
using UnityEngine;

// Token: 0x020003AE RID: 942
public class ElectrifiedPuddleScript : MonoBehaviour
{
	// Token: 0x0600192A RID: 6442 RVA: 0x000E839C File Offset: 0x000E679C
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 9)
		{
			StudentScript component = other.gameObject.GetComponent<StudentScript>();
			if (component != null && !component.Electrified)
			{
				component.Yandere.GazerEyes.ElectrocuteStudent(component);
				base.gameObject.SetActive(false);
				this.PowerSwitch.On = false;
			}
		}
		if (other.gameObject.layer == 13)
		{
			YandereScript component2 = other.gameObject.GetComponent<YandereScript>();
			if (component2 != null)
			{
				component2.StudentManager.Headmaster.Taze();
				component2.StudentManager.Headmaster.Heartbroken.Headmaster = false;
			}
		}
	}

	// Token: 0x04001D3D RID: 7485
	public PowerSwitchScript PowerSwitch;
}
