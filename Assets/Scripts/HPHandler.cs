using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPHandler : MonoBehaviour
{
    Text text;
    Slider slider;

    void Start()
    {
        Charactor.OnHit.AddListener(modifyHealth);
        text = GetComponentInChildren<Text>();
        slider = GetComponent<Slider>();

        setHealth(GameManager.instance.charactor.HP,Charactor.maxhp);

        LifePackController.OnUsedMedikit.AddListener(modifyHealth);
    }

    private void setHealth(int current, int max)
    {
        text.text = current.ToString() + " / " + max.ToString();
        slider.value = (float)current / max;
    }
    private void modifyHealth()
    {
        setHealth(GameManager.instance.charactor.HP, Charactor.maxhp);
    }
}