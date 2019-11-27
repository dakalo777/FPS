using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    public bool isActive;
    [SerializeField] private float timeBetweenSpawns;
    [SerializeField] private int maxEnemiesToSpawn;
    private int currentEnemiesSpawned;
    public void SpawnEnemy()
    {
        currentEnemiesSpawned++;
        if (currentEnemiesSpawned <= maxEnemiesToSpawn)
        {
            var randomIndex = Random.Range(0, enemies.Length);
            var currentEnemy = Instantiate(enemies[randomIndex], this.transform.position, Quaternion.identity);            
        }
      

    }


    private IEnumerator Timer()
    {
        while (isActive)
        {
            yield return new WaitForSeconds(timeBetweenSpawns);
            SpawnEnemy();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isActive = true;
            StartCoroutine(Timer());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isActive = false;
            StopAllCoroutines();
        }
    }
}
