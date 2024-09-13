using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public GameObject mainPanel;    
    public GameObject inGamePanel;
    public GameObject settingInGamePanel;
    public GameObject indicatorPanel;
    public GameObject awardPanel;
    public GameObject shopPanel;

    public Button playGame;
    public Button settingInGameBtn;
    public Button home;
    public Button quitSetting;
    public Button shopButton;

    public JoystickControl joystick;
    public TextMeshProUGUI goldText;
    // Start is called before the first frame update
    void Start()
    {
        InitGameState(1);
        playGame.onClick.AddListener(() => InitGameState(2));
        InitGold();

        settingInGameBtn.onClick.AddListener(() => { settingInGamePanel.SetActive(true); });
        home.onClick.AddListener(() => HomeClick());
        quitSetting.onClick.AddListener(() => { settingInGamePanel.SetActive(false); });
        shopButton.onClick.AddListener(() => OpenShop());
    }

    public void HomeClick()
    {
        awardPanel.SetActive(false);
        settingInGamePanel.SetActive(false);
        InitGameState(1);
        GameController.Instance.ReplayGame();   
    }

    public void OpenShop()
    {
        mainPanel.SetActive(true);
        shopPanel.SetActive(true);
        CameraFollow.Instance.ChangeState(3);
    }

    public void OpenAwardUI(int gold)
    {
        awardPanel.SetActive(true);
        awardPanel.GetComponent<Award>().InitAwardUI(gold, GameController.Instance.botInStage.Count + 1);
        GameController.Instance.DeleteAllBots();    
    }

    public void InitGold()
    {
        goldText.text = GameController.Instance.gold.ToString();
    }

    private void InitGameState(int state)
    {
        mainPanel.SetActive(state == 1);
        inGamePanel.SetActive(state == 2);
        joystick.gameObject.SetActive(state == 2);
        if (state == 2)
        {
            indicatorPanel.SetActive(true);
            GameController.Instance.StartGame();
        }
        else if (state == 1)
        {
            indicatorPanel.SetActive(false);
        }
        CameraFollow.Instance.ChangeState(state);
    }
}
