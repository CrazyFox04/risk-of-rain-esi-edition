using UnityEngine;

public class Initializer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject[] prefabs;
        
    void Start()
    {
        
        foreach (GameObject prefab in prefabs)
        {
            Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
            
        }

    }
}
