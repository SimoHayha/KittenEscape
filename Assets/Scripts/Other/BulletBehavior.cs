using UnityEngine;
using System.Collections;

public class BulletBehavior : MonoBehaviour {

    public float Lifetime = 5.0f;

    private float aliveTimer = 0.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //transform.forward = rigidbody.velocity;

        aliveTimer += Time.deltaTime;

        if (aliveTimer > Lifetime)
            Destroy(gameObject);
	}
    
    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
