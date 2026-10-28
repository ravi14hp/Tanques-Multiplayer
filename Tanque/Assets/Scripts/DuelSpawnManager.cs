using Fusion;
using UnityEngine;

public class DuelSpawnManager : SimulationBehaviour, IPlayerJoined
{
    [SerializeField] private NetworkObject fighterPrefab;
    [SerializeField] private NetworkObject gamePrefab;

    public void PlayerJoined(PlayerRef player)
    {
        if (player != Runner.LocalPlayer)
            return;

        bool isPlayer1 = Runner.IsSharedModeMasterClient;

        if (isPlayer1)
            Runner.Spawn(gamePrefab, Vector3.zero, Quaternion.identity);

        Vector3 position = new Vector3(isPlayer1 ? -4 : 4, 0, 0);
        Quaternion rotation = Quaternion.Euler(0, 0, isPlayer1 ? 0 : 180);
        Runner.Spawn(fighterPrefab, position, rotation);
    }
}
