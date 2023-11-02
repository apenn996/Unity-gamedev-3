using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject grunt;
    [SerializeField] private GameObject wolf;
    [SerializeField] private GameObject ogre;

    // Start is called before the first frame update
    [SerializeField] private int nEnemiesInWave = 0;
    [SerializeField] private float spawnEnemyInWaveWaitTime = 1.3f;
    [SerializeField] private bool isWaveSpawning = false;
    [SerializeField] private float spawnTimer = 5f;
    [SerializeField] private float timer = 5f;
    [SerializeField] private Text timerText;
    [SerializeField] private Button GoNext;
    [SerializeField] private Text levelClear;
    [SerializeField] private Text EnemiesRemaining;
    public static int curEnemies = 2;

    private void Start()
    {
        levelClear.enabled = false;
        GoNext.enabled = true;
    }
    private void Update()
    {
        //print(curEnemies);
        timerText.text = "Time: " + ((int)timer).ToString();
        EnemiesRemaining.text = "Enemies Alive: " + ((int)curEnemies).ToString();
        if (curEnemies == 0 && !isWaveSpawning)
        {
            levelClear.enabled = true;
            GoNext.enabled = true;
            GoNext.gameObject.SetActive(true);
        }
       // if (!isWaveSpawning)
           // timer -= Time.deltaTime;
    }
    public void startNextWave()
    {
        isWaveSpawning = true;
        levelClear.enabled = false;
        GoNext.enabled = false;
        GoNext.gameObject.SetActive(false);
        timer = spawnTimer;
        StartCoroutine("SW");
        
    }
    IEnumerator SW()
    {
        while (timer > 0)
        {
            yield return new WaitForSeconds(1);
            timer--;
        }
        StartCoroutine("SpawnWave");
        PlayerController.curRound++;
    }
    IEnumerator SpawnWave()
    {
        
        nEnemiesInWave+=(2*PlayerController.curLevel) + 2;
        curEnemies = nEnemiesInWave;

        for (int i = 0; i < nEnemiesInWave; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnEnemyInWaveWaitTime);
        }
       // curEnemies -= 1;
        spawnEnemyInWaveWaitTime -= .1f;
        isWaveSpawning = false;
       

    }
    private void SpawnEnemy()
    {
        int rand = Random.Range(1, 10);
        if(rand <=10 && rand > 5)
        {
            Instantiate(grunt, transform);

        }
        else if(rand > 2 && rand <=5 )
        {
            Instantiate(wolf, transform);
        }
        else
        {
            Instantiate(ogre, transform);
        }
        
       //Instantiate(enemyPrefab, transform);
    }
}
