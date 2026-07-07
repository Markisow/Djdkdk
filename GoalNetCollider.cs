using System;
using UnityEngine;

// Token: 0x0200000E RID: 14
public class GoalNetCollider : MonoBehaviour
{
	// Token: 0x0600003B RID: 59 RVA: 0x00015CF0 File Offset: 0x00013EF0
	private void OnCollisionEnter(Collision collision)
	{
		Puck componentInParent = collision.gameObject.GetComponentInParent<Puck>();
		if (!componentInParent)
		{
			return;
		}
		if (componentInParent.IsGrounded)
		{
			return;
		}
		componentInParent.Rigidbody.linearVelocity *= 1f - this.damping;
		componentInParent.Rigidbody.angularVelocity *= 1f - this.damping;
		if (componentInParent.Rigidbody.linearVelocity.magnitude > this.linearVelocityMaximumMagnitude)
		{
			componentInParent.Rigidbody.linearVelocity = componentInParent.Rigidbody.linearVelocity.normalized * this.linearVelocityMaximumMagnitude;
		}
		if (componentInParent.Rigidbody.angularVelocity.magnitude > this.angularVelocityMaximumMagnitude)
		{
			componentInParent.Rigidbody.angularVelocity = componentInParent.Rigidbody.angularVelocity.normalized * this.angularVelocityMaximumMagnitude;
		}
	}

	// Token: 0x0400001E RID: 30
	[Header("Settings")]
	[SerializeField]
	private float damping = 0.25f;

	// Token: 0x0400001F RID: 31
	[SerializeField]
	private float linearVelocityMaximumMagnitude = 2f;

	// Token: 0x04000020 RID: 32
	[SerializeField]
	private float angularVelocityMaximumMagnitude = 2f;
}
