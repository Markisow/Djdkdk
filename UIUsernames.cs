using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

// Token: 0x020001D8 RID: 472
public class UIUsernames : UIView
{
	// Token: 0x1700011F RID: 287
	// (get) Token: 0x06000E35 RID: 3637 RVA: 0x00013B92 File Offset: 0x00011D92
	[HideInInspector]
	public float MaximumDistance
	{
		get
		{
			return Mathf.Max(this.Bounds.size.x, this.Bounds.size.z);
		}
	}

	// Token: 0x17000120 RID: 288
	// (get) Token: 0x06000E36 RID: 3638 RVA: 0x00013BB9 File Offset: 0x00011DB9
	[HideInInspector]
	public float FadeRange
	{
		get
		{
			return this.MaximumDistance / 4f;
		}
	}

	// Token: 0x06000E37 RID: 3639 RVA: 0x0004B3F8 File Offset: 0x000495F8
	public void Initialize(VisualElement rootVisualElement)
	{
		this.RootVisualElement = rootVisualElement;
		base.View = rootVisualElement.Query("UsernamesView", null);
		this.usernames = base.View.Query("Usernames", null);
		this.usernames.Clear();
	}

	// Token: 0x06000E38 RID: 3640 RVA: 0x0004B44C File Offset: 0x0004964C
	private void Update()
	{
		if (ApplicationManager.IsDedicatedGameServer)
		{
			return;
		}
		foreach (KeyValuePair<PlayerBody, VisualElement> keyValuePair in this.playerBodyVisualElementMap)
		{
			PlayerBody key = keyValuePair.Key;
			VisualElement value = keyValuePair.Value;
			if (!(key == null))
			{
				this.UsernameWorldToScreen(value, key);
			}
		}
	}

	// Token: 0x06000E39 RID: 3641 RVA: 0x00013BC7 File Offset: 0x00011DC7
	public override bool Show()
	{
		return SettingsManager.ShowPlayerUsernames && base.Show();
	}

	// Token: 0x06000E3A RID: 3642 RVA: 0x0004B4C4 File Offset: 0x000496C4
	public void AddPlayerBody(PlayerBody playerBody)
	{
		TemplateContainer templateContainer = this.playerUsernameAsset.Instantiate();
		this.playerBodyVisualElementMap.Add(playerBody, templateContainer);
		this.StyleUsername(playerBody);
		this.usernames.Add(templateContainer);
	}

	// Token: 0x06000E3B RID: 3643 RVA: 0x0004B500 File Offset: 0x00049700
	public void RemovePlayerBody(PlayerBody playerBody)
	{
		if (!this.playerBodyVisualElementMap.ContainsKey(playerBody))
		{
			return;
		}
		VisualElement element = this.playerBodyVisualElementMap[playerBody];
		this.playerBodyVisualElementMap.Remove(playerBody);
		this.usernames.Remove(element);
	}

	// Token: 0x06000E3C RID: 3644 RVA: 0x0004B544 File Offset: 0x00049744
	public void StyleUsername(PlayerBody playerBody)
	{
		if (!this.playerBodyVisualElementMap.ContainsKey(playerBody))
		{
			return;
		}
		this.playerBodyVisualElementMap[playerBody].Query("UsernameLabel", null).text = string.Format("#{0} {1}", playerBody.Player.Number.Value, playerBody.Player.Username.Value);
	}

	// Token: 0x06000E3D RID: 3645 RVA: 0x0004B5B8 File Offset: 0x000497B8
	private void UsernameWorldToScreen(VisualElement playerVisualElement, PlayerBody playerBody)
	{
		if (Camera.main == null)
		{
			return;
		}
		Vector3 position = Camera.main.transform.position;
		Vector3 position2 = playerBody.transform.position;
		float value = Vector3.Distance(position, position2);
		Vector3 vector = Camera.main.WorldToScreenPoint(position2 + Vector3.up * this.yOffset);
		vector.y = (float)Screen.height - vector.y;
		RuntimePanelUtils.ScreenToPanel(this.RootVisualElement.panel, vector);
		Vector2 vector2 = RuntimePanelUtils.ScreenToPanel(this.RootVisualElement.panel, vector);
		if (vector.z < 0f)
		{
			playerVisualElement.style.display = DisplayStyle.None;
			return;
		}
		float num = Utils.Map(value, this.MaximumDistance * this.FadeThreshold, this.MaximumDistance * this.FadeThreshold + this.FadeRange, 1f, 0f);
		num = Mathf.Clamp01(num);
		playerVisualElement.style.display = DisplayStyle.Flex;
		playerVisualElement.style.left = vector2.x;
		playerVisualElement.style.top = vector2.y;
		playerVisualElement.style.opacity = new StyleFloat(num);
	}

	// Token: 0x04000864 RID: 2148
	private static readonly global::Logger Logger = new global::Logger("UIUsernames");

	// Token: 0x04000865 RID: 2149
	[Header("Settings")]
	[SerializeField]
	private float yOffset = 2.5f;

	// Token: 0x04000866 RID: 2150
	[Header("References")]
	[SerializeField]
	private VisualTreeAsset playerUsernameAsset;

	// Token: 0x04000867 RID: 2151
	[HideInInspector]
	public float FadeThreshold = 0.5f;

	// Token: 0x04000868 RID: 2152
	[HideInInspector]
	public Bounds Bounds;

	// Token: 0x04000869 RID: 2153
	private Dictionary<PlayerBody, VisualElement> playerBodyVisualElementMap = new Dictionary<PlayerBody, VisualElement>();

	// Token: 0x0400086A RID: 2154
	private VisualElement usernames;
}
