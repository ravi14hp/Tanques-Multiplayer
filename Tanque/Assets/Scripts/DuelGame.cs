using Fusion;
using UnityEngine;

public class DuelGame : NetworkBehaviour
{
    public static DuelGame Current { get; private set; }

    [Networked] public PlayerRef Loser { get; set; }

    public bool GameOver => Loser != PlayerRef.None;

    public static bool CanFight =>
        Current != null && !Current.GameOver && Current.Runner.SessionInfo.PlayerCount == 2;

    public override void Spawned()
    {
        Current = this;
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_PlayerDefeated(PlayerRef loser)
    {
        if (!GameOver)
            Loser = loser;
    }
}
