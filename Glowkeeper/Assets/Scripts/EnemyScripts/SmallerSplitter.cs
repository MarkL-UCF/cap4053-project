using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SmallerSplitter : MonoBehaviour
{
    public float moveSpeed = 1.2f; // Faster than the original
    private Transform player;
    private NavMeshAgent agent;
    public float enemyHealth = 2f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = moveSpeed;

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }

    }

    void Update()
    {
        if (player != null)
        {
            agent.SetDestination(player.position);
        }
    }

  public void EnemyDamage(float amount)
    {
        enemyHealth -= amount;
        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void SetPlayerTarget(Transform target)
    {
        player = target;
        if (agent != null)
        {
            Debug.Log("New Splitter is now tracking player!");
            agent.SetDestination(player.position);
        }
    }


}