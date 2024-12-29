using UnityEngine;

public class Initializer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject[] prefabs;
        
    void Start()
    {
        
        // foreach (var prefab in prefabs)
        // {
        //     Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
        // }
        
        Instantiate(prefabs[0], new Vector3(0, 0, 0), Quaternion.identity);
        Instantiate(prefabs[1], new Vector3(0, 0, 0), Quaternion.identity);
        Instantiate(prefabs[2], new Vector3(0,0,0), Quaternion.identity);
        Instantiate(prefabs[3], new Vector3(60,10), Quaternion.identity);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
