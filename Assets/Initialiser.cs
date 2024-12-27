using UnityEngine;

public class Initialiser : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject[] prefabs;
        
    void Start()
    {
        foreach (var prefab in prefabs)
        {
            Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
