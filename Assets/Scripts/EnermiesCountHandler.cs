using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnermiesCountHandler : MonoBehaviour
{
    Text text;

    void Start()
    {
        GameManager.onUpdateEnemyCount.AddListener(setCount);
        text = GetComponent<Text>();
    }
    private void setCount()
    {
        int current = GameManager.instance.enemiesCount;
        int max = GameManager.instance.enemiesTotal;
        text.text = current.ToString() + " / " + max.ToString();
    }
}
