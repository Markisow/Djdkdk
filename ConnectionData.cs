using System;

// Token: 0x020000D5 RID: 213
public class ConnectionData
{
	// Token: 0x170000AC RID: 172
	// (get) Token: 0x0600067B RID: 1659 RVA: 0x0000D258 File Offset: 0x0000B458
	// (set) Token: 0x0600067C RID: 1660 RVA: 0x0000D260 File Offset: 0x0000B460
	public string SteamId { get; set; }

	// Token: 0x170000AD RID: 173
	// (get) Token: 0x0600067D RID: 1661 RVA: 0x0000D269 File Offset: 0x0000B469
	// (set) Token: 0x0600067E RID: 1662 RVA: 0x0000D271 File Offset: 0x0000B471
	public string Key { get; set; }

	// Token: 0x170000AE RID: 174
	// (get) Token: 0x0600067F RID: 1663 RVA: 0x0000D27A File Offset: 0x0000B47A
	// (set) Token: 0x06000680 RID: 1664 RVA: 0x0000D282 File Offset: 0x0000B482
	public string Password { get; set; }

	// Token: 0x170000AF RID: 175
	// (get) Token: 0x06000681 RID: 1665 RVA: 0x0000D28B File Offset: 0x0000B48B
	// (set) Token: 0x06000682 RID: 1666 RVA: 0x0000D293 File Offset: 0x0000B493
	public string[] EnabledModIds { get; set; }

	// Token: 0x170000B0 RID: 176
	// (get) Token: 0x06000683 RID: 1667 RVA: 0x0000D29C File Offset: 0x0000B49C
	// (set) Token: 0x06000684 RID: 1668 RVA: 0x0000D2A4 File Offset: 0x0000B4A4
	public PlayerHandedness Handedness { get; set; }

	// Token: 0x170000B1 RID: 177
	// (get) Token: 0x06000685 RID: 1669 RVA: 0x0000D2AD File Offset: 0x0000B4AD
	// (set) Token: 0x06000686 RID: 1670 RVA: 0x0000D2B5 File Offset: 0x0000B4B5
	public int FlagID { get; set; }

	// Token: 0x170000B2 RID: 178
	// (get) Token: 0x06000687 RID: 1671 RVA: 0x0000D2BE File Offset: 0x0000B4BE
	// (set) Token: 0x06000688 RID: 1672 RVA: 0x0000D2C6 File Offset: 0x0000B4C6
	public int HeadgearIDBlueAttacker { get; set; }

	// Token: 0x170000B3 RID: 179
	// (get) Token: 0x06000689 RID: 1673 RVA: 0x0000D2CF File Offset: 0x0000B4CF
	// (set) Token: 0x0600068A RID: 1674 RVA: 0x0000D2D7 File Offset: 0x0000B4D7
	public int HeadgearIDRedAttacker { get; set; }

	// Token: 0x170000B4 RID: 180
	// (get) Token: 0x0600068B RID: 1675 RVA: 0x0000D2E0 File Offset: 0x0000B4E0
	// (set) Token: 0x0600068C RID: 1676 RVA: 0x0000D2E8 File Offset: 0x0000B4E8
	public int HeadgearIDBlueGoalie { get; set; }

	// Token: 0x170000B5 RID: 181
	// (get) Token: 0x0600068D RID: 1677 RVA: 0x0000D2F1 File Offset: 0x0000B4F1
	// (set) Token: 0x0600068E RID: 1678 RVA: 0x0000D2F9 File Offset: 0x0000B4F9
	public int HeadgearIDRedGoalie { get; set; }

	// Token: 0x170000B6 RID: 182
	// (get) Token: 0x0600068F RID: 1679 RVA: 0x0000D302 File Offset: 0x0000B502
	// (set) Token: 0x06000690 RID: 1680 RVA: 0x0000D30A File Offset: 0x0000B50A
	public int MustacheID { get; set; }

	// Token: 0x170000B7 RID: 183
	// (get) Token: 0x06000691 RID: 1681 RVA: 0x0000D313 File Offset: 0x0000B513
	// (set) Token: 0x06000692 RID: 1682 RVA: 0x0000D31B File Offset: 0x0000B51B
	public int BeardID { get; set; }

	// Token: 0x170000B8 RID: 184
	// (get) Token: 0x06000693 RID: 1683 RVA: 0x0000D324 File Offset: 0x0000B524
	// (set) Token: 0x06000694 RID: 1684 RVA: 0x0000D32C File Offset: 0x0000B52C
	public int JerseyIDBlueAttacker { get; set; }

	// Token: 0x170000B9 RID: 185
	// (get) Token: 0x06000695 RID: 1685 RVA: 0x0000D335 File Offset: 0x0000B535
	// (set) Token: 0x06000696 RID: 1686 RVA: 0x0000D33D File Offset: 0x0000B53D
	public int JerseyIDRedAttacker { get; set; }

	// Token: 0x170000BA RID: 186
	// (get) Token: 0x06000697 RID: 1687 RVA: 0x0000D346 File Offset: 0x0000B546
	// (set) Token: 0x06000698 RID: 1688 RVA: 0x0000D34E File Offset: 0x0000B54E
	public int JerseyIDBlueGoalie { get; set; }

	// Token: 0x170000BB RID: 187
	// (get) Token: 0x06000699 RID: 1689 RVA: 0x0000D357 File Offset: 0x0000B557
	// (set) Token: 0x0600069A RID: 1690 RVA: 0x0000D35F File Offset: 0x0000B55F
	public int JerseyIDRedGoalie { get; set; }

	// Token: 0x170000BC RID: 188
	// (get) Token: 0x0600069B RID: 1691 RVA: 0x0000D368 File Offset: 0x0000B568
	// (set) Token: 0x0600069C RID: 1692 RVA: 0x0000D370 File Offset: 0x0000B570
	public int StickSkinIDBlueAttacker { get; set; }

	// Token: 0x170000BD RID: 189
	// (get) Token: 0x0600069D RID: 1693 RVA: 0x0000D379 File Offset: 0x0000B579
	// (set) Token: 0x0600069E RID: 1694 RVA: 0x0000D381 File Offset: 0x0000B581
	public int StickSkinIDRedAttacker { get; set; }

	// Token: 0x170000BE RID: 190
	// (get) Token: 0x0600069F RID: 1695 RVA: 0x0000D38A File Offset: 0x0000B58A
	// (set) Token: 0x060006A0 RID: 1696 RVA: 0x0000D392 File Offset: 0x0000B592
	public int StickSkinIDBlueGoalie { get; set; }

	// Token: 0x170000BF RID: 191
	// (get) Token: 0x060006A1 RID: 1697 RVA: 0x0000D39B File Offset: 0x0000B59B
	// (set) Token: 0x060006A2 RID: 1698 RVA: 0x0000D3A3 File Offset: 0x0000B5A3
	public int StickSkinIDRedGoalie { get; set; }

	// Token: 0x170000C0 RID: 192
	// (get) Token: 0x060006A3 RID: 1699 RVA: 0x0000D3AC File Offset: 0x0000B5AC
	// (set) Token: 0x060006A4 RID: 1700 RVA: 0x0000D3B4 File Offset: 0x0000B5B4
	public int StickShaftTapeIDBlueAttacker { get; set; }

	// Token: 0x170000C1 RID: 193
	// (get) Token: 0x060006A5 RID: 1701 RVA: 0x0000D3BD File Offset: 0x0000B5BD
	// (set) Token: 0x060006A6 RID: 1702 RVA: 0x0000D3C5 File Offset: 0x0000B5C5
	public int StickShaftTapeIDRedAttacker { get; set; }

	// Token: 0x170000C2 RID: 194
	// (get) Token: 0x060006A7 RID: 1703 RVA: 0x0000D3CE File Offset: 0x0000B5CE
	// (set) Token: 0x060006A8 RID: 1704 RVA: 0x0000D3D6 File Offset: 0x0000B5D6
	public int StickShaftTapeIDBlueGoalie { get; set; }

	// Token: 0x170000C3 RID: 195
	// (get) Token: 0x060006A9 RID: 1705 RVA: 0x0000D3DF File Offset: 0x0000B5DF
	// (set) Token: 0x060006AA RID: 1706 RVA: 0x0000D3E7 File Offset: 0x0000B5E7
	public int StickShaftTapeIDRedGoalie { get; set; }

	// Token: 0x170000C4 RID: 196
	// (get) Token: 0x060006AB RID: 1707 RVA: 0x0000D3F0 File Offset: 0x0000B5F0
	// (set) Token: 0x060006AC RID: 1708 RVA: 0x0000D3F8 File Offset: 0x0000B5F8
	public int StickBladeTapeIDBlueAttacker { get; set; }

	// Token: 0x170000C5 RID: 197
	// (get) Token: 0x060006AD RID: 1709 RVA: 0x0000D401 File Offset: 0x0000B601
	// (set) Token: 0x060006AE RID: 1710 RVA: 0x0000D409 File Offset: 0x0000B609
	public int StickBladeTapeIDRedAttacker { get; set; }

	// Token: 0x170000C6 RID: 198
	// (get) Token: 0x060006AF RID: 1711 RVA: 0x0000D412 File Offset: 0x0000B612
	// (set) Token: 0x060006B0 RID: 1712 RVA: 0x0000D41A File Offset: 0x0000B61A
	public int StickBladeTapeIDBlueGoalie { get; set; }

	// Token: 0x170000C7 RID: 199
	// (get) Token: 0x060006B1 RID: 1713 RVA: 0x0000D423 File Offset: 0x0000B623
	// (set) Token: 0x060006B2 RID: 1714 RVA: 0x0000D42B File Offset: 0x0000B62B
	public int StickBladeTapeIDRedGoalie { get; set; }
}
