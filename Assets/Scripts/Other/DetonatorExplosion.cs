using UnityEngine;
using System.Collections;

public class DetonatorExplosion : MonoBehaviour {

    public static DetonatorExplosion Instance;

    public float TimeScale = 0.25f;

    public float TimeExplosion = 5.0f;

    public float TimeBetweenExplosion = 2.0f;

    public float TimeBetweenSubExplosion = 0.5f;

    public GameObject Explosion;

    public GameObject SubExplosion;

    public GameObject LastExplosion;

    private bool once = false;

    private bool go = false;

    private float step = float.MaxValue;

    private float explosion = float.MaxValue;

    private float subExplosion = float.MaxValue;

    private Vector3[] rand = new Vector3[256];

    private int index = 0;

    private BoxCollider box;

    void Awake()
    {
        box = GetComponent<BoxCollider>();

        Instance = this;

        for (int i = 0; i < rand.Length; ++i)
        {
            rand[i] = new Vector3(transform.position.x + Random.Range(-box.size.x, box.size.x),
                                  transform.position.y + Random.Range(-box.size.y, box.size.y),
                                  transform.position.z + Random.Range(-box.size.z, box.size.z));
            rand[i] = transform.TransformPoint(rand[i] / 2.5f);
        }
    }

	void Start () {

	}
	
	void Update () {
        if (go)
        {
            index++;

            if (index >= rand.Length)
                index = 0;

            if (Time.time > step)
            {
                Time.timeScale = 1.0f;
                Application.LoadLevel("Test3C");
            }

            if (Time.time > explosion)
            {
                explosion += TimeBetweenExplosion;
                GameObject exp = Instantiate(Explosion, rand[index], Quaternion.identity) as GameObject;
                Destroy(exp, 5.0f);
                renderer.enabled = false;

            }

            if (Time.time > subExplosion)
            {
                subExplosion += TimeBetweenSubExplosion;
                GameObject exp = Instantiate(SubExplosion, rand[index], Quaternion.identity) as GameObject;
                Destroy(exp, 5.0f);
            }

            /*if (Time.time > step * 0.5f && !once)
            {
                once = true;
                GameObject exp = Instantiate(LastExplosion, rand[index], Quaternion.identity) as GameObject;
                Destroy(exp, 5.0f);
            }*/


            //step += Time.deltaTime;
            //explosion += Time.deltaTime;
        }
	}

    /*Vector3 GetSpawnPosition()
    {
        BoxCollider box = GetComponent<BoxCollider>();
        float t = Random.value;
        Vector3 localPosition = Vector3.Lerp((box.center - box.size) / 2.0f, (box.center + box.size) / 2.0f, t);

        return this.transform.TransformPoint(localPosition);
    }*/

    public void Activate()
    {
        go = true;
        step = Time.time + TimeExplosion;
        explosion = Time.time + TimeBetweenExplosion;
        subExplosion = Time.time + TimeBetweenSubExplosion;
        Time.timeScale = TimeScale;

        rigidbody.isKinematic = false;
        //rigidbody.useGravity = true;
    }
}
