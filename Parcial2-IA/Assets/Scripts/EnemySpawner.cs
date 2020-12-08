using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float enemyCooldown;
    public int maxEnemies;

    int currentEnemies = 0;
    float spawnTimer = 0;

    // Update is called once per frame
    void Update()
    {
        if (currentEnemies >= maxEnemies)
            return;

        spawnTimer += Time.deltaTime;
        if(spawnTimer >= enemyCooldown)
        {
            spawnTimer = 0;
            GameObject go = Instantiate(enemyPrefab, transform.position - Vector3.up * 3, Quaternion.identity);
            go.GetComponent<BehaviorExecutor>().SetBehaviorParam("EnemyBase", this.gameObject);
            go.GetComponent<Enemy>().onEnemyDestroyed = RemoveEnemy;
            currentEnemies++;
        }
    }

    public void RemoveEnemy()
    {
        currentEnemies--;
    }
}
