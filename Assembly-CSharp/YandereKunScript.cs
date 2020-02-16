using System;
using UnityEngine;

// Token: 0x02000595 RID: 1429
public class YandereKunScript : MonoBehaviour
{
	// Token: 0x06002285 RID: 8837 RVA: 0x001A2D40 File Offset: 0x001A1140
	private void Start()
	{
		if (!this.Kizuna)
		{
			if (this.KunHips != null)
			{
				this.KunHips.parent = this.ChanHips;
			}
			if (this.KunSpine != null)
			{
				this.KunSpine.parent = this.ChanSpine;
			}
			if (this.KunSpine1 != null)
			{
				this.KunSpine1.parent = this.ChanSpine1;
			}
			if (this.KunSpine2 != null)
			{
				this.KunSpine2.parent = this.ChanSpine2;
			}
			if (this.KunSpine3 != null)
			{
				this.KunSpine3.parent = this.ChanSpine3;
			}
			if (this.KunNeck != null)
			{
				this.KunNeck.parent = this.ChanNeck;
			}
			if (this.KunHead != null)
			{
				this.KunHead.parent = this.ChanHead;
			}
			this.KunRightUpLeg.parent = this.ChanRightUpLeg;
			this.KunRightLeg.parent = this.ChanRightLeg;
			this.KunRightFoot.parent = this.ChanRightFoot;
			this.KunRightToes.parent = this.ChanRightToes;
			this.KunLeftUpLeg.parent = this.ChanLeftUpLeg;
			this.KunLeftLeg.parent = this.ChanLeftLeg;
			this.KunLeftFoot.parent = this.ChanLeftFoot;
			this.KunLeftToes.parent = this.ChanLeftToes;
			this.KunRightShoulder.parent = this.ChanRightShoulder;
			this.KunRightArm.parent = this.ChanRightArm;
			if (this.KunRightArmRoll != null)
			{
				this.KunRightArmRoll.parent = this.ChanRightArmRoll;
			}
			this.KunRightForeArm.parent = this.ChanRightForeArm;
			if (this.KunRightForeArmRoll != null)
			{
				this.KunRightForeArmRoll.parent = this.ChanRightForeArmRoll;
			}
			this.KunRightHand.parent = this.ChanRightHand;
			this.KunLeftShoulder.parent = this.ChanLeftShoulder;
			this.KunLeftArm.parent = this.ChanLeftArm;
			if (this.KunLeftArmRoll != null)
			{
				this.KunLeftArmRoll.parent = this.ChanLeftArmRoll;
			}
			this.KunLeftForeArm.parent = this.ChanLeftForeArm;
			if (this.KunLeftForeArmRoll != null)
			{
				this.KunLeftForeArmRoll.parent = this.ChanLeftForeArmRoll;
			}
			this.KunLeftHand.parent = this.ChanLeftHand;
			if (!this.Man)
			{
				this.KunLeftHandPinky1.parent = this.ChanLeftHandPinky1;
				this.KunLeftHandPinky2.parent = this.ChanLeftHandPinky2;
				this.KunLeftHandPinky3.parent = this.ChanLeftHandPinky3;
				this.KunLeftHandRing1.parent = this.ChanLeftHandRing1;
				this.KunLeftHandRing2.parent = this.ChanLeftHandRing2;
				this.KunLeftHandRing3.parent = this.ChanLeftHandRing3;
				this.KunLeftHandMiddle1.parent = this.ChanLeftHandMiddle1;
				this.KunLeftHandMiddle2.parent = this.ChanLeftHandMiddle2;
				this.KunLeftHandMiddle3.parent = this.ChanLeftHandMiddle3;
				this.KunLeftHandIndex1.parent = this.ChanLeftHandIndex1;
				this.KunLeftHandIndex2.parent = this.ChanLeftHandIndex2;
				this.KunLeftHandIndex3.parent = this.ChanLeftHandIndex3;
				this.KunLeftHandThumb1.parent = this.ChanLeftHandThumb1;
				this.KunLeftHandThumb2.parent = this.ChanLeftHandThumb2;
				this.KunLeftHandThumb3.parent = this.ChanLeftHandThumb3;
				this.KunRightHandPinky1.parent = this.ChanRightHandPinky1;
				this.KunRightHandPinky2.parent = this.ChanRightHandPinky2;
				this.KunRightHandPinky3.parent = this.ChanRightHandPinky3;
				this.KunRightHandRing1.parent = this.ChanRightHandRing1;
				this.KunRightHandRing2.parent = this.ChanRightHandRing2;
				this.KunRightHandRing3.parent = this.ChanRightHandRing3;
				this.KunRightHandMiddle1.parent = this.ChanRightHandMiddle1;
				this.KunRightHandMiddle2.parent = this.ChanRightHandMiddle2;
				this.KunRightHandMiddle3.parent = this.ChanRightHandMiddle3;
				this.KunRightHandIndex1.parent = this.ChanRightHandIndex1;
				this.KunRightHandIndex2.parent = this.ChanRightHandIndex2;
				this.KunRightHandIndex3.parent = this.ChanRightHandIndex3;
				this.KunRightHandThumb1.parent = this.ChanRightHandThumb1;
				this.KunRightHandThumb2.parent = this.ChanRightHandThumb2;
				this.KunRightHandThumb3.parent = this.ChanRightHandThumb3;
			}
		}
		if (this.MyRenderer != null)
		{
			this.MyRenderer.enabled = true;
		}
		if (this.SecondRenderer != null)
		{
			this.SecondRenderer.enabled = true;
		}
		if (this.ThirdRenderer != null)
		{
			this.ThirdRenderer.enabled = true;
		}
		base.gameObject.SetActive(false);
	}

	// Token: 0x06002286 RID: 8838 RVA: 0x001A324C File Offset: 0x001A164C
	private void LateUpdate()
	{
		if (this.Man)
		{
			this.ChanItemParent.position = this.KunItemParent.position;
			if (!this.Adjusted)
			{
				this.KunRightShoulder.position += new Vector3(0f, 0.1f, 0f);
				this.KunRightArm.position += new Vector3(0f, 0.1f, 0f);
				this.KunRightForeArm.position += new Vector3(0f, 0.1f, 0f);
				this.KunRightHand.position += new Vector3(0f, 0.1f, 0f);
				this.KunLeftShoulder.position += new Vector3(0f, 0.1f, 0f);
				this.KunLeftArm.position += new Vector3(0f, 0.1f, 0f);
				this.KunLeftForeArm.position += new Vector3(0f, 0.1f, 0f);
				this.KunLeftHand.position += new Vector3(0f, 0.1f, 0f);
				this.Adjusted = true;
			}
		}
		if (this.Kizuna)
		{
			this.KunItemParent.localPosition = new Vector3(0.066666f, -0.033333f, 0.02f);
			this.ChanItemParent.position = this.KunItemParent.position;
			this.KunHips.localPosition = this.ChanHips.localPosition;
			if (this.KunHips != null)
			{
				this.KunHips.eulerAngles = this.ChanHips.eulerAngles;
			}
			if (this.KunSpine != null)
			{
				this.KunSpine.eulerAngles = this.ChanSpine.eulerAngles;
			}
			if (this.KunSpine1 != null)
			{
				this.KunSpine1.eulerAngles = this.ChanSpine1.eulerAngles;
			}
			if (this.KunSpine2 != null)
			{
				this.KunSpine2.eulerAngles = this.ChanSpine2.eulerAngles;
			}
			if (this.KunSpine3 != null)
			{
				this.KunSpine3.eulerAngles = this.ChanSpine3.eulerAngles;
			}
			if (this.KunNeck != null)
			{
				this.KunNeck.eulerAngles = this.ChanNeck.eulerAngles;
			}
			if (this.KunHead != null)
			{
				this.KunHead.eulerAngles = this.ChanHead.eulerAngles;
			}
			this.KunRightUpLeg.eulerAngles = this.ChanRightUpLeg.eulerAngles;
			this.KunRightLeg.eulerAngles = this.ChanRightLeg.eulerAngles;
			this.KunRightFoot.eulerAngles = this.ChanRightFoot.eulerAngles;
			this.KunRightToes.eulerAngles = this.ChanRightToes.eulerAngles;
			this.KunLeftUpLeg.eulerAngles = this.ChanLeftUpLeg.eulerAngles;
			this.KunLeftLeg.eulerAngles = this.ChanLeftLeg.eulerAngles;
			this.KunLeftFoot.eulerAngles = this.ChanLeftFoot.eulerAngles;
			this.KunLeftToes.eulerAngles = this.ChanLeftToes.eulerAngles;
			this.KunRightShoulder.eulerAngles = this.ChanRightShoulder.eulerAngles;
			this.KunRightArm.eulerAngles = this.ChanRightArm.eulerAngles;
			if (this.KunLeftArmRoll != null)
			{
				this.KunRightArmRoll.eulerAngles = this.ChanRightArmRoll.eulerAngles;
			}
			this.KunRightForeArm.eulerAngles = this.ChanRightForeArm.eulerAngles;
			if (this.KunLeftArmRoll != null)
			{
				this.KunRightForeArmRoll.eulerAngles = this.ChanRightForeArmRoll.eulerAngles;
			}
			this.KunRightHand.eulerAngles = this.ChanRightHand.eulerAngles;
			this.KunLeftShoulder.eulerAngles = this.ChanLeftShoulder.eulerAngles;
			this.KunLeftArm.eulerAngles = this.ChanLeftArm.eulerAngles;
			if (this.KunLeftArmRoll != null)
			{
				this.KunLeftArmRoll.eulerAngles = this.ChanLeftArmRoll.eulerAngles;
			}
			this.KunLeftForeArm.eulerAngles = this.ChanLeftForeArm.eulerAngles;
			if (this.KunLeftForeArmRoll != null)
			{
				this.KunLeftForeArmRoll.eulerAngles = this.ChanLeftForeArmRoll.eulerAngles;
			}
			this.KunLeftHand.eulerAngles = this.ChanLeftHand.eulerAngles;
			this.KunLeftHandPinky1.eulerAngles = this.ChanLeftHandPinky1.eulerAngles;
			this.KunLeftHandPinky2.eulerAngles = this.ChanLeftHandPinky2.eulerAngles;
			this.KunLeftHandPinky3.eulerAngles = this.ChanLeftHandPinky3.eulerAngles;
			this.KunLeftHandRing1.eulerAngles = this.ChanLeftHandRing1.eulerAngles;
			this.KunLeftHandRing2.eulerAngles = this.ChanLeftHandRing2.eulerAngles;
			this.KunLeftHandRing3.eulerAngles = this.ChanLeftHandRing3.eulerAngles;
			this.KunLeftHandMiddle1.eulerAngles = this.ChanLeftHandMiddle1.eulerAngles;
			this.KunLeftHandMiddle2.eulerAngles = this.ChanLeftHandMiddle2.eulerAngles;
			this.KunLeftHandMiddle3.eulerAngles = this.ChanLeftHandMiddle3.eulerAngles;
			this.KunLeftHandIndex1.eulerAngles = this.ChanLeftHandIndex1.eulerAngles;
			this.KunLeftHandIndex2.eulerAngles = this.ChanLeftHandIndex2.eulerAngles;
			this.KunLeftHandIndex3.eulerAngles = this.ChanLeftHandIndex3.eulerAngles;
			this.KunLeftHandThumb1.eulerAngles = this.ChanLeftHandThumb1.eulerAngles;
			this.KunLeftHandThumb2.eulerAngles = this.ChanLeftHandThumb2.eulerAngles;
			this.KunLeftHandThumb3.eulerAngles = this.ChanLeftHandThumb3.eulerAngles;
			this.KunRightHandPinky1.eulerAngles = this.ChanRightHandPinky1.eulerAngles;
			this.KunRightHandPinky2.eulerAngles = this.ChanRightHandPinky2.eulerAngles;
			this.KunRightHandPinky3.eulerAngles = this.ChanRightHandPinky3.eulerAngles;
			this.KunRightHandRing1.eulerAngles = this.ChanRightHandRing1.eulerAngles;
			this.KunRightHandRing2.eulerAngles = this.ChanRightHandRing2.eulerAngles;
			this.KunRightHandRing3.eulerAngles = this.ChanRightHandRing3.eulerAngles;
			this.KunRightHandMiddle1.eulerAngles = this.ChanRightHandMiddle1.eulerAngles;
			this.KunRightHandMiddle2.eulerAngles = this.ChanRightHandMiddle2.eulerAngles;
			this.KunRightHandMiddle3.eulerAngles = this.ChanRightHandMiddle3.eulerAngles;
			this.KunRightHandIndex1.eulerAngles = this.ChanRightHandIndex1.eulerAngles;
			this.KunRightHandIndex2.eulerAngles = this.ChanRightHandIndex2.eulerAngles;
			this.KunRightHandIndex3.eulerAngles = this.ChanRightHandIndex3.eulerAngles;
			this.KunRightHandThumb1.eulerAngles = this.ChanRightHandThumb1.eulerAngles;
			this.KunRightHandThumb2.eulerAngles = this.ChanRightHandThumb2.eulerAngles;
			this.KunRightHandThumb3.eulerAngles = this.ChanRightHandThumb3.eulerAngles;
			if (Input.GetKeyDown(KeyCode.Space))
			{
				if (this.ID > -1)
				{
					for (int i = 0; i < 32; i++)
					{
						this.SecondRenderer.SetBlendShapeWeight(i, 0f);
					}
					if (this.ID > 32)
					{
						this.ID = 0;
					}
					this.SecondRenderer.SetBlendShapeWeight(this.ID, 100f);
				}
				this.ID++;
			}
		}
	}

	// Token: 0x04003881 RID: 14465
	public Transform ChanItemParent;

	// Token: 0x04003882 RID: 14466
	public Transform KunItemParent;

	// Token: 0x04003883 RID: 14467
	public Transform ChanHips;

	// Token: 0x04003884 RID: 14468
	public Transform ChanSpine;

	// Token: 0x04003885 RID: 14469
	public Transform ChanSpine1;

	// Token: 0x04003886 RID: 14470
	public Transform ChanSpine2;

	// Token: 0x04003887 RID: 14471
	public Transform ChanSpine3;

	// Token: 0x04003888 RID: 14472
	public Transform ChanNeck;

	// Token: 0x04003889 RID: 14473
	public Transform ChanHead;

	// Token: 0x0400388A RID: 14474
	public Transform ChanRightUpLeg;

	// Token: 0x0400388B RID: 14475
	public Transform ChanRightLeg;

	// Token: 0x0400388C RID: 14476
	public Transform ChanRightFoot;

	// Token: 0x0400388D RID: 14477
	public Transform ChanRightToes;

	// Token: 0x0400388E RID: 14478
	public Transform ChanLeftUpLeg;

	// Token: 0x0400388F RID: 14479
	public Transform ChanLeftLeg;

	// Token: 0x04003890 RID: 14480
	public Transform ChanLeftFoot;

	// Token: 0x04003891 RID: 14481
	public Transform ChanLeftToes;

	// Token: 0x04003892 RID: 14482
	public Transform ChanRightShoulder;

	// Token: 0x04003893 RID: 14483
	public Transform ChanRightArm;

	// Token: 0x04003894 RID: 14484
	public Transform ChanRightArmRoll;

	// Token: 0x04003895 RID: 14485
	public Transform ChanRightForeArm;

	// Token: 0x04003896 RID: 14486
	public Transform ChanRightForeArmRoll;

	// Token: 0x04003897 RID: 14487
	public Transform ChanRightHand;

	// Token: 0x04003898 RID: 14488
	public Transform ChanLeftShoulder;

	// Token: 0x04003899 RID: 14489
	public Transform ChanLeftArm;

	// Token: 0x0400389A RID: 14490
	public Transform ChanLeftArmRoll;

	// Token: 0x0400389B RID: 14491
	public Transform ChanLeftForeArm;

	// Token: 0x0400389C RID: 14492
	public Transform ChanLeftForeArmRoll;

	// Token: 0x0400389D RID: 14493
	public Transform ChanLeftHand;

	// Token: 0x0400389E RID: 14494
	public Transform ChanLeftHandPinky1;

	// Token: 0x0400389F RID: 14495
	public Transform ChanLeftHandPinky2;

	// Token: 0x040038A0 RID: 14496
	public Transform ChanLeftHandPinky3;

	// Token: 0x040038A1 RID: 14497
	public Transform ChanLeftHandRing1;

	// Token: 0x040038A2 RID: 14498
	public Transform ChanLeftHandRing2;

	// Token: 0x040038A3 RID: 14499
	public Transform ChanLeftHandRing3;

	// Token: 0x040038A4 RID: 14500
	public Transform ChanLeftHandMiddle1;

	// Token: 0x040038A5 RID: 14501
	public Transform ChanLeftHandMiddle2;

	// Token: 0x040038A6 RID: 14502
	public Transform ChanLeftHandMiddle3;

	// Token: 0x040038A7 RID: 14503
	public Transform ChanLeftHandIndex1;

	// Token: 0x040038A8 RID: 14504
	public Transform ChanLeftHandIndex2;

	// Token: 0x040038A9 RID: 14505
	public Transform ChanLeftHandIndex3;

	// Token: 0x040038AA RID: 14506
	public Transform ChanLeftHandThumb1;

	// Token: 0x040038AB RID: 14507
	public Transform ChanLeftHandThumb2;

	// Token: 0x040038AC RID: 14508
	public Transform ChanLeftHandThumb3;

	// Token: 0x040038AD RID: 14509
	public Transform ChanRightHandPinky1;

	// Token: 0x040038AE RID: 14510
	public Transform ChanRightHandPinky2;

	// Token: 0x040038AF RID: 14511
	public Transform ChanRightHandPinky3;

	// Token: 0x040038B0 RID: 14512
	public Transform ChanRightHandRing1;

	// Token: 0x040038B1 RID: 14513
	public Transform ChanRightHandRing2;

	// Token: 0x040038B2 RID: 14514
	public Transform ChanRightHandRing3;

	// Token: 0x040038B3 RID: 14515
	public Transform ChanRightHandMiddle1;

	// Token: 0x040038B4 RID: 14516
	public Transform ChanRightHandMiddle2;

	// Token: 0x040038B5 RID: 14517
	public Transform ChanRightHandMiddle3;

	// Token: 0x040038B6 RID: 14518
	public Transform ChanRightHandIndex1;

	// Token: 0x040038B7 RID: 14519
	public Transform ChanRightHandIndex2;

	// Token: 0x040038B8 RID: 14520
	public Transform ChanRightHandIndex3;

	// Token: 0x040038B9 RID: 14521
	public Transform ChanRightHandThumb1;

	// Token: 0x040038BA RID: 14522
	public Transform ChanRightHandThumb2;

	// Token: 0x040038BB RID: 14523
	public Transform ChanRightHandThumb3;

	// Token: 0x040038BC RID: 14524
	public Transform KunHips;

	// Token: 0x040038BD RID: 14525
	public Transform KunSpine;

	// Token: 0x040038BE RID: 14526
	public Transform KunSpine1;

	// Token: 0x040038BF RID: 14527
	public Transform KunSpine2;

	// Token: 0x040038C0 RID: 14528
	public Transform KunSpine3;

	// Token: 0x040038C1 RID: 14529
	public Transform KunNeck;

	// Token: 0x040038C2 RID: 14530
	public Transform KunHead;

	// Token: 0x040038C3 RID: 14531
	public Transform KunRightUpLeg;

	// Token: 0x040038C4 RID: 14532
	public Transform KunRightLeg;

	// Token: 0x040038C5 RID: 14533
	public Transform KunRightFoot;

	// Token: 0x040038C6 RID: 14534
	public Transform KunRightToes;

	// Token: 0x040038C7 RID: 14535
	public Transform KunLeftUpLeg;

	// Token: 0x040038C8 RID: 14536
	public Transform KunLeftLeg;

	// Token: 0x040038C9 RID: 14537
	public Transform KunLeftFoot;

	// Token: 0x040038CA RID: 14538
	public Transform KunLeftToes;

	// Token: 0x040038CB RID: 14539
	public Transform KunRightShoulder;

	// Token: 0x040038CC RID: 14540
	public Transform KunRightArm;

	// Token: 0x040038CD RID: 14541
	public Transform KunRightArmRoll;

	// Token: 0x040038CE RID: 14542
	public Transform KunRightForeArm;

	// Token: 0x040038CF RID: 14543
	public Transform KunRightForeArmRoll;

	// Token: 0x040038D0 RID: 14544
	public Transform KunRightHand;

	// Token: 0x040038D1 RID: 14545
	public Transform KunLeftShoulder;

	// Token: 0x040038D2 RID: 14546
	public Transform KunLeftArm;

	// Token: 0x040038D3 RID: 14547
	public Transform KunLeftArmRoll;

	// Token: 0x040038D4 RID: 14548
	public Transform KunLeftForeArm;

	// Token: 0x040038D5 RID: 14549
	public Transform KunLeftForeArmRoll;

	// Token: 0x040038D6 RID: 14550
	public Transform KunLeftHand;

	// Token: 0x040038D7 RID: 14551
	public Transform KunLeftHandPinky1;

	// Token: 0x040038D8 RID: 14552
	public Transform KunLeftHandPinky2;

	// Token: 0x040038D9 RID: 14553
	public Transform KunLeftHandPinky3;

	// Token: 0x040038DA RID: 14554
	public Transform KunLeftHandRing1;

	// Token: 0x040038DB RID: 14555
	public Transform KunLeftHandRing2;

	// Token: 0x040038DC RID: 14556
	public Transform KunLeftHandRing3;

	// Token: 0x040038DD RID: 14557
	public Transform KunLeftHandMiddle1;

	// Token: 0x040038DE RID: 14558
	public Transform KunLeftHandMiddle2;

	// Token: 0x040038DF RID: 14559
	public Transform KunLeftHandMiddle3;

	// Token: 0x040038E0 RID: 14560
	public Transform KunLeftHandIndex1;

	// Token: 0x040038E1 RID: 14561
	public Transform KunLeftHandIndex2;

	// Token: 0x040038E2 RID: 14562
	public Transform KunLeftHandIndex3;

	// Token: 0x040038E3 RID: 14563
	public Transform KunLeftHandThumb1;

	// Token: 0x040038E4 RID: 14564
	public Transform KunLeftHandThumb2;

	// Token: 0x040038E5 RID: 14565
	public Transform KunLeftHandThumb3;

	// Token: 0x040038E6 RID: 14566
	public Transform KunRightHandPinky1;

	// Token: 0x040038E7 RID: 14567
	public Transform KunRightHandPinky2;

	// Token: 0x040038E8 RID: 14568
	public Transform KunRightHandPinky3;

	// Token: 0x040038E9 RID: 14569
	public Transform KunRightHandRing1;

	// Token: 0x040038EA RID: 14570
	public Transform KunRightHandRing2;

	// Token: 0x040038EB RID: 14571
	public Transform KunRightHandRing3;

	// Token: 0x040038EC RID: 14572
	public Transform KunRightHandMiddle1;

	// Token: 0x040038ED RID: 14573
	public Transform KunRightHandMiddle2;

	// Token: 0x040038EE RID: 14574
	public Transform KunRightHandMiddle3;

	// Token: 0x040038EF RID: 14575
	public Transform KunRightHandIndex1;

	// Token: 0x040038F0 RID: 14576
	public Transform KunRightHandIndex2;

	// Token: 0x040038F1 RID: 14577
	public Transform KunRightHandIndex3;

	// Token: 0x040038F2 RID: 14578
	public Transform KunRightHandThumb1;

	// Token: 0x040038F3 RID: 14579
	public Transform KunRightHandThumb2;

	// Token: 0x040038F4 RID: 14580
	public Transform KunRightHandThumb3;

	// Token: 0x040038F5 RID: 14581
	public SkinnedMeshRenderer MyRenderer;

	// Token: 0x040038F6 RID: 14582
	public SkinnedMeshRenderer SecondRenderer;

	// Token: 0x040038F7 RID: 14583
	public SkinnedMeshRenderer ThirdRenderer;

	// Token: 0x040038F8 RID: 14584
	public bool Kizuna;

	// Token: 0x040038F9 RID: 14585
	public bool Man;

	// Token: 0x040038FA RID: 14586
	public int ID;

	// Token: 0x040038FB RID: 14587
	private bool Adjusted;
}
