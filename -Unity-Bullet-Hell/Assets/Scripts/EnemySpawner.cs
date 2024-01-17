using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyToSpawn;
    public Transform mixSpawn;
    public Transform maxSpawn;
    public float timeToSpawn;
    public int checkPerFrame;

    private float spawnCounter;
    private Transform target;
    private float despawnDistance;
    private List<GameObject> spawnedEnemies = new List<GameObject>();
    private int enemyToCheck;
    
    // Start is called before the first frame update
    void Start()
    {
        spawnCounter = timeToSpawn;
        target = PlayerHealthController.instance.transform;
        despawnDistance = Vector3.Distance(transform.position, maxSpawn.position) + 10f;
    }

    // Update is called once per frame
    void Update()
    {
        // Spawn
        spawnCounter -= Time.deltaTime;
        if (spawnCounter <= 0)
        {
            spawnCounter = timeToSpawn;

            GameObject newEnemy = Instantiate(enemyToSpawn, SelectSpawnPoint(), transform.rotation);
            spawnedEnemies.Add(newEnemy);
        }

        transform.position = target.position;

        // Despawn
        int checkTarget = enemyToCheck + checkPerFrame;
        while (enemyToCheck < checkTarget)
        {
            if (enemyToCheck < spawnedEnemies.Count)
            {
                if (spawnedEnemies[enemyToCheck] != null)
                {
                    if (Vector3.Distance(transform.position, spawnedEnemies[enemyToCheck].transform.position) > despawnDistance)
                    {
                        Destroy(spawnedEnemies[enemyToCheck]);
                        spawnedEnemies.RemoveAt(enemyToCheck);
                        checkTarget--;
                    }
                    else
                    {
                        enemyToCheck++;
                    }
                }
                else
                {
                    spawnedEnemies.RemoveAt(enemyToCheck);
                    checkTarget --;
                }
            }
            else
            {
                enemyToCheck = 0;
                checkTarget = 0;
            }
        }
    }

    public Vector3 SelectSpawnPoint()
    {
        Vector3 spawnPoint = Vector3.zero;
        bool spawnVerticalEdge = Random.Range(0f, 1f) > 0.5f;

        if (spawnVerticalEdge)
        {
            spawnPoint.y = Random.Range(mixSpawn.position.y, maxSpawn.position.y);
            if (Random.Range(0f,1f)> 0.5f)
            {
                spawnPoint.x = maxSpawn.position.x;
            }
            else
            {
                spawnPoint.x = mixSpawn.position.x;
            }
        }
        else
        {
            spawnPoint.x = Random.Range(mixSpawn.position.x, maxSpawn.position.x);
            if (Random.Range(0f,1f)> 0.5f)
            {
                spawnPoint.y = maxSpawn.position.y;
            }
            else
            {
                spawnPoint.y = mixSpawn.position.y;
            }
        }
        return spawnPoint;
    }
}
