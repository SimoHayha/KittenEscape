using UnityEngine;
using System.Collections;

[RequireComponent (typeof(ParticleSystem))]

public class NeuralLink : MonoBehaviour {

    public Color NormalColor;
    public Color OnHitColor;

    public Transform Player = null;
    public Transform Drone = null;

    public float rotationSpeed = 150.0f;

    private float angle = 0.0f;

	void Start () {
        if (!Player || !Drone)
            gameObject.SetActive(false);
	}
	
	void Update () {
        transform.LookAt(Drone);

        angle += rotationSpeed * Time.deltaTime;
        if (angle > 360.0f)
            angle = 0.0f;
        transform.Rotate(new Vector3(0.0f, 0.0f, angle));

        particleSystem.startLifetime = Vector3.Distance(Player.position, Drone.position) / 2.0f + Random.Range(-0.5f, 0.0f);
	}

    void SetPlayerStun(bool mode)
    {
        if (mode)
            particleSystem.startColor = OnHitColor;
        else
            particleSystem.startColor = NormalColor;
    }
}
