using UnityEngine;
using System.Collections;

public class Queen : MonoBehaviour {

    public GameObject antPrefab;
    public int antCost = 3;
    public Transform patrol;

    public Ant.Team team;

    public void SpawnAnt() {
        switch (team) {
            case Ant.Team.PLAYER:
                if (GameControl.instance.playerResources >= antCost) {
                    GameObject temp = Instantiate(antPrefab, transform.position, Quaternion.identity) as GameObject;
                    temp.GetComponent<NavMeshAgent>().SetDestination(patrol.position);
                }
                break;

            case Ant.Team.QUEEN:
                if (GameControl.instance.enemyResources >= antCost)
                    Instantiate(antPrefab, transform.position - Vector3.forward * 2, Quaternion.identity);
                break;
        }
    }
}
