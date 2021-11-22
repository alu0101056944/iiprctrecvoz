/// Marcos Barrios
/// Interfaces Inteligentes
///
/// Spawn objects within a Bounds Object area.
///

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGameObjectWithinBounds : MonoBehaviour {

  private GameObject[] gameObjectsToSpawn;
  private GameObject[] instantiatedGameObjects;
  private Bounds spawnArea;

  // Start is called before the first frame update
  void Start() {
    GameObject spawner = GameObject.FindWithTag("Spawner");
    spawnArea = spawner.GetComponent<BoxCollider>().bounds;
    gameObjectsToSpawn = GameObject.FindGameObjectsWithTag("Spawnable");
    instantiatedGameObjects = new GameObject[gameObjectsToSpawn.Length];
  }

  /// Game of keywords needs to respawn objects.
  public void spawnAndDestroy() {
    for (int i = 0; i < gameObjectsToSpawn.Length; i++) {
      if (instantiatedGameObjects[i] != null) {
        Destroy(instantiatedGameObjects[i]);
      }
      float xWithinSpawn = Random.Range(spawnArea.min.x, spawnArea.max.x);
      float zWithinSpawn = Random.Range(spawnArea.min.z, spawnArea.max.z);
      Vector3 randomPosWithinSpawn =
          new Vector3(xWithinSpawn, spawnArea.center.y, zWithinSpawn);
      instantiatedGameObjects[i] = 
          Instantiate(gameObjectsToSpawn[i], randomPosWithinSpawn,
              Quaternion.identity);
    }
  }

}
