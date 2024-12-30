using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class EnemyHealthBar : MonoBehaviour
{
    public Slider slider;
    GameController gameController;
    private int enemyId = -1;
    

    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }
    
    private void Update()
    {
        if (enemyId != -1)
        {
            slider.maxValue = gameController.GetCharacterMaxHealth(enemyId);
            slider.value = gameController.GetCharacterHealth(enemyId);
        }
    }

    public void setEnemyId(int id)
    {
        Debug.Log("Setting enemy id to " + id);
        enemyId = id;
    }
}