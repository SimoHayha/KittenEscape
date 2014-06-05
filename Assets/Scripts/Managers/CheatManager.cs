using UnityEngine;
using System.Collections;

public class CheatManager : MonoBehaviour {

    public ControllerTest controller;
    public CastManager castManager;
    public LevelInfo LevelInfo;

    public bool unlimitedBullet = false;
    public bool noBulletCooldown = false;
    public bool god = false;
    public bool instaWin = false;
    public bool instaLoose = false;

    private float bulletCooldownSave = 0.0f;

	// Use this for initialization
	void Start () {
        bulletCooldownSave = castManager.BulletCooldown;
	}
	
	// Update is called once per frame
	void Update () {
        if (noBulletCooldown)
            castManager.BulletCooldown = 0.0f;
        else
            castManager.BulletCooldown = bulletCooldownSave;

        if (unlimitedBullet)
            LevelInfo.AvailableShot = 999;

        controller.GodMode = god;

        if (instaWin)
            LevelInfo.Win = true;

        if (instaLoose)
            LevelInfo.Loose = true;
	}
}
