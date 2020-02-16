using System;
using UnityEngine;

// Token: 0x02000325 RID: 805
public class HatredScript : MonoBehaviour
{
	// Token: 0x06001707 RID: 5895 RVA: 0x000B238B File Offset: 0x000B078B
	private void Start()
	{
		this.Character.SetActive(false);
	}

	// Token: 0x04001624 RID: 5668
	public DepthOfFieldScatter DepthOfField;

	// Token: 0x04001625 RID: 5669
	public HomeDarknessScript HomeDarkness;

	// Token: 0x04001626 RID: 5670
	public HomeCameraScript HomeCamera;

	// Token: 0x04001627 RID: 5671
	public GrayscaleEffect Grayscale;

	// Token: 0x04001628 RID: 5672
	public Bloom Bloom;

	// Token: 0x04001629 RID: 5673
	public GameObject CrackPanel;

	// Token: 0x0400162A RID: 5674
	public AudioSource Voiceover;

	// Token: 0x0400162B RID: 5675
	public GameObject SenpaiPhoto;

	// Token: 0x0400162C RID: 5676
	public GameObject RivalPhotos;

	// Token: 0x0400162D RID: 5677
	public GameObject Character;

	// Token: 0x0400162E RID: 5678
	public GameObject Panties;

	// Token: 0x0400162F RID: 5679
	public GameObject Yandere;

	// Token: 0x04001630 RID: 5680
	public GameObject Shrine;

	// Token: 0x04001631 RID: 5681
	public Transform AntennaeR;

	// Token: 0x04001632 RID: 5682
	public Transform AntennaeL;

	// Token: 0x04001633 RID: 5683
	public Transform Corkboard;

	// Token: 0x04001634 RID: 5684
	public UISprite CrackDarkness;

	// Token: 0x04001635 RID: 5685
	public UISprite Darkness;

	// Token: 0x04001636 RID: 5686
	public UITexture Crack;

	// Token: 0x04001637 RID: 5687
	public UITexture Logo;

	// Token: 0x04001638 RID: 5688
	public bool Begin;

	// Token: 0x04001639 RID: 5689
	public float Timer;

	// Token: 0x0400163A RID: 5690
	public int Phase;

	// Token: 0x0400163B RID: 5691
	public Texture[] CrackTexture;
}
