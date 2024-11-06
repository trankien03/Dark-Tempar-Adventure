using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waveSpawner : MonoBehaviour
{
    public List<Enemy> enemies = new List<Enemy>();
    public int currWave;
    public int waveValue;
    public List<GameObject> enemiesToSpawn = new List<GameObject>();

    public int numberOfWorm = 5;
    public int numberGoblin = 5;
    public int numberBee = 5;
    public int numberFly2 = 5;

    public Transform spawnLocation;
    public int waveDuration;
    private float waveTimer;
    public float spawnInterval;
    private float spawnTimer;
    // Start is called before the first frame update
    void Start()
    {
        GenerateWave();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (spawnTimer <= 0) { 
            //generate enemies
            if (enemiesToSpawn.Count > 0)
            {
                Instantiate(enemiesToSpawn[0], spawnLocation.position, Quaternion.identity); //spawn first enemy in our list
                enemiesToSpawn.RemoveAt(0);
                spawnTimer = spawnInterval;
            }
            else
            {
                waveTimer = 0; //end wave
            }
        }
        else
        {
            spawnTimer -= Time.fixedDeltaTime;
            waveTimer -= Time.fixedDeltaTime;
        }
    }
    
    public void GenerateWave()
    {
        waveValue = currWave * 10;
        GenerateEnemies();

        //spawnInterval = waveDuration / enemiesToSpawn.Count; //give a fixed time between each enemies
        waveTimer = waveDuration; //waveDuration is read Only
    }

    public void GenerateEnemies()
    {
        //create a temporary list of enemies to generate 
        //in a loob of grab a random enemy 
        // see if we can afford it 
        //if we can, add it to our list, and reduce the cost? -> repeat... -> if we have no point left, leave the loop
        List<GameObject> generateEnemies = new List<GameObject>();
        
        //add loai enemy 1, co bao nhieu enemy, tao bay nhieu vong for.
        /*for (int i =0; i < numberOfWorm; i++)
        {
            generateEnemies.Add(enemies[0].enemyPrefab);
        }
        for (int i = 0; i < numberGoblin; i++)
        {
            generateEnemies.Add(enemies[1].enemyPrefab);
        }*/

        if (numberOfWorm >= numberGoblin)
        {
            for (int i = 0; i < numberOfWorm; i++)
            {
                generateEnemies.Add(enemies[0].enemyPrefab);
                if (i + 1 == numberGoblin)
                    continue;
                generateEnemies.Add(enemies[1].enemyPrefab);
            }
        }else
        {
            for (int i = 0; i < numberGoblin; i++)
            {
                generateEnemies.Add(enemies[1].enemyPrefab);
                if (i + 1 == numberOfWorm)
                    continue;
                generateEnemies.Add(enemies[0].enemyPrefab);
            }
        }

        if (numberBee >= numberFly2)
        {
            for (int i = 0; i < numberBee; i++)
            {
                generateEnemies.Add(enemies[2].enemyPrefab);
                if (i + 1 == numberFly2)
                    continue;
                generateEnemies.Add(enemies[3].enemyPrefab);
            }
        }
        else
        {
            for (int i = 0; i < numberFly2; i++)
            {
                generateEnemies.Add(enemies[3].enemyPrefab);
                if (i + 1 == numberBee)
                    continue;
                generateEnemies.Add(enemies[2].enemyPrefab);
            }
        }
        enemiesToSpawn.Clear();
        enemiesToSpawn = generateEnemies;
    }
}

[System.Serializable]
public class Enemy
{
    
    public GameObject enemyPrefab; 
}
