using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Tutorial : MonoBehaviour {


    public int stage = 0;

    public GameObject text;

    public Image image;
    public Sprite[] sprite;
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
            stage++;
        if (Input.GetMouseButtonDown(1))
            stage--;
        stage = Mathf.Clamp(stage, 0, 2);

        if (stage == 1)
            text.SetActive(true);
        else
        text.SetActive(false);

        if (stage == 2)
            GameControl.instance.ChangeScene("TestScene");
        if (stage <2)
        image.sprite = sprite[stage];

	}
}
