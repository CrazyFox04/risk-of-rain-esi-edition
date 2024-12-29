
using System.Collections.Generic;
using UnityEngine;


public class GenerateAreas : MonoBehaviour
{
    [SerializeField] private GameController gameController;
    [SerializeField] private GameObject[] areaPrefabs;
    [SerializeField] private int[] areaGIDs;
    [SerializeField] Vector2 areaSize = new(128f, 64f);
    [SerializeField] private int rows = 3;
    [SerializeField] private int columns = 3;

    void Start() {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        if (areaPrefabs.Length == 0) {
            Debug.LogError("Prefabs array is empty. Please add prefabs to the array.");
            return;
        }
        PlaceAreas();
    }

    private void PlaceAreas() {
        
        

        for (int row = 0; row < rows; row++) {
            
            for (int column = 0; column < columns; column++) {
                int prefabGID = gameController.GetAreaGuidCurrentLevel(column, row);

                if (prefabGID == -1) {
                    Debug.LogError($"GID {prefabGID} not found in areaGIDs.");
                    continue; 
                }
                                    
                // Debug.LogError($"Prefab GID: {prefabGID}");
                int prefabIdx = getIdFromGID(prefabGID);

            
                Vector3 position = new Vector3(
                    column * areaSize.x * 0.5f, // x
                    row * areaSize.y * 0.5f,    // y
                    0f                          // z
                );


                // Debug.LogError($"Prefab IDX: {prefabIdx} and Length: {areaPrefabs.Length}");

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
