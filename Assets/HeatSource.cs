using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatSource : MonoBehaviour
{
    public float heat = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D col) {
      if(heat != 0) {
        col.attachedRigidbody.velocity += (col.attachedRigidbody.velocity.normalized * heat / Mathf.Pow(Vector2.Distance(transform.position, col.transform.position),2)) * Time.deltaTime;
        //Debug.Log("HEAT+ " + col.gameObject.name);
      }
    }
}
