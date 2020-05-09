using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private static bool coroutineActive = false;
    private static List<Rigidbody2D> atoms = new List<Rigidbody2D>();
    private Rigidbody2D rb2d;
    private bool primaryCollider = false;
    //private float collisionTotalMagnitude = 0;
    
    public float lastMagnitude = 0;

    // Start is called before the first frame update
    void Start()
    {
      rb2d = GetComponent<Rigidbody2D>();
      atoms.Add(rb2d);

      if(!coroutineActive) {
        coroutineActive = true;
        StartCoroutine(CalculateTKEDaemon(1.0f));
      }
    }

    // Update is called once per frame
    void Update()
    {
      if(lastMagnitude == 0) lastMagnitude = rb2d.velocity.magnitude;
    }

    IEnumerator CalculateTKEDaemon(float waitTime) {
      while(true) {
        yield return new WaitForSeconds(waitTime);
        Debug.Log("T.K.E. = " + CalculateTKE());
      }
    }

    float CalculateTKE() {
      float tke = 0;
      foreach(var atom in atoms) {
        tke += atom.velocity.magnitude;
      }
      return tke;
    }

    void OnCollisionEnter2D(Collision2D col) {
      //Debug.Log("OnCollisionEnter2D");
      if(rb2d.velocity.magnitude > col.rigidbody.velocity.magnitude) {
        
        CollisionHandler otherCollisionHandler = col.gameObject.GetComponent<CollisionHandler>();
        float otherLastMagnitude = otherCollisionHandler ? otherCollisionHandler.lastMagnitude : 0;
        Debug.Log("1: " + lastMagnitude + " 2: " + otherLastMagnitude);
        Debug.Log("1: " + rb2d.velocity.magnitude + " 2: " + col.rigidbody.velocity.magnitude);

        float collisionTotalMagnitude = lastMagnitude + otherLastMagnitude;
        float newCollisionTotalMagnitude = rb2d.velocity.magnitude + col.rigidbody.velocity.magnitude;

        if(newCollisionTotalMagnitude > collisionTotalMagnitude) {
          float ratio1 = rb2d.velocity.magnitude / newCollisionTotalMagnitude;
          float ratio2 = col.rigidbody.velocity.magnitude / newCollisionTotalMagnitude;

          rb2d.velocity = Vector2.ClampMagnitude(rb2d.velocity, collisionTotalMagnitude * ratio1);
          col.rigidbody.velocity = Vector2.ClampMagnitude(col.rigidbody.velocity, collisionTotalMagnitude * ratio2);

          Debug.Log("1: " + rb2d.velocity.magnitude + " 2: " + col.rigidbody.velocity.magnitude);
        }
        lastMagnitude = rb2d.velocity.magnitude;
        if(otherCollisionHandler) otherCollisionHandler.lastMagnitude = col.rigidbody.velocity.magnitude; 
      }
    }

    // void OnCollisionExit2D(Collision2D col) {
    //   if(primaryCollider) {
    //     primaryCollider = false;
    //     float newCollisionTotalMagnitude = rb2d.velocity.magnitude + col.rigidbody.velocity.magnitude;
    //     float keLoss = newCollisionTotalMagnitude - collisionTotalMagnitude;

    //     Debug.Log("1: " + rb2d.velocity.magnitude + " 2: " + col.rigidbody.velocity.magnitude);
    //     //Debug.Log("K.E.Loss = " + keLoss);

    //     if(keLoss > 0) {
    //       //Debug.Log(name + " - " + col.gameObject.name + " - " + keLoss);
    //       float ratio1 = rb2d.velocity.magnitude / newCollisionTotalMagnitude;
    //       float ratio2 = col.rigidbody.velocity.magnitude / newCollisionTotalMagnitude;

    //       // TODO: clamp magnitude proportionaly to keep K.E. same
    //       Debug.Log(rb2d.velocity + " -> " + collisionTotalMagnitude * ratio1);
    //       Debug.Log(col.rigidbody.velocity + " -> " + collisionTotalMagnitude * ratio2);
    //       rb2d.velocity = Vector2.ClampMagnitude(rb2d.velocity, collisionTotalMagnitude * ratio1);
    //       col.rigidbody.velocity = Vector2.ClampMagnitude(col.rigidbody.velocity, collisionTotalMagnitude * ratio2);
    //       
    //       //Debug.Break();
    //     }
    //     //totalKE -= keLoss;
    //     //Debug.Log("K.E.Loss: " + keLoss);
    //     //if(keLoss > 1) Debug.Break();
    //     //if(keLoss > 1) Debug.Log("T.K.E. = " + totalKE);
    //   }
    // }
}
