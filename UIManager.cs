using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

// Token: 0x02000151 RID: 337
public class UIManager : MonoBehaviourSingleton<UIManager>
{
	// Token: 0x17000105 RID: 261
	// (get) Token: 0x06000A18 RID: 2584 RVA: 0x000105CC File Offset: 0x0000E7CC
	[HideInInspector]
	public PanelSettings PanelSettings
	{
		get
		{
			return this.UIDocument.panelSettings;
		}
	}

	// Token: 0x17000106 RID: 262
	// (get) Token: 0x06000A19 RID: 2585 RVA: 0x000105D9 File Offset: 0x0000E7D9
	[HideInInspector]
	public VisualElement RootVisualElement
	{
		get
		{
			return this.UIDocument.rootVisualElement;
		}
	}

	// Token: 0x06000A1A RID: 2586 RVA: 0x0003B880 File Offset: 0x00039A80
	public override void Awake()
	{
		base.Awake();
		this.UIDocument = base.GetComponent<UIDocument>();
		this.AudioSource = base.GetComponent<AudioSource>();
		this.RootVisualElement.style.display = (ApplicationManager.IsDedicatedGameServer ? DisplayStyle.None : DisplayStyle.Flex);
		this.RootVisualElement.RegisterCallback<PointerEnterEvent>(delegate(PointerEnterEvent e)
		{
			VisualElement visualElement = e.target as VisualElement;
			bool flag = visualElement != null && visualElement is Button && visualElement.enabledInHierarchy;
			bool flag2 = visualElement != null && visualElement.name == "unity-tab__header";
			if (flag || flag2)
			{
				this.PlaySelectSound();
			}
		}, TrickleDown.TrickleDown);
		this.RootVisualElement.RegisterCallback<PointerCaptureOutEvent>(delegate(PointerCaptureOutEvent e)
		{
			VisualElement visualElement = e.target as VisualElement;
			if (visualElement != null && visualElement is Button && visualElement.enabledInHierarchy)
			{
				this.PlayClickSound();
			}
		}, TrickleDown.TrickleDown);
		this.RootVisualElement.RegisterCallback<PointerDownEvent>(delegate(PointerDownEvent e)
		{
			VisualElement visualElement = e.target as VisualElement;
			if (visualElement != null && visualElement.name.Contains("unity-tab__header"))
			{
				this.PlayClickSound();
			}
		}, TrickleDown.TrickleDown);
		this.MainMenu = base.gameObject.GetComponent<UIMainMenu>();
		this.MainMenu.Initialize(this.RootVisualElement);
		this.views.Add(this.MainMenu);
		this.PauseMenu = base.gameObject.GetComponent<UIPauseMenu>();
		this.PauseMenu.Initialize(this.RootVisualElement);
		this.views.Add(this.PauseMenu);
		this.ServerBrowser = base.gameObject.GetComponent<UIServerBrowser>();
		this.ServerBrowser.Initialize(this.RootVisualElement);
		this.views.Add(this.ServerBrowser);
		this.GameState = base.gameObject.GetComponent<UIGameState>();
		this.GameState.Initialize(this.RootVisualElement);
		this.views.Add(this.GameState);
		this.Chat = base.gameObject.GetComponent<UIChat>();
		this.Chat.Initialize(this.RootVisualElement);
		this.views.Add(this.Chat);
		this.TeamSelect = base.gameObject.GetComponent<UITeamSelect>();
		this.TeamSelect.Initialize(this.RootVisualElement);
		this.views.Add(this.TeamSelect);
		this.PositionSelect = base.gameObject.GetComponent<UIPositionSelect>();
		this.PositionSelect.Initialize(this.RootVisualElement);
		this.views.Add(this.PositionSelect);
		this.Scoreboard = base.gameObject.GetComponent<UIScoreboard>();
		this.Scoreboard.Initialize(this.RootVisualElement);
		this.views.Add(this.Scoreboard);
		this.Settings = base.gameObject.GetComponent<UISettings>();
		this.Settings.Initialize(this.RootVisualElement);
		this.views.Add(this.Settings);
		this.Hud = base.gameObject.GetComponent<UIHUD>();
		this.Hud.Initialize(this.RootVisualElement);
		this.views.Add(this.Hud);
		this.Announcements = base.gameObject.GetComponent<UIAnnouncements>();
		this.Announcements.Initialize(this.RootVisualElement);
		this.views.Add(this.Announcements);
		this.Minimap = base.gameObject.GetComponent<UIMinimap>();
		this.Minimap.Initialize(this.RootVisualElement);
		this.views.Add(this.Minimap);
		this.NewServer = base.gameObject.GetComponent<UINewServer>();
		this.NewServer.Initialize(this.RootVisualElement);
		this.views.Add(this.NewServer);
		this.ToastManager = base.gameObject.GetComponent<UIToastManager>();
		this.ToastManager.Initialize(this.RootVisualElement);
		this.views.Add(this.ToastManager);
		this.OverlayManager = base.gameObject.GetComponent<UIOverlayManager>();
		this.OverlayManager.Initialize(this.RootVisualElement);
		this.views.Add(this.OverlayManager);
		this.PlayerMenu = base.gameObject.GetComponent<UIPlayerMenu>();
		this.PlayerMenu.Initialize(this.RootVisualElement);
		this.views.Add(this.PlayerMenu);
		this.Identity = base.gameObject.GetComponent<UIIdentity>();
		this.Identity.Initialize(this.RootVisualElement);
		this.views.Add(this.Identity);
		this.Appearance = base.gameObject.GetComponent<UIAppearance>();
		this.Appearance.Initialize(this.RootVisualElement);
		this.views.Add(this.Appearance);
		this.PopupManager = base.gameObject.GetComponent<UIPopupManager>();
		this.PopupManager.Initialize(this.RootVisualElement);
		this.views.Add(this.PopupManager);
		this.Usernames = base.gameObject.GetComponent<UIUsernames>();
		this.Usernames.Initialize(this.RootVisualElement);
		this.views.Add(this.Usernames);
		this.Debug = base.gameObject.GetComponent<UIDebug>();
		this.Debug.Initialize(this.RootVisualElement);
		this.views.Add(this.Debug);
		this.Mods = base.gameObject.GetComponent<UIMods>();
		this.Mods.Initialize(this.RootVisualElement);
		this.views.Add(this.Mods);
		this.Footer = base.gameObject.GetComponent<UIFooter>();
		this.Footer.Initialize(this.RootVisualElement);
		this.views.Add(this.Footer);
		this.Friends = base.gameObject.GetComponent<UIFriends>();
		this.Friends.Initialize(this.RootVisualElement);
		this.views.Add(this.Friends);
		this.Play = base.gameObject.GetComponent<UIPlay>();
		this.Play.Initialize(this.RootVisualElement);
		this.views.Add(this.Play);
		this.Matchmaking = base.gameObject.GetComponent<UIMatchmaking>();
		this.Matchmaking.Initialize(this.RootVisualElement);
		this.views.Add(this.Matchmaking);
		foreach (UIView uiview in this.views)
		{
			uiview.OnVisibility = (Action<UIView>)Delegate.Combine(uiview.OnVisibility, new Action<UIView>(this.OnViewVisibilityChanged));
			uiview.OnFocus = (Action<UIView>)Delegate.Combine(uiview.OnFocus, new Action<UIView>(this.OnViewFocusChanged));
		}
		InputManager.PauseAction.performed += this.OnPauseActionPerformed;
		InputManager.AllChatAction.canceled += this.OnAllChatActionPerformed;
		InputManager.TeamChatAction.canceled += this.OnTeamChatActionPerformed;
		InputManager.ScoreboardAction.started += this.OnScoreboardActionStarted;
		InputManager.ScoreboardAction.canceled += this.OnScoreboardActionCanceled;
	}

	// Token: 0x06000A1B RID: 2587 RVA: 0x0003BF30 File Offset: 0x0003A130
	private void OnDestroy()
	{
		foreach (UIView uiview in this.views)
		{
			uiview.OnVisibility = (Action<UIView>)Delegate.Remove(uiview.OnVisibility, new Action<UIView>(this.OnViewVisibilityChanged));
			uiview.OnFocus = (Action<UIView>)Delegate.Remove(uiview.OnFocus, new Action<UIView>(this.OnViewFocusChanged));
		}
		InputManager.PauseAction.performed -= this.OnPauseActionPerformed;
		InputManager.AllChatAction.canceled -= this.OnAllChatActionPerformed;
		InputManager.TeamChatAction.canceled -= this.OnTeamChatActionPerformed;
		InputManager.ScoreboardAction.started -= this.OnScoreboardActionStarted;
		InputManager.ScoreboardAction.canceled -= this.OnScoreboardActionCanceled;
	}

	// Token: 0x06000A1C RID: 2588 RVA: 0x000105E6 File Offset: 0x0000E7E6
	private void Update()
	{
		if (!ApplicationManager.IsDedicatedGameServer && GlobalStateManager.UIState.IsMouseRequired)
		{
			this.CheckMouseOverUI();
		}
	}

	// Token: 0x06000A1D RID: 2589 RVA: 0x0003C02C File Offset: 0x0003A22C
	private void HideAllViews()
	{
		foreach (UIView uiview in this.views)
		{
			uiview.Hide();
		}
	}

	// Token: 0x06000A1E RID: 2590 RVA: 0x0003C080 File Offset: 0x0003A280
	public void ShowPhaseViews(UIPhase phase)
	{
		this.HideAllViews();
		switch (phase)
		{
		case UIPhase.None:
			break;
		case UIPhase.LockerRoom:
			this.Chat.Show();
			this.MainMenu.Show();
			this.Footer.Show();
			return;
		case UIPhase.Playing:
			this.Chat.Show();
			this.GameState.Show();
			this.Announcements.Show();
			this.Usernames.Show();
			break;
		default:
			return;
		}
	}

	// Token: 0x06000A1F RID: 2591 RVA: 0x00010601 File Offset: 0x0000E801
	public void SetUIScale(float value)
	{
		this.PanelSettings.scale = value;
	}

	// Token: 0x06000A20 RID: 2592 RVA: 0x0001060F File Offset: 0x0000E80F
	public void PlaySelectSound()
	{
		if (this.selectAudioClip != null)
		{
			this.AudioSource.PlayOneShot(this.selectAudioClip);
		}
	}

	// Token: 0x06000A21 RID: 2593 RVA: 0x00010630 File Offset: 0x0000E830
	public void PlayClickSound()
	{
		if (this.clickAudioClip != null)
		{
			this.AudioSource.PlayOneShot(this.clickAudioClip);
		}
	}

	// Token: 0x06000A22 RID: 2594 RVA: 0x00010651 File Offset: 0x0000E851
	public void PlayNotificationSound()
	{
		if (this.notificationAudioClip != null)
		{
			this.AudioSource.PlayOneShot(this.notificationAudioClip);
		}
	}

	// Token: 0x06000A23 RID: 2595 RVA: 0x00010672 File Offset: 0x0000E872
	public void PlayWhooshSound()
	{
		if (this.whooshAudioClip != null)
		{
			this.AudioSource.PlayOneShot(this.whooshAudioClip);
		}
	}

	// Token: 0x06000A24 RID: 2596 RVA: 0x00010693 File Offset: 0x0000E893
	public void PlayTickSound()
	{
		if (this.tickAudioClip != null)
		{
			this.AudioSource.PlayOneShot(this.tickAudioClip);
		}
	}

	// Token: 0x06000A25 RID: 2597 RVA: 0x0003C0FC File Offset: 0x0003A2FC
	private void CheckMouseRequirement()
	{
		foreach (UIView uiview in this.views)
		{
			if ((uiview.VisibilityRequiresMouse && uiview.IsVisible) || (uiview.FocusRequiresMouse && uiview.IsFocused))
			{
				GlobalStateManager.SetUIState(new Dictionary<string, object>
				{
					{
						"isMouseRequired",
						true
					}
				});
				return;
			}
		}
		GlobalStateManager.SetUIState(new Dictionary<string, object>
		{
			{
				"isMouseRequired",
				false
			}
		});
	}

	// Token: 0x06000A26 RID: 2598 RVA: 0x0003C1A0 File Offset: 0x0003A3A0
	private void CheckInteraction()
	{
		List<UIView> list = new List<UIView>();
		foreach (UIView uiview in this.views)
		{
			if ((uiview.VisibilityIsInteractive && uiview.IsVisible) || (uiview.FocusIsInteractive && uiview.IsFocused))
			{
				list.Add(uiview);
			}
		}
		GlobalStateManager.SetUIState(new Dictionary<string, object>
		{
			{
				"interactingViews",
				list
			}
		});
	}

	// Token: 0x06000A27 RID: 2599 RVA: 0x0003C230 File Offset: 0x0003A430
	private void CheckMouseOverUI()
	{
		if (this.RootVisualElement == null)
		{
			return;
		}
		IPanel panel = this.RootVisualElement.panel;
		if (panel == null)
		{
			return;
		}
		Vector2 vector = InputManager.PointAction.ReadValue<Vector2>();
		vector.y = (float)Screen.height - vector.y;
		if (vector == this.lastPointerPosition)
		{
			return;
		}
		this.lastPointerPosition = vector;
		Vector2 point = RuntimePanelUtils.ScreenToPanel(panel, vector);
		bool flag = panel.Pick(point) != null;
		if (flag != GlobalStateManager.UIState.IsMouseOverUI)
		{
			GlobalStateManager.SetUIState(new Dictionary<string, object>
			{
				{
					"isMouseOverUI",
					flag
				}
			});
		}
	}

	// Token: 0x06000A28 RID: 2600 RVA: 0x000106B4 File Offset: 0x0000E8B4
	private void OnViewVisibilityChanged(UIView uiView)
	{
		if (uiView.VisibilityRequiresMouse)
		{
			this.CheckMouseRequirement();
		}
		if (uiView.VisibilityIsInteractive)
		{
			this.CheckInteraction();
		}
	}

	// Token: 0x06000A29 RID: 2601 RVA: 0x000106D2 File Offset: 0x0000E8D2
	private void OnViewFocusChanged(UIView uiView)
	{
		if (uiView.FocusRequiresMouse)
		{
			this.CheckMouseRequirement();
		}
		if (uiView.FocusIsInteractive)
		{
			this.CheckInteraction();
		}
	}

	// Token: 0x06000A2A RID: 2602 RVA: 0x0003C2C8 File Offset: 0x0003A4C8
	private void OnPauseActionPerformed(InputAction.CallbackContext context)
	{
		if (GlobalStateManager.UIState.Phase != UIPhase.Playing)
		{
			return;
		}
		if (this.PauseMenu.IsVisible && GlobalStateManager.UIState.IsViewTopmostInteracting<UIPauseMenu>())
		{
			this.PauseMenu.Hide();
			return;
		}
		if (!this.PauseMenu.IsVisible)
		{
			this.PauseMenu.Show();
		}
	}

	// Token: 0x06000A2B RID: 2603 RVA: 0x0003C328 File Offset: 0x0003A528
	private void OnAllChatActionPerformed(InputAction.CallbackContext context)
	{
		if (GlobalStateManager.UIState.Phase != UIPhase.Playing)
		{
			return;
		}
		if (GlobalStateManager.UIState.IsInteracting)
		{
			return;
		}
		this.Chat.StartInput(false);
	}

	// Token: 0x06000A2C RID: 2604 RVA: 0x0003C360 File Offset: 0x0003A560
	private void OnTeamChatActionPerformed(InputAction.CallbackContext context)
	{
		if (GlobalStateManager.UIState.Phase != UIPhase.Playing)
		{
			return;
		}
		if (GlobalStateManager.UIState.IsInteracting)
		{
			return;
		}
		this.Chat.StartInput(true);
	}

	// Token: 0x06000A2D RID: 2605 RVA: 0x0003C398 File Offset: 0x0003A598
	private void OnScoreboardActionStarted(InputAction.CallbackContext context)
	{
		if (GlobalStateManager.UIState.Phase != UIPhase.Playing)
		{
			return;
		}
		if (GlobalStateManager.UIState.IsInteracting)
		{
			return;
		}
		this.Scoreboard.Show();
	}

	// Token: 0x06000A2E RID: 2606 RVA: 0x000106F0 File Offset: 0x0000E8F0
	private void OnScoreboardActionCanceled(InputAction.CallbackContext context)
	{
		this.Scoreboard.Hide();
	}

	// Token: 0x040005E1 RID: 1505
	[Header("References")]
	[SerializeField]
	private AudioClip selectAudioClip;

	// Token: 0x040005E2 RID: 1506
	[SerializeField]
	private AudioClip clickAudioClip;

	// Token: 0x040005E3 RID: 1507
	[SerializeField]
	private AudioClip notificationAudioClip;

	// Token: 0x040005E4 RID: 1508
	[SerializeField]
	private AudioClip whooshAudioClip;

	// Token: 0x040005E5 RID: 1509
	[SerializeField]
	private AudioClip tickAudioClip;

	// Token: 0x040005E6 RID: 1510
	[HideInInspector]
	public UIDocument UIDocument;

	// Token: 0x040005E7 RID: 1511
	[HideInInspector]
	public AudioSource AudioSource;

	// Token: 0x040005E8 RID: 1512
	[HideInInspector]
	public UIMainMenu MainMenu;

	// Token: 0x040005E9 RID: 1513
	[HideInInspector]
	public UIPauseMenu PauseMenu;

	// Token: 0x040005EA RID: 1514
	[HideInInspector]
	public UIServerBrowser ServerBrowser;

	// Token: 0x040005EB RID: 1515
	[HideInInspector]
	public UIGameState GameState;

	// Token: 0x040005EC RID: 1516
	[HideInInspector]
	public UIChat Chat;

	// Token: 0x040005ED RID: 1517
	[HideInInspector]
	public UITeamSelect TeamSelect;

	// Token: 0x040005EE RID: 1518
	[HideInInspector]
	public UIPositionSelect PositionSelect;

	// Token: 0x040005EF RID: 1519
	[HideInInspector]
	public UIScoreboard Scoreboard;

	// Token: 0x040005F0 RID: 1520
	[HideInInspector]
	public UISettings Settings;

	// Token: 0x040005F1 RID: 1521
	[HideInInspector]
	public UIHUD Hud;

	// Token: 0x040005F2 RID: 1522
	[HideInInspector]
	public UIAnnouncements Announcements;

	// Token: 0x040005F3 RID: 1523
	[HideInInspector]
	public UIMinimap Minimap;

	// Token: 0x040005F4 RID: 1524
	[HideInInspector]
	public UINewServer NewServer;

	// Token: 0x040005F5 RID: 1525
	[HideInInspector]
	public UIToastManager ToastManager;

	// Token: 0x040005F6 RID: 1526
	[HideInInspector]
	public UIOverlayManager OverlayManager;

	// Token: 0x040005F7 RID: 1527
	[HideInInspector]
	public UIPlayerMenu PlayerMenu;

	// Token: 0x040005F8 RID: 1528
	[HideInInspector]
	public UIIdentity Identity;

	// Token: 0x040005F9 RID: 1529
	[HideInInspector]
	public UIAppearance Appearance;

	// Token: 0x040005FA RID: 1530
	[HideInInspector]
	public UIPopupManager PopupManager;

	// Token: 0x040005FB RID: 1531
	[HideInInspector]
	public UIUsernames Usernames;

	// Token: 0x040005FC RID: 1532
	[HideInInspector]
	public UIDebug Debug;

	// Token: 0x040005FD RID: 1533
	[HideInInspector]
	public UIMods Mods;

	// Token: 0x040005FE RID: 1534
	[HideInInspector]
	public UIFooter Footer;

	// Token: 0x040005FF RID: 1535
	[HideInInspector]
	public UIFriends Friends;

	// Token: 0x04000600 RID: 1536
	[HideInInspector]
	public UIPlay Play;

	// Token: 0x04000601 RID: 1537
	[HideInInspector]
	public UIMatchmaking Matchmaking;

	// Token: 0x04000602 RID: 1538
	private List<UIView> views = new List<UIView>();

	// Token: 0x04000603 RID: 1539
	private Vector2 lastPointerPosition = Vector2.zero;
}
