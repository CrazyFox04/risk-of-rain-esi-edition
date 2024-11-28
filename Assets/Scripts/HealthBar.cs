using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public TextMeshProUGUI text;

    public void setMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        text.text = health + "/" + health;
    }
    
    public void setHealth(int health)
    {
        slider.value = health;
        text.text = health + "/" + slider.maxValue;
    }

}
