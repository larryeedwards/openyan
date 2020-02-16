using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace AmplifyMotion
{
	// Token: 0x0200028F RID: 655
	[Serializable]
	internal abstract class MotionState
	{
		// Token: 0x060014FC RID: 5372 RVA: 0x000A23D1 File Offset: 0x000A07D1
		public MotionState(AmplifyMotionCamera owner, AmplifyMotionObjectBase obj)
		{
			this.m_error = false;
			this.m_initialized = false;
			this.m_owner = owner;
			this.m_obj = obj;
			this.m_transform = obj.transform;
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x060014FD RID: 5373 RVA: 0x000A2401 File Offset: 0x000A0801
		public AmplifyMotionCamera Owner
		{
			get
			{
				return this.m_owner;
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x060014FE RID: 5374 RVA: 0x000A2409 File Offset: 0x000A0809
		public bool Initialized
		{
			get
			{
				return this.m_initialized;
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x060014FF RID: 5375 RVA: 0x000A2411 File Offset: 0x000A0811
		public bool Error
		{
			get
			{
				return this.m_error;
			}
		}

		// Token: 0x06001500 RID: 5376 RVA: 0x000A2419 File Offset: 0x000A0819
		internal virtual void Initialize()
		{
			this.m_initialized = true;
		}

		// Token: 0x06001501 RID: 5377 RVA: 0x000A2422 File Offset: 0x000A0822
		internal virtual void Shutdown()
		{
		}

		// Token: 0x06001502 RID: 5378 RVA: 0x000A2424 File Offset: 0x000A0824
		internal virtual void AsyncUpdate()
		{
		}

		// Token: 0x06001503 RID: 5379
		internal abstract void UpdateTransform(CommandBuffer updateCB, bool starting);

		// Token: 0x06001504 RID: 5380 RVA: 0x000A2426 File Offset: 0x000A0826
		internal virtual void RenderVectors(Camera camera, CommandBuffer renderCB, float scale, Quality quality)
		{
		}

		// Token: 0x06001505 RID: 5381 RVA: 0x000A2428 File Offset: 0x000A0828
		internal virtual void RenderDebugHUD()
		{
		}

		// Token: 0x06001506 RID: 5382 RVA: 0x000A242C File Offset: 0x000A082C
		protected MotionState.MaterialDesc[] ProcessSharedMaterials(Material[] mats)
		{
			MotionState.MaterialDesc[] array = new MotionState.MaterialDesc[mats.Length];
			for (int i = 0; i < mats.Length; i++)
			{
				array[i].material = mats[i];
				bool flag = mats[i].GetTag("RenderType", false) == "TransparentCutout" || mats[i].IsKeywordEnabled("_ALPHATEST_ON");
				array[i].propertyBlock = new MaterialPropertyBlock();
				array[i].coverage = (mats[i].HasProperty("_MainTex") && flag);
				array[i].cutoff = mats[i].HasProperty("_Cutoff");
				if (flag && !array[i].coverage && !MotionState.m_materialWarnings.Contains(array[i].material))
				{
					Debug.LogWarning(string.Concat(new string[]
					{
						"[AmplifyMotion] TransparentCutout material \"",
						array[i].material.name,
						"\" {",
						array[i].material.shader.name,
						"} not using _MainTex standard property."
					}));
					MotionState.m_materialWarnings.Add(array[i].material);
				}
			}
			return array;
		}

		// Token: 0x06001507 RID: 5383 RVA: 0x000A2580 File Offset: 0x000A0980
		protected static bool MatrixChanged(MotionState.Matrix3x4 a, MotionState.Matrix3x4 b)
		{
			return Vector4.SqrMagnitude(new Vector4(a.m00 - b.m00, a.m01 - b.m01, a.m02 - b.m02, a.m03 - b.m03)) > 0f || Vector4.SqrMagnitude(new Vector4(a.m10 - b.m10, a.m11 - b.m11, a.m12 - b.m12, a.m13 - b.m13)) > 0f || Vector4.SqrMagnitude(new Vector4(a.m20 - b.m20, a.m21 - b.m21, a.m22 - b.m22, a.m23 - b.m23)) > 0f;
		}

		// Token: 0x06001508 RID: 5384 RVA: 0x000A2684 File Offset: 0x000A0A84
		protected static void MulPoint3x4_XYZ(ref Vector3 result, ref MotionState.Matrix3x4 mat, Vector4 vec)
		{
			result.x = mat.m00 * vec.x + mat.m01 * vec.y + mat.m02 * vec.z + mat.m03;
			result.y = mat.m10 * vec.x + mat.m11 * vec.y + mat.m12 * vec.z + mat.m13;
			result.z = mat.m20 * vec.x + mat.m21 * vec.y + mat.m22 * vec.z + mat.m23;
		}

		// Token: 0x06001509 RID: 5385 RVA: 0x000A273C File Offset: 0x000A0B3C
		protected static void MulPoint3x4_XYZW(ref Vector3 result, ref MotionState.Matrix3x4 mat, Vector4 vec)
		{
			result.x = mat.m00 * vec.x + mat.m01 * vec.y + mat.m02 * vec.z + mat.m03 * vec.w;
			result.y = mat.m10 * vec.x + mat.m11 * vec.y + mat.m12 * vec.z + mat.m13 * vec.w;
			result.z = mat.m20 * vec.x + mat.m21 * vec.y + mat.m22 * vec.z + mat.m23 * vec.w;
		}

		// Token: 0x0600150A RID: 5386 RVA: 0x000A280C File Offset: 0x000A0C0C
		protected static void MulAddPoint3x4_XYZW(ref Vector3 result, ref MotionState.Matrix3x4 mat, Vector4 vec)
		{
			result.x += mat.m00 * vec.x + mat.m01 * vec.y + mat.m02 * vec.z + mat.m03 * vec.w;
			result.y += mat.m10 * vec.x + mat.m11 * vec.y + mat.m12 * vec.z + mat.m13 * vec.w;
			result.z += mat.m20 * vec.x + mat.m21 * vec.y + mat.m22 * vec.z + mat.m23 * vec.w;
		}

		// Token: 0x040011D3 RID: 4563
		public const int AsyncUpdateTimeout = 100;

		// Token: 0x040011D4 RID: 4564
		protected bool m_error;

		// Token: 0x040011D5 RID: 4565
		protected bool m_initialized;

		// Token: 0x040011D6 RID: 4566
		protected Transform m_transform;

		// Token: 0x040011D7 RID: 4567
		protected AmplifyMotionCamera m_owner;

		// Token: 0x040011D8 RID: 4568
		protected AmplifyMotionObjectBase m_obj;

		// Token: 0x040011D9 RID: 4569
		private static HashSet<Material> m_materialWarnings = new HashSet<Material>();

		// Token: 0x02000290 RID: 656
		protected struct MaterialDesc
		{
			// Token: 0x040011DA RID: 4570
			public Material material;

			// Token: 0x040011DB RID: 4571
			public MaterialPropertyBlock propertyBlock;

			// Token: 0x040011DC RID: 4572
			public bool coverage;

			// Token: 0x040011DD RID: 4573
			public bool cutoff;
		}

		// Token: 0x02000291 RID: 657
		protected struct Matrix3x4
		{
			// Token: 0x0600150C RID: 5388 RVA: 0x000A2900 File Offset: 0x000A0D00
			public Vector4 GetRow(int i)
			{
				if (i == 0)
				{
					return new Vector4(this.m00, this.m01, this.m02, this.m03);
				}
				if (i == 1)
				{
					return new Vector4(this.m10, this.m11, this.m12, this.m13);
				}
				if (i == 2)
				{
					return new Vector4(this.m20, this.m21, this.m22, this.m23);
				}
				return new Vector4(0f, 0f, 0f, 1f);
			}

			// Token: 0x0600150D RID: 5389 RVA: 0x000A2994 File Offset: 0x000A0D94
			public static implicit operator MotionState.Matrix3x4(Matrix4x4 from)
			{
				return new MotionState.Matrix3x4
				{
					m00 = from.m00,
					m01 = from.m01,
					m02 = from.m02,
					m03 = from.m03,
					m10 = from.m10,
					m11 = from.m11,
					m12 = from.m12,
					m13 = from.m13,
					m20 = from.m20,
					m21 = from.m21,
					m22 = from.m22,
					m23 = from.m23
				};
			}

			// Token: 0x0600150E RID: 5390 RVA: 0x000A2A54 File Offset: 0x000A0E54
			public static implicit operator Matrix4x4(MotionState.Matrix3x4 from)
			{
				Matrix4x4 result = default(Matrix4x4);
				result.m00 = from.m00;
				result.m01 = from.m01;
				result.m02 = from.m02;
				result.m03 = from.m03;
				result.m10 = from.m10;
				result.m11 = from.m11;
				result.m12 = from.m12;
				result.m13 = from.m13;
				result.m20 = from.m20;
				result.m21 = from.m21;
				result.m22 = from.m22;
				result.m23 = from.m23;
				result.m30 = (result.m31 = (result.m32 = 0f));
				result.m33 = 1f;
				return result;
			}

			// Token: 0x0600150F RID: 5391 RVA: 0x000A2B40 File Offset: 0x000A0F40
			public static MotionState.Matrix3x4 operator *(MotionState.Matrix3x4 a, MotionState.Matrix3x4 b)
			{
				return new MotionState.Matrix3x4
				{
					m00 = a.m00 * b.m00 + a.m01 * b.m10 + a.m02 * b.m20,
					m01 = a.m00 * b.m01 + a.m01 * b.m11 + a.m02 * b.m21,
					m02 = a.m00 * b.m02 + a.m01 * b.m12 + a.m02 * b.m22,
					m03 = a.m00 * b.m03 + a.m01 * b.m13 + a.m02 * b.m23 + a.m03,
					m10 = a.m10 * b.m00 + a.m11 * b.m10 + a.m12 * b.m20,
					m11 = a.m10 * b.m01 + a.m11 * b.m11 + a.m12 * b.m21,
					m12 = a.m10 * b.m02 + a.m11 * b.m12 + a.m12 * b.m22,
					m13 = a.m10 * b.m03 + a.m11 * b.m13 + a.m12 * b.m23 + a.m13,
					m20 = a.m20 * b.m00 + a.m21 * b.m10 + a.m22 * b.m20,
					m21 = a.m20 * b.m01 + a.m21 * b.m11 + a.m22 * b.m21,
					m22 = a.m20 * b.m02 + a.m21 * b.m12 + a.m22 * b.m22,
					m23 = a.m20 * b.m03 + a.m21 * b.m13 + a.m22 * b.m23 + a.m23
				};
			}

			// Token: 0x040011DE RID: 4574
			public float m00;

			// Token: 0x040011DF RID: 4575
			public float m01;

			// Token: 0x040011E0 RID: 4576
			public float m02;

			// Token: 0x040011E1 RID: 4577
			public float m03;

			// Token: 0x040011E2 RID: 4578
			public float m10;

			// Token: 0x040011E3 RID: 4579
			public float m11;

			// Token: 0x040011E4 RID: 4580
			public float m12;

			// Token: 0x040011E5 RID: 4581
			public float m13;

			// Token: 0x040011E6 RID: 4582
			public float m20;

			// Token: 0x040011E7 RID: 4583
			public float m21;

			// Token: 0x040011E8 RID: 4584
			public float m22;

			// Token: 0x040011E9 RID: 4585
			public float m23;
		}
	}
}
