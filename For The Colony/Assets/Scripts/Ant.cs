using UnityEngine;
using System.Collections.Generic;

public class Ant : MonoBehaviour {

    public enum Team { PLAYER, QUEEN, SLAVER };
    public Team team;

    public enum Stance { PASSIVE, HOSTILE };
    public Stance stance = Stance.PASSIVE;

    public GameObject leader;

    NavMeshAgent agent;

    Ant enemy;
    public int attackStrength = 1;
    public float attackRange = 3;
    public float attackCooldown = 1;
    float timer = 0;
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
        agent.SetDestination(enemy.transform.position);
        //transform.LookAt(enemy.transform.position);

        if (Vector3.Distance(transform.position, enemy.transform.position) < attackRange && timer > attackCooldown) {

            enemy.health -= attackStrength * Random.Range(1, 3);
            timer = 0;

            if (enemy.health <= 0) {
                if (team == Team.PLAYER)
                    Instantiate(GameControl.instance.rebelPrefab, enemy.transform.position, enemy.transform.rotation);
                enemy = null;
                agent.Stop();
            }
        }  
    }

    void Update() {
       
        timer += Time.deltaTime;

        if (enemy!=null)
            Attack();


         if (health <= 0)
            Destroy(gameObject);
    }
	

    void OnTriggerEnter(Collider other) {
        if(other.GetComponent<Ant>()!=null && other.GetComponent<Ant>().team != team) {
            if(enemy!= null) {
                float dst = Vector3.Distance(transform.position, other.transform.position);
                if(Vector3.Distance(transform.position, enemy.transform.position) < dst) {
                    enemy = other.GetComponent<Ant>();
                }
            }
            else
                enemy = other.GetComponent<Ant>();
            
        }
    }

    void OnDrawGizmos() {
        Gizmos.DrawLine(transform.position, transform.position+ transform.forward*2);
    }

}
