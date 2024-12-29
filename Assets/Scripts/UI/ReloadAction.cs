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
    private bool isMovementType = false;
    GameController gameController;

    private int index = -1;

    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        playerId = gameController.GetPlayerId();
    }

    private void Update()
    {
        if (index != -1)
        {
            setStatus();
        }
    }

    public void setIndex(int index)
    {
        this.index = index;
    }

    public void setMovementType(bool isMovement)
    {
        isMovementType = isMovement;
    }


    private void setStatus()
    {
        //IsReloading
        if (!gameController.CanCharacterAttack(playerId, index))
        {
            // if (!isMovementType && (3 == gameController.IsMoving(playerId))|| 2 == gameController.IsMoving(playerId))
            // {
            //     StartCoroutine(ReloadAction());
            // }
            
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
    //         slider.value = (elapsed / targetTime) * 100;
    //         yield return null; // Wait next frame
    //     }
    //
    //     slider.value = 100;
    //     isReloading = false;
    // }

    private IEnumerator setColor()
    {
        if (isMovementType)
        {
            while (!gameController.CanCharacterMove(playerId, index))
            {
                fill.color = Color.gray;
                yield return null;
            }
        }
        else
        {
            while (!gameController.CanCharacterAttack(playerId, index))
            {
                fill.color = Color.gray;
                yield return null;
            }
        }

        fill.color = new Color32(0x6B, 0xBE, 0x38, 0xFF);
    }
}