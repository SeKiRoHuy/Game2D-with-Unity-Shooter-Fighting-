using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LifePackController : MonoBehaviour
{
    public int cur;
    public bool open;
    AudioSource source;
    public AudioClip clip;

    private Animator anim;
    public static UnityEvent OnUsedMedikit = new UnityEvent();

    private void Start()
    {
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Charactor charactor = collision.gameObject.GetComponent<Charactor>();
        if (charactor && charactor.ShowHP() && charactor.HP + cur <= Charactor.maxhp && !open)
        {
           charactor.Currren(cur);

            cur = 0;
            open = true;

            anim.SetBool("open", open);
            source.PlayOneShot(clip);
            OnUsedMedikit.Invoke();
        }
    }
}
