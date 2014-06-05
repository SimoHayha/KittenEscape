using UnityEngine;
using System.Collections;

public class Teleporter : MonoBehaviour {

    public string LevelName;

    public int KittenRequired;

    public GameObject Available;

    public Material Green;
    public Material Red;

    void Start()
    {
    }

    void Update()
    {
        if (ProfilManager.Profil.Current.TotalKitten >= KittenRequired)
            Available.renderer.material = Green;
        else
            Available.renderer.material = Red;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (ProfilManager.Profil.Current != null && collider.tag == "Player")
        {
            if (ProfilManager.Profil.Current.TotalKitten >= KittenRequired)
            {
                ControllerManager.Instance.SetOutroMode(true);
                ControllerTest.Instance.ForceStop();
                StartCoroutine(Teleport());
                audio.Play();
            }
        }
    }

    IEnumerator Fade()
    {
        yield return new WaitForSeconds(0.05f);
    }

    IEnumerator Teleport()
    {
        yield return new WaitForSeconds(1.0f);

        Application.LoadLevel(LevelName);
    }
}
