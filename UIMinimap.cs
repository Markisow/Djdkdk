using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

// Token: 0x0200019B RID: 411
public class UIMinimap : UIView
{
	// Token: 0x17000115 RID: 277
	// (get) Token: 0x06000C01 RID: 3073 RVA: 0x00042950 File Offset: 0x00040B50
	[HideInInspector]
	public Vector2 Position
	{
		get
		{
			return new Vector2(this.minimap.style.left.value.value, this.minimap.style.top.value.value);
		}
	}

	// Token: 0x06000C02 RID: 3074 RVA: 0x000429A4 File Offset: 0x00040BA4
	private void Update()
	{
		if (ApplicationManager.IsDedicatedGameServer)
		{
			return;
		}
		this.updateAccumulator += Time.deltaTime;
		if (this.updateAccumulator < 1f / (float)this.updateRate)
		{
			return;
		}
		this.updateAccumulator = 0f;
		foreach (KeyValuePair<PlayerBody, VisualElement> keyValuePair in this.playerBodyVisualElementMap)
		{
			PlayerBody key = keyValuePair.Key;
			VisualElement value = keyValuePair.Value;
			if (key)
			{
				VisualElement visualElement = value.Query("Body", null);
				Vector3 position = (this.Team == PlayerTeam.Blue) ? key.transform.position : (-key.transform.position);
				float value2 = (this.Team == PlayerTeam.Blue) ? key.transform.rotation.eulerAngles.y : (key.transform.rotation.eulerAngles.y + 180f);
				Vector2 vector = this.WorldPositionToMinimapPosition(position, this.Bounds);
				value.style.translate = new Translate(-vector.x, vector.y);
				visualElement.style.rotate = new Rotate(value2);
			}
		}
		foreach (KeyValuePair<Puck, VisualElement> keyValuePair2 in this.puckVisualElementMap)
		{
			Puck key2 = keyValuePair2.Key;
			VisualElement value3 = keyValuePair2.Value;
			if (key2)
			{
				Vector3 position2 = (this.Team == PlayerTeam.Blue) ? key2.transform.position : (-key2.transform.position);
				float value4 = (this.Team == PlayerTeam.Blue) ? key2.transform.rotation.eulerAngles.y : (key2.transform.rotation.eulerAngles.y + 180f);
				Vector2 vector2 = this.WorldPositionToMinimapPosition(position2, this.Bounds);
				value3.style.translate = new Translate(-vector2.x, vector2.y);
				value3.style.rotate = new Rotate(value4);
			}
		}
	}

	// Token: 0x06000C03 RID: 3075 RVA: 0x00042C6C File Offset: 0x00040E6C
	public void Initialize(VisualElement rootVisualElement)
	{
		base.View = rootVisualElement.Query("MinimapView", null);
		this.minimap = base.View.Query("Minimap", null);
		this.background = this.minimap.Query("Background", null);
		this.foreground = this.minimap.Query("Foreground", null);
		this.content = this.minimap.Query("Content", null);
		this.content.Clear();
	}

	// Token: 0x06000C04 RID: 3076 RVA: 0x00010F42 File Offset: 0x0000F142
	public override bool Show()
	{
		return SettingsManager.ShowGameUserInterface && base.Show();
	}

	// Token: 0x06000C05 RID: 3077 RVA: 0x00042D0C File Offset: 0x00040F0C
	public void AddPlayerBody(PlayerBody playerBody)
	{
		if (!playerBody)
		{
			return;
		}
		if (this.playerBodyVisualElementMap.ContainsKey(playerBody))
		{
			return;
		}
		TemplateContainer templateContainer = this.playerAsset.Instantiate();
		this.playerBodyVisualElementMap.Add(playerBody, templateContainer);
		this.content.Add(templateContainer);
		templateContainer.SendToBack();
		this.StylePlayer(playerBody);
	}

	// Token: 0x06000C06 RID: 3078 RVA: 0x00042D64 File Offset: 0x00040F64
	public void StylePlayer(PlayerBody playerBody)
	{
		if (!playerBody)
		{
			return;
		}
		if (!this.playerBodyVisualElementMap.ContainsKey(playerBody))
		{
			return;
		}
		Player player = playerBody.Player;
		if (!player)
		{
			return;
		}
		VisualElement visualElement = this.playerBodyVisualElementMap[playerBody].Query("Player", null);
		Label label = visualElement.Query("NumberLabel", null);
		UIUtils.SetTeamClass(visualElement, player.Team);
		visualElement.EnableInClassList("isLocalPlayer", player.IsLocalPlayer);
		label.text = player.Number.Value.ToString();
	}

	// Token: 0x06000C07 RID: 3079 RVA: 0x00011E02 File Offset: 0x00010002
	public void RemovePlayerBody(PlayerBody playerBody)
	{
		if (!playerBody)
		{
			return;
		}
		if (!this.playerBodyVisualElementMap.ContainsKey(playerBody))
		{
			return;
		}
		this.content.Remove(this.playerBodyVisualElementMap[playerBody]);
		this.playerBodyVisualElementMap.Remove(playerBody);
	}

	// Token: 0x06000C08 RID: 3080 RVA: 0x00042DFC File Offset: 0x00040FFC
	public void AddPuck(Puck puck)
	{
		if (!puck)
		{
			return;
		}
		if (this.puckVisualElementMap.ContainsKey(puck))
		{
			return;
		}
		TemplateContainer templateContainer = this.puckAsset.Instantiate();
		this.puckVisualElementMap.Add(puck, templateContainer);
		this.content.Add(templateContainer);
		templateContainer.BringToFront();
	}

	// Token: 0x06000C09 RID: 3081 RVA: 0x00011E40 File Offset: 0x00010040
	public void RemovePuck(Puck puck)
	{
		if (!puck)
		{
			return;
		}
		if (!this.puckVisualElementMap.ContainsKey(puck))
		{
			return;
		}
		this.content.Remove(this.puckVisualElementMap[puck]);
		this.puckVisualElementMap.Remove(puck);
	}

	// Token: 0x06000C0A RID: 3082 RVA: 0x00042E4C File Offset: 0x0004104C
	private Vector2 WorldPositionToMinimapPosition(Vector3 position, Bounds bounds)
	{
		Vector2 vector = new Vector2((position.x + bounds.center.x) / bounds.size.x, (position.z + bounds.center.z) / bounds.size.z);
		Vector2 vector2 = new Vector2(this.content.resolvedStyle.width, this.content.resolvedStyle.height);
		return new Vector2(vector2.x * vector.x, vector2.y * vector.y);
	}

	// Token: 0x06000C0B RID: 3083 RVA: 0x00011E7E File Offset: 0x0001007E
	public void SetOpacity(float opacity)
	{
		if (this.minimap == null)
		{
			return;
		}
		this.minimap.style.opacity = opacity;
	}

	// Token: 0x06000C0C RID: 3084 RVA: 0x00042EE8 File Offset: 0x000410E8
	public void SetPosition(Vector2 position)
	{
		if (this.minimap == null)
		{
			return;
		}
		Length x = new Length(Utils.Map(position.x, 0f, 100f, 0f, -100f), LengthUnit.Percent);
		Length y = new Length(Utils.Map(position.y, 0f, 100f, 0f, -100f), LengthUnit.Percent);
		Length x2 = new Length(-x.value, LengthUnit.Percent);
		Length y2 = new Length(-y.value, LengthUnit.Percent);
		this.minimap.style.left = new Length(position.x, LengthUnit.Percent);
		this.minimap.style.top = new Length(position.y, LengthUnit.Percent);
		this.minimap.style.translate = new Translate(x, y);
		this.minimap.style.transformOrigin = new TransformOrigin(x2, y2);
	}

	// Token: 0x06000C0D RID: 3085 RVA: 0x00011E9F File Offset: 0x0001009F
	public void SetBackgroundOpacity(float opacity)
	{
		if (this.background == null)
		{
			return;
		}
		this.background.style.opacity = opacity;
	}

	// Token: 0x06000C0E RID: 3086 RVA: 0x00011EC0 File Offset: 0x000100C0
	public void SetScale(float scale)
	{
		if (this.minimap == null)
		{
			return;
		}
		this.minimap.style.scale = new Vector2(scale, scale);
	}

	// Token: 0x0400072E RID: 1838
	[Header("Settings")]
	[SerializeField]
	private int updateRate = 30;

	// Token: 0x0400072F RID: 1839
	[Header("References")]
	[SerializeField]
	private VisualTreeAsset playerAsset;

	// Token: 0x04000730 RID: 1840
	[SerializeField]
	private VisualTreeAsset puckAsset;

	// Token: 0x04000731 RID: 1841
	[HideInInspector]
	public PlayerTeam Team;

	// Token: 0x04000732 RID: 1842
	[HideInInspector]
	public Bounds Bounds;

	// Token: 0x04000733 RID: 1843
	private VisualElement minimap;

	// Token: 0x04000734 RID: 1844
	private VisualElement background;

	// Token: 0x04000735 RID: 1845
	private VisualElement foreground;

	// Token: 0x04000736 RID: 1846
	private VisualElement content;

	// Token: 0x04000737 RID: 1847
	private Dictionary<PlayerBody, VisualElement> playerBodyVisualElementMap = new Dictionary<PlayerBody, VisualElement>();

	// Token: 0x04000738 RID: 1848
	private Dictionary<Puck, VisualElement> puckVisualElementMap = new Dictionary<Puck, VisualElement>();

	// Token: 0x04000739 RID: 1849
	private float updateAccumulator;
}
