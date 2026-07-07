using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200006B RID: 107
public class SpectatorPosition : MonoBehaviour
{
	// Token: 0x0600037C RID: 892 RVA: 0x0000B2E8 File Offset: 0x000094E8
	private void Start()
	{
		EventManager.TriggerEvent("Event_OnSpectatorPositionSpawned", new Dictionary<string, object>
		{
			{
				"spectatorPosition",
				this
			}
		});
	}

	// Token: 0x0600037D RID: 893 RVA: 0x0000B305 File Offset: 0x00009505
	private void OnDestroy()
	{
		EventManager.TriggerEvent("Event_OnSpectatorPositionDespawned", new Dictionary<string, object>
		{
			{
				"spectatorPosition",
				this
			}
		});
	}

	// Token: 0x0600037E RID: 894 RVA: 0x000252A8 File Offset: 0x000234A8
	private void OnDrawGizmos()
	{
		if (!Application.isEditor)
		{
			return;
		}
		Gizmos.color = Color.white;
		Gizmos.DrawLine(base.transform.position, base.transform.position + Vector3.up * 0.5f);
	}
}
