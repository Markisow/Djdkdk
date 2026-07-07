using System;
using System.Collections.Generic;

// Token: 0x020001D9 RID: 473
public class UIUsernamesController : UIViewController<UIUsernames>
{
	// Token: 0x06000E40 RID: 3648 RVA: 0x0004B704 File Offset: 0x00049904
	public override void Awake()
	{
		base.Awake();
		this.uiUsernames = base.GetComponent<UIUsernames>();
		EventManager.AddEventListener("Event_Everyone_OnLevelSpawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnLevelSpawned));
		EventManager.AddEventListener("Event_Everyone_OnPlayerBodySpawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerBodySpawned));
		EventManager.AddEventListener("Event_Everyone_OnPlayerBodyDespawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerBodyDespawned));
		EventManager.AddEventListener("Event_Everyone_OnPlayerUsernameChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerUsernameChanged));
		EventManager.AddEventListener("Event_Everyone_OnPlayerNumberChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerNumberChanged));
		EventManager.AddEventListener("Event_OnShowPlayerUsernamesChanged", new Action<Dictionary<string, object>>(this.Event_OnShowPlayerUsernamesChanged));
		EventManager.AddEventListener("Event_OnPlayerUsernamesFadeThresholdChanged", new Action<Dictionary<string, object>>(this.Event_OnPlayerUsernamesFadeThresholdChanged));
	}

	// Token: 0x06000E41 RID: 3649 RVA: 0x00013C12 File Offset: 0x00011E12
	private void Start()
	{
		this.uiUsernames.FadeThreshold = SettingsManager.PlayerUsernamesFadeThreshold;
	}

	// Token: 0x06000E42 RID: 3650 RVA: 0x0004B7C0 File Offset: 0x000499C0
	public override void OnDestroy()
	{
		EventManager.RemoveEventListener("Event_Everyone_OnLevelSpawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnLevelSpawned));
		EventManager.RemoveEventListener("Event_Everyone_OnPlayerBodySpawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerBodySpawned));
		EventManager.RemoveEventListener("Event_Everyone_OnPlayerBodyDespawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerBodyDespawned));
		EventManager.RemoveEventListener("Event_Everyone_OnPlayerUsernameChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerUsernameChanged));
		EventManager.RemoveEventListener("Event_Everyone_OnPlayerNumberChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerNumberChanged));
		EventManager.RemoveEventListener("Event_OnShowPlayerUsernamesChanged", new Action<Dictionary<string, object>>(this.Event_OnShowPlayerUsernamesChanged));
		EventManager.RemoveEventListener("Event_OnPlayerUsernamesFadeThresholdChanged", new Action<Dictionary<string, object>>(this.Event_OnPlayerUsernamesFadeThresholdChanged));
		base.OnDestroy();
	}

	// Token: 0x06000E43 RID: 3651 RVA: 0x0004B870 File Offset: 0x00049A70
	private void Event_Everyone_OnLevelSpawned(Dictionary<string, object> message)
	{
		Level level = (Level)message["level"];
		this.uiUsernames.Bounds = level.Bounds;
	}

	// Token: 0x06000E44 RID: 3652 RVA: 0x0004B8A0 File Offset: 0x00049AA0
	private void Event_Everyone_OnPlayerBodySpawned(Dictionary<string, object> message)
	{
		PlayerBody playerBody = (PlayerBody)message["playerBody"];
		this.uiUsernames.AddPlayerBody(playerBody);
	}

	// Token: 0x06000E45 RID: 3653 RVA: 0x0004B8CC File Offset: 0x00049ACC
	private void Event_Everyone_OnPlayerBodyDespawned(Dictionary<string, object> message)
	{
		PlayerBody playerBody = (PlayerBody)message["playerBody"];
		this.uiUsernames.RemovePlayerBody(playerBody);
	}

	// Token: 0x06000E46 RID: 3654 RVA: 0x0004B8F8 File Offset: 0x00049AF8
	private void Event_Everyone_OnPlayerUsernameChanged(Dictionary<string, object> message)
	{
		PlayerBody playerBody = ((Player)message["player"]).PlayerBody;
		if (playerBody)
		{
			this.uiUsernames.StyleUsername(playerBody);
		}
	}

	// Token: 0x06000E47 RID: 3655 RVA: 0x0004B8F8 File Offset: 0x00049AF8
	private void Event_Everyone_OnPlayerNumberChanged(Dictionary<string, object> message)
	{
		PlayerBody playerBody = ((Player)message["player"]).PlayerBody;
		if (playerBody)
		{
			this.uiUsernames.StyleUsername(playerBody);
		}
	}

	// Token: 0x06000E48 RID: 3656 RVA: 0x00013C24 File Offset: 0x00011E24
	private void Event_OnShowPlayerUsernamesChanged(Dictionary<string, object> message)
	{
		if ((bool)message["value"])
		{
			this.uiUsernames.Show();
			return;
		}
		this.uiUsernames.Hide();
	}

	// Token: 0x06000E49 RID: 3657 RVA: 0x0004B930 File Offset: 0x00049B30
	private void Event_OnPlayerUsernamesFadeThresholdChanged(Dictionary<string, object> message)
	{
		float fadeThreshold = (float)message["value"];
		this.uiUsernames.FadeThreshold = fadeThreshold;
	}

	// Token: 0x0400086B RID: 2155
	private static readonly Logger Logger = new Logger("UIUsernamesController");

	// Token: 0x0400086C RID: 2156
	private UIUsernames uiUsernames;
}
