using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Character character;
    public Image imageFill;
    public Camera Camera => CameraFollow.Instance.gameCamera;    

    private float hp;
    private float maxHp;
    private float healthBarY = 2.5f;

    private Vector3 viewPoint;
    private Vector3 screenHalf = new Vector2(Screen.width, Screen.height) / 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        viewPoint = Camera.WorldToViewportPoint(character.transform.position + Vector3.up * healthBarY);
        GetComponent<RectTransform>().anchoredPosition = Camera.ViewportToScreenPoint(viewPoint) - screenHalf;
    }

    public void InitHealthBar(float maxHp, Color color)
    {
        this.maxHp = maxHp;
        hp = maxHp;
        imageFill.color = color;
        imageFill.fillAmount = 1;
    }

    public void SetNewHp(float hp)
    {
        this.hp = hp;
        imageFill.fillAmount = Mathf.Clamp01(hp / maxHp);
    }
}
