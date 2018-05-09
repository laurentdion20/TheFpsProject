using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletVelocity : MonoBehaviour {



    public GameObject bullet;
    private float startVelocity;
    private Vector3 StartRotation;

	// Use this for initialization
	void Start () {


        startVelocity = 90f;

         StartRotation = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)).direction;

    }
	
	// Update is called once per frame
	void Update () {
        
        bullet.GetComponent<Rigidbody>().velocity = StartRotation * startVelocity;
    }
}
