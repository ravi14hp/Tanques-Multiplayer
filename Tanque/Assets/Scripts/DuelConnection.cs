using Fusion;
using TMPro;
using UnityEngine;

public class DuelConnection : MonoBehaviour
{
    [SerializeField] private NetworkRunner runner;
    [SerializeField] private TMP_InputField roomNameInput;
    [SerializeField] private TMP_Text statusText;
    [SerializeField] private GameObject menuPanel;

    private bool isConnecting;

    public async void JoinRoom()
    {
        if (isConnecting || runner.IsRunning)
            return;

        if (string.IsNullOrWhiteSpace(roomNameInput.text))
        {
            statusText.text = "Digite o codigo da sala";
            return;
        }

        isConnecting = true;
        statusText.text = "Criando seu avatar";
        runner.ProvideInput = false;

        var result = await runner.StartGame(new StartGameArgs
        {
            GameMode = GameMode.Shared,
            SessionName = roomNameInput.text,
            PlayerCount = 2,
            SceneManager = GetComponent<NetworkSceneManagerDefault>()
        });

        isConnecting = false;

        if (result.Ok)
            menuPanel.SetActive(false);
        else
            statusText.text = "Erro: " + result.ShutdownReason;
    }
}
