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

    private int totalCharacter = 0;
    // Start is called before the first frame update
    void Start()
    {
        TargetIndicator playerIndicator = Instantiate(indicator, indicatorCanvas.transform);
        player.indicator = playerIndicator;
        playerIndicator.character = player;

        SetUpCharacterInGame();
        totalCharacter = botInStage.Count + 1;
        InitTextAlive();
    }

    // Update is called once per frame
    void Update()
    {
        
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
            Debug.Log("Win");
        }
    }

    private void SetUpCharacterInGame()
    {
        player.OnInit();
        for(int i = 0; i < botNumber; i++)
        {
            NavMeshHit hit;
            Vector3 point = new Vector3(Random.Range(-40f, 40f), 0f, Random.Range(-40f, 40f));
            if(NavMesh.SamplePosition(point, out hit, 2.0f, NavMesh.AllAreas))
            {
                Bot bot = Instantiate(botPrefabs, hit.position, Quaternion.identity);
                TargetIndicator botIndicator = Instantiate(indicator, indicatorCanvas.transform);
                bot.indicator = botIndicator;
                botIndicator.character = bot;
                botInStage.Add(bot);
                Color color = Random.ColorHSV();
                string botName = GetRandomCharacterNames();
                botIndicator.InitTarget(color, 1, botName);
            }
        }
    }

    private string GetRandomCharacterNames()
    {
        int index = Random.Range(0, Constant.characterNames.Count);
        return Constant.characterNames[index];
    }
}
