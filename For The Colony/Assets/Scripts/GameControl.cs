using UnityEngine;
using System.Collections;

public class GameControl : MonoBehaviour {

    public static GameControl instance;

    public GameObject rebelPrefab;

    public float Timer {
        get;private set;
    }

    void Awake() {
        instance = this;
    }

	void Start () {
	
	}
	
    public static void SpawnRebel(Vector3 pos, GameObject _leader) {
        GameObject ant = (GameObject)Instantiate(instance.rebelPrefab, pos, Quaternion.identity);
        ant.GetComponent<Ant>().leader = _leader;
    }

	// Update is called once per frame
	void Update () {
        Timer += Time.deltaTime;
	}
}
