using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Award : MonoBehaviour
{
    public TextMeshProUGUI textGoldLevel;
    public TextMeshProUGUI textGoldCollected;
    public TextMeshProUGUI textPosition;
    public Button claimButton;

    private int goldLevel = 0;
    private int goldCollected = 0;
    // Start is called before the first frame update
    void Start()
    {
        claimButton.onClick.AddListener(() => ClaimButtonClick());
    }

    public void InitAwardUI(int goldLevel, int goldCollected, int pos)
    {
        this.goldLevel = goldLevel;
        textGoldLevel.text = "X " + goldLevel;
        this.goldCollected = goldCollected;
        textGoldCollected.text = "X " + goldCollected;
        textPosition.text = "You are at #" + pos;
    }

    private void ClaimButtonClick()
    {
        GameController.Instance.GainGold(goldLevel); 
        UIManager.Instance.HomeClick(); 
    }
}
