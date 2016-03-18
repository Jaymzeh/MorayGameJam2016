using UnityEngine;
using System.Collections;

public class ResourceSpawn : MonoBehaviour {

    public Transform[] spawnPoints;
    float spawnRate = 2;

    float timer = 0;

	void Update () {
        timer += Time.deltaTime;

        if(timer >= spawnRate) {
            int random = Random.Range(0, spawnPoints.Length - 1);


        }
	}
}
