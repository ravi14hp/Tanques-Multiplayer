using Fusion;
using UnityEngine;

public class DuelSpawnManager : SimulationBehaviour, IPlayerJoined
{
    [SerializeField] private NetworkObject fighterPrefab;
    [SerializeField] private NetworkObject gamePrefab;

    public void PlayerJoined(PlayerRef player)
    {
        if (player != Runner.LocalPlayer)
        {
            return;
        }

        bool isPlayer1 = Runner.IsSharedModeMasterClient;

        if (isPlayer1 == true)
        {
            Runner.Spawn(gamePrefab, Vector3.zero, Quaternion.identity);
        }

        Vector3 position;
        Quaternion rotation;

        if (isPlayer1 == true)
        {
            position = new Vector3(-4, 0, 0);
            rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            position = new Vector3(4, 0, 0);
            rotation = Quaternion.Euler(0, 0, 180);
        }

        Runner.Spawn(fighterPrefab, position, rotation);
    }
}