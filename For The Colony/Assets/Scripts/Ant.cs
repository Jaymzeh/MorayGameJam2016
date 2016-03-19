using UnityEngine;
using System.Collections.Generic;

public class Ant : MonoBehaviour {

    public enum Team { PLAYER, QUEEN, SLAVER };
    public Team team;

    public enum Stance { PASSIVE, HOSTILE };
    public Stance stance = Stance.PASSIVE;

    public GameObject leader;

    NavMeshAgent agent;

    List<Ant> enemies = new List<Ant>();
    public float attackRange = 3;

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
	}

    public int health = 10;

     Transform GetClosestEnemy(List<Ant> _enemies) {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Ant ant in _enemies) {
            float dist = Vector3.Distance(ant.transform.position, currentPos);
            if (dist < minDist) {
                tMin = ant.transform;
                minDist = dist;
            }
        }
        return tMin;
    }

    void Attack() {
        if (enemies[0] != null) {
            agent.SetDestination(enemies[0].transform.position);
            transform.LookAt(enemies[0].transform.position);

            if (Vector3.Distance(transform.position, enemies[0].transform.position) < attackRange) {
                enemies[0].health -= 1;
                if (enemies[0].health <= 0) {
                    if (team == Team.PLAYER)
                        Instantiate(GameControl.instance.rebelPrefab, enemies[0].transform.position, enemies[0].transform.rotation);
                    enemies.Remove(enemies[0]);
                }
            }
        }
        else
            agent.Stop();
    }

    void Update() {
        if (health <= 0)
            Destroy(gameObject);

        
        Attack();
    }
	

    void OnTriggerEnter(Collider other) {
        if(other.GetComponent<Ant>()!=null && other.GetComponent<Ant>().team != team) {
            enemies.Add(other.GetComponent<Ant>());
            
        }
    }

    void OnTriggerExit(Collider other) {
        if(other.GetComponent<Ant>()!=null && other.GetComponent<Ant>().team != team) {
            enemies.Remove(other.GetComponent<Ant>());
        }
    }

    void OnDrawGizmos() {
        Gizmos.DrawLine(transform.position, transform.position+ transform.forward*2);
    }

}
