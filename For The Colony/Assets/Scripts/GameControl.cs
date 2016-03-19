using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameControl : MonoBehaviour {

    public static GameControl instance;
    public GameObject rebelPrefab;


    public int playerResources = 0;
    public int enemyResources = 0;

    void Awake() {
        instance = this;
        InputEnabled = true;
    }

    public bool InputEnabled {
        get; set;
    }

	public void ChangeScene(string scene) {
        SceneManager.LoadScene(scene);
    }

    public void Exit() {
        Application.Quit();
    }

    public static void SpawnRebel(Vector3 pos, GameObject _leader) {
        GameObject ant = (GameObject)Instantiate(instance.rebelPrefab, pos, Quaternion.identity);
        ant.GetComponent<Ant>().leader = _leader;
    }

	// Update is called once per frame
	void Update () {
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, Mathf.Infinity)) {
            if (Input.GetMouseButtonDown(0)){
                Debug.Log("Target: " + hit.collider.name);
                if (hit.collider.GetComponent<Queen>() != null)
                    hit.collider.GetComponent<Queen>().SpawnAnt();
            }
        }
	}
}
