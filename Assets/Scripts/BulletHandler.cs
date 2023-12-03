using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletHandler : MonoBehaviour
{
    Text texto;
    Slider slider;

    void Start()
    {
        Arma.OnShot.AddListener(modifyBullets);
        BulletChest.OnUsedChest.AddListener(modifyBullets);

        texto = GetComponentInChildren<Text>();
        slider = GetComponent<Slider>();

        int quantity = GameManager.instance.charactor.GetComponentInChildren<Arma>().quantity;
        setBullets(quantity, Arma.maxQuantity);
    }

    private void setBullets(int current, int max)
    {
        texto.text = current.ToString() + " / " + max.ToString();
        slider.value = (float)current / max;
    }
    private void modifyBullets()
    {
        setBullets(GameManager.instance.charactor.GetComponentInChildren<Arma>().quantity, Arma.maxQuantity);
    }
}
