using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class wHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI text;
    private GameController gameController;
    

    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }
    
    private void Update()
    {
        setMaxHealth(gameController.GetPlayerMaxHealth());
        setHealth(gameController.GetPlayerCurrentHealth());
    }
    
    private void setMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        text.text = health + "/" + health;
    }

    public void setHealth(int health)
    {
        StartCoroutine(SmoothHealthChange(health));
    }

    private IEnumerator SmoothHealthChange(int targetHealth)
    {
        float currentHealth = slider.value;
        float duration = 0.4f; // Duration of animation
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            slider.value = Mathf.Lerp(currentHealth, targetHealth, elapsed / duration);
            text.text = Mathf.RoundToInt(slider.value) + "/" + slider.maxValue;
            yield return null; // Wait next frame
        }

        slider.value = targetHealth;
        text.text = targetHealth + "/" + slider.maxValue;
    }
}
