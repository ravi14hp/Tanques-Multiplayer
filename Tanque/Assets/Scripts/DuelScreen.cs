using TMPro;
using UnityEngine;

public class DuelScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text statusText;
    [SerializeField] private GameObject resultPanel;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private TMP_Text resultLabel;

    private void Start()
    {
        resultPanel.SetActive(false);
        menuPanel.SetActive(true);
    }

    void Update()
    {
        if (DuelGame.Current == null)
        {
            return;
        }

        if (DuelGame.Current.GameOver == true)
        {
            resultPanel.SetActive(true);
            statusText.text = "Duelo acabado";

            if (DuelGame.Current.Loser == DuelGame.Current.Runner.LocalPlayer)
            {
                resultLabel.text = "VOCÊ NÃO!";
            }
            else
            {
                resultLabel.text = "VOCÊ GANHOU!";
            }
        }
        else
        {
            if (DuelGame.CanFight == true)
            {
                statusText.text = "Batalha em andamento";
            }
            else
            {
                statusText.text = "Esperando desafiante";
            }
        }
    }
}
