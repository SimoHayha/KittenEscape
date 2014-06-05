using UnityEngine;
using System.Collections;

public class DroneBehavior : MonoBehaviour {

    public static DroneBehavior Instance;

    private bool isUnderwater = false;
    public bool IsUnderwater
    {
        get
        {
            return isUnderwater;
        }
        set
        {
            isUnderwater = value;
            Debug.Log(value);
        }
    }

    public bool LightOn = false;

    public float maxDistance = 2.0f;
    public float minDistance = 0.1f;
    public float OffsetFromWall = 0.1f;
    public LayerMask CollisionLayer = -1;

    public Transform target;
    public Transform darkPlacePosition;
    public Transform head;

    public float rotationSpeed = 1.0f;

    private float angle = 0.0f;
    public bool inDarkPlace = false;

    private MeshRenderer[] renderers;

    public float TargetHeight = 1.0f;

    public GameObject Led;

    private float desiredDistance;
    //private float distance;
    private float correctedDistance;
    private float currentDistance;

    private Light Torch;

    void Awake()
    {
        Instance = this;
    }

	// Use this for initialization
	void Start () {
        renderers = GetComponentsInChildren<MeshRenderer>();
        Torch = FindDroneLight("DroneTorch");
	}
	
	// Update is called once per frame
	void Update () {
        Torch.enabled = LightOn;

        if (!Input.GetMouseButton(1))
            angle += rotationSpeed * Time.deltaTime;

        if (target)
        {
            transform.LookAt(target);
        }

        if (inDarkPlace)
        {
            float distance = Vector3.Distance(transform.position, darkPlacePosition.position);
            Vector3 posTarget = Vector3.Lerp(transform.position, darkPlacePosition.position, distance / 20.0f);

            transform.position = posTarget;
        }
        else
        {
            if (head)
            {
                Vector3 posTarget = Vector3.Lerp(transform.position, GetPosAroundHead(), currentDistance);

                transform.position = posTarget;

                if (transform.InverseTransformPoint(head.position).z >= -0.1f)
                {
                    for (int i = 0; i < renderers.Length; ++i)
                    {
                        Color c = renderers[i].material.color;
                        c.a = 0.8f;
                        renderers[i].material.color = c;
                    }
                }
                else
                {
                    for (int i = 0; i < renderers.Length; ++i)
                    {
                        Color c = renderers[i].material.color;
                        c.a = 1.0f;
                        renderers[i].material.color = c;
                    }
                }
            }
        }

        if (IsUnderwater)
            Led.SetActive(false);
        else
            Led.SetActive(true);

        bool active = !Input.GetMouseButton(1);
        for (int i = 0; i < renderers.Length; ++i)
            renderers[i].enabled = active;
	}

    Vector3 GetPosAroundHead()
    {
        Vector3 vTargetOffset;

        desiredDistance = Mathf.Clamp(desiredDistance + 2.0f, minDistance, maxDistance);
        correctedDistance = desiredDistance;

        vTargetOffset = new Vector3(0.0f, -TargetHeight, 0.0f);

        Vector3 position = Vector3.zero;

        position.x = head.position.x + Mathf.Cos(angle) * desiredDistance;
        position.y = head.position.y;
        position.z = head.position.z + Mathf.Sin(angle) * desiredDistance;

        RaycastHit collisionHit;
        Vector3 trueTargetPosition = new Vector3(head.position.x, head.position.y + TargetHeight, head.position.z);

        bool isCorrected = false;
        if (Physics.Linecast(trueTargetPosition, position, out collisionHit, CollisionLayer))
        {
            correctedDistance = Vector3.Distance(trueTargetPosition, collisionHit.point) - OffsetFromWall;
            isCorrected = true;
        }

        currentDistance = !isCorrected || correctedDistance > currentDistance ? Mathf.Lerp(currentDistance, correctedDistance, Time.deltaTime) : correctedDistance;

        currentDistance = Mathf.Clamp(currentDistance, minDistance, maxDistance);

        position.x = head.position.x + Mathf.Cos(angle) * currentDistance;
        position.y = head.position.y;
        position.z = head.position.z + Mathf.Sin(angle) * currentDistance;

        return position;
    }

    Light FindDroneLight(string tag)
    {
        Light[] lights = GetComponentsInChildren<Light>();

        for (int i = 0; i < lights.Length; ++i)
        {
            if (lights[i].tag == tag)
                return lights[i];
        }

        return null;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "DroneLightZone")
        {
            Light torch = FindDroneLight("DroneTorch");
            Light led = FindDroneLight("DroneLed");

            if (torch)
                LightOn = true;
                //torch.enabled = true;
            if (led)
                led.light.intensity = 2.0f;

            inDarkPlace = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "DroneLightZone")
        {
            Light torch = FindDroneLight("DroneTorch");
            Light led = FindDroneLight("DroneLed");

            if (torch)
                LightOn = false;
                //torch.enabled = false;
            if (led)
                led.light.intensity = 0.5f;

            inDarkPlace = false;
        }
    }
}
