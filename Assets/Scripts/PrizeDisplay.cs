using UnityEngine;
using TMPro;

public class PrizeDisplay : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI prizeText;

    private void Start()
    {
        GameControl.PrizeWon += SetPrizeText;
        GameControl.OnStartSpin += ResetPrizeText;
    }

    private void OnDestroy()
    {
        GameControl.PrizeWon -= SetPrizeText;
        GameControl.OnStartSpin -= ResetPrizeText;
    }

    private void SetPrizeText(int prizeValue)
    {
        prizeText.enabled = true;
        prizeText.text = "Prize: " + prizeValue;
    }

    private void ResetPrizeText()
    {
        //prizeValue = 0;
        prizeText.enabled = false;
        prizeText.text = "";
        //resultsChecked = false;
    }
}
