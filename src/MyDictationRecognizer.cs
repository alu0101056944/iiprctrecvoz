/// Marcos Barrios
/// Interfaces Inteligentes
///
/// Destroy GameObjects using Dictation Recognition to speech process the names.
/// "eliminar" is the command for object destruction.
///

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class MyDictationRecognizer : MonoBehaviour {

  private DictationRecognizer dictationRecognizer;

  private GameObject[] gameObjectsToSpawn;

  // Start is called before the first frame update
  void Start() {
    gameObjectsToSpawn = GameObject.FindGameObjectsWithTag("Spawnable");
    dictationRecognizer = new DictationRecognizer();
    subscribeToEvents();
  }

  public void startRecognition() {
    if (dictationRecognizer.Status != SpeechSystemStatus.Running) {
      dictationRecognizer.Start();
    }
  }

  public void stopRecognition() {
    if (dictationRecognizer != null) {
      dictationRecognizer.Stop();
    }
  }

  private void subscribeToEvents() {
    dictationRecognizer.DictationResult += processDictationResult;
    dictationRecognizer.DictationHypothesis += (text) => {
      Debug.LogFormat("Dictation hypothesis: {0}", text);
    };
    dictationRecognizer.DictationComplete += (completionCause) => {
      if (completionCause != DictationCompletionCause.Complete) {
        Debug.LogErrorFormat("Dictation completed unsuccessfully: {0}.", completionCause);
      }
    };
    dictationRecognizer.DictationError += (error, hresult) => {
      Debug.LogErrorFormat("Dictation error: {0}; HResult = {1}.", error, hresult);
    };
  }

  /// "eliminar" is the command for object destruction
  private void processDictationResult(string text,
      ConfidenceLevel confidence) {
    Debug.LogFormat("Dictation result: {0}", text);
    string[] words = text.Split(' ');
    if (words[0] == "eliminar") {
      destroySpawnablesMatchedByName(words);
    }
  }

  /// Because a phrase is given with a lot of words and it is simple to just
  /// search if any word matches a spawnable's name.
  private void destroySpawnablesMatchedByName(string[] wordsMaybeSpawnable) {
    foreach (var name in wordsMaybeSpawnable) {
      foreach (var gameObject in gameObjectsToSpawn) {
        if (name == gameObject.name) {
          Destroy(GameObject.Find(name + "(Clone)"));
        }
      }
    }
  }

  void OnApplicationQuit() {
    if (dictationRecognizer != null) {
      dictationRecognizer.Dispose();
    }
  }

}
