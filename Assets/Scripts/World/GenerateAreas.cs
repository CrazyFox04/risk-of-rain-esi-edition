
using System.Collections.Generic;
using UnityEngine;


public class GenerateAreas : MonoBehaviour
{
    public GameController gameController;
    public GameObject[] areaPrefabs;
    public int[] areaGIDs;
    public Vector2 areaSize = new(128f, 64f);
    public int rows = 3;
    public int columns = 3;
    public GameObject player;

    void Start() {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        PlaceAreas();
    }

    private void PlaceAreas() {
    for (int row = -1; row <= rows; row++) {
        for (int column = -1; column <= columns; column++) {
            if (row == -1 || row == rows || column == -1 || column == columns) {
                // Place the border prefab
                Vector3 position = new Vector3(
                    column * areaSize.x * 0.5f, // x
                    row * areaSize.y * 0.5f,    // y
                    0f                          // z
                );
                Instantiate(areaPrefabs[0], position, Quaternion.identity, transform);
            } else {
                // Place the regular area prefab
                int prefabGID = gameController.GetAreaGuidCurrentLevel(column, row);

                if (prefabGID == -1) {
                    Debug.LogError($"GID {prefabGID} not found in areaGIDs.");
                    continue;
                }

                int prefabIdx = getIdFromGID(prefabGID);

                Vector3 position = new Vector3(
                    column * areaSize.x * 0.5f, // x
                    row * areaSize.y * 0.5f,    // y
                    0f                          // z
                );

                GameObject area = Instantiate(areaPrefabs[prefabIdx], position, Quaternion.identity, transform);

                if (row == 0 && column == 0) {
                    Transform playerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawn").transform;
                    Instantiate(player, playerSpawn.position, Quaternion.identity);
                }

                EnemySpawner[] spawners = area.GetComponentsInChildren<EnemySpawner>();
                for (int i = 0; i < spawners.Length; i++) {
                    spawners[i].set(row, column, i+1);
                }
                BossSpawner[] bossSpawners = area.GetComponentsInChildren<BossSpawner>();
                for (int i = 0; i < bossSpawners.Length; i++) {
                    bossSpawners[i].set(row, column, 3);
                }
                Chest[] chests = area.GetComponentsInChildren<Chest>();
                for (int i = 0; i < chests.Length; i++) {
                    chests[i].set(row, column, i+1);
                }
            }
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
