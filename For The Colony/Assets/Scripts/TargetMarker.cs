using UnityEngine;
using System.Collections;

public class TargetMarker : MonoBehaviour {

    SpriteRenderer sprite;
	void Start () {
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = new Color(1, 1, 1, 0);
        StartCoroutine("Fade", 1);
	}
	
    public IEnumerator Fade(float time) {
        float alpha = 0;

        while (alpha < 255) {
            alpha = Mathf.Lerp(alpha, 255, time);
            yield return null;
        }
        while (alpha > 0) {
            alpha = Mathf.Lerp(alpha, 0, time);
            yield return null;
        }

        
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(2))
            StartCoroutine("Fade", 1);
	}
}
