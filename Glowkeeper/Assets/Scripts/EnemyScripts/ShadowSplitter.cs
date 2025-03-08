using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShadowSplitter : MonoBehaviour
{
    public GameObject smallerSplitterPrefab; // The weaker version
    public float enemyHealth = 4f;
    private bool hasSplit = false;
    private Transform player;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
        if (player != null && agent != null)
        {
            agent.SetDestination(player.position);
        }
    }

    public void EnemyDamage(int amount)
    {
        enemyHealth -= amount;

        if (enemyHealth <= 0 && !hasSplit)
        {
            SplitIntoSmallerVersions();
            Destroy(gameObject);
        }

    }

    void SplitIntoSmallerVersions()
    {
        hasSplit = true;

        Debug.Log("Shadow Splitter is splitting!");

        // Spawn two smaller versions at slight offsets
        GameObject split1 = Instantiate(smallerSplitterPrefab, transform.position + new Vector3(1, 0, 0), Quaternion.identity);
        GameObject split2 = Instantiate(smallerSplitterPrefab, transform.position + new Vector3(-1, 0, 0), Quaternion.identity);

        // Destroy this original enemy
        Destroy(gameObject);
    }

}