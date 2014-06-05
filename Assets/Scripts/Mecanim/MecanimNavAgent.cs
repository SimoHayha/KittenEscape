using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Animator))]
[RequireComponent (typeof(NavMeshAgent))]

public class MecanimNavAgent : MonoBehaviour {

    public Vector3 Target;

    private float speed = 1.0f;
    public float Speed
    {
        get
        {
            return speed;
        }

        set
        {
            speed = value;
            Agent.speed = speed;
        }
    }

    private Animator Anim;
    private NavMeshAgent Agent;

    private bool knockedBack = false;

    private float knockOutRecovery;

	void Start () {
        Agent = GetComponent<NavMeshAgent>();
        Anim = GetComponent<Animator>();
	}
	
	void Update () {
        if (knockedBack)
        {
            if (Time.time > knockOutRecovery)
            {
                knockedBack = false;
                Agent.acceleration = 8.0f;
                // Trick to reset speed at normal speed
                LevelInfo.Instance.CurrentKittens = LevelInfo.Instance.CurrentKittens;
            }
        }
        else
        {
            if (Target != Vector3.zero)
            {
                Agent.SetDestination(Target);
                Anim.SetFloat("Speed", Agent.speed);
            }
        }
	}

    public void Stop()
    {
        Agent.Stop(true);
    }

    public void Resume()
    {
        Agent.Resume();
    }

    public void KnockBack(Vector3 direction)
    {
        if (!knockedBack)
        {
            knockedBack = true;
            knockOutRecovery = Time.time + 2.0f;
            Agent.SetDestination(transform.position + direction);
            Agent.speed = 50.0f;
            Agent.acceleration = 50.0f;
        }
    }
}
