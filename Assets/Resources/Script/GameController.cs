using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameController : MonoBehaviour
{
    public Player player;
    public Bot botPrefabs;
    public int botNumber = 10;
    public List<Bot> botInStage = new List<Bot>();  
    // Start is called before the first frame update
    void Start()
    {
        SetUpCharacterInGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetUpCharacterInGame()
    {
        for(int i = 0; i < botNumber; i++)
        {
            NavMeshHit hit;
            Vector3 point = new Vector3(Random.Range(-40f, 40f), 0f, Random.Range(-40f, 40f));
            if(NavMesh.SamplePosition(point, out hit, 2.0f, NavMesh.AllAreas))
            {
                Bot bot = Instantiate(botPrefabs, hit.position, Quaternion.identity);
                botInStage.Add(bot);
            }
        }
    }
}
