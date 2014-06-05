using UnityEngine;
using System.Collections;

public class OutroManager : MonoBehaviour {

    public static OutroManager Instance;

    public GameObject Ship;

    public GameObject OutroCamera;

    public GameObject[] Waypoints;

    public float MoveSpeed = 1.0f;

    public bool started = false;

    private float weight = 0.0f;

    private int indexInWaypoints = 0;

    private Vector3 startingPoint;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        startingPoint = Ship.transform.position;
    }

	void Update () {
        if (started)
        {
            Debug.Log(weight);
            //Ship.transform.LookAt(Waypoints[indexInWaypoints].transform);
            Ship.transform.position = Vector3.Lerp(startingPoint, Waypoints[indexInWaypoints].transform.position, weight);

            weight += Time.deltaTime * MoveSpeed;
        }
	}

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log(collider.name);
        if (Waypoints[indexInWaypoints] == collider.gameObject)
        {
            indexInWaypoints++;
            weight = 0.0f;

            if (indexInWaypoints >= Waypoints.Length)
            {
                // End of cinematic
            }
            else
            {
                startingPoint = Waypoints[indexInWaypoints - 1].transform.position;
            }
        }
    }
}
