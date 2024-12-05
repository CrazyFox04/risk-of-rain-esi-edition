using UnityEngine;

public class ProceduralGeneration : MonoBehaviour
{
    [SerializeField] int width = 128, int height = 128;
    [SerializeField] GameObject dirt_0;
    [SerializeField] GameObject dirt_1;
    [SerializeField] GameObject dirt_2;
    [SerializeField] GameObject dirt_3;
    [SerializeField] int idDirt = 0, idGrass = 1;
    void Start()
    {
        GenerateMap();
    }

    void GenerateMap()
    {
        // GET the total width and total height from the model
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {

            }
        }
    }
}
