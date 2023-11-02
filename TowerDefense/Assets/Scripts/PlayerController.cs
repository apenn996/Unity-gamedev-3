using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int startHealth = 100;
    [SerializeField] private int curHealth = 100;
    [SerializeField] private int startMoney = 60;
    [SerializeField] private Text healthText;
    [SerializeField] private Text moneyText;
    [SerializeField] private Text roundText;
    [SerializeField] private Text levelText;

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject nextLevelPanel;
    public static int curLevel = 0;
    public static int curRound = 0;
    public static int curMoney;
    // Start is called before the first frame update
    void Start()
    {
        curRound = 0;
        
        curMoney = startMoney;
        curHealth = startHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (curRound == 4 && EnemySpawner.curEnemies == 0)
        {
            //load next level
           
            nextLevelPanel.SetActive(true);
            
        }
        healthText.text = "Health: " + curHealth.ToString();
        roundText.text = "Round: " + curRound.ToString();
        levelText.text = "Current Level: " + (curLevel + 1).ToString();
        moneyText.text = "Money: $" + curMoney.ToString();
    }

    public void TakeDamage(int dmg)
    {
        curHealth -= dmg;
        if(curHealth <= 0)
        {
            gameOverPanel.SetActive(true);
            //GameOver Panel
        }

    }
    public void nextLevel()
    {
        curLevel = SceneManager.GetActiveScene().buildIndex + 1;
        EnemySpawner.curEnemies = 2;
        SceneManager.LoadScene((curLevel)%3);
    }

    public void restart()
    {
        gameOverPanel.SetActive(false);
        EnemySpawner.curEnemies = 2;
        curLevel = 0;
        SceneManager.LoadScene(0);
        

    }
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}
