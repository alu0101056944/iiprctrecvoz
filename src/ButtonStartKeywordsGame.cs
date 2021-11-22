/// Marcos Barrios
/// Interfaces Inteligentes
///
/// Start the keywords game when the player is looking at the button and presses
/// left mouse click.
///

using System.Collections;
using System.Collections.Generic;
using UnityEngine.Windows.Speech;
using UnityEngine;

public class ButtonStartKeywordsGame : MonoBehaviour {

  [SerializeField]
  private float distanceToAllowInteraction = 2f;

  private bool playerCanInteract = false;
  private GameObject player;
  private SpawnGameObjectWithinBounds spawnerScript;
  private MyKeywordRecognizer keywordRecognizerScript;

  // Start is called before the first frame update
  void Start() {
    PlayerRaycast.RaycastCollisionEvent += enableInteraction;
    player = GameObject.FindWithTag("Player");
    spawnerScript = GameObject.FindWithTag("Spawner")
        .GetComponent<SpawnGameObjectWithinBounds>();
    keywordRecognizerScript = GameObject.FindWithTag("KeywordRecognizer")
        .GetComponent<MyKeywordRecognizer>();
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
        keywordRecognizerScript.startRecognition();
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
