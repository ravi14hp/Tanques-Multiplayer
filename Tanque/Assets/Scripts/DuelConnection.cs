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
        if (isConnecting == true || runner.IsRunning == true)
        {
            return;
        }

        if (string.IsNullOrWhiteSpace(roomNameInput.text))
        {
            statusText.text = "Digite o codigo da sala";
            return;
        }

        isConnecting = true;
        statusText.text = "Criando seu avatar";
        runner.ProvideInput = false;

        StartGameArgs argumentos = new StartGameArgs();
        argumentos.GameMode = GameMode.Shared;
        argumentos.SessionName = roomNameInput.text;
        argumentos.PlayerCount = 2;
        argumentos.SceneManager = GetComponent<NetworkSceneManagerDefault>();

        StartGameResult resultado = await runner.StartGame(argumentos);

        isConnecting = false;

        if (resultado.Ok == true)
        {
            menuPanel.SetActive(false);
        }
        else
        {
            statusText.text = "Erro: " + resultado.ShutdownReason;
        }
    }
}