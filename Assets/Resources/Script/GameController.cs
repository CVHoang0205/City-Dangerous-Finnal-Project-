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
    public int totalBot = 20;
    public int spawnBot = 0;
    public List<Transform> listBottransform = new List<Transform>();

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
        //GainGold(500);
    }

    // Update is called once per frame
    void Update()
    {
        
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
            UIManager.Instance.OpenAwardUI(player.level);
        }
    }

    private void SetUpCharacterInGame()
    {
        player.OnInit();
        for (int i = 0; i < botNumber; i++)
        {
            NavMeshHit hit;
            Vector3 point = new Vector3(Random.Range(-40f, 40f), 0f, Random.Range(-40f, 40f));
            if (NavMesh.SamplePosition(point, out hit, 2.0f, NavMesh.AllAreas))
            {
                Bot bot = Instantiate(botPrefabs, hit.position, Quaternion.identity);
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
    }

    //IEnumerator SetUpCharacterInGame()
    //{
    //    player.OnInit();
    //    while (spawnBot < totalBot) {
    //        for (int i = 0; i < listBottransform.Count; i++)
    //        {
    //            Bot bot = Instantiate(botPrefabs, listBottransform[i].transform.position, Quaternion.identity);
    //            spawnBot++;
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
    //            yield return new WaitForSeconds(5f);
    //        }
    //    }   
    //}

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
