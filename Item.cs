using System;
using System.Linq;
using System.Text.Json.Serialization;

// Token: 0x020000C1 RID: 193
public class Item
{
	// Token: 0x1700008C RID: 140
	// (get) Token: 0x060005EB RID: 1515 RVA: 0x0000CAE6 File Offset: 0x0000ACE6
	// (set) Token: 0x060005EC RID: 1516 RVA: 0x0000CAEE File Offset: 0x0000ACEE
	public int id { get; set; }

	// Token: 0x1700008D RID: 141
	// (get) Token: 0x060005ED RID: 1517 RVA: 0x0000CAF7 File Offset: 0x0000ACF7
	// (set) Token: 0x060005EE RID: 1518 RVA: 0x0000CAFF File Offset: 0x0000ACFF
	public string name { get; set; }

	// Token: 0x1700008E RID: 142
	// (get) Token: 0x060005EF RID: 1519 RVA: 0x0000CB08 File Offset: 0x0000AD08
	// (set) Token: 0x060005F0 RID: 1520 RVA: 0x0000CB10 File Offset: 0x0000AD10
	public string description { get; set; }

	// Token: 0x1700008F RID: 143
	// (get) Token: 0x060005F1 RID: 1521 RVA: 0x0000CB19 File Offset: 0x0000AD19
	// (set) Token: 0x060005F2 RID: 1522 RVA: 0x0000CB21 File Offset: 0x0000AD21
	public string[] categories { get; set; } = new string[0];

	// Token: 0x17000090 RID: 144
	// (get) Token: 0x060005F3 RID: 1523 RVA: 0x0000CB2A File Offset: 0x0000AD2A
	// (set) Token: 0x060005F4 RID: 1524 RVA: 0x0000CB32 File Offset: 0x0000AD32
	public int price { get; set; }

	// Token: 0x17000091 RID: 145
	// (get) Token: 0x060005F5 RID: 1525 RVA: 0x0000CB3B File Offset: 0x0000AD3B
	[JsonIgnore]
	public bool IsFlag
	{
		get
		{
			return this.categories.Contains("flag");
		}
	}

	// Token: 0x17000092 RID: 146
	// (get) Token: 0x060005F6 RID: 1526 RVA: 0x0000CB4D File Offset: 0x0000AD4D
	[JsonIgnore]
	public bool IsHeadgear
	{
		get
		{
			return this.categories.Contains("headgear");
		}
	}

	// Token: 0x17000093 RID: 147
	// (get) Token: 0x060005F7 RID: 1527 RVA: 0x0000CB5F File Offset: 0x0000AD5F
	[JsonIgnore]
	public bool IsMustache
	{
		get
		{
			return this.categories.Contains("mustache");
		}
	}

	// Token: 0x17000094 RID: 148
	// (get) Token: 0x060005F8 RID: 1528 RVA: 0x0000CB71 File Offset: 0x0000AD71
	[JsonIgnore]
	public bool IsBeard
	{
		get
		{
			return this.categories.Contains("beard");
		}
	}

	// Token: 0x17000095 RID: 149
	// (get) Token: 0x060005F9 RID: 1529 RVA: 0x0000CB83 File Offset: 0x0000AD83
	[JsonIgnore]
	public bool IsJersey
	{
		get
		{
			return this.categories.Contains("jersey");
		}
	}

	// Token: 0x17000096 RID: 150
	// (get) Token: 0x060005FA RID: 1530 RVA: 0x0000CB95 File Offset: 0x0000AD95
	[JsonIgnore]
	public bool IsStickSkin
	{
		get
		{
			return this.categories.Contains("stickSkin");
		}
	}

	// Token: 0x17000097 RID: 151
	// (get) Token: 0x060005FB RID: 1531 RVA: 0x0000CBA7 File Offset: 0x0000ADA7
	[JsonIgnore]
	public bool IsStickShaftTape
	{
		get
		{
			return this.categories.Contains("stickShaftTape");
		}
	}

	// Token: 0x17000098 RID: 152
	// (get) Token: 0x060005FC RID: 1532 RVA: 0x0000CBB9 File Offset: 0x0000ADB9
	[JsonIgnore]
	public bool IsStickBladeTape
	{
		get
		{
			return this.categories.Contains("stickBladeTape");
		}
	}

	// Token: 0x17000099 RID: 153
	// (get) Token: 0x060005FD RID: 1533 RVA: 0x0000CBCB File Offset: 0x0000ADCB
	[JsonIgnore]
	public bool HasRolePostfix
	{
		get
		{
			return this.categories.Contains("attacker") || this.categories.Contains("goalie");
		}
	}

	// Token: 0x1700009A RID: 154
	// (get) Token: 0x060005FE RID: 1534 RVA: 0x0000CBF1 File Offset: 0x0000ADF1
	[JsonIgnore]
	public bool IsAttackerItem
	{
		get
		{
			return !this.HasRolePostfix || this.categories.Contains("attacker");
		}
	}

	// Token: 0x1700009B RID: 155
	// (get) Token: 0x060005FF RID: 1535 RVA: 0x0000CC0D File Offset: 0x0000AE0D
	[JsonIgnore]
	public bool IsGoalieItem
	{
		get
		{
			return !this.HasRolePostfix || this.categories.Contains("goalie");
		}
	}

	// Token: 0x1700009C RID: 156
	// (get) Token: 0x06000600 RID: 1536 RVA: 0x0000CC29 File Offset: 0x0000AE29
	[JsonIgnore]
	public bool IsPurchased
	{
		get
		{
			return BackendManager.PlayerState.PlayerData != null && BackendManager.PlayerState.PlayerData.items.Any((PlayerItem item) => item.itemId == this.id);
		}
	}

	// Token: 0x1700009D RID: 157
	// (get) Token: 0x06000601 RID: 1537 RVA: 0x0000CC59 File Offset: 0x0000AE59
	[JsonIgnore]
	public bool IsOwned
	{
		get
		{
			return true;
		}
	}

	// Token: 0x1700009E RID: 158
	// (get) Token: 0x06000602 RID: 1538 RVA: 0x0000CC5C File Offset: 0x0000AE5C
	[JsonIgnore]
	public bool IsUnlisted
	{
		get
		{
			return this.categories.Contains("unlisted");
		}
	}

	// Token: 0x1700009F RID: 159
	// (get) Token: 0x06000603 RID: 1539 RVA: 0x0000CC6E File Offset: 0x0000AE6E
	[JsonIgnore]
	public string EditorDisplayName
	{
		get
		{
			return string.Format("{0} ({1})", this.name, this.id);
		}
	}
}
