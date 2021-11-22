/// Marcos Barrios
/// Interfaces Inteligentes
///
/// Start the dictation game when the player is looking at the button and presses
/// left mouse click.
///

using System.Collections;
using System.Collections.Generic;
using UnityEngine.Windows.Speech;
using UnityEngine;

public class ButtonStartDictationGame : MonoBehaviour {

  [SerializeField]
  private float distanceToAllowInteraction = 2f;

  private bool playerCanInteract = false;
  private GameObject player;
  private SpawnGameObjectWithinBounds spawnerScript;
  private MyDictationRecognizer dictationRecognizer;

  // Start is called before the first frame update
  void Start() {
    PlayerRaycast.RaycastCollisionEvent += enableInteraction;
    player = GameObject.FindWithTag("Player");
    spawnerScript = GameObject.FindWithTag("Spawner")
        .GetComponent<SpawnGameObjectWithinBounds>();
    dictationRecognizer = GameObject.FindWithTag("DictationRecognizer")
        .GetComponent<MyDictationRecognizer>();
  }

  void OnDestroy() {
    PlayerRaycast.RaycastCollisionEvent -= enableInteraction;    
  }

  // Update is called once per frame
  void Update() {
    Vector3 vectorToPlayer = transform.position - player.transform.position;
    float distanceToPlayer = vectorToPlayer.magnitude;
    if (distanceToPlayer <= distanceToAllowInteraction && playerCanInteract) {
      if (Input.GetMouseButtonDown(0)) {
        spawnerScript.spawnAndDestroy();
        dictationRecognizer.startRecognition();
      }
    }
  }

  private void enableInteraction(Collider collider) {
    if (collider.gameObject.name == gameObject.name) {
      playerCanInteract = true;
    } else {
      playerCanInteract = false;
    }
  }
}
