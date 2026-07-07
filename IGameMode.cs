using System;

// Token: 0x020000A9 RID: 169
internal interface IGameMode
{
	// Token: 0x17000082 RID: 130
	// (get) Token: 0x0600056F RID: 1391
	// (set) Token: 0x06000570 RID: 1392
	bool IsInitialized { get; set; }

	// Token: 0x06000571 RID: 1393
	bool Initialize(Level level, ServerManager serverManager, GameManager gameManager, PlayerManager playerManager, PuckManager puckManager, ChatManager chatManager, ReplayManager replayManager, VoteManager voteManager);

	// Token: 0x06000572 RID: 1394
	bool Dispose();
}
