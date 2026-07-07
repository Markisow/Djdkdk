using System;
using UnityEngine;

// Token: 0x0200000F RID: 15
public class GoalTrigger : MonoBehaviour
{
	// Token: 0x0600003D RID: 61 RVA: 0x00015DE8 File Offset: 0x00013FE8
	private void OnTriggerEnter(Collider collider)
	{
		Puck componentInParent = collider.GetComponentInParent<Puck>();
		if (!componentInParent)
		{
			return;
		}
		this.goal.Server_OnPuckEnterGoal(componentInParent);
	}

	// Token: 0x04000021 RID: 33
	[Header("References")]
	[SerializeField]
	private Goal goal;
}
