using UnityEngine;
using System.Collections;

public class Queen : MonoBehaviour {

    public GameObject antPrefab;
    public int antCost = 3;
    public float AIspawnRate = 3;
    public Transform patrol;
    public int health = 100;

    public Ant.Team team;
    float timer = 0;

    public void SpawnAnt() {
        switch (team) {
            case Ant.Team.PLAYER:
                if (GameControl.instance.playerResources >= antCost) {
                    GameObject temp = Instantiate(antPrefab, transform.position, Quaternion.identity) as GameObject;
                    GameControl.instance.allyCount++;
                    temp.GetComponent<NavMeshAgent>().SetDestination(patrol.position);
                    GameControl.instance.playerResources -= antCost;
                }
                break;
        }
    }

    void Update() {
        if (health <= 0) {
            if (team == Ant.Team.PLAYER)
                GameControl.instance.ShowGameOver();
            Destroy(gameObject);
        }

        if (team != Ant.Team.PLAYER) {
            timer += Time.deltaTime;

            if (team == Ant.Team.QUEEN) {
                if (GameControl.instance.enemyResources >= antCost && timer >= AIspawnRate) {
                    Instantiate(antPrefab, transform.position, Quaternion.identity);
                    GameControl.instance.enemyResources -= antCost;
                    GameControl.instance.enemyCount++;
                    timer = 0;
                }
            }
            else
            if (team == Ant.Team.SLAVER) {
                if (GameControl.instance.slaverResources != 1) {
                    if (GameControl.instance.slaverResources >= antCost && timer >= AIspawnRate) {
                        Instantiate(antPrefab, transform.position, Quaternion.identity);
                        GameControl.instance.slaverResources -= antCost;
                        GameControl.instance.enemyCount++;
                        timer = 0;
                    }
                }
                else
                    Destroy(gameObject);
            }
        }
    }
}
