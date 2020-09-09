using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    
    public static UIManager Instance{
        get{
            if (_instance == null)
                throw new UnityException("UI Manager not instantiated");

            return _instance;
        }
    }

    public Text playerGemCountText;
    public Text gemCountText;
    public Image SelectionImage;
    public Image[] healthBars;

    public void Awake() {
        _instance = this;
    }

    public void UpdateItemSelction(int yPos){
        SelectionImage.rectTransform.anchoredPosition = new Vector2(SelectionImage.rectTransform.anchoredPosition.x, yPos);
    }

    public void OpenShop(int noOfGems){
        playerGemCountText.text = noOfGems + "G";
    }

    public void UpdateGemCount(int count){
        gemCountText.text = count.ToString();
    }

    public void UpdateLives(int livesRemaining){
        healthBars[livesRemaining].enabled = false;
    }
}
