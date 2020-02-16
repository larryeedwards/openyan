﻿using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020005C3 RID: 1475
public class YSRetargeting : MonoBehaviour
{
	// Token: 0x06002367 RID: 9063 RVA: 0x001BFC10 File Offset: 0x001BE010
	private void Start()
	{
		this.SourceBones = new Component[160];
		this.TargetBones = new Component[160];
		this.SourceSkelNodes = this.Source.GetComponentsInChildren<Component>();
		this.TargetSkelNodes = this.Target.GetComponentsInChildren<Component>();
		string[] array = new string[]
		{
			"PelvisRoot",
			"Hips",
			"LeftUpLeg",
			"LeftLeg",
			"LeftFoot",
			"LeftToes",
			"LeftToesNull",
			"LeftLeg_Helper",
			"LeftHip_Helper",
			"RightUpLeg",
			"RightLeg",
			"RightFoot",
			"RightToes",
			"RightToesNull",
			"RightLeg_Helper",
			"RightHip_Helper",
			"Front_skart1",
			"Front_skart2",
			"Front_skart3",
			"Left_skart1",
			"Left_skart2",
			"Left_skart3",
			"Back_skart1",
			"Back_skart2",
			"Back_skart3",
			"Right_skart1",
			"Right_skart2",
			"Right_skart3",
			"Spine",
			"Spine1",
			"Spine2",
			"Spine3",
			"Neck",
			"Head",
			"Head_null",
			"FrontHair",
			"BackHair1",
			"BackHair2",
			"BackHair3",
			"BackHair4",
			"LeftHair1",
			"LeftHair2",
			"LeftHair3",
			"RightHair1",
			"RightHair2",
			"RightHair3",
			"face",
			"atama_n",
			"mayu",
			"eyebrow_a_Left",
			"eyebrow_b_Left",
			"temple_Left",
			"eyebrow_a_Right",
			"eyebrow_b_Right",
			"temple_Right",
			"eyerid_Left",
			"eye_Left",
			"eyelid_und_Left",
			"cheek_Left",
			"u_lip_center",
			"lip_top_Left",
			"lip_top_Right",
			"jaw",
			"chin",
			"b_lip_center",
			"rip_btm_Left",
			"lip_u_Left",
			"rip_btm_Right",
			"lip_u_Right",
			"nose",
			"nostrir_Right",
			"nostrir_Left",
			"inner_corner_of_eye_Left",
			"tail_of_eye_Left",
			"lip_Left",
			"lip_t_Left",
			"lip_Right",
			"lip_t_Right",
			"eyerid1_Left",
			"eyerid2_Left",
			"eyelid_und1_Left",
			"eyelid_und2_Left",
			"eyerid_Right",
			"eyerid1_Right",
			"eyerid2_Right",
			"tail_of_eye_Right",
			"eyelid_und2_Right",
			"eyelid_und_Right",
			"cheek_Right",
			"eyelid_und1_Right",
			"eye_Right",
			"inner_corner_of_eye_Right",
			"LeftShoulder",
			"LeftArm",
			"LeftArmRoll",
			"LeftForeArm",
			"LeftForeArmRoll",
			"LeftHand",
			"LeftHandThumb1",
			"LeftHandThumb2",
			"LeftHandThumb3",
			"LeftHandThumbNull",
			"LeftHandIndex1",
			"LeftHandIndex2",
			"LeftHandIndex3",
			"LeftHandIndexNull",
			"LeftHandMiddle1",
			"LeftHandMiddle2",
			"LeftHandMiddle3",
			"LeftHandMiddleNull",
			"LeftHandRing1",
			"LeftHandRing2",
			"LeftHandRing3",
			"LeftHandRingNull",
			"LeftHandPinky1",
			"LeftHandPinky2",
			"LeftHandPinky3",
			"LeftHandPinkyNull",
			"LeftForeArm_Helper",
			"RightShoulder",
			"RightArm",
			"RightArmRoll",
			"RightForeArm",
			"RightForeArmRoll",
			"RightHand",
			"RightHandThumb1",
			"RightHandThumb2",
			"RightHandThumb3",
			"RightHandThumbNull",
			"RightHandIndex1",
			"RightHandIndex2",
			"RightHandIndex3",
			"RightHandIndexNull",
			"RightHandMiddle1",
			"RightHandMiddle2",
			"RightHandMiddle3",
			"RightHandMiddleNull",
			"RightHandRing1",
			"RightHandRing2",
			"RightHandRing3",
			"RightHandRingNull",
			"RightHandPinky1",
			"RightHandPinky2",
			"RightHandPinky3",
			"RightHandPinkyNull",
			"RightForeArm_Helper",
			"LeftBreast",
			"LeftNipple",
			"RightBreast",
			"RightNipple"
		};
		List<string> list = new List<string>();
		foreach (string item in array)
		{
			list.Add(item);
		}
		int num = 0;
		for (int j = 0; j < this.SourceSkelNodes.Length; j++)
		{
			if (list.Contains(this.SourceSkelNodes[j].name))
			{
				this.SourceBones[num] = this.SourceSkelNodes[j];
				num++;
			}
		}
		num = 0;
		for (int k = 0; k < this.TargetSkelNodes.Length; k++)
		{
			if (list.Contains(this.TargetSkelNodes[k].name))
			{
				this.TargetBones[num] = this.TargetSkelNodes[k];
				num++;
			}
		}
	}

	// Token: 0x06002368 RID: 9064 RVA: 0x001C02B7 File Offset: 0x001BE6B7
	private void Update()
	{
	}

	// Token: 0x06002369 RID: 9065 RVA: 0x001C02BC File Offset: 0x001BE6BC
	private void LateUpdate()
	{
		for (int i = 0; i < this.TargetBones.Length; i++)
		{
			if (this.TargetBones[i])
			{
				this.TargetBones[i].transform.localPosition = this.SourceBones[i].transform.localPosition;
				this.TargetBones[i].transform.localRotation = this.SourceBones[i].transform.localRotation;
			}
		}
	}

	// Token: 0x04003D30 RID: 15664
	public GameObject Source;

	// Token: 0x04003D31 RID: 15665
	public GameObject Target;

	// Token: 0x04003D32 RID: 15666
	private Component[] SourceSkelNodes;

	// Token: 0x04003D33 RID: 15667
	private Component[] TargetSkelNodes;

	// Token: 0x04003D34 RID: 15668
	private Component[] SourceBones;

	// Token: 0x04003D35 RID: 15669
	private Component[] TargetBones;
}
