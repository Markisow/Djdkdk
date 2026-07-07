using System;

// Token: 0x0200003D RID: 61
public class PlayerCameraController : BaseCameraController
{
	// Token: 0x06000180 RID: 384 RVA: 0x00009C69 File Offset: 0x00007E69
	public override void Awake()
	{
		base.Awake();
		this.playerCamera = base.GetComponent<PlayerCamera>();
	}

	// Token: 0x04000128 RID: 296
	private PlayerCamera playerCamera;
}
