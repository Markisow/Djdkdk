using System;
using UnityEngine;

// Token: 0x0200006E RID: 110
public class SpectatorController : MonoBehaviour
{
	// Token: 0x0600038A RID: 906 RVA: 0x0000B388 File Offset: 0x00009588
	private void Awake()
	{
		this.spectator = base.GetComponent<Spectator>();
	}

	// Token: 0x0600038B RID: 907 RVA: 0x0000895D File Offset: 0x00006B5D
	private void OnDestroy()
	{
	}

	// Token: 0x04000287 RID: 647
	private Spectator spectator;
}
