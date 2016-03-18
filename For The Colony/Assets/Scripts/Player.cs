using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    Vector3 targetPosition;
    NavMeshAgent agent;

	void Start () {
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, Mathf.Infinity)) {
            if (Input.GetMouseButtonDown(0)){
                targetPosition = hit.point;
                Debug.Log("Target changed");
            }
        }
        agent.SetDestination(targetPosition);
        
	}
}
