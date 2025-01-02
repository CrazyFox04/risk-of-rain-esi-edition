using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    private GameController gameController;
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
        enemyId = id;
    }
}