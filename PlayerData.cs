using System;

// Token: 0x02000227 RID: 551
public class PlayerData
{
	// Token: 0x17000143 RID: 323
	// (get) Token: 0x06000FBD RID: 4029 RVA: 0x0001488D File Offset: 0x00012A8D
	// (set) Token: 0x06000FBE RID: 4030 RVA: 0x00014895 File Offset: 0x00012A95
	public string steamId { get; set; }

	// Token: 0x17000144 RID: 324
	// (get) Token: 0x06000FBF RID: 4031 RVA: 0x0001489E File Offset: 0x00012A9E
	// (set) Token: 0x06000FC0 RID: 4032 RVA: 0x000148A6 File Offset: 0x00012AA6
	public string username { get; set; }

	// Token: 0x17000145 RID: 325
	// (get) Token: 0x06000FC1 RID: 4033 RVA: 0x000148AF File Offset: 0x00012AAF
	// (set) Token: 0x06000FC2 RID: 4034 RVA: 0x000148B7 File Offset: 0x00012AB7
	public int number { get; set; }

	// Token: 0x17000146 RID: 326
	// (get) Token: 0x06000FC3 RID: 4035 RVA: 0x000148C0 File Offset: 0x00012AC0
	// (set) Token: 0x06000FC4 RID: 4036 RVA: 0x000148C8 File Offset: 0x00012AC8
	public double? usernameChangedAt { get; set; }

	// Token: 0x17000147 RID: 327
	// (get) Token: 0x06000FC5 RID: 4037 RVA: 0x000148D1 File Offset: 0x00012AD1
	// (set) Token: 0x06000FC6 RID: 4038 RVA: 0x000148D9 File Offset: 0x00012AD9
	public int patreonLevel { get; set; }

	// Token: 0x17000148 RID: 328
	// (get) Token: 0x06000FC7 RID: 4039 RVA: 0x000148E2 File Offset: 0x00012AE2
	// (set) Token: 0x06000FC8 RID: 4040 RVA: 0x000148EA File Offset: 0x00012AEA
	public int mmr { get; set; }

	// Token: 0x17000149 RID: 329
	// (get) Token: 0x06000FC9 RID: 4041 RVA: 0x000148F3 File Offset: 0x00012AF3
	// (set) Token: 0x06000FCA RID: 4042 RVA: 0x000148FB File Offset: 0x00012AFB
	public int adminLevel { get; set; }

	// Token: 0x1700014A RID: 330
	// (get) Token: 0x06000FCB RID: 4043 RVA: 0x00014904 File Offset: 0x00012B04
	// (set) Token: 0x06000FCC RID: 4044 RVA: 0x0001490C File Offset: 0x00012B0C
	public PlayerItem[] items { get; set; }

	// Token: 0x1700014B RID: 331
	// (get) Token: 0x06000FCD RID: 4045 RVA: 0x00014915 File Offset: 0x00012B15
	// (set) Token: 0x06000FCE RID: 4046 RVA: 0x0001491D File Offset: 0x00012B1D
	public PlayerMute[] mutes { get; set; }

	// Token: 0x1700014C RID: 332
	// (get) Token: 0x06000FCF RID: 4047 RVA: 0x00014926 File Offset: 0x00012B26
	// (set) Token: 0x06000FD0 RID: 4048 RVA: 0x0001492E File Offset: 0x00012B2E
	public PlayerBan[] bans { get; set; }

	// Token: 0x1700014D RID: 333
	// (get) Token: 0x06000FD1 RID: 4049 RVA: 0x00014937 File Offset: 0x00012B37
	// (set) Token: 0x06000FD2 RID: 4050 RVA: 0x0001493F File Offset: 0x00012B3F
	public PlayerCooldown[] cooldowns { get; set; }
}
