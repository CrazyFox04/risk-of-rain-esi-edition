
using System.Collections.Generic;
using UnityEngine;


public class GenerateAreas : MonoBehaviour
{
    [SerializeField] private GameObject[] areaPrefabs;
    [SerializeField] private int[] areaGIDs;
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

                int prefabGID = 111; // TODO: Method from the model to get the index of the prefab
                                    // getAreaID(int x, int y) -> int AreaID (index of the are in the index list ==> voir organisation)
                
                int prefabIdx = getIdFromGID(prefabGID);

                if (prefabIdx == -1) {
                    Debug.LogError($"GID {prefabGID} not found in areaGIDs.");
                    continue; 
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

    private int getIdFromGID(int gid) {
        int index = 0;

        while (index < areaGIDs.Length && areaGIDs[index] != gid) {
            index++;
        }

        return (index < areaGIDs.Length) ? index : -1;
    }
}
