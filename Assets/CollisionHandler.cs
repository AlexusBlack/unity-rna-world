using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
  // Flag so that coroutine would be launched only once
  private static bool coroutineActive = false;
  // List of all atoms in the simulation
  //private static List<Rigidbody2D> atoms = new List<Rigidbody2D>();

  // Rigidbody component cache
  private Rigidbody2D rb2d;
  private EnergyConservationWatchDog ecwd;
  
  // Record of last magnitude
  public float lastMagnitude = 0;

  // Start is called before the first frame update
  void Start()
  {
    // Caching components
    ecwd = EnergyConservationWatchDog.instance;
    rb2d = GetComponent<Rigidbody2D>();
    ecwd.atoms.Add(rb2d);
    //atoms.Add(rb2d);

    // Launching coroutine if it isn't running yet
    if(!coroutineActive) {
      coroutineActive = true;
      StartCoroutine(CalculateTKEDaemon(1.0f));
    }
  }

  // Total Kinetic Energy Caclulation demon
  IEnumerator CalculateTKEDaemon(float waitTime) {
    while(true) {
      yield return new WaitForSeconds(waitTime);
      Debug.Log("T.K.E. = " + CalculateTKE());
    }
  }

  // Method for Total Kinetic Energy calculation
  float CalculateTKE() {
    float tke = 0;
    foreach(var atom in ecwd.atoms) {
      tke += atom.velocity.magnitude;
    }
    return tke;
  }

  // Update is called once per frame
  // void FixedUpdate()
  // {
  //   // Updating recorded velocity magnitude
  //   lastMagnitude = rb2d.velocity.magnitude;
  // }

  // // Monitoring all colisions
  // void OnCollisionEnter2D(Collision2D col) {
  //   // Participant with highest magnitude will handle the collision
  //   //Debug.Log(rb2d.velocity.magnitude + " : " + col.rigidbody.velocity.magnitude);
  //   if(rb2d.velocity.magnitude > col.rigidbody.velocity.magnitude) {
  //     // Retriving other CollisionHandler
  //     CollisionHandler otherCollisionHandler = col.gameObject.GetComponent<CollisionHandler>();
  //     // Extracting its last magnitude value
  //     float otherLastMagnitude = otherCollisionHandler ? otherCollisionHandler.lastMagnitude : 0;
  //     //Debug.Log("1: " + lastMagnitude + " 2: " + otherLastMagnitude);
  //     //Debug.Log("1: " + rb2d.velocity.magnitude + " 2: " + col.rigidbody.velocity.magnitude);

  //     // Calculating total velocity magnitude before and after the collision
  //     float collisionTotalMagnitude = lastMagnitude + otherLastMagnitude;
  //     float newCollisionTotalMagnitude = rb2d.velocity.magnitude + col.rigidbody.velocity.magnitude;
  //     //Debug.Log("O: " + collisionTotalMagnitude + " N: " + newCollisionTotalMagnitude);

  //     // If new total velocity magnitude gain excess energy or loose energy clamping it to stay on previous level proportionately
  //     if(newCollisionTotalMagnitude != collisionTotalMagnitude) {
  //       // calculating clamping proportions
  //       float ratio1 = rb2d.velocity.magnitude / newCollisionTotalMagnitude;
  //       float ratio2 = col.rigidbody.velocity.magnitude / newCollisionTotalMagnitude;

  //       // clamping veloicty values
  //       //rb2d.velocity = Vector2.ClampMagnitude(rb2d.velocity, collisionTotalMagnitude * ratio1);
  //       rb2d.velocity = rb2d.velocity.normalized * collisionTotalMagnitude * ratio1;
  //       // don't try to clamp walls velocity
  //       //if(otherLastMagnitude != 0) col.rigidbody.velocity = Vector2.ClampMagnitude(col.rigidbody.velocity, collisionTotalMagnitude * ratio2);
  //       if(col.rigidbody.bodyType == RigidbodyType2D.Dynamic) col.rigidbody.velocity = col.rigidbody.velocity.normalized * collisionTotalMagnitude * ratio2;

  //       //Debug.Log("F1: " + rb2d.velocity.magnitude + " 2: " + col.rigidbody.velocity.magnitude);
  //     } //else if (newCollisionTotalMagnitude < collisionTotalMagnitude) Debug.Break();
  //   }
  // }
}
