using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

// Token: 0x020001BC RID: 444
public class UIPositionSelect : UIView
{
	// Token: 0x1700011B RID: 283
	// (get) Token: 0x06000D0A RID: 3338 RVA: 0x00012C18 File Offset: 0x00010E18
	// (set) Token: 0x06000D0B RID: 3339 RVA: 0x00046528 File Offset: 0x00044728
	public PlayerTeam Team
	{
		get
		{
			return this.team;
		}
		set
		{
			if (this.team == value)
			{
				return;
			}
			PlayerTeam oldTeam = this.team;
			this.team = value;
			this.OnTeamChanged(oldTeam, this.team);
		}
	}

	// Token: 0x06000D0C RID: 3340 RVA: 0x00012C20 File Offset: 0x00010E20
	public void Initialize(VisualElement rootVisualElement)
	{
		this.RootVisualElement = rootVisualElement;
		base.View = rootVisualElement.Query("PositionsView", null);
		this.positions = base.View.Query("Positions", null);
	}

	// Token: 0x06000D0D RID: 3341 RVA: 0x0004655C File Offset: 0x0004475C
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
		foreach (KeyValuePair<PlayerPosition, VisualElement> keyValuePair in this.playerPositionVisualElementMap)
		{
			PlayerPosition key = keyValuePair.Key;
			VisualElement value = keyValuePair.Value;
			if (!(key == null))
			{
				this.PositionWorldToScreen(value, key);
			}
		}
	}

	// Token: 0x06000D0E RID: 3342 RVA: 0x00046604 File Offset: 0x00044804
	public void AddPosition(PlayerPosition playerPosition)
	{
		if (this.playerPositionVisualElementMap.ContainsKey(playerPosition))
		{
			return;
		}
		VisualElement visualElement = this.positionAsset.Instantiate();
		visualElement.Query(null, null).RegisterCallback<ClickEvent>(delegate(ClickEvent e)
		{
			this.OnPositionClicked(playerPosition);
		}, TrickleDown.NoTrickleDown);
		this.positions.Add(visualElement);
		this.playerPositionVisualElementMap.Add(playerPosition, visualElement);
		this.StylePosition(playerPosition);
	}

	// Token: 0x06000D0F RID: 3343 RVA: 0x00046690 File Offset: 0x00044890
	public void StylePosition(PlayerPosition playerPosition)
	{
		if (!this.playerPositionVisualElementMap.ContainsKey(playerPosition))
		{
			return;
		}
		VisualElement visualElement = this.playerPositionVisualElementMap[playerPosition];
		VisualElement visualElement2 = visualElement.Query("Position", null);
		Button button = visualElement.Query(null, null);
		Label label = visualElement.Query("UsernameLabel", null);
		UIUtils.SetTeamClass(visualElement2, playerPosition.Team);
		visualElement2.EnableInClassList("claimed", playerPosition.IsClaimed);
		button.text = playerPosition.Name.ToString();
		if (playerPosition.IsClaimed)
		{
			label.text = playerPosition.ClaimedByPlayer.Username.Value.ToString();
		}
		else
		{
			label.text = null;
		}
		visualElement.style.display = ((this.Team == playerPosition.Team) ? DisplayStyle.Flex : DisplayStyle.None);
	}

	// Token: 0x06000D10 RID: 3344 RVA: 0x00012C5C File Offset: 0x00010E5C
	public void RemovePosition(PlayerPosition playerPosition)
	{
		if (!this.playerPositionVisualElementMap.ContainsKey(playerPosition))
		{
			return;
		}
		this.positions.Remove(this.playerPositionVisualElementMap[playerPosition]);
		this.playerPositionVisualElementMap.Remove(playerPosition);
	}

	// Token: 0x06000D11 RID: 3345 RVA: 0x00046770 File Offset: 0x00044970
	private void PositionWorldToScreen(VisualElement positionVisualElement, PlayerPosition playerPosition)
	{
		if (Camera.main == null)
		{
			return;
		}
		Vector3 vector = Camera.main.WorldToScreenPoint(playerPosition.transform.position);
		vector.y = (float)Screen.height - vector.y;
		RuntimePanelUtils.ScreenToPanel(this.RootVisualElement.panel, vector);
		Vector2 vector2 = RuntimePanelUtils.ScreenToPanel(this.RootVisualElement.panel, vector);
		if (vector.z < 0f)
		{
			positionVisualElement.style.visibility = Visibility.Hidden;
			return;
		}
		positionVisualElement.style.visibility = Visibility.Visible;
		positionVisualElement.style.left = vector2.x;
		positionVisualElement.style.top = vector2.y;
	}

	// Token: 0x06000D12 RID: 3346 RVA: 0x00012C91 File Offset: 0x00010E91
	private void OnPositionClicked(PlayerPosition playerPosition)
	{
		EventManager.TriggerEvent("Event_OnPositionSelectClickPosition", new Dictionary<string, object>
		{
			{
				"playerPosition",
				playerPosition
			}
		});
	}

	// Token: 0x06000D13 RID: 3347 RVA: 0x00046844 File Offset: 0x00044A44
	private void OnTeamChanged(PlayerTeam oldTeam, PlayerTeam newTeam)
	{
		foreach (PlayerPosition playerPosition in this.playerPositionVisualElementMap.Keys)
		{
			this.StylePosition(playerPosition);
		}
	}

	// Token: 0x040007D1 RID: 2001
	[Header("Settings")]
	[SerializeField]
	private int updateRate = 30;

	// Token: 0x040007D2 RID: 2002
	[Header("References")]
	[SerializeField]
	private VisualTreeAsset positionAsset;

	// Token: 0x040007D3 RID: 2003
	private PlayerTeam team;

	// Token: 0x040007D4 RID: 2004
	private Dictionary<PlayerPosition, VisualElement> playerPositionVisualElementMap = new Dictionary<PlayerPosition, VisualElement>();

	// Token: 0x040007D5 RID: 2005
	private float updateAccumulator;

	// Token: 0x040007D6 RID: 2006
	private VisualElement positions;
}
