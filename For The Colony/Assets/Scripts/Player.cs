using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    Vector3 targetPosition;
    NavMeshAgent agent;

	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, Mathf.Infinity)) {
            if (Input.GetMouseButtonDown(0)){
                Debug.Log("Target: " + hit.collider.name);
            }
        }
        
        
	}
}
