using System;
using UnityEngine;

// Token: 0x0200006C RID: 108
public class SpectatorPositionController : MonoBehaviour
{
	// Token: 0x06000380 RID: 896 RVA: 0x0000B322 File Offset: 0x00009522
	private void Awake()
	{
		this.spectatorPosition = base.GetComponent<SpectatorPosition>();
	}

	// Token: 0x06000381 RID: 897 RVA: 0x0000895D File Offset: 0x00006B5D
	public void Start()
	{
	}

	// Token: 0x06000382 RID: 898 RVA: 0x0000895D File Offset: 0x00006B5D
	private void OnDestroy()
	{
	}

	// Token: 0x0400027D RID: 637
	private SpectatorPosition spectatorPosition;
}
