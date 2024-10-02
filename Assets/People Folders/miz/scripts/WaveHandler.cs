using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveHandler : MonoBehaviour
{
    public int currency;
    public TMP_Text waveAmt;
    public TMP_Text currencyUI;
    public TMP_Text healthUI;
    public int secondsleft;
    public int currentWave;

    public Transform[] waypoints;
    public int currentAlive;
    public int waveEarnings;
    public int health;
    public AudioSource crunch;
    public AudioSource victory;
    public AudioSource countdown;
    public int startingcurrency;

    public Dictionary<int, int[]> waves = new Dictionary<int, int[]>();

    public GameObject speedy;
    public GameObject candy;
    public GameObject brown;






    /// Wavedata info  <summary>
    ///  0 = Speedy
    ///  1 = Candy
    ///  2 = Brown
    ///  3 = small bunny max time
    /// </summary>



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
        currency = startingcurrency;
        health = 100; // Initialize health
       

        // Populate waves dictionary
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

    public void EnemyKilled(int enemworth)
    {
        print("Killed an enemy");
        currentAlive -= 1;
        currency += enemworth;
       
    }


    public void EnemyDestroyed(int DMG)
    {
        print("Enemy reached the end");
        currentAlive -= 1;
        health -= DMG;
        crunch.Play();

        if (health <= 0)
        {
            Debug.Log("Game Over");
            healthUI.text = "DEATH";
         
        }
    }

    public void RemoveCurrency(int amount)
    {
        print("Tower bought for" + amount.ToString() + "Currency");
        currency -= amount;
    }

  void Update()
    {
        currencyUI.text = currency.ToString();
        healthUI.text = health.ToString();
        if (Input.GetKey("h") && Input.GetKey("y"))
        {
            currency += 100;
            print("Devtools money");
        }

    }

    IEnumerator ExecuteEverySecond()
    {
        countdown.Play();
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
        countdown.Stop();
        waveAmt.text = "Wave " + currentWave.ToString();

        if (waves.ContainsKey(currentWave))
        {
            int[] currentWaveData = waves[currentWave];
            Debug.Log("Starting Wave " + currentWave);
            int speedyAmt = currentWaveData[0];
            int candyamt = currentWaveData[1];
            int brownamt = currentWaveData[2];

            // Reset currentAlive to the total number of enemies to spawn
            int newEnemies = speedyAmt + candyamt + brownamt; // Count of new enemies to be added
            waveEarnings = newEnemies * 25;
            currentAlive = newEnemies;

            // Start coroutines for spawning enemies
            Coroutine spawnEnemy1Coroutine = StartCoroutine(SpawnEnemies(speedy, speedyAmt, 0.1f, currentWaveData[3]));
            Coroutine spawnEnemy2Coroutine = StartCoroutine(SpawnEnemies(candy, candyamt, 0.1f, currentWaveData[3]));
            Coroutine spawnEnemy3Coroutine = StartCoroutine(SpawnEnemies(brown, brownamt, 0.1f, currentWaveData[3]));

            // Wait for both coroutines to complete
            yield return spawnEnemy1Coroutine;
            yield return spawnEnemy2Coroutine;
            yield return spawnEnemy3Coroutine;

            // Wait until all enemies are destroyed

            while (currentAlive > 0)
            {
                yield return null; // Wait until the next frame and check again
            }


            Debug.Log("All enemies are destroyed. Proceeding to the next wave.");
            currentWave += 1;
            currency += waveEarnings;
            victory.Play();
            
          

            if (waves.ContainsKey(currentWave))
            {
                secondsleft = 6;
                StartCoroutine(ExecuteEverySecond());
            }
            else
            {
                Debug.Log("You Win!");
            }
        }
        else
        {
            Debug.LogError("No data for current wave: " + currentWave);
        }
    }

    IEnumerator SpawnEnemies(GameObject enemyPrefab, int amount, float spawnDelay1, float spawnDelay2)
    {
        int placed = 0;

        while (placed < amount)
        {
            yield return new WaitForSeconds(Random.Range(spawnDelay1, spawnDelay2));

            GameObject enemy = Instantiate(enemyPrefab);
            // Adjust the enemy's behavior if necessary to reduce its health in EnemyDestroyed
            // Increment currentAlive in the StartWave method to account for spawned enemies
            placed += 1;
        }
    }
}