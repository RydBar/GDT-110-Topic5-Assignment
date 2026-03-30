using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    // Prefab of the enemy EnemySpawner will spawn
    [SerializeField]
    private GameObject enemyPrefab;

    public float minSpawnTime = 1f;
    public float maxSpawnTime = 5f;

    private float spawnDelay;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnDelay > 0)
        {
            spawnDelay -= Time.deltaTime;
        }
        else
        {
            spawnDelay = Random.Range(minSpawnTime, maxSpawnTime);
            spawnEnemy();
        }
    }

    private void spawnEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab, transform.position, transform.rotation);
    }
}
