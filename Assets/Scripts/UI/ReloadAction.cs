using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ReloadActions : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Image fill;
    private int playerId;
    private bool isReloading = false;
    private bool isMovementType = false;
    private GameController gameController;

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

    public void set(int index, bool isMovement = false)
    {
        this.index = index;
        isMovementType = isMovement;
    }
    

    private void setStatus()
    {
        //IsReloading
        if (!gameController.CanCharacterAttack(playerId, index))
        {
            StartCoroutine(setColor());
        }
    }
    

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