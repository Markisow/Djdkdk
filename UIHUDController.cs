using System;
using System.Collections.Generic;

// Token: 0x02000194 RID: 404
public class UIHUDController : UIViewController<UIHUD>
{
	// Token: 0x06000BBC RID: 3004 RVA: 0x00041CA0 File Offset: 0x0003FEA0
	public override void Awake()
	{
		base.Awake();
		this.uiHud = base.GetComponent<UIHUD>();
		EventManager.AddEventListener("Event_Everyone_OnPlayerBodySpawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerBodySpawned));
		EventManager.AddEventListener("Event_Everyone_OnPlayerBodyStaminaChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerBodyStaminaChanged));
		EventManager.AddEventListener("Event_Everyone_OnPlayerBodySpeedChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerBodySpeedChanged));
		EventManager.AddEventListener("Event_OnUnitsChanged", new Action<Dictionary<string, object>>(this.Event_OnUnitsChanged));
	}

	// Token: 0x06000BBD RID: 3005 RVA: 0x00041D18 File Offset: 0x0003FF18
	public override void OnDestroy()
	{
		EventManager.RemoveEventListener("Event_Everyone_OnPlayerBodySpawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerBodySpawned));
		EventManager.RemoveEventListener("Event_Everyone_OnPlayerBodyStaminaChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerBodyStaminaChanged));
		EventManager.RemoveEventListener("Event_Everyone_OnPlayerBodySpeedChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerBodySpeedChanged));
		EventManager.RemoveEventListener("Event_OnUnitsChanged", new Action<Dictionary<string, object>>(this.Event_OnUnitsChanged));
		base.OnDestroy();
	}

	// Token: 0x06000BBE RID: 3006 RVA: 0x00041D84 File Offset: 0x0003FF84
	private void Event_Everyone_OnPlayerBodySpawned(Dictionary<string, object> message)
	{
		PlayerBody playerBody = (PlayerBody)message["playerBody"];
		if (!playerBody.Player.IsLocalPlayer)
		{
			return;
		}
		this.uiHud.Show();
		this.uiHud.SetStamina(playerBody.Stamina.Value);
	}

	// Token: 0x06000BBF RID: 3007 RVA: 0x00041DD4 File Offset: 0x0003FFD4
	private void Event_Everyone_OnPlayerBodyStaminaChanged(Dictionary<string, object> message)
	{
		PlayerBody playerBody = (PlayerBody)message["playerBody"];
		float stamina = (float)message["newStamina"];
		if (!playerBody.Player.IsLocalPlayer)
		{
			return;
		}
		this.uiHud.SetStamina(stamina);
	}

	// Token: 0x06000BC0 RID: 3008 RVA: 0x00041E1C File Offset: 0x0004001C
	private void Event_Everyone_OnPlayerBodySpeedChanged(Dictionary<string, object> message)
	{
		PlayerBody playerBody = (PlayerBody)message["playerBody"];
		float speed = (float)message["newSpeed"];
		if (!playerBody.Player.IsLocalPlayer)
		{
			return;
		}
		this.uiHud.SetSpeed(speed);
	}

	// Token: 0x06000BC1 RID: 3009 RVA: 0x00041E64 File Offset: 0x00040064
	private void Event_OnUnitsChanged(Dictionary<string, object> message)
	{
		Units units = (Units)message["value"];
		if (units == Units.Metric)
		{
			this.uiHud.SetUnits("KPH");
			return;
		}
		if (units != Units.Imperial)
		{
			return;
		}
		this.uiHud.SetUnits("MPH");
	}

	// Token: 0x0400070B RID: 1803
	private UIHUD uiHud;
}
