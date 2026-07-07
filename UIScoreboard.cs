using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

// Token: 0x020001BF RID: 447
public class UIScoreboard : UIView
{
	// Token: 0x06000D20 RID: 3360 RVA: 0x00046AF0 File Offset: 0x00044CF0
	public void Initialize(VisualElement rootVisualElement)
	{
		base.View = rootVisualElement.Query("ScoreboardView", null);
		this.scoreboard = base.View.Query("Scoreboard", null);
		this.header = this.scoreboard.Query("Header", null);
		this.players = this.scoreboard.Query("Players", null);
		this.nameLabel = this.header.Query("NameLabel", null);
		this.playersLabel = this.header.Query("PlayersLabel", null);
		this.players.Clear();
	}

	// Token: 0x06000D21 RID: 3361 RVA: 0x00046BAC File Offset: 0x00044DAC
	public void AddPlayer(Player player)
	{
		if (this.playerVisualElementMap.ContainsKey(player))
		{
			return;
		}
		VisualElement visualElement = this.playerAsset.Instantiate();
		this.players.Add(visualElement);
		visualElement.Query(null, null).RegisterCallback<ClickEvent, Player>(new EventCallback<ClickEvent, Player>(this.OnPlayerClicked), player, TrickleDown.NoTrickleDown);
		this.playerVisualElementMap.Add(player, visualElement);
		this.StylePlayer(player);
	}

	// Token: 0x06000D22 RID: 3362 RVA: 0x00046C14 File Offset: 0x00044E14
	public void RemovePlayer(Player player)
	{
		if (!this.playerVisualElementMap.ContainsKey(player))
		{
			return;
		}
		this.playerVisualElementMap[player].Query(null, null).UnregisterCallback<ClickEvent, Player>(new EventCallback<ClickEvent, Player>(this.OnPlayerClicked), TrickleDown.NoTrickleDown);
		this.players.Remove(this.playerVisualElementMap[player]);
		this.playerVisualElementMap.Remove(player);
	}

	// Token: 0x06000D23 RID: 3363 RVA: 0x00046C80 File Offset: 0x00044E80
	public void StylePlayer(Player player)
	{
		if (!this.playerVisualElementMap.ContainsKey(player))
		{
			return;
		}
		VisualElement visualElement = this.playerVisualElementMap[player].Query("Player", null);
		UIUtils.SetTeamClass(visualElement, player.Team);
		visualElement.EnableInClassList("patreon", player.PatreonLevel.Value > 0);
		visualElement.EnableInClassList("moderator", player.AdminLevel.Value == 1);
		visualElement.EnableInClassList("admin", player.AdminLevel.Value == 2);
		visualElement.EnableInClassList("developer", player.AdminLevel.Value == 3);
		Label label = visualElement.Query("PositionLabel", null);
		Label label2 = visualElement.Query("UsernameLabel", null);
		Label label3 = visualElement.Query("GoalsLabel", null);
		Label label4 = visualElement.Query("AssistsLabel", null);
		Label label5 = visualElement.Query("PointsLabel", null);
		TextElement textElement = visualElement.Query("PingLabel", null);
		bool flag = player.Team != PlayerTeam.Blue && player.Team != PlayerTeam.Red;
		label.text = (player.PlayerPosition ? player.PlayerPosition.Name.ToString() : string.Empty);
		label2.text = string.Format("#{0} {1}", player.Number.Value, player.Username.Value);
		label3.text = (flag ? string.Empty : player.Goals.Value.ToString());
		label4.text = (flag ? string.Empty : player.Assists.Value.ToString());
		label5.text = (flag ? string.Empty : (player.Goals.Value + player.Assists.Value).ToString());
		textElement.text = string.Format("{0}ms", player.Ping.Value);
		this.SortPlayers();
	}

	// Token: 0x06000D24 RID: 3364 RVA: 0x00012CE4 File Offset: 0x00010EE4
	public void StyleServer(Server server, int playerCount)
	{
		this.nameLabel.text = server.Name.Value;
		this.playersLabel.text = string.Format("{0}/{1}", playerCount, server.MaxPlayers);
	}

	// Token: 0x06000D25 RID: 3365 RVA: 0x00046EA8 File Offset: 0x000450A8
	public void SortPlayers()
	{
		this.players.hierarchy.Sort(delegate(VisualElement a, VisualElement b)
		{
			Player key = this.playerVisualElementMap.FirstOrDefault((KeyValuePair<Player, VisualElement> x) => x.Value == a).Key;
			Player key2 = this.playerVisualElementMap.FirstOrDefault((KeyValuePair<Player, VisualElement> x) => x.Value == b).Key;
			int num = UIScoreboard.<SortPlayers>g__GetTeamOrder|12_1(key.Team);
			int num2 = UIScoreboard.<SortPlayers>g__GetTeamOrder|12_1(key2.Team);
			int num3 = key.Goals.Value + key.Assists.Value;
			int num4 = key2.Goals.Value + key2.Assists.Value;
			if (num != num2)
			{
				return num.CompareTo(num2);
			}
			if (num3 != num4)
			{
				return num4.CompareTo(num3);
			}
			return key.Username.Value.CompareTo(key2.Username.Value);
		});
	}

	// Token: 0x06000D26 RID: 3366 RVA: 0x00012D23 File Offset: 0x00010F23
	private void OnPlayerClicked(ClickEvent clickEvent, Player player)
	{
		EventManager.TriggerEvent("Event_OnScoreboardClickPlayer", new Dictionary<string, object>
		{
			{
				"player",
				player
			}
		});
	}

	// Token: 0x06000D29 RID: 3369 RVA: 0x00012D53 File Offset: 0x00010F53
	[CompilerGenerated]
	internal static int <SortPlayers>g__GetTeamOrder|12_1(PlayerTeam team)
	{
		if (team == PlayerTeam.Blue)
		{
			return 0;
		}
		if (team != PlayerTeam.Red)
		{
			return 2;
		}
		return 1;
	}

	// Token: 0x040007DA RID: 2010
	[Header("References")]
	public VisualTreeAsset playerAsset;

	// Token: 0x040007DB RID: 2011
	private VisualElement scoreboard;

	// Token: 0x040007DC RID: 2012
	private VisualElement header;

	// Token: 0x040007DD RID: 2013
	private VisualElement players;

	// Token: 0x040007DE RID: 2014
	private Label nameLabel;

	// Token: 0x040007DF RID: 2015
	private Label playersLabel;

	// Token: 0x040007E0 RID: 2016
	private Dictionary<Player, VisualElement> playerVisualElementMap = new Dictionary<Player, VisualElement>();
}
