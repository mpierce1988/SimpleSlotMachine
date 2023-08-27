using UnityEngine;
using TMPro;

public class PrizeDisplay : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI prizeText;

    private void Start()
    {
        GameControl.PrizeWon += SetPrizeText;
        GameControl.OnHandlePulled += ResetPrizeText;
    }

    private void OnDestroy()
    {
        GameControl.PrizeWon -= SetPrizeText;
        GameControl.OnHandlePulled -= ResetPrizeText;
    }

    private void SetPrizeText(int prizeValue)
    {
        prizeText.enabled = true;
        prizeText.text = "Prize: " + prizeValue;
    }

    private void ResetPrizeText()
    {
        prizeText.enabled = false;
        prizeText.text = "";
    }
}
