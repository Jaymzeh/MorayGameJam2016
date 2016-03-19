using UnityEngine;
using System.Collections;

public class ResourceSpawn : MonoBehaviour {

    public GameObject resourcePrefab;
    public Transform[] spawnPoints;
    public float spawnRate = 2;

    float timer = 0;

	void Update () {
        timer += Time.deltaTime;

        if(timer >= spawnRate) {
            int random = Random.Range(0, spawnPoints.Length - 1);

            

            Instantiate(resourcePrefab, spawnPoints[random].transform.position, spawnPoints[random].transform.rotation);
            timer = 0;
        }
	}
}
