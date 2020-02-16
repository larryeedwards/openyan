using System;
using UnityEngine;

// Token: 0x0200030F RID: 783
public class ExampleWheelController : MonoBehaviour
{
	// Token: 0x060016B3 RID: 5811 RVA: 0x000AE265 File Offset: 0x000AC665
	private void Start()
	{
		this.m_Rigidbody = base.GetComponent<Rigidbody>();
		this.m_Rigidbody.maxAngularVelocity = 100f;
	}

	// Token: 0x060016B4 RID: 5812 RVA: 0x000AE284 File Offset: 0x000AC684
	private void Update()
	{
		if (Input.GetKey(KeyCode.UpArrow))
		{
			this.m_Rigidbody.AddRelativeTorque(new Vector3(-1f * this.acceleration, 0f, 0f), ForceMode.Acceleration);
		}
		else if (Input.GetKey(KeyCode.DownArrow))
		{
			this.m_Rigidbody.AddRelativeTorque(new Vector3(1f * this.acceleration, 0f, 0f), ForceMode.Acceleration);
		}
		float value = -this.m_Rigidbody.angularVelocity.x / 100f;
		if (this.motionVectorRenderer)
		{
			this.motionVectorRenderer.material.SetFloat(ExampleWheelController.Uniforms._MotionAmount, Mathf.Clamp(value, -0.25f, 0.25f));
		}
	}

	// Token: 0x04001454 RID: 5204
	public float acceleration;

	// Token: 0x04001455 RID: 5205
	public Renderer motionVectorRenderer;

	// Token: 0x04001456 RID: 5206
	private Rigidbody m_Rigidbody;

	// Token: 0x02000310 RID: 784
	private static class Uniforms
	{
		// Token: 0x04001457 RID: 5207
		internal static readonly int _MotionAmount = Shader.PropertyToID("_MotionAmount");
	}
}
