using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TopUIManager : MonoBehaviour
{
    [SerializeField] PlayerClass player;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI roundText;
    [SerializeField] Image[] hearts;

    private void Update()
    {
        scoreText.text = $"Score: {player.score}";
        roundText.text = $"Round: {player.roundNo}";
        DisableHearts();
    }

    private void DisableHearts()
    {
        int disableAmt = 0;
        if (player.hp > 8) disableAmt = 0;
        else if (player.hp > 6) disableAmt = 1;
        else if (player.hp > 4) disableAmt = 2;
        else if (player.hp > 2) disableAmt = 3;
        else if (player.hp > 0) disableAmt = 4;
        else disableAmt = 5;

        for (int i = 0; i < disableAmt; i++)
        {
            if (hearts[i].enabled) hearts[i].enabled = false;
        }
    }
}
