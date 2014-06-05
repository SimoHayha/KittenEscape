using UnityEngine;
using System.Collections;

public class PlayerBehavior : MonoBehaviour {

    public LevelInfo LevelInfo;
    public GameObject Trap;

    private ControllerTest controller = null;

    public GameObject catInContact = null;

	// Use this for initialization
	void Start () {
        controller = GetComponent<ControllerTest>();
	}
	
	// Update is called once per frame
	void Update () {
        if (catInContact)
        {
            HUD.Instance.CatchCatActive = true;

            if (Input.GetKey(KeyCode.E))
            {
                if (ProfilManager.Profil.Current.ScreenEnabled && !Trap.active)
                {
                    HUD.Instance.CatTrapEnabled = true;
                    CameraManager.Instance.SetScreenshotMode(true);
                    ControllerManager.Instance.SetScreenshotMode(true);
                    ScreenCamera.Instance.GetComponent<MouseOrbit>().target = collider.transform;
                    ScreenCamera.Instance.Cat = catInContact;
                    ScreenCamera.Instance.Trap = Trap;
                }
                else if (!ProfilManager.Profil.Current.ScreenEnabled && !Trap.active)
                {
                    HUD.Instance.CatTrapEnabled = true;
                    Trap.gameObject.SetActive(true);
                    catInContact.SetActive(false);
                    catInContact = null;
                }

                //HUD.Instance.CatchCatRatio += Time.deltaTime * 0.5f;
            }

            /*if (HUD.Instance.CatchCatRatio >= 1.0f && !Trap.active)
            {
                if (ProfilManager.Profil.Current != null && ProfilManager.Profil.Current.ScreenEnabled)
                {
                    CameraManager.Instance.SetScreenshotMode(true);
                    ControllerManager.Instance.SetScreenshotMode(true);
                    ScreenCamera.Instance.GetComponent<MouseOrbit>().target = collider.transform;
                    ScreenCamera.Instance.Cat = catInContact;
                    ScreenCamera.Instance.Trap = Trap;
                }
            }*/
        }
        else
        {
            HUD.Instance.CatchCatActive = false;
        }
	}

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject == catInContact)
        {
            catInContact = null;
            HUD.Instance.CatchCatRatio = 0.0f;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Warrior")
            controller.ApplyWarriorHit();

        if (collider.tag == "Cat")
        {
            if (EnergyBar.Instance.Active)
                collider.GetComponent<MecanimNavAgent>().KnockBack(transform.forward * 5.0f);
            else
                catInContact = collider.gameObject;
        }

        /*if (collider.tag == "Cat" && !Trap.active)
        {
            if (ProfilManager.Profil.Current != null && ProfilManager.Profil.Current.ScreenEnabled)
            {
                CameraManager.Instance.SetScreenshotMode(true);
                ControllerManager.Instance.SetScreenshotMode(true);
                ScreenCamera.Instance.GetComponent<MouseOrbit>().target = collider.transform;
                ScreenCamera.Instance.Cat = collider.gameObject;
                ScreenCamera.Instance.Trap = Trap;
            }
        }*/

        if (collider.tag == "ShipTeleporter" && Trap.active)
        {
            HUD.Instance.CatTrapEnabled = false;
            Trap.SetActive(false);
            LevelInfo.CurrentKittens++;
            if (ProfilManager.Profil.Current != null)
                ProfilManager.Profil.Current.TotalKitten += 1;
            collider.gameObject.GetComponentInChildren<AudioSource>().Play();
            Debug.Log("Send cat to ship");
        }
    }
}
