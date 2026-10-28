using TMPro;
using UnityEngine;

public class DuelScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text statusText;
    [SerializeField] private GameObject resultPanel;
    [SerializeField] private TMP_Text resultLabel;

    private void Start()
    {
        resultPanel.SetActive(false);
    }

    private void Update()
    {
        DuelGame game = DuelGame.Current;
        if (game == null)
            return;

        if (game.GameOver)
        {
            resultPanel.SetActive(true);
            statusText.text = "Duelo acabado";
            resultLabel.text = game.Loser == game.Runner.LocalPlayer ? "VOCÊ NÃO!" : "VOCÊ GANHOU!";
        }
        else
        {
            statusText.text = DuelGame.CanFight ? "Batalha em andamento" : "Esperando desafiante";
        }
    }
}
