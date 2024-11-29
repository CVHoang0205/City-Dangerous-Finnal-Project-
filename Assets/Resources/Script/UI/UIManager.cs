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
    public GameObject settingMainMenuPanel;
    public GameObject indicatorPanel;
    public GameObject awardPanel;
    public GameObject shopPanel;

    public Button playGame;
    public Button settingInGameBtn;
    public Button settingMainMenuGameBtn;
    public Button homeInGame;
    public Button quitSettingInGame;
    public Button homeMainMenu;
    public Button quitSettingMainMenu;
    public Button shopButton;
    public Button backButton;

    public JoystickControl joystick;
    public TextMeshProUGUI goldText;

    public ChangeVolume settingInGameVolume;
    public ChangeVolume settingMainMenuVolume;
    // Start is called before the first frame update
    void Start()
    {
        InitGameState(1);
        playGame.onClick.AddListener(() => InitGameState(2));
        InitGold();

        //InGame
        //settingInGameBtn.onClick.AddListener(() => { settingInGamePanel.SetActive(true); });
        settingInGameBtn.onClick.AddListener(() => OpenInGameSettings());
        homeInGame.onClick.AddListener(() => HomeClick());
        quitSettingInGame.onClick.AddListener(() => { settingInGamePanel.SetActive(false); });

        //MainMenu
        //settingMainMenuGameBtn.onClick.AddListener(() => { settingMainMenuPanel.SetActive(true); });
        settingMainMenuGameBtn.onClick.AddListener(() => OpenMainMenuSettings());
        homeMainMenu.onClick.AddListener(() => HomeClick());
        quitSettingMainMenu.onClick.AddListener(() => { settingMainMenuPanel.SetActive(false); });

        shopButton.onClick.AddListener(() => OpenShop());
        backButton.onClick.AddListener(() => BackHome());
    }

    public void HomeClick()
    {
        awardPanel.SetActive(false);
        settingInGamePanel.SetActive(false);
        settingMainMenuPanel.SetActive(false);
        InitGameState(1);
        GameController.Instance.ReplayGame();   
    }

    private void OpenMainMenuSettings()
    {
        SyncVolumeSliders();
        settingMainMenuPanel.SetActive(true);
    }

    private void OpenInGameSettings()
    {
        SyncVolumeSliders();
        settingInGamePanel.SetActive(true);
    }

    public void BackHome()
    {
        InitGameState(1);
        shopPanel.SetActive(false);
    }

    public void OpenShop()
    {
        mainPanel.SetActive(true);
        shopPanel.SetActive(true);
        CameraFollow.Instance.ChangeState(3);
    }

    public void OpenAwardUI(int goldLevel, int goldCollected)
    {
        awardPanel.SetActive(true);
        awardPanel.GetComponent<Award>().InitAwardUI(goldLevel, goldCollected, GameController.Instance.botInStage.Count + 1);
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
            SoundManager.Instance.PlayBackgroundMusic(SoundList.BackgroundInGame);
        }
        else if (state == 1)
        {
            indicatorPanel.SetActive(false);
            SoundManager.Instance.PlayBackgroundMusic(SoundList.BackgroundMainMenu);
        }
        CameraFollow.Instance.ChangeState(state);
    }

    private void SyncVolumeSliders()
    {
        // Đồng bộ giá trị giữa hai Slider
        float currentVolume = PlayerPrefs.GetFloat("musicVolume", 1);
        settingMainMenuVolume.SetVolume(currentVolume);
        settingInGameVolume.SetVolume(currentVolume);
    }
}