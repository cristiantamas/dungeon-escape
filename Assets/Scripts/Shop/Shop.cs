using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject shopPanel;
    public int currentItem;
    public int currentItemValue;

    private Player _player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player"){
            _player = other.GetComponent<Player>();

            if(_player != null){
                UIManager.Instance.OpenShop(_player.gems);
            }
            shopPanel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            shopPanel.SetActive(false);
        }
    }

    public void SelectItem(int item){

        switch(item){
            case 0:
                currentItem = 0;
                currentItemValue = 5;
                UIManager.Instance.UpdateItemSelction(-489);
                break;
            case 1:
                currentItem = 1;
                currentItemValue = 3;
                UIManager.Instance.UpdateItemSelction(-589);
                break;
            case 2:
                currentItem = 2;
                currentItemValue = 10;
                UIManager.Instance.UpdateItemSelction(-689);
                break;
        }
    }

    public void BuyItem() {

        if(_player.gems >= currentItemValue) {
            _player.gems -= currentItemValue;

            if(currentItem == 2) GameManager.Instance.HasKeyToCastle = true;

            shopPanel.SetActive(false);
        }
        else {
            Debug.Log("Not enough gems. Closing shop");
            shopPanel.SetActive(false);
        }
    }
}
