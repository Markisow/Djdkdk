using System;
using System.Collections.Generic;

// Token: 0x02000169 RID: 361
internal class UIAnnouncementsController : UIViewController<UIAnnouncements>
{
	// Token: 0x06000AC7 RID: 2759 RVA: 0x00010F63 File Offset: 0x0000F163
	public override void Awake()
	{
		base.Awake();
		this.uiAnnouncements = base.GetComponent<UIAnnouncements>();
		EventManager.AddEventListener("Event_Everyone_OnGoalScored", new Action<Dictionary<string, object>>(this.Event_Everyone_OnGoalScored));
	}

	// Token: 0x06000AC8 RID: 2760 RVA: 0x00010F8D File Offset: 0x0000F18D
	public override void OnDestroy()
	{
		EventManager.RemoveEventListener("Event_Everyone_OnGoalScored", new Action<Dictionary<string, object>>(this.Event_Everyone_OnGoalScored));
		base.OnDestroy();
	}

	// Token: 0x06000AC9 RID: 2761 RVA: 0x0003E104 File Offset: 0x0003C304
	public void Event_Everyone_OnGoalScored(Dictionary<string, object> message)
	{
		PlayerTeam team = (PlayerTeam)message["byTeam"];
		Player goalPlayer = (Player)message["goalPlayer"];
		Player assistPlayer = (Player)message["assistPlayer"];
		Player secondAssistPlayer = (Player)message["secondAssistPlayer"];
		this.uiAnnouncements.ShowScore(team, goalPlayer, assistPlayer, secondAssistPlayer);
	}

	// Token: 0x04000654 RID: 1620
	private UIAnnouncements uiAnnouncements;
}
