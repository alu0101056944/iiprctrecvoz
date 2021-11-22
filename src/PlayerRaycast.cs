/// Marcos Barrios
/// Interfaces Inteligentes
///
/// Raycast from camera view, call event when it colides with something. Meant
/// for knowing what the player is looking at.
///


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycast : MonoBehaviour {

  public delegate void RaycastCollisionDelegate(Collider collider);
  public static event RaycastCollisionDelegate RaycastCollisionEvent;

  [SerializeField]
  private float raycastDistance = 5f;

  /// Draw raycast from camera direction to know what the player is looking at.
  void FixedUpdate() {
    Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
    Debug.DrawRay(transform.position, ray.direction * raycastDistance);
    RaycastHit rayHit; /// to get info of the ray
    if (Physics.Raycast(ray, out rayHit /* it's a reference */)) {
      if(rayHit.collider != null && RaycastCollisionEvent != null) {
        RaycastCollisionEvent(rayHit.collider);
      }
    }
  }
}
