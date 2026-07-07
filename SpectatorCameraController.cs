using System;

// Token: 0x0200006A RID: 106
public class SpectatorCameraController : BaseCameraController
{
	// Token: 0x0600037A RID: 890 RVA: 0x0000B2D4 File Offset: 0x000094D4
	public override void Awake()
	{
		base.Awake();
		this.spectatorCamera = base.GetComponent<SpectatorCamera>();
	}

	// Token: 0x0400027C RID: 636
	private SpectatorCamera spectatorCamera;
}
