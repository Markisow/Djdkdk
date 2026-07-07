using System;
using Unity.Netcode;
using UnityEngine;

// Token: 0x02000062 RID: 98
public class PuckCollisionDetectionModeSwitcher : MonoBehaviour
{
	// Token: 0x0600034A RID: 842 RVA: 0x0000B001 File Offset: 0x00009201
	private void Awake()
	{
		this.Rigidbody = base.GetComponent<Rigidbody>();
		Utils.SetRigidbodyCollisionDetectionMode(this.Rigidbody, CollisionDetectionMode.ContinuousDynamic);
	}

	// Token: 0x0600034B RID: 843 RVA: 0x0000B01B File Offset: 0x0000921B
	private void FixedUpdate()
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		if (this.IsContactingStick)
		{
			Utils.SetRigidbodyCollisionDetectionMode(this.Rigidbody, CollisionDetectionMode.ContinuousSpeculative);
		}
		else
		{
			Utils.SetRigidbodyCollisionDetectionMode(this.Rigidbody, CollisionDetectionMode.ContinuousDynamic);
		}
		this.IsContactingStick = false;
	}

	// Token: 0x0600034C RID: 844 RVA: 0x0000B053 File Offset: 0x00009253
	private void OnCollisionEnter(Collision collision)
	{
		if (!collision.gameObject.GetComponent<Stick>())
		{
			return;
		}
		this.IsContactingStick = true;
		Utils.SetRigidbodyCollisionDetectionMode(this.Rigidbody, CollisionDetectionMode.ContinuousSpeculative);
	}

	// Token: 0x0600034D RID: 845 RVA: 0x0000B053 File Offset: 0x00009253
	private void OnCollisionStay(Collision collision)
	{
		if (!collision.gameObject.GetComponent<Stick>())
		{
			return;
		}
		this.IsContactingStick = true;
		Utils.SetRigidbodyCollisionDetectionMode(this.Rigidbody, CollisionDetectionMode.ContinuousSpeculative);
	}

	// Token: 0x0600034E RID: 846 RVA: 0x00024764 File Offset: 0x00022964
	public void OnDrawGizmos()
	{
		if (!Application.isEditor)
		{
			return;
		}
		if (this.Rigidbody)
		{
			Gizmos.color = ((this.Rigidbody.collisionDetectionMode == CollisionDetectionMode.ContinuousSpeculative) ? Color.red : Color.green);
		}
		Gizmos.DrawWireSphere(base.transform.position, 0.5f);
	}

	// Token: 0x04000250 RID: 592
	[HideInInspector]
	public Rigidbody Rigidbody;

	// Token: 0x04000251 RID: 593
	[HideInInspector]
	public bool IsContactingStick;
}
