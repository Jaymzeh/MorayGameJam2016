using UnityEngine;
using System.Collections;

public class MapLoader : MonoBehaviour {

    public Texture2D sourceTex;
    public Rect sourceRect;

    public GameObject prefab;

    void Start() {
        LoadFromFile();
    }


	public void LoadFromFile() {

        int x = 0;
        int y = 0;
        int width = sourceTex.width;
        int height = sourceTex.height;
        Color[] pix = sourceTex.GetPixels();
        Texture2D destTex = new Texture2D(width, height);
        destTex.SetPixels(pix);
        destTex.Apply();
        GetComponent<Renderer>().material.mainTexture = destTex;

        int transparent = 0;
        foreach(Color px in pix) {
            if (px.Equals(Color.white))
                transparent++;
        }

        Debug.Log("Number of empty pixels: " + transparent);
    }
}
