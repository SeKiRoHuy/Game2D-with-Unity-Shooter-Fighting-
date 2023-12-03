using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Charactor charactor;

    public GameObject enemigosContainer;
    

    
    public int enemiesCount;
    public int enemiesTotal;

    public static UnityEvent onUpdateEnemyCount = new UnityEvent();

    public int currentLevel;

    private void Awake()
    {
        if (instance)
            Destroy(this.gameObject);
        else
            instance = this; 
    }

    private void Start()
    {
        Enermys.onDieEnemy.AddListener(modifyEnemiesCount);
        enemiesCount = enemigosContainer.transform.childCount;
        enemiesTotal = enemiesCount;
        onUpdateEnemyCount.Invoke();
        Boos_Fight.onDieEnemy.AddListener(modifyEnemiesCount);
        enemiesCount = enemigosContainer.transform.childCount;
        enemiesTotal = enemiesCount;
        onUpdateEnemyCount.Invoke();

    }
    IEnumerator NextLevelCorutine(float time)
    {
        yield return new WaitForSeconds(time);

        if(currentLevel == 1)
        {
            SceneManager.LoadScene("Level2");

        } 
        if (currentLevel == 2)
        {
            SceneManager.LoadScene("Boss_Room");
        }
        if (currentLevel == 3)
        {
            SceneManager.LoadScene("Victory");
        }
    }
    private void modifyEnemiesCount()
    {
        enemiesCount--;
        onUpdateEnemyCount.Invoke();

        if (enemiesCount <= 0)
        {
            StartCoroutine(NextLevelCorutine(1f));
        }
    }

}
