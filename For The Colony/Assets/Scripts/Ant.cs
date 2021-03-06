﻿using UnityEngine;
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
    public GameObject queen;
    public Resource resource;
    public int attackStrength = 1;
    public float attackRange = 3;
    public float attackCooldown = 1;
    float attackTimer = 0;
    float resourceTimer = 0;

    public Sprite portrait;
    SpriteRenderer sprite;

    Animator anim;
    AudioSource SFX;

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

        anim = GetComponentInChildren<Animator>();

        SFX = GameObject.FindGameObjectWithTag("AttackSFX").GetComponent<AudioSource>();

        if (team == Team.PLAYER) {
            gameObject.name = GetComponent<AntProfile>().GetName();
            portrait = GetComponent<Ant>().portrait = GetComponent<AntProfile>().GetPortait();
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
            if (!SFX.isPlaying)
                SFX.Play();
            
            attackTimer = 0;
            if (enemy.GetComponent<Ant>().health <= 0) {
                if (team == Team.PLAYER) {
                    if (Random.Range(1, 10) < 5) {
                        GameObject newAnt = (GameObject)Instantiate(GameControl.instance.rebelPrefab, enemy.transform.position, enemy.transform.rotation);
                        newAnt.GetComponent<Ant>().leader = gameObject;
                        GameControl.instance.allyCount++;
                    }
                }
                enemy = null;
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

        if (leader != null) {
            leader.GetComponentInChildren<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f);
        }
        
    }

    void CollectResource() {
        resourceTimer += Time.deltaTime;

        if (resourceTimer > 5) {
            if (team == Team.PLAYER)
                GameControl.instance.playerResources += 10;
            else
                GameControl.instance.enemyResources += 10;
            Destroy(resource.gameObject);
            resource = null;
            anim.SetBool("Dig", false);
        }

    }

    void Update() {

        attackTimer += Time.deltaTime;

        if (queen != null) {
            agent.SetDestination(queen.transform.position);
            if (Vector3.Distance(transform.position, queen.transform.position) < attackRange && attackTimer > attackCooldown) {
                queen.GetComponent<Queen>().health -= attackStrength * Random.Range(1, 3);
                if (!SFX.isPlaying)
                    SFX.Play();
                attackTimer = 0;
            }
        }
        else
        if (enemy != null)
            Attack();
        else
        if (resource != null) {
            agent.SetDestination(resource.transform.position);
            if (Vector3.Distance(transform.position, resource.transform.position) < 2) {
                anim.SetBool("Dig", true);
                CollectResource();
            }
        }
        else
        if (resource == null && anim.GetBool("Dig") == true) {
            anim.SetBool("Dig", false);
        }
        else
        if (leader != null) {
            Vector3 back = leader.transform.position;
            agent.SetDestination(back);
        }
        else
            if (team != Team.PLAYER)
            Patrol();


        UpdateSprite();

        if (health <= 0) {
            if (team == Team.PLAYER)
                GameControl.instance.allyCount--;
            else
                GameControl.instance.enemyCount--;
            Destroy(gameObject);
        }
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

        if (other.GetComponent<Resource>() != null) {
            resource = other.GetComponent<Resource>();
        }

        if (other.GetComponent<Queen>() != null && other.GetComponent<Queen>().team != team) {
            queen = other.gameObject;
        }
    }

    void OnDrawGizmos() {
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 2);
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

}
