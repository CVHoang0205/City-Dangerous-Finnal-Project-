using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InitItem : MonoBehaviour
{
    public Image itemImage;
    public GameObject price;
    public TextMeshProUGUI priceText;
    public Button actionButton;
    public TextMeshProUGUI textButton;

    private int state = 0;
    private GameItem thisItem;
    // Start is called before the first frame update
    void Start()
    {
        actionButton.onClick.AddListener(() => ButtonAction());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonAction()
    {
        //chua mua
        if (state == 1)
        {
            int currentGold = GameController.Instance.gold;
            int priceItem = thisItem.item.Price;

            if (currentGold >= priceItem)
            {
                GameController.Instance.ReduceGold(priceItem);
                ItemJsonDatabase.Instance.PurchasedItem(thisItem);
                ShopController.Instance.CreateItem(thisItem.item.Type);
            }
        }
        else if (state == 2) //Da mua, chua mac
        {
            ItemJsonDatabase.Instance.EquipItem(thisItem);
            ShopController.Instance.CreateItem(thisItem.item.Type);
            GameController.Instance.InitPlayerItems();
        }
        else if (state == 3) //Da mua, da mac
        {
            ItemJsonDatabase.Instance.UnEquipItem(thisItem);
            ShopController.Instance.CreateItem(thisItem.item.Type);
            GameController.Instance.InitPlayerItems();
        }
    }

    public void InitItemUI(GameItem item)
    {
        thisItem = item;
        itemImage.sprite = Resources.Load<Sprite>("UI/" + item.item.Type + "/" + item.item.Id);
        if (item.Purchased == false)
        {
            state = 1;
            price.SetActive(true);
            priceText.text = item.item.Price.ToString();
        }
        else
        {
            price.SetActive(false);
            if (item.IsEquip)
            {
                state = 3;
            }
            else
            {
                state = 2;
            }
        }
        InitButtonState();
    }

    private void InitButtonState()
    {
        if (state == 1)
        {
            actionButton.GetComponent<Image>().color = Color.white;
            textButton.text = "Buy";
        }
        else if (state == 2)
        {
            actionButton.GetComponent<Image>().color = Color.green;
            textButton.text = "Equip";
        }
        else if (state == 3)
        {
            actionButton.GetComponent<Image>().color = Color.yellow;
            textButton.text = "Used";
        }
    }
}
