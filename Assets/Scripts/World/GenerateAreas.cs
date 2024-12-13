
using System.Collections.Generic;
using UnityEngine;


public class GenerateAreas : MonoBehaviour
{
    [SerializeField] private GameObject[] areaPrefabs;
    [SerializeField] Vector2 areaSize = new(128f, 128f);
    [SerializeField] private int rows = 2;
    [SerializeField] private int columns = 3;
    void Start()
    {
        if (areaPrefabs.Length == 0) {
            Debug.LogError("Prefabs array is empty. Please add prefabs to the array.");
            return;
        }
        PlaceAreas();
    }

    private void PlaceAreas() {
        for (int row = 0; row < rows; row++) {
            for (int column = 0; column < columns; column++) {
                int prefabIdx = 0; // TODO: Method from the model to get the index of the prefab
                                    // getAreaID(int x, int y) -> int AreaID (index of the are in the index list ==> voir organisation)
                if (prefabIdx < 0 || prefabIdx >= areaPrefabs.Length) {
                    Debug.LogError($"Invalid prefab index {prefabIdx} returned by the MODEL");
                    return;
                }

                Vector3 position = new Vector3(
                    column * areaSize.x * 0.7f, // x
                    row * areaSize.y * 0.7f,    // y
                    0f                          // z
                );

                Instantiate(areaPrefabs[prefabIdx], position, Quaternion.identity, transform);
            }
        }
    }
}
