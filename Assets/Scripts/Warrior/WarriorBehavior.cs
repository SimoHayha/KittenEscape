using UnityEngine;
using System.Collections;

public class WarriorBehavior : MonoBehaviour {

    public GameObject Idle;
    public GameObject Run;
    public LayerMask Ennemy;
    public NavMeshAgent Agent = null;
    public Transform[] NavPoints = null;
    public float IdleTime = 2.0f;
    public Transform Target = null;
    public float BulletRecovery = 5.0f;
    public float BlinkRecovery = 0.1f;

    private BoxCollider collider = null;
    private float nextTick = 0.0f;
    private bool stunned = false;

	void Start () {
        collider = GetComponent<BoxCollider>();
        SetRandomTarget();
	}
	
	void Update () {
	    if (Agent.velocity == Vector3.zero)
        {
            Idle.SetActive(true);
            Run.SetActive(false);
        }
        else
        {
            Idle.SetActive(false);
            Run.SetActive(true);
        }

        if (Time.time > nextTick)
        {
            stunned = false;
            Agent.Resume();
            if (Target)
                Agent.SetDestination(Target.position);
        }

        if (Target)
        {
            if (Target.tag != "Player")
                SetRandomTarget();
            else
            {
                if (Vector3.Distance(Target.position, transform.position) > 10.0f)
                    SetRandomTarget();
            }
        }

        Collider[] objects = Physics.OverlapSphere(transform.position, 10.0f, Ennemy);
        for (int i = 0; i < objects.Length; ++i)
        {
            if (objects[i].tag == "Player" && !stunned)
            {
                Target = objects[i].transform;
                nextTick = -1.0f;
            }
        }
	}

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Bullet")
            ApplyBulletHit();

        if (collider.tag != "NavPoints")
            return;

        SetRandomTarget();

        nextTick = Time.time + IdleTime;
    }

    void SetRandomTarget()
    {
        bool ok = false;
        int rand = -1;

        while (!ok)
        {
            rand = Random.Range(0, NavPoints.Length);

            if (NavPoints[rand].transform.position.x != Agent.destination.x &&
                NavPoints[rand].transform.position.z != Agent.destination.z)
                ok = true;
        }

        Target = NavPoints[rand].transform;
    }

    void ApplyBulletHit()
    {
        if (!stunned)
        {
            stunned = true;
            Agent.Stop(true);
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
