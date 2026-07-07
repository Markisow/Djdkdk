using System;
using System.Collections.Generic;

// Token: 0x02000014 RID: 20
public class LockerRoomCameraController : BaseCameraController
{
	// Token: 0x06000059 RID: 89 RVA: 0x00016060 File Offset: 0x00014260
	public override void Awake()
	{
		base.Awake();
		this.lockerRoomCamera = base.GetComponent<LockerRoomCamera>();
		EventManager.AddEventListener("Event_OnMainMenuShow", new Action<Dictionary<string, object>>(this.Event_OnMainMenuShow));
		EventManager.AddEventListener("Event_OnPlayerMenuShow", new Action<Dictionary<string, object>>(this.Event_OnPlayerMenuShow));
		EventManager.AddEventListener("Event_OnAppearanceShow", new Action<Dictionary<string, object>>(this.Event_OnAppearanceShow));
		EventManager.AddEventListener("Event_OnAppearanceCategoryChanged", new Action<Dictionary<string, object>>(this.Event_OnAppearanceCategoryChanged));
	}

	// Token: 0x0600005A RID: 90 RVA: 0x000160D8 File Offset: 0x000142D8
	public override void OnDestroy()
	{
		EventManager.RemoveEventListener("Event_OnMainMenuShow", new Action<Dictionary<string, object>>(this.Event_OnMainMenuShow));
		EventManager.RemoveEventListener("Event_OnPlayerMenuShow", new Action<Dictionary<string, object>>(this.Event_OnPlayerMenuShow));
		EventManager.RemoveEventListener("Event_OnAppearanceShow", new Action<Dictionary<string, object>>(this.Event_OnAppearanceShow));
		EventManager.RemoveEventListener("Event_OnAppearanceCategoryChanged", new Action<Dictionary<string, object>>(this.Event_OnAppearanceCategoryChanged));
		base.OnDestroy();
	}

	// Token: 0x0600005B RID: 91 RVA: 0x00008D98 File Offset: 0x00006F98
	private void SetAppearancePosition(AppearanceCategory category, AppearanceSubcategory subcategory)
	{
		if (category == AppearanceCategory.Head)
		{
			this.lockerRoomCamera.SetPosition("headCloseUp");
			return;
		}
		if (category != AppearanceCategory.Stick)
		{
			this.lockerRoomCamera.SetPosition("bodyCloseUp");
			return;
		}
		this.lockerRoomCamera.SetPosition("stickCloseUp");
	}

	// Token: 0x0600005C RID: 92 RVA: 0x00008DD5 File Offset: 0x00006FD5
	private void Event_OnMainMenuShow(Dictionary<string, object> message)
	{
		this.lockerRoomCamera.SetPosition("default");
	}

	// Token: 0x0600005D RID: 93 RVA: 0x00008DE7 File Offset: 0x00006FE7
	private void Event_OnPlayerMenuShow(Dictionary<string, object> message)
	{
		this.lockerRoomCamera.SetPosition("bodyCloseUp");
	}

	// Token: 0x0600005E RID: 94 RVA: 0x00016144 File Offset: 0x00014344
	private void Event_OnAppearanceShow(Dictionary<string, object> message)
	{
		AppearanceCategory category = (AppearanceCategory)message["category"];
		AppearanceSubcategory subcategory = (AppearanceSubcategory)message["subcategory"];
		this.SetAppearancePosition(category, subcategory);
	}

	// Token: 0x0600005F RID: 95 RVA: 0x00016144 File Offset: 0x00014344
	private void Event_OnAppearanceCategoryChanged(Dictionary<string, object> message)
	{
		AppearanceCategory category = (AppearanceCategory)message["category"];
		AppearanceSubcategory subcategory = (AppearanceSubcategory)message["subcategory"];
		this.SetAppearancePosition(category, subcategory);
	}

	// Token: 0x0400002E RID: 46
	private LockerRoomCamera lockerRoomCamera;
}
