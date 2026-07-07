using System;
using System.Collections.Generic;

// Token: 0x02000196 RID: 406
public class UIIdentityController : UIViewController<UIIdentity>
{
	// Token: 0x06000BCE RID: 3022 RVA: 0x00011B15 File Offset: 0x0000FD15
	public override void Awake()
	{
		base.Awake();
		this.uiIdentity = base.GetComponent<UIIdentity>();
		EventManager.AddEventListener("Event_OnPlayerDataChanged", new Action<Dictionary<string, object>>(this.Event_OnPlayerDataChanged));
	}

	// Token: 0x06000BCF RID: 3023 RVA: 0x00011B3F File Offset: 0x0000FD3F
	public override void OnDestroy()
	{
		EventManager.RemoveEventListener("Event_OnPlayerDataChanged", new Action<Dictionary<string, object>>(this.Event_OnPlayerDataChanged));
		base.OnDestroy();
	}

	// Token: 0x06000BD0 RID: 3024 RVA: 0x00042078 File Offset: 0x00040278
	private void Event_OnPlayerDataChanged(Dictionary<string, object> message)
	{
		PlayerData playerData = (PlayerData)message["newPlayerData"];
		if (playerData == null)
		{
			return;
		}
		this.uiIdentity.SetIdentity(playerData.username, playerData.number);
	}

	// Token: 0x04000713 RID: 1811
	private UIIdentity uiIdentity;
}
