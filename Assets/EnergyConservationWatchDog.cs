using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyConservationWatchDog : MonoBehaviour
{
  public static EnergyConservationWatchDog instance = null;

  public float intervalSeconds = 1.0f;
  public float totalSystemEnergy = 40.0f;
  public List<Rigidbody2D> atoms = new List<Rigidbody2D>();
  // Start is called before the first frame update
  void Start()
  {
    if(instance == null) {
      instance = this;
      StartCoroutine(RestoreEnergyBallanceCoroutine());
    } else Debug.LogError("Only one Energy Conservation Watch Dog allowed!");
  }

  IEnumerator RestoreEnergyBallanceCoroutine() {
    while(true) {
      yield return new WaitForSeconds(intervalSeconds);
      RestoreEnergyBallance();
    }
  }

  void RestoreEnergyBallance() {
    float tke = 0;
    foreach(var atom in atoms) tke += atom.velocity.magnitude;
    foreach(var atom in atoms) atom.velocity = atom.velocity.normalized * totalSystemEnergy * (atom.velocity.magnitude / tke);
  }

  // Update is called once per frame
  void Update()
  {
      
  }
}
