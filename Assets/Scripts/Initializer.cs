using UnityEngine;

public class Initializer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameObject gameController;
    [SerializeField] private GameObject generateWorld;
    [SerializeField] private GameObject gameUi;
    private int currentLevel;
        
    void Start()
    {
        Instantiate(gameController, new Vector3(0, 0, 0), Quaternion.identity);
        Instantiate(generateWorld, new Vector3(0, 0, 0), Quaternion.identity);
        Instantiate(gameUi, new Vector3(0, 0, 0), Quaternion.identity);
    }
}
