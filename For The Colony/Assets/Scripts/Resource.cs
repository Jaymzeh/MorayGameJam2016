using UnityEngine;
using System.Collections;

public class Resource : MonoBehaviour {

    IEnumerator MineResource(float wait) {

        float timer = 0;

        while (timer < wait) {
            timer += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other) {
        Debug.Log("Resource touched");
        if(other.GetComponent<Ant>()!=null) {
            other.GetComponent<NavMeshAgent>().Stop();
            
            StartCoroutine(MineResource(3));
        }
    }

}
