using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveHandler : MonoBehaviour
{
    public int Currency;
    public TMP_Text waveAmt;
    public TMP_Text currencyUI;
    public TMP_Text healthUI;
    public int secondsleft;
    public int currentWave;
    public GameObject Enemy1;
    public GameObject Enemy2;
    public Transform[] waypoints;
    public int currentAlive;
    public int waveEarnings;
    public int health;

    public Dictionary<int, int[]> waves = new Dictionary<int, int[]>();

    public int[] Wave1;
    public int[] Wave2;
    public int[] Wave3;
    public int[] Wave4;
    public int[] Wave5;
    public int[] Wave6;
    public int[] Wave7;
    public int[] Wave8;
    public int[] Wave9;
    public int[] Wave10;
    public int[] Wave11;
    public int[] Wave12;
    public int[] Wave13;
    public int[] Wave14;
    public int[] Wave15;

    void Start()
    {
        Currency = 200;
        UpdateUI();
        waves.Add(1, Wave1);
        waves.Add(2, Wave2);
        waves.Add(3, Wave3);
        waves.Add(4, Wave4);
        waves.Add(5, Wave5);
        waves.Add(6, Wave6);
        waves.Add(7, Wave7);
        waves.Add(8, Wave8);
        waves.Add(9, Wave9);
        waves.Add(10, Wave10);
        waves.Add(11, Wave11);
        waves.Add(12, Wave12);
        waves.Add(13, Wave13);
        waves.Add(14, Wave14);
        waves.Add(15, Wave15);

        secondsleft = 11;
        StartCoroutine(ExecuteEverySecond());
        currentWave = 1;
    }

    public void EnemyDestroyed(int DMG)
    {
        currentAlive -= 1;
        health -= DMG;
        UpdateUI();
        if (health <= 0 )
        {
            print("Lose");
            healthUI.text = "DEATH";
        }
    }
    public void UpdateUI()
    {
        currencyUI.text = Currency.ToString();
        healthUI.text = health.ToString();
    }
    IEnumerator ExecuteEverySecond()
    {
        while (secondsleft > 0)
        {
            yield return new WaitForSeconds(1);
            secondsleft -= 1;
            waveAmt.text = secondsleft.ToString();
        }
        yield return StartCoroutine(StartWave());
    }

    IEnumerator StartWave()
    {
        waveAmt.text = "Wave " + currentWave.ToString();

        if (waves.ContainsKey(currentWave))
        {
            int[] currentWaveData = waves[currentWave];
            Debug.Log("Starting Wave " + currentWave);
            int enemy1Amt = currentWaveData[0];
            int enemy2Amt = currentWaveData[1];

            // Reset currentAlive to the total number of enemies to spawn
            currentAlive = enemy1Amt + enemy2Amt;
            waveEarnings = currentAlive * 50;

            // Start coroutines for spawning enemies
            Coroutine spawnEnemy1Coroutine = StartCoroutine(SpawnEnemies(Enemy1, enemy1Amt, 1f, 12f));
            Coroutine spawnEnemy2Coroutine = StartCoroutine(SpawnEnemies(Enemy2, enemy2Amt, 0.1f, 4f));

            // Wait for both coroutines to complete
            yield return spawnEnemy1Coroutine;
            yield return spawnEnemy2Coroutine;

            // Wait until all enemies are destroyed
            while (currentAlive > 0)
            {
                yield return null; // Wait until the next frame and check again
            }

            Debug.Log("All enemies are destroyed. Proceeding to the next wave.");
            currentWave += 1;
            Currency += waveEarnings;
            UpdateUI();

            if (waves.ContainsKey(currentWave))
            {
                secondsleft = 6;
                StartCoroutine(ExecuteEverySecond());
            }
            else
            {
                Debug.Log("Win");
            }
        }
    }

    IEnumerator SpawnEnemies(GameObject enemyPrefab, int amount, float spawnDelay1, float spawnDelay2)
    {
        int placed = 0;

        while (placed < amount)
        {
            yield return new WaitForSeconds(Random.Range(spawnDelay1, spawnDelay2));

            GameObject enemy = Instantiate(enemyPrefab);


            placed += 1;
        }
    }
}