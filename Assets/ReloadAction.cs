using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ReloadActions : MonoBehaviour
{
    
    public Slider slider;
    public Image fill;
    private int playerId;
    private bool isReloading = false;
    GameController gameController;

    private int actionIndex = -1;
    
    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        playerId = gameController.GetPlayerId();
    }
    
    private void Update()
    {
        if (actionIndex != -1)
        {
            setStatus();
        }    
    }
    
    public void setAttackIndex(int index)
    {
        actionIndex = index;
    }
    

    private void setStatus()
    {
        //IsReloading
        if (!gameController.CanCharacterAttack(playerId, actionIndex))
        {
            // StartCoroutine(ReloadAction());
            StartCoroutine(setColor());
        }
        
    }
    
    // private IEnumerator ReloadAction()
    // {
    //     isReloading = true;
    //     int targetTime = (int)gameController.GetCharacterCoolDownAttackTime(playerId, 2) + 1;
    //     float elapsed = 0f;
    //
    //     while (elapsed < targetTime)
    //     {
    //         elapsed += Time.deltaTime;
    //         slider.value =  (elapsed / targetTime) * 100;
    //         yield return null; // Wait next frame
    //         
    //     }
    //     slider.value = 100;
    //     isReloading = false;
    // }
    
    private IEnumerator setColor()
    {
        while (!gameController.CanCharacterAttack(playerId, actionIndex))
        {
            fill.color = Color.gray;
            yield return null;    
        }
        fill.color = new Color32(0x6B, 0xBE, 0x38, 0xFF);
    }
}