using System;
using UnityEngine;

// Token: 0x0200001A RID: 26
public class LockerRoomController : MonoBehaviour
{
	// Token: 0x0600008C RID: 140 RVA: 0x0000904C File Offset: 0x0000724C
	private void Awake()
	{
		this.lockerRoom = base.GetComponent<LockerRoom>();
	}

	// Token: 0x0600008D RID: 141 RVA: 0x0000895D File Offset: 0x00006B5D
	private void OnDestroy()
	{
	}

	// Token: 0x0400003F RID: 63
	private LockerRoom lockerRoom;
}
