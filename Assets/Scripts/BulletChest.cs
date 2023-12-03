using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BulletChest : MonoBehaviour
{
    public int quantity;
    public bool open;
    AudioSource source;
    public AudioClip clip;

    private Animator anim;
    public static UnityEvent OnUsedChest = new UnityEvent();

    private void Start()
    {
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Charactor charactor = collision.gameObject.GetComponent<Charactor>();
        if (charactor && !open)
        {
            Arma arma = charactor.GetComponentInChildren<Arma>();
            arma.agrerquantity(quantity);

            quantity = 0;
            open = true;

            anim.SetBool("open", open);
            source.PlayOneShot(clip);
            OnUsedChest.Invoke();
        }
    }
}
