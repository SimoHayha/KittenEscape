using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScreenshotManager : MonoBehaviour {

    public static ScreenshotManager Instance;

    public Gallery Gallery = null;
    public int ResWidth = 2550;
    public int ResHeight = 3300;

    public bool takeHiResShot = false;
    [HideInInspector]
    public GameObject ObjectToDisable;

    private Camera main = null;
    private RenderTexture rt = null;
    private bool screeDone = false;

    private struct Screen
    {
        public byte[] screen;
        public string filename;
    }

    private List<Screen> screens = new List<Screen>();

    void Awake()
    {
        Instance = this;
    }

    public string ScreenShotName(int width, int height)
    {
        if (Gallery/* && Gallery.OK*/)
        {
            return string.Format("{0}/screen_{1}x{2}_{3}.png",
                                 Gallery.PathImageAssets,
                                 width, height,
                                 System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
        }

        return null;
    }

    void LateUpdate()
    {
        //takeHiResShot |= Input.GetKeyDown(screenKey);
        if (takeHiResShot)
        {
            rt = new RenderTexture(ResWidth, ResHeight, 24);
            main = ScreenCamera.Instance.GetComponent<Camera>();
            //main = Camera.main;

            main.targetTexture = rt;
            RenderTexture.active = rt;

            StartCoroutine(TakeScreen());

            takeHiResShot = false;
        }

        if (screeDone && rt != null)
        {
            Destroy(rt);
            screeDone = false;
            main.targetTexture = null;
            RenderTexture.active = null;
        }
    }

    IEnumerator TakeScreen()
    {
        Screen screen = new Screen();
        Texture2D screenShot = new Texture2D(ResWidth, ResHeight, TextureFormat.RGB24, false);
        main.Render();
        screenShot.ReadPixels(new Rect(0.0f, 0.0f, ResWidth, ResHeight), 0, 0);
        screeDone = true;
        byte[] bytes = screenShot.EncodeToPNG();
        string filename = ScreenShotName(ResWidth, ResHeight);
        screen.screen = bytes;
        screen.filename = filename;
        screens.Add(screen);
        /*if (filename != null)
        {
            System.IO.File.WriteAllBytes(filename, bytes);
            Debug.Log(string.Format("Took screenshot to: {0}", filename));
        }*/
        takeHiResShot = false;

        if (ObjectToDisable)
            ObjectToDisable.SetActive(false);

        yield return null;
    }

    public void SaveScreens(bool b)
    {
        if (b)
        {
            foreach (Screen s in screens)
                System.IO.File.WriteAllBytes(s.filename, s.screen);
        }

        screens.Clear();
    }
}
