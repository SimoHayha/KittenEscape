using UnityEngine;
using System.Collections;

public class DroneHUD : MonoBehaviour {

    public Texture2D KittyTexture = null;
    public float KittyTextureRatio = 1.0f;

    public LevelInfo LevelInfo;

    void OnGUI()
    {
        if (Input.GetMouseButton(1))
        {
            float xPosition = 10.0f;
            Color original = GUI.color;

            for (int i = LevelInfo.Kittens - 1; i >= 0; --i)
            {
                Color texColor = Color.white;

                if (LevelInfo.CurrentKittens > i)
                    texColor.a = 0.25f;
                else
                    texColor.a = 1.0f;

                GUI.color = texColor;
                GUI.DrawTexture(new Rect(xPosition, 10.0f, KittyTexture.width * KittyTextureRatio, KittyTexture.height * KittyTextureRatio), KittyTexture);
                xPosition += KittyTexture.width * KittyTextureRatio;


            }

            Color textColor = Color.white;

            GUI.skin.label.normal.textColor = textColor;
            GUI.Label(new Rect(Screen.width * 0.015f, Screen.height * 0.125f, 100.0f, 20.0f), new GUIContent("Ammo : " + LevelInfo.AvailableShot.ToString()));
            //GUI.Label(new Rect(Screen.width * 0.015f, Screen.height * 0.165f, 200.0f, 20.0f), new GUIContent("Time : " + LevelInfo.Time.ToString()));

            GUI.Label(new Rect(Screen.width * 0.8f, Screen.height * 0.025f, 150.0f, 20.0f), new GUIContent("Profil active : " + ProfilManager.Profil.Current.Name));

            GUI.Label(new Rect(Screen.width * 0.015f, Screen.height * 0.30f, 200.0f, 25.0f), new GUIContent("Ability to sprint : " + (ProfilManager.Profil.Current.canSprint ? "Acquired" : "No")));
            GUI.Label(new Rect(Screen.width * 0.015f, Screen.height * 0.35f, 200.0f, 25.0f), new GUIContent("Ability to shoot : " + (ProfilManager.Profil.Current.canShoot ? "Acquired" : "No")));

            GUI.color = original;
        }
    }
}
