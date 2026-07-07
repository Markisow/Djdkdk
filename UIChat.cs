using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UIElements;

// Token: 0x02000182 RID: 386
public class UIChat : UIView
{
	// Token: 0x17000114 RID: 276
	// (get) Token: 0x06000B3A RID: 2874 RVA: 0x000112F5 File Offset: 0x0000F4F5
	// (set) Token: 0x06000B3B RID: 2875 RVA: 0x000112FD File Offset: 0x0000F4FD
	public bool IsTeamChat { get; private set; }

	// Token: 0x06000B3C RID: 2876 RVA: 0x00040584 File Offset: 0x0003E784
	public void Initialize(VisualElement rootVisualElement)
	{
		base.View = rootVisualElement.Query("ChatView", null);
		this.chat = base.View.Query("Chat", null);
		this.quickChat = base.View.Query("QuickChat", null);
		this.scrollView = this.chat.Query(null, null);
		this.messages = this.scrollView.Query("Messages", null);
		this.padding = this.scrollView.Query("Padding", null);
		this.textField = this.chat.Query(null, null);
		this.quickChatCategoryLabel = this.quickChat.Query(null, null);
		this.quickChatMessages = this.quickChat.Query("Messages", null);
		this.textField.RegisterCallback<NavigationSubmitEvent>(delegate(NavigationSubmitEvent e)
		{
			this.SubmitMessage();
		}, TrickleDown.TrickleDown);
		this.textField.RegisterCallback<NavigationCancelEvent>(delegate(NavigationCancelEvent e)
		{
			this.StopInput();
		}, TrickleDown.TrickleDown);
		this.chat.RegisterCallback<FocusOutEvent>(delegate(FocusOutEvent e)
		{
			if (UIUtils.GetVisualElementChildren(this.chat, true).Contains(e.relatedTarget))
			{
				return;
			}
			this.StopInput();
		}, TrickleDown.TrickleDown);
		this.messages.RegisterCallback<ChildAddedEvent>(delegate(ChildAddedEvent childAddedEvent)
		{
			UIChat.<>c__DisplayClass18_0 CS$<>8__locals1 = new UIChat.<>c__DisplayClass18_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.child = childAddedEvent.child;
			CS$<>8__locals1.child.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(CS$<>8__locals1.<Initialize>g__OnChildGeometryChanged|8), TrickleDown.NoTrickleDown);
		}, TrickleDown.NoTrickleDown);
		this.messages.RegisterCallback<BeforeChildRemovedEvent>(delegate(BeforeChildRemovedEvent e)
		{
			VisualElement child = e.child;
			this.padding.style.height = new StyleLength(this.padding.resolvedStyle.height + child.resolvedStyle.height);
		}, TrickleDown.NoTrickleDown);
		this.messages.RegisterCallback<HierarchyChangedEvent>(delegate(HierarchyChangedEvent e)
		{
			if (this.messages.childCount == 0)
			{
				this.padding.style.height = StyleKeyword.Initial;
			}
		}, TrickleDown.NoTrickleDown);
		this.scrollView.verticalScroller.valueChanged += delegate(float value)
		{
			this.LimitScrollToPaddingHeight();
			if (!this.isScrolling)
			{
				int num = Mathf.RoundToInt(this.scrollView.verticalScroller.highValue - this.scrollView.verticalScroller.value);
				this.autoScroll = (num <= 0);
			}
		};
		UIUtils.GetVisualElementChildren(this.chat, true).ForEach(delegate(VisualElement visualElement)
		{
			visualElement.focusable = true;
		});
		this.ClearChatMessages();
		this.StopInput();
		this.HideQuickChat();
	}

	// Token: 0x06000B3D RID: 2877 RVA: 0x00010F42 File Offset: 0x0000F142
	public override bool Show()
	{
		return SettingsManager.ShowGameUserInterface && base.Show();
	}

	// Token: 0x06000B3E RID: 2878 RVA: 0x00011306 File Offset: 0x0000F506
	public void StartInput(bool isTeamChat = false)
	{
		base.IsFocused = true;
		this.IsTeamChat = isTeamChat;
		this.ShowTextField();
		this.uiChatMessages.ForEach(delegate(UIChatMessage uiChatMessage)
		{
			uiChatMessage.Focus();
		});
	}

	// Token: 0x06000B3F RID: 2879 RVA: 0x00011346 File Offset: 0x0000F546
	public void StopInput()
	{
		base.IsFocused = false;
		this.IsTeamChat = false;
		this.HideTextField();
		this.uiChatMessages.ForEach(delegate(UIChatMessage uiChatMessage)
		{
			uiChatMessage.Blur();
		});
	}

	// Token: 0x06000B40 RID: 2880 RVA: 0x0004076C File Offset: 0x0003E96C
	private void ShowTextField()
	{
		this.textField.style.opacity = 1f;
		this.textField.pickingMode = PickingMode.Position;
		this.textField.value = string.Empty;
		this.textField.Focus();
	}

	// Token: 0x06000B41 RID: 2881 RVA: 0x000407BC File Offset: 0x0003E9BC
	private void HideTextField()
	{
		this.textField.style.opacity = 0f;
		this.textField.pickingMode = PickingMode.Ignore;
		this.textField.value = string.Empty;
		this.textField.Blur();
	}

	// Token: 0x06000B42 RID: 2882 RVA: 0x0004080C File Offset: 0x0003EA0C
	public void ShowQuickChat(QuickChatCategory category, QuickChat[] quickChats)
	{
		this.quickChatCategoryLabel.text = category.ToString().ToUpper();
		for (int i = 0; i < quickChats.Length; i++)
		{
			VisualElement visualElement = this.quickChatMessageAsset.Instantiate();
			TextElement textElement = visualElement.Query(null, null);
			string content = quickChats[i].Content;
			textElement.text = string.Format("{0}. {1}", i + 1, content);
			this.quickChatMessages.Add(visualElement);
		}
		this.quickChat.style.display = DisplayStyle.Flex;
	}

	// Token: 0x06000B43 RID: 2883 RVA: 0x00011386 File Offset: 0x0000F586
	public void HideQuickChat()
	{
		this.quickChat.style.display = DisplayStyle.None;
		this.quickChatMessages.Clear();
	}

	// Token: 0x06000B44 RID: 2884 RVA: 0x000408A4 File Offset: 0x0003EAA4
	private void SmoothScrollToVerticalPosition(float position, bool isBottomPosition = false)
	{
		Vector2 vector = new Vector2(0f, position - (isBottomPosition ? this.scrollView.contentViewport.resolvedStyle.height : 0f));
		vector = Utils.Vector2Clamp(vector, Vector2.zero, Vector2.positiveInfinity);
		Tween tween = this.smoothScrollTween;
		if (tween != null)
		{
			tween.Kill(false);
		}
		this.smoothScrollTween = DOTween.To(() => this.scrollView.scrollOffset, delegate(Vector2 x)
		{
			this.scrollView.scrollOffset = x;
		}, vector, 0.2f).OnStart(delegate
		{
			this.isScrolling = true;
		}).OnComplete(delegate
		{
			this.isScrolling = false;
		}).SetEase(Ease.Linear);
	}

	// Token: 0x06000B45 RID: 2885 RVA: 0x00040958 File Offset: 0x0003EB58
	private void LimitScrollToPaddingHeight()
	{
		if (this.scrollView.scrollOffset.y < this.padding.resolvedStyle.height)
		{
			this.scrollView.scrollOffset = new Vector2(this.scrollView.scrollOffset.x, this.padding.resolvedStyle.height);
		}
	}

	// Token: 0x06000B46 RID: 2886 RVA: 0x000409B8 File Offset: 0x0003EBB8
	private void SubmitMessage()
	{
		EventManager.TriggerEvent("Event_OnChatSubmitMessage", new Dictionary<string, object>
		{
			{
				"content",
				this.textField.value
			},
			{
				"isTeamChat",
				this.IsTeamChat
			}
		});
		this.StopInput();
	}

	// Token: 0x06000B47 RID: 2887 RVA: 0x00040A08 File Offset: 0x0003EC08
	public void AddChatMessage(ChatMessage chatMessage, Units units, bool filterProfanity)
	{
		VisualElement visualElement = this.messageAsset.Instantiate();
		visualElement.focusable = true;
		Label label = visualElement.Query(null, null);
		label.focusable = true;
		string chatMessagePrefix = this.GetChatMessagePrefix(chatMessage);
		string text = this.ParseChatContent(chatMessage.Content.ToString(), chatMessage.IsSystem, units, filterProfanity);
		label.text = chatMessagePrefix + text;
		label.style.display = ((text.Length > 0) ? DisplayStyle.Flex : DisplayStyle.None);
		UIChatMessage uichatMessage = new UIChatMessage(chatMessage, visualElement, 5f);
		this.messages.Add(uichatMessage.VisualElement);
		this.uiChatMessages.Add(uichatMessage);
	}

	// Token: 0x06000B48 RID: 2888 RVA: 0x00040AB8 File Offset: 0x0003ECB8
	public void RemoveChatMessage(ChatMessage chatMessage)
	{
		UIChatMessage uichatMessage = this.uiChatMessages.FirstOrDefault((UIChatMessage m) => m.ChatMessage == chatMessage);
		if (uichatMessage == null)
		{
			return;
		}
		uichatMessage.Dispose();
		this.messages.Remove(uichatMessage.VisualElement);
		this.uiChatMessages.Remove(uichatMessage);
	}

	// Token: 0x06000B49 RID: 2889 RVA: 0x00040B14 File Offset: 0x0003ED14
	public void ClearChatMessages()
	{
		this.uiChatMessages.ForEach(delegate(UIChatMessage uiChatMessage)
		{
			uiChatMessage.Dispose();
		});
		this.messages.Clear();
		this.uiChatMessages.Clear();
		Tween tween = this.smoothScrollTween;
		if (tween != null)
		{
			tween.Kill(false);
		}
		this.autoScroll = true;
		this.isScrolling = false;
	}

	// Token: 0x06000B4A RID: 2890 RVA: 0x000113A9 File Offset: 0x0000F5A9
	public void SetOpacity(float opacity)
	{
		this.chat.style.opacity = new StyleFloat(opacity);
	}

	// Token: 0x06000B4B RID: 2891 RVA: 0x000113C1 File Offset: 0x0000F5C1
	public void SetScale(float scale)
	{
		this.chat.style.scale = new StyleScale(new Scale(new Vector2(scale, scale)));
	}

	// Token: 0x06000B4C RID: 2892 RVA: 0x00040B84 File Offset: 0x0003ED84
	private string GetChatMessagePrefix(ChatMessage chatMessage)
	{
		string text = string.Empty;
		if (!chatMessage.IsSystem)
		{
			if (chatMessage.IsTeamChat)
			{
				text += "[TEAM] ";
			}
			string str = StringUtils.WrapInTeamColor(chatMessage.Username.ToString(), chatMessage.Team.Value);
			text = text + str + ": ";
		}
		return text;
	}

	// Token: 0x06000B4D RID: 2893 RVA: 0x00040BE4 File Offset: 0x0003EDE4
	private string ParseChatContent(string content, bool isSystem, Units units, bool filterProfanity)
	{
		if (isSystem)
		{
			content = Regex.Replace(content, "<united>([^<]+)</united>", delegate(Match match)
			{
				string value = match.Groups[1].Value;
				float value2;
				if (float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out value2))
				{
					return ((units == Units.Metric) ? Utils.GameUnitsToMetric(value2) : Utils.GameUnitsToImperial(value2)).ToString("F1", CultureInfo.InvariantCulture);
				}
				return value;
			});
			content = Regex.Replace(content, "&units", (units == Units.Metric) ? "KPH" : "MPH");
		}
		else
		{
			content = StringUtils.FilterStringRichText(content);
			content = StringUtils.FilterStringSpecialCharacters(content, Constants.CHAT_WHITELIST, filterProfanity ? Constants.CHAT_BLACKLIST : null);
			if (filterProfanity)
			{
				content = StringUtils.FilterStringProfanity(content, true);
			}
		}
		return content;
	}

	// Token: 0x040006C7 RID: 1735
	[Header("References")]
	[SerializeField]
	private VisualTreeAsset messageAsset;

	// Token: 0x040006C8 RID: 1736
	[SerializeField]
	private VisualTreeAsset quickChatMessageAsset;

	// Token: 0x040006CA RID: 1738
	private VisualElement chat;

	// Token: 0x040006CB RID: 1739
	private VisualElement quickChat;

	// Token: 0x040006CC RID: 1740
	private ScrollView scrollView;

	// Token: 0x040006CD RID: 1741
	private VisualElement padding;

	// Token: 0x040006CE RID: 1742
	private VisualElement messages;

	// Token: 0x040006CF RID: 1743
	private TextField textField;

	// Token: 0x040006D0 RID: 1744
	private Label quickChatCategoryLabel;

	// Token: 0x040006D1 RID: 1745
	private VisualElement quickChatMessages;

	// Token: 0x040006D2 RID: 1746
	private List<UIChatMessage> uiChatMessages = new List<UIChatMessage>();

	// Token: 0x040006D3 RID: 1747
	private bool autoScroll = true;

	// Token: 0x040006D4 RID: 1748
	private bool isScrolling;

	// Token: 0x040006D5 RID: 1749
	private Tween smoothScrollTween;
}
