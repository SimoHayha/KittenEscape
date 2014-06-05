using UnityEngine;
using System.Collections;

public class CastManager : MonoBehaviour {

    public static CastManager Instance;

    public LevelInfo LevelInfo;

    public GameObject Bullet;
    public Transform WeaponPosition;
    public Transform FpsCamera;

    public float Recovery1 = 1.0f;
    public float Recovery2 = 2.0f;
    public float Recovery3 = 5.0f;
    public float Recovery4 = 10.0f;

    public float BulletVelocity = 0.0f;
    public float MaxBulletVelocity = 10.0f;

    public float BulletCooldown = 3.0f;
    public bool CanShootBullet = false;

    private bool isFps = false;

    private float timeLeftButtonDown = 0.0f;

    private float nextBulletAttackAvailable = 0.0f;

    void Awake()
    {
        Instance = this;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        CanShootBullet = Time.time > nextBulletAttackAvailable && ProfilManager.Profil.Current.canShoot;

        if (Input.GetMouseButtonDown(1))
        {
            isFps = true;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            isFps = false;
        }

        if (isFps)
        {
            WeaponPosition.forward = FpsCamera.forward;

            if (Input.GetMouseButton(0))
            {
                if (CanShootBullet)
                {
                    timeLeftButtonDown += Time.deltaTime * 1.5f;

                    BulletVelocity = Mathf.Lerp(0.0f, MaxBulletVelocity, timeLeftButtonDown);
                }
            }
            else
            {
                if (timeLeftButtonDown > 0.0f)
                {
                    timeLeftButtonDown -= Time.deltaTime * 2.5f;
                    BulletVelocity = Mathf.Lerp(0.0f, MaxBulletVelocity, timeLeftButtonDown);
                }
            }


            if (Input.GetMouseButtonUp(0) && CanShootBullet)
            {
                //SendMessage("ShootBullet");
                if (LevelInfo.AvailableShot >= 2)
                {
                    LevelInfo.AvailableShot -= 2;
                    GameObject obj = Instantiate(Bullet, FpsCamera.position + FpsCamera.right * 0.5f, Quaternion.identity) as GameObject;
                    obj.rigidbody.velocity = FpsCamera.forward * (BulletVelocity + 7.5f);
                    obj = Instantiate(Bullet, FpsCamera.position - FpsCamera.right * 0.5f, Quaternion.identity) as GameObject;
                    obj.rigidbody.velocity = FpsCamera.forward * (BulletVelocity + 7.5f);
                    nextBulletAttackAvailable = Time.time + BulletCooldown;
                }
            }
        }
        else
        {
            BulletVelocity = 0.0f;
            timeLeftButtonDown = 0.0f;
        }
	}
}
