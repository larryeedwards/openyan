using System;
using UnityEngine;

// Token: 0x02000208 RID: 520
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Internal/Property Binding")]
public class PropertyBinding : MonoBehaviour
{
	// Token: 0x06000FF8 RID: 4088 RVA: 0x0008285C File Offset: 0x00080C5C
	private void Start()
	{
		this.UpdateTarget();
		if (this.update == PropertyBinding.UpdateCondition.OnStart)
		{
			base.enabled = false;
		}
	}

	// Token: 0x06000FF9 RID: 4089 RVA: 0x00082876 File Offset: 0x00080C76
	private void Update()
	{
		if (this.update == PropertyBinding.UpdateCondition.OnUpdate)
		{
			this.UpdateTarget();
		}
	}

	// Token: 0x06000FFA RID: 4090 RVA: 0x0008288A File Offset: 0x00080C8A
	private void LateUpdate()
	{
		if (this.update == PropertyBinding.UpdateCondition.OnLateUpdate)
		{
			this.UpdateTarget();
		}
	}

	// Token: 0x06000FFB RID: 4091 RVA: 0x0008289E File Offset: 0x00080C9E
	private void FixedUpdate()
	{
		if (this.update == PropertyBinding.UpdateCondition.OnFixedUpdate)
		{
			this.UpdateTarget();
		}
	}

	// Token: 0x06000FFC RID: 4092 RVA: 0x000828B2 File Offset: 0x00080CB2
	private void OnValidate()
	{
		if (this.source != null)
		{
			this.source.Reset();
		}
		if (this.target != null)
		{
			this.target.Reset();
		}
	}

	// Token: 0x06000FFD RID: 4093 RVA: 0x000828E0 File Offset: 0x00080CE0
	[ContextMenu("Update Now")]
	public void UpdateTarget()
	{
		if (this.source != null && this.target != null && this.source.isValid && this.target.isValid)
		{
			if (this.direction == PropertyBinding.Direction.SourceUpdatesTarget)
			{
				this.target.Set(this.source.Get());
			}
			else if (this.direction == PropertyBinding.Direction.TargetUpdatesSource)
			{
				this.source.Set(this.target.Get());
			}
			else if (this.source.GetPropertyType() == this.target.GetPropertyType())
			{
				object obj = this.source.Get();
				if (this.mLastValue == null || !this.mLastValue.Equals(obj))
				{
					this.mLastValue = obj;
					this.target.Set(obj);
				}
				else
				{
					obj = this.target.Get();
					if (!this.mLastValue.Equals(obj))
					{
						this.mLastValue = obj;
						this.source.Set(obj);
					}
				}
			}
		}
	}

	// Token: 0x04000E01 RID: 3585
	public PropertyReference source;

	// Token: 0x04000E02 RID: 3586
	public PropertyReference target;

	// Token: 0x04000E03 RID: 3587
	public PropertyBinding.Direction direction;

	// Token: 0x04000E04 RID: 3588
	public PropertyBinding.UpdateCondition update = PropertyBinding.UpdateCondition.OnUpdate;

	// Token: 0x04000E05 RID: 3589
	public bool editMode = true;

	// Token: 0x04000E06 RID: 3590
	private object mLastValue;

	// Token: 0x02000209 RID: 521
	public enum UpdateCondition
	{
		// Token: 0x04000E08 RID: 3592
		OnStart,
		// Token: 0x04000E09 RID: 3593
		OnUpdate,
		// Token: 0x04000E0A RID: 3594
		OnLateUpdate,
		// Token: 0x04000E0B RID: 3595
		OnFixedUpdate
	}

	// Token: 0x0200020A RID: 522
	public enum Direction
	{
		// Token: 0x04000E0D RID: 3597
		SourceUpdatesTarget,
		// Token: 0x04000E0E RID: 3598
		TargetUpdatesSource,
		// Token: 0x04000E0F RID: 3599
		BiDirectional
	}
}
