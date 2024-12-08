using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class GameController : Singleton<GameController> 
{
    public Player player;
    public Bot botPrefabs;
    public int botNumber = 10;
    public Canvas indicatorCanvas;
    public TargetIndicator indicator;
    public TextMeshProUGUI aliveText;
    public List<Bot> botInStage = new List<Bot>();
    public int gold;
    public bool isStartGame = false;
    public Canvas healthBarCanvas;
    public HealthBar healthBar;
    //public int totalBot = 20;
    //public int spawnBot = 0;
    public List<Transform> listSpawn = new List<Transform>();
    public List<MoveCar> cars = new List<MoveCar>();

    private int totalCharacter = 0;
    // Start is called before the first frame update
    void Start()
    {
        TargetIndicator playerIndicator = Instantiate(indicator, indicatorCanvas.transform);
        player.indicator = playerIndicator;
        playerIndicator.character = player;

        HealthBar playerHealthBar = Instantiate(healthBar, healthBarCanvas.transform);
        player.healthBar = playerHealthBar;
        playerHealthBar.character = player;

        SetUpCharacterInGame();
        //StartCoroutine(SetUpCharacterInGame());
        totalCharacter = botInStage.Count + 1;
        InitTextAlive();
        InitGold();
        //GainGold(200);
    }

    void Awake()
    {
        // Kiểm tra và tải giá trị âm lượng khi game khởi động
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1.0f); // Mặc định là 100% âm lượng
        }

        // Áp dụng giá trị âm lượng đã lưu
        float savedVolume = PlayerPrefs.GetFloat("musicVolume");
        AudioListener.volume = savedVolume;
    }

    public void InitPlayerItems()
    {
        player.skin.PlayerEquipItems();
        player.bulletPrefabs = ItemDatabase.Instance.bullets[player.skin.weaponId];
    }

    public void StartGame()
    {
        totalCharacter = botInStage.Count + 1;
        InitTextAlive();
        for (int i = 0; i < botInStage.Count; i++)
        {
            botInStage[i].OnInit(); 
        }
    }

    public void ReplayGame()
    {
        DeleteAllBots();
        player.OnDespawn();
        SetUpCharacterInGame();
        foreach(MoveCar car in cars)
        {
            car.ResetPosition();
        }
        DestroyAllCoins();
    }

    public void InitGold()
    {
        if (!PlayerPrefs.HasKey("Gold"))
        {
            gold = 0;
            PlayerPrefs.SetInt("Gold", 0);
        }
        else
        {
            gold = PlayerPrefs.GetInt("Gold");
        }
    }

    public void GainGold(int num)
    {
        gold += num;
        PlayerPrefs.SetInt("Gold", gold);
        UIManager.Instance.InitGold();
    }

    public void ReduceGold(int num)
    {
        gold -= num;
        PlayerPrefs.SetInt("Gold", gold);
        UIManager.Instance.InitGold();
    }

    public void DeleteAllBots()
    {
        for(int i = 0; i < botInStage.Count; i++)
        {
            Destroy(botInStage[i].indicator.gameObject);
            Destroy(botInStage[i].healthBar.gameObject);
            Destroy(botInStage[i].gameObject);
        }
        botInStage.Clear(); 
    }

    public void InitTextAlive()
    {
        aliveText.text = "Alive: " + totalCharacter;
    }

    public void CharacterDead()
    {
        totalCharacter--;
        aliveText.text = "Alive: " + totalCharacter;
        if(totalCharacter == 1)
        {
            UIManager.Instance.OpenAwardUI(player.level, player.totalGold);
            SoundManager.Instance.PlayOneShotMusic(SoundList.Win);
        }
    }

    //private void SetUpCharacterInGame()
    //{
    //    player.OnInit();
    //    for (int i = 0; i < botNumber; i++)
    //    {
    //        NavMeshHit hit;
    //        Vector3 point = new Vector3(Random.Range(-40f, 40f), 0f, Random.Range(-40f, 40f));
    //        if (NavMesh.SamplePosition(point, out hit, 2.0f, NavMesh.AllAreas))
    //        {
    //            Bot bot = Instantiate(botPrefabs, hit.position, Quaternion.identity);
    //            TargetIndicator botIndicator = Instantiate(indicator, indicatorCanvas.transform);
    //            bot.indicator = botIndicator;
    //            botIndicator.character = bot;
    //            HealthBar botHealthBar = Instantiate(healthBar, healthBarCanvas.transform);
    //            bot.healthBar = botHealthBar;
    //            botHealthBar.character = bot;
    //            botInStage.Add(bot);
    //            Color color = Random.ColorHSV();
    //            string botName = GetRandomCharacterNames();
    //            botIndicator.InitTarget(color, 1, botName);
    //            botHealthBar.InitHealthBar(100, Color.red);
    //        }
    //    }
    //}

    private void SetUpCharacterInGame()
    {
        player.transform.position = listSpawn[listSpawn.Count - 1].position;
        player.OnInit();
        for (int i = 0; i < botNumber; i++)
        {
            Bot bot = Instantiate(botPrefabs, listSpawn[i].position, Quaternion.identity);
            TargetIndicator botIndicator = Instantiate(indicator, indicatorCanvas.transform);
            bot.indicator = botIndicator;
            botIndicator.character = bot;
            HealthBar botHealthBar = Instantiate(healthBar, healthBarCanvas.transform);
            bot.healthBar = botHealthBar;
            botHealthBar.character = bot;
            botInStage.Add(bot);
            Color color = Random.ColorHSV();
            string botName = GetRandomCharacterNames();
            botIndicator.InitTarget(color, 1, botName);
            botHealthBar.InitHealthBar(100, Color.red);
        }
    }

    public void DestroyAllCoins()
    {
        GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");
        foreach (GameObject coin in coins)
        {
            Destroy(coin);  
        }
    }

    private string GetRandomCharacterNames()
    {
        int index = Random.Range(0, Constant.characterNames.Count);
        return Constant.characterNames[index];
    }

    public void EndGame()
    {
        JoystickControl.direct = Vector3.zero;
        JoystickControl.Instance.gameObject.SetActive(false);
    }
}
