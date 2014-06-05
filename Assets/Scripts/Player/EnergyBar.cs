using UnityEngine;
using System.Collections;

public class EnergyBar : MonoBehaviour {

    public static EnergyBar Instance;

    public float Energy = 100.0f;

    public float CostPerSecond = 25.0f;

    public float DeadTimeBeforeRecovery = 2.0f;

    public float RecoveryPerSecond = 20.0f;

    public bool Show = false;

    public bool Active = false;

    public GameObject Dash;

    public GUIStyle MyStyle;

    private float nextDeadTimeTick = float.MaxValue;

    void Awake()
    {
        Instance = this;
    }

	void Update () {
        Show = ProfilManager.Profil.Current.canSprint;
        Active = Show && Input.GetButton("Run") && Energy > 0.0f;
        bool released = Input.GetButtonUp("Run");

        if (Active && ControllerTest.Instance.inputY > 0.0f)
        {
            Dash.SetActive(true);
            Energy -= CostPerSecond * Time.deltaTime;
            if (Energy <= 0.0f)
                Energy = 0.0f;
        }
        else
        {
            Dash.SetActive(false);
            if (released)
            {
                nextDeadTimeTick = Time.time + DeadTimeBeforeRecovery;
                released = false;
                Active = false;
            }

            if (Time.time > nextDeadTimeTick)
            {
                Energy += RecoveryPerSecond * Time.deltaTime;
                if (Energy >= 100.0f)
                    nextDeadTimeTick = float.MaxValue;
            }
        }


	}

    void OnGUI()
    {
        if (Show)
        {
            if (Energy >= 100.0f)
                MyStyle.normal.textColor = Color.green;
            else if (Energy <= 25.0f && Energy > 0.0f)
                MyStyle.normal.textColor = Color.yellow;
            else if (Energy <= 0.0f)
                MyStyle.normal.textColor = Color.red;
            else
                MyStyle.normal.textColor = Color.white;
            Color save = GUI.color;

            int energy = (int)Energy;
            GUI.Label(new Rect(Screen.width * 0.025f, Screen.height * 0.925f, 150.0f, 30.0f), new GUIContent("Energy " + energy.ToString()), MyStyle);
        }
    }
}
