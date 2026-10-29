using Fusion;
using UnityEngine;

public class DuelGame : NetworkBehaviour
{
    public static DuelGame Current { get; private set; }

    [Networked]
    public PlayerRef Loser { get; set; }

    public bool GameOver
    {
        get
        {
            if (Loser != PlayerRef.None)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public static bool CanFight
    {
        get
        {
            if (Current != null && Current.GameOver == false &&
                Current.Runner.SessionInfo.PlayerCount == 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public override void Spawned()
    {
        Current = this;
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_PlayerDefeated(PlayerRef loser)
    {
        if (GameOver == false)
        {
            Loser = loser;
        }
    }
}
