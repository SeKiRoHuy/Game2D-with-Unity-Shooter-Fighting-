using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dame_Enermy: MonoBehaviour
{
    public float Speed;
    public int hust;

    public GameObject bulletDie;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enermys enermy = collision.gameObject.GetComponent<Enermys>();
        Charactor charactor = collision.gameObject.GetComponent<Charactor>();
        Boos_Fight boos_Fight = collision.gameObject.GetComponent<Boos_Fight>();


        if (enermy)
        {
            enermy.currenHust(hust);
        }
        if (boos_Fight)
        {
            boos_Fight.currenHust(hust);
        }

        if (charactor)
        {
            charactor.currenHust(hust);
        }
        Instantiate(bulletDie, transform.position, transform.rotation);
        

        
        Destroy(this.gameObject);
    }


    private void FixedUpdate()
    {
        transform.position += transform.right * Time.deltaTime * Speed;
    }
}
