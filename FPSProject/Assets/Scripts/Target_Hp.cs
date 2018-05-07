using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target_Hp : MonoBehaviour {

    public float hp;

    public void TakeDamage(float amount)
    {
        hp -= amount;
    }

    void Update()
    {
      if(hp <= 0)
        {
            Destroy(gameObject);
        }      
    }
}
