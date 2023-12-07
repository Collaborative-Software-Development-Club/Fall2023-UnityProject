using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class WaveSystemScript : MonoBehaviour
{
    public GameObject enemy;
    public Transform[] spawnPositions;
    [HideInInspector] public int currWave = 0;
    private List<Wave> waves;
    private List<GameObject> enemies;
    private List<GameObject> enemiesToRemove;
    private bool isInit = false;
    public struct Wave {
        public int numEnemyOne;
    }
    public int enemyKilled = 0;

    void Start() {
        waves = new List<Wave>();
        enemies = new List<GameObject>();
        enemiesToRemove = new List<GameObject>();
        // make waves here
        Wave one = new();
        one.numEnemyOne = 3;
        waves.Add(one);

        Wave two = new();
        two.numEnemyOne = 5;
        waves.Add(two);
    }

    void Update()
    {
        foreach(GameObject enemy in enemies) {
            EnemyHealth h = enemy.GetComponent<EnemyHealth>();
            if (h && h.currentHealth <= 0) {
                enemiesToRemove.Add(enemy);
            }
        }
        foreach(GameObject enemyToRemove in enemiesToRemove) {
            enemies.Remove(enemyToRemove);
            Destroy(enemyToRemove);
            enemyKilled++;
        }
        if (enemies.Count == 0 && !isInit && currWave != waves.Count) {
            enemiesToRemove.Clear();
            isInit = true;
            StartCoroutine(initWave());
            }
    }
    private IEnumerator initWave() {
        yield return new WaitForSeconds(5);
        
        for (int i = 0; (currWave != waves.Count) && i < waves[currWave].numEnemyOne && spawnPositions.Length != 0; i++) {
            int random = UnityEngine.Random.Range(0, spawnPositions.Length - 1);
            GameObject spawnedEnemy = Instantiate(enemy, spawnPositions[random].position, Quaternion.identity);
            enemies.Add(spawnedEnemy);
        }
        currWave++;
        isInit = false;
    }
}
