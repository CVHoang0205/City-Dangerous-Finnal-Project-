using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public GameObject inGamePanel;
    public GameObject indicatorPanel;
    // Start is called before the first frame update
    void Start()
    {
        InitGameState(2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitGameState(int state)
    {
        inGamePanel.SetActive(state == 2);
        if (state == 2)
        {
            indicatorPanel.SetActive(true);
        }
    }
}
