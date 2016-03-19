using UnityEngine;
using System.Collections.Generic;

public class Ant : MonoBehaviour {

    public enum Team { PLAYER, QUEEN, SLAVER };
    public Team team;

    public GameObject leader;
    NavMeshAgent agent;

    public Transform[] path;
    int pathIndex = 0;

    Vector2 smoothDeltaPosition = Vector2.zero;

    public GameObject enemy;
    public int attackStrength = 1;
    public float attackRange = 3;
    public float attackCooldown = 1;
    float attackTimer = 0;


    void Start() {
        agent = GetComponent<NavMeshAgent>();

        if (team != Team.PLAYER) {
            int choosePath = Random.Range(1, 30);
            if (choosePath <= 10)
                path = GameControl.instance.path0;
            else
            if (choosePath <= 20)
                path = GameControl.instance.path1;
            else
                path = GameControl.instance.path2;

            agent.SetDestination(path[0].position);
        }
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

        if (Vector3.Distance(transform.position, enemy.transform.position) < attackRange && attackTimer > attackCooldown) {

            enemy.GetComponent<Ant>().health -= attackStrength * Random.Range(1, 3);
            attackTimer = 0;

            if (enemy.GetComponent<Ant>().health <= 0) {
                if (team == Team.PLAYER) {
                    GameObject newAnt = (GameObject)Instantiate(GameControl.instance.rebelPrefab, enemy.transform.position, enemy.transform.rotation);
                    newAnt.GetComponent<Ant>().leader = gameObject;
                }
                enemy = null;
                if (team == Team.PLAYER)
                    agent.Stop();
            }
        }
    }

    void Patrol() {
        if (Vector3.Distance(transform.position, path[pathIndex].position) < 1) {
            int newIndex = Random.Range(0, path.Length);

            pathIndex = newIndex;

            
        }
        agent.SetDestination(path[pathIndex].position);
    }

    void UpdateSprite() {

        if (agent.destination.x > transform.position.x)
            transform.localScale = new Vector3(-1, 1, 1);
        else
            if (agent.destination.x < transform.position.x)
            transform.localScale = new Vector3(1, 1, 1);
    }

    void Update() {

        attackTimer += Time.deltaTime;

        if (enemy != null)
            Attack();
        else
        if (leader != null) {
            Vector3 back = leader.transform.position;
            agent.SetDestination(back);
        }
        else
            if (team != Team.PLAYER)
            Patrol();


        UpdateSprite();

        if (health <= 0)
            Destroy(gameObject);
    }


    void OnTriggerEnter(Collider other) {
        if (other.GetComponent<Ant>() != null && other.GetComponent<Ant>().team != team) {
            if (enemy != null) {
                float dst = Vector3.Distance(transform.position, enemy.transform.position);
                if (Vector3.Distance(transform.position, other.transform.position) < dst) {
                    enemy = other.gameObject;
                }
            }
            else
                enemy = other.gameObject;
        }
    }

    void OnDrawGizmos() {
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 2);
        
    }

}
