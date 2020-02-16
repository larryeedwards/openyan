using System;
using UnityEngine;

// Token: 0x0200042E RID: 1070
public class HomeZoomScript : MonoBehaviour
{
	// Token: 0x06001CE8 RID: 7400 RVA: 0x0010A950 File Offset: 0x00108D50
	private void Update()
	{
		AudioSource component = base.GetComponent<AudioSource>();
		if (Input.GetKeyDown(KeyCode.Z))
		{
			if (!this.Zoom)
			{
				this.Zoom = true;
				component.Play();
			}
			else
			{
				this.Zoom = false;
			}
		}
		if (this.Zoom)
		{
			base.transform.localPosition = new Vector3(base.transform.localPosition.x, Mathf.MoveTowards(base.transform.localPosition.y, 1.5f, Time.deltaTime * 0.0333333351f), base.transform.localPosition.z);
			this.YandereDestination.localPosition = Vector3.MoveTowards(this.YandereDestination.localPosition, new Vector3(-1.5f, 1.5f, 1f), Time.deltaTime * 0.0333333351f);
			component.volume += Time.deltaTime * 0.01f;
		}
		else
		{
			base.transform.localPosition = new Vector3(base.transform.localPosition.x, Mathf.MoveTowards(base.transform.localPosition.y, 1f, Time.deltaTime * 10f), base.transform.localPosition.z);
			this.YandereDestination.localPosition = Vector3.MoveTowards(this.YandereDestination.localPosition, new Vector3(-2.271312f, 2f, 3.5f), Time.deltaTime * 10f);
			component.volume = 0f;
		}
	}

	// Token: 0x040022BC RID: 8892
	public Transform YandereDestination;

	// Token: 0x040022BD RID: 8893
	public bool Zoom;
}
