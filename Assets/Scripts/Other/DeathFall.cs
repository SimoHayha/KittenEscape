using UnityEngine;
using System.Collections;

public class DeathFall : MonoBehaviour {

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log(collider.tag);

        if (collider.tag == "Player")
            LevelInfo.Instance.Dead = true;

        gameObject.SetActive(false);
    }
}
