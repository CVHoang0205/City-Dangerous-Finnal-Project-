using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Award : MonoBehaviour
{
    public TextMeshProUGUI textGold;
    public TextMeshProUGUI textPosition;
    public Button claimButton;

    private int gold = 0;
    // Start is called before the first frame update
    void Start()
    {
        claimButton.onClick.AddListener(() => ClaimButtonClick());
    }

    public void InitAwardUI(int gold, int pos)
    {
        this.gold = gold;
        textGold.text = "X " + gold;
        textPosition.text = "You are at #" + pos;
    }

    private void ClaimButtonClick()
    {
        GameController.Instance.GainGold(gold); 
        UIManager.Instance.HomeClick(); 
    }
}
