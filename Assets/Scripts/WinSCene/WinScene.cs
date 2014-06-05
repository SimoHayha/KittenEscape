using UnityEngine;
using System.Collections;

public class WinScene : MonoBehaviour {

    public GameObject CatPrefab;

    public GameObject Explosion;

    public int NumberOfCat = 100;

    public float TimeBeforeDetonation = 2.0f;

    public float TimeScale = 0.25f;

    public float TimeToLobby = 7.5f;

    private float detonationTime;

    private BoxCollider box;

    private float nextTimeLobby;

	void Start () {

        box = GetComponent<BoxCollider>();

	    for (int i = 0; i < NumberOfCat; ++i)
        {
            Vector3 pos = new Vector3(box.center.x + Random.Range(-box.size.x, box.size.x),
                                      1.0f,
                                      box.center.z + Random.Range(-box.size.z, box.size.z));

            pos *= 4.0f;

            Instantiate(CatPrefab, pos, Random.rotation);
        }

        detonationTime = Time.time + TimeBeforeDetonation;

        nextTimeLobby = Time.time + TimeToLobby;
	}
	
	void Update () {
        if (Time.time > detonationTime)
        {
            Instantiate(Explosion, Vector3.zero, Quaternion.identity);
            Time.timeScale = TimeScale;
            detonationTime = float.MaxValue;
        }

        if (Time.time > nextTimeLobby || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            Application.LoadLevel("Test3C");
            Time.timeScale = 1.0f;
        }
	}
}
