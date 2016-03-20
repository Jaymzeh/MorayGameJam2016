using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameControl : MonoBehaviour {

    public static GameControl instance;
    public GameObject rebelPrefab;

    public int playerResources = 0;
    public int enemyResources = 0;
    public int slaverResources = 0;

    public int allyCount;
    public int enemyCount;

    public Transform[] pathParent;

    public Transform[] path0;
    public Transform[] path1;
    public Transform[] path2;

    public GameObject selectionHighlight;
    public GameObject hoverHighlight;

    public bool paused = false;
    public GameObject pauseScreen;

    public float slaverSpawnTime;
    float slaverCoutndown = 0;

    public GameObject gameWinScreen, gameLoseScreen;

    public GameObject playerQueen, redQueen, slaverSpawn;

    public GameObject selectedAnt {
        get; set;
    }

    public void TogglePause() {
        paused = !paused;

        if (paused) {
            Time.timeScale = 0;
            InputEnabled = false;
            pauseScreen.SetActive(true);
        }
        if (!paused) {
            Time.timeScale = 1;
            InputEnabled = true;
            pauseScreen.SetActive(false);
        }

    }

    void Awake() {
        instance = this;
        InputEnabled = true;

        if (pathParent.Length > 0)
            for (int i = 0; i < pathParent[0].childCount; i++) {
                path0[i] = pathParent[0].GetChild(i).transform;
            }

        if (pathParent.Length > 0)
            for (int i = 0; i < pathParent[1].childCount; i++) {
                path1[i] = pathParent[1].GetChild(i).transform;
            }

        if (pathParent.Length > 0)
            for (int i = 0; i < pathParent[2].childCount; i++) {
                path2[i] = pathParent[2].GetChild(i).transform;
            }
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
    void Update() {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetKeyDown(KeyCode.Escape))
            TogglePause();

        if (InputEnabled) {

            if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
                if (Input.GetMouseButtonDown(0)) {
                    Debug.Log("Target: " + hit.collider.name);
                    if (hit.collider.GetComponent<Queen>() != null)
                        hit.collider.GetComponent<Queen>().SpawnAnt();
                    else
                    if (hit.collider.GetComponent<Ant>() != null && hit.collider.GetComponent<Ant>().team == Ant.Team.PLAYER) {
                        if (instance.selectedAnt == null) {
                            instance.selectedAnt = hit.collider.gameObject;
                        }
                        else {
                            hit.collider.GetComponent<Ant>().leader = selectedAnt;
                        }
                    }
                    else
                        if (instance.selectedAnt != null)
                        instance.selectedAnt.GetComponent<NavMeshAgent>().SetDestination(hit.point);


                }
                if (Input.GetMouseButtonDown(1)) {
                    instance.selectedAnt = null;
                //    if (instance.selectedAnt != null) {
                //        if (hit.collider.GetComponent<Ant>() != null && hit.collider.GetComponent<Ant>().team == Ant.Team.PLAYER)
                //            selectedAnt.GetComponent<Ant>().leader = hit.collider.gameObject;
                //        else {
                //            instance.selectedAnt.GetComponent<NavMeshAgent>().SetDestination(hit.point);
                //            if (selectedAnt.GetComponent<Ant>().leader != null)
                //                selectedAnt.GetComponent<Ant>().leader.GetComponentInChildren<SpriteRenderer>().color = new Color(1, 1, 1);
                //            selectedAnt.GetComponent<Ant>().leader = null;
                //        }

                //        if (hit.collider.GetComponent<Resource>() != null) {
                //            selectedAnt.GetComponent<NavMeshAgent>().SetDestination(hit.collider.transform.position);
                //        }
                //    }
                }

            }
            if (instance.selectedAnt != null)
                selectionHighlight.transform.position = instance.selectedAnt.transform.position;
            else
                if (selectionHighlight != null)
                selectionHighlight.transform.position = Vector3.zero;
        }



        if (slaverCoutndown < slaverSpawnTime) {
            slaverCoutndown += Time.deltaTime;
        }
        if (slaverCoutndown > slaverSpawnTime) {
            slaverResources = 31;
            slaverCoutndown = slaverSpawnTime;
        }

    }

    public void ShowGameOver() {
        gameLoseScreen.SetActive(true);
    }
    void FixedUpdate() {

        if (slaverSpawn == null)
            return;

        if (playerQueen != null && redQueen == null && enemyCount == 0)
            gameWinScreen.SetActive(true);
        else {
            if (playerQueen == null) {
                gameLoseScreen.SetActive(true);
            }
        }
    }


}
