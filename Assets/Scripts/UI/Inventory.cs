using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public TextMeshProUGUI amount0;
    public TextMeshProUGUI amount1;
    public TextMeshProUGUI amount2;
    public TextMeshProUGUI amount3;
    public TextMeshProUGUI amount4;
    public TextMeshProUGUI amount5;
    
    public Image image0;
    public Image image1;
    public Image image2;
    public Image image3;
    public Image image4;
    public Image image5;
    
    private GameController gameController;
    
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    void FixedUpdate()
    {
        amount0.text = gameController.getNumberOfItem(gameController.GetPlayerId(), 0).ToString();
        amount1.text = gameController.getNumberOfItem(gameController.GetPlayerId(), 1).ToString();
        amount2.text = gameController.getNumberOfItem(gameController.GetPlayerId(), 2).ToString();
        amount3.text = gameController.getNumberOfItem(gameController.GetPlayerId(), 3).ToString();
        amount4.text = gameController.getNumberOfItem(gameController.GetPlayerId(), 4).ToString();
        amount5.text = gameController.getNumberOfItem(gameController.GetPlayerId(), 5).ToString();
        UpdateItemColor(amount0, image0, 0);
        UpdateItemColor(amount1, image1, 1);
        UpdateItemColor(amount2, image2, 2);
        UpdateItemColor(amount3, image3, 3);
        UpdateItemColor(amount4, image4, 4);
        UpdateItemColor(amount5, image5, 5);
    }
    private void UpdateItemColor(TextMeshProUGUI amountText, Image itemImage, int itemIndex)
    {
        int amount = gameController.getNumberOfItem(gameController.GetPlayerId(), itemIndex);
        amountText.text = amount.ToString();
        itemImage.color = amount == 0 ? new Color(0.5f, 0.5f, 0.5f, 0.5f) : Color.white;
    }
}
