/// Marcos Barrios
/// Interfaces Inteligentes
///
/// Destroy GameObjects using Keyword Recognition to speech process the names.
///

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class MyKeywordRecognizer : MonoBehaviour {

  [SerializeField]
  private KeywordRecognizer keywordRecognizer;

  private GameObject[] gameObjectsToSpawn;

  void Start() {
    gameObjectsToSpawn = GameObject.FindGameObjectsWithTag("Spawnable");
    string[] keywords = getNamesOfSpawnableGameObjects();
    keywordRecognizer = new KeywordRecognizer(keywords);
    keywordRecognizer.OnPhraseRecognized += destroySpawnableOnSpeech;
  }

  public void startRecognition() {
    if (!keywordRecognizer.IsRunning) {
      keywordRecognizer.Start();
    }
  }

  public void stopRecognition() {
    if (keywordRecognizer != null) {
      keywordRecognizer.Stop();
    }
  }

  /// Used to initialize keywords on Start()
  private string[] getNamesOfSpawnableGameObjects() {
    string[] names = new string[gameObjectsToSpawn.Length];
    for (int i = 0; i < gameObjectsToSpawn.Length; i++) {
      names[i] = gameObjectsToSpawn[i].name;
    }
    return names;
  }

  /// Because game is won when there isn't any spawnable left
  private void destroySpawnableOnSpeech(PhraseRecognizedEventArgs args) {
    Debug.Log(args.text);
    foreach (var gameObject in gameObjectsToSpawn) {
      if (args.text == gameObject.name) {
        Destroy(GameObject.Find(args.text + "(Clone)"));
        return; // only destroy one at a time
      }
    }
  }

  void OnApplicationQuit() {
    if (keywordRecognizer != null) {
      keywordRecognizer.Dispose();
    }
  }
}
