using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class force : MonoBehaviour
{
    private Rigidbody2D rb2D;  

    public float thrust = 10.0f;

    public bool randomDirection = true;
    public Vector2 direction = new Vector2(0,0);

    // Start is called before the first frame update
    void Start()
    {
      rb2D = GetComponent<Rigidbody2D>();
      //rb2D.AddForce(transform.up * thrust);
      if(randomDirection) {
        rb2D.AddForce(Random.insideUnitCircle.normalized * thrust);
      } else {
        rb2D.AddForce(direction.normalized * thrust);
      }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
