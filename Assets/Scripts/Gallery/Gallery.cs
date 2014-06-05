using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Gallery : MonoBehaviour {

    public bool Load = true;
    public bool OK = false;
    public string PathPrefix = @"file://";
    public string PathAppData = null;
    public string PathImageAssets = null;
    public string[] FilePaths = null;

    public Material Board = null;

    public bool NeedReload = false;

    //private ArrayList imageBuffer = null;
    private int currentImageIndex = 0;

    private struct Screen
    {
        public Texture2D screen;
        public string filename;
    }

    private List<Screen> imageBuffer = new List<Screen>();


	void Start () {
        PathAppData = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
        PathImageAssets = PathAppData + @"\KittenEscape\Gallery";

        if (Load)
            StartCoroutine(LoadImages());
	}
	
	void Update () {
	    if (NeedReload)
        {
            StartCoroutine(LoadImages());
            NeedReload = false;
        }

        if (Input.GetKeyDown(KeyCode.M))
            currentImageIndex++;

        if (Input.GetKeyDown(KeyCode.N))
            currentImageIndex--;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            string lastScene = PlayerPrefs.GetString("LastScene");

            if (lastScene.Length > 0)
                Application.LoadLevel(lastScene);
            else
                Application.LoadLevel("scene_0");
        }

        Display();
	}

    void Display()
    {
        if (imageBuffer.Count <= 0)
            return;

        if (currentImageIndex >= imageBuffer.Count)
            currentImageIndex = 0;
        if (currentImageIndex < 0)
            currentImageIndex = imageBuffer.Count - 1;

        Board.SetTexture("_MainTex", imageBuffer[currentImageIndex].screen);
        Board.SetTextureScale("_MainTex", new Vector2(-1.0f, -1.0f));
    }

    IEnumerator LoadImages()
    {
        bool ok = true;

        if (!CheckForDirectory())
        {
            Debug.Log("Directory not exist");

            if (!CreateGalleryDirectory())
            {
                Debug.Log("Error, cannot create directory. Gallery will not be usable");
                ok = false;
            }

            Debug.Log("Directory created with success");
        }

        try
        {
            FilePaths = System.IO.Directory.GetFiles(PathImageAssets, "*.png");
        }
        catch (System.UnauthorizedAccessException e)
        {
            Debug.Log(e.Message);
            ok = false;
        }

        if (FilePaths.Length <= 0)
        {
            Debug.Log("No images to load");
            ok = false;
        }

        if (ok)
        {
            for (int i = 0; i < FilePaths.Length; ++i)
            {
                WWW www = new WWW(PathPrefix + FilePaths[i]);
                yield return www;

                Texture2D texTmp = new Texture2D(UnityEngine.Screen.width, UnityEngine.Screen.height, TextureFormat.RGB24, false);
                www.LoadImageIntoTexture(texTmp);

                Screen s = new Screen();
                s.screen = texTmp;
                s.filename = FilePaths[i];

                imageBuffer.Add(s);
            }
        }

        OK = ok;
    }

    bool CheckForDirectory()
    {
        return System.IO.Directory.Exists(PathImageAssets);
    }

    bool CreateGalleryDirectory()
    {
        try
        {
            System.IO.Directory.CreateDirectory(PathImageAssets);
        }
        catch (System.IO.IOException)
        {
            return false;
        }

        return true;
    }

    public void Right()
    {
        currentImageIndex++;
    }

    public void Left()
    {
        currentImageIndex--;
    }

    public void Remove()
    {
        if (System.IO.File.Exists(imageBuffer[currentImageIndex].filename))
        {
            System.IO.File.Delete(imageBuffer[currentImageIndex].filename);
            imageBuffer.RemoveAt(currentImageIndex);
        }
    }
}
