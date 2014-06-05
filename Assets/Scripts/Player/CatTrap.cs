using UnityEngine;
using System.Collections;

public class CatTrap : MonoBehaviour {

    public static CatTrap Instance;

    void Awake()
    {
        Instance = this;
    }

	void Start () {

    }
	
	void Update () {

	}

    public void PutCat()
    {
        enabled = true;
    }

    public bool IsEnabled()
    {
        return enabled;
    }
}
