using UnityEngine;
using System.Collections.Generic;

public class Ant : MonoBehaviour {

    public enum Team { PLAYER, QUEEN, SLAVER };
    public Team team;

    public enum Stance { PASSIVE, HOSTILE };
    public Stance stance = Stance.PASSIVE;

    public GameObject leader;

    NavMeshAgent agent;

    List<Transform> enemies = new List<Transform>();
    public Ant closestTarget;

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
	}

    public int health = 10;

     Transform GetClosestEnemy(List<Transform> _enemies) {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Transform t in _enemies) {
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist) {
                tMin = t.transform;
                minDist = dist;
            }
        }
        return tMin;
    }

    void Attack() {
        Transform temp = null;
        if (enemies.Count > 0)
            temp = GetClosestEnemy(enemies);
        else
            agent.Stop();

        if (temp != null) {
            closestTarget = temp.GetComponent<Ant>();
            agent.SetDestination(closestTarget.transform.position);

            if (Vector3.Distance(transform.position, closestTarget.transform.position) < 1) {
                closestTarget.health -= 1;
                if (team == Team.PLAYER && closestTarget.health <= 0) {
                    Instantiate(GameControl.instance.rebelPrefab, closestTarget.transform.position, closestTarget.transform.rotation);
                    Destroy(closestTarget.gameObject);

                }
            }
        }
    }

    void FixedUpdate() {
        if (health <= 0)
            Destroy(gameObject);

        enemies = new List<Transform>();
        Ant[] temp = FindObjectsOfType<Ant>();

        foreach (Ant ant in temp) {
            if (ant.team != team)
                enemies.Add(ant.transform);
        }

        switch (stance) {
            case Stance.PASSIVE:

                if (leader != null) {
                    Vector3 pos = leader.transform.position;
                    agent.SetDestination(pos);
                }
                break;

                case Stance.HOSTILE:
                Attack();
                break;
        }

    }
	void Update () {

        
        

	}
}
