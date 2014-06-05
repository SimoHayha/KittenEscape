using UnityEngine;
using System.Collections;

[RequireComponent (typeof(NavMeshAgent))]

public class CatBehavior : MonoBehaviour {

    public Transform Target;

    public LayerMask Ennemy;

    public float BulletRecovery = 2.0f;

    public float BlinkRecovery = 0.1f;

    private bool stunned = false;

    private MecanimNavAgent Agent;

    private float nextTick = -1.0f;

	void Start () {
        Agent = GetComponent<MecanimNavAgent>();
	}
	
	void Update () {
        if (Time.time > nextTick)
        {
            stunned = false;
            Agent.Resume();
        }

        if (!stunned)
        {
            Collider[] objects = Physics.OverlapSphere(transform.position, 10.0f, Ennemy);
            for (int i = 0; i < objects.Length; ++i)
            {
                if (objects[i].tag == "Player")
                {
                    Vector3 escapeVector = transform.position - objects[i].transform.position;

                    Agent.Target = transform.position + escapeVector.normalized;
                    nextTick = -1.0f;
                }
            }
        }
        else
            Agent.Target = Vector3.zero;
	}

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Bullet")
            ApplyBulletHit();
    }

    void ApplyBulletHit()
    {
        if (!stunned)
        {
            stunned = true;
            Agent.Stop();
            nextTick = Time.time + BulletRecovery;
            StartCoroutine(Blink());
        }
    }

    IEnumerator Blink()
    {
        SkinnedMeshRenderer[] myRend = GetComponentsInChildren<SkinnedMeshRenderer>(true);
        bool myState = true;

        while (stunned)
        {
            myState = !myState;
            for (int i = 0; i < myRend.Length; ++i)
                myRend[i].enabled = myState;
            yield return new WaitForSeconds(BlinkRecovery);
        }

        for (int i = 0; i < myRend.Length; ++i)
            myRend[i].enabled = true;
    }
}
