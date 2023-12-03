using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Boos_Fight : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D bc;
    
    public GameObject Super_bullet;
    

    public Charactor charactor;

    public Transform aim;

    private Animator anim;

    private LayerMask layerMask;

    public float Speed;
    public int HP;
    private float time;
    private int nextAction;
    private float x, y;

    string[] status = { "clam", "violent", "friendly" };
    int est = 0;

    public float theshold;

    AudioSource source;
    public AudioClip clip;
    public AudioClip die;

    public static UnityEvent onDieEnemy = new UnityEvent();

    private int generateRandRange()
    {
        return 30 + Random.Range(-5, 10);
    }

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        bc = GetComponent<BoxCollider2D>();
        source = GetComponent<AudioSource>();

        x = 0;
        y = 0;
        time = 0;
        nextAction = generateRandRange();

        
        layerMask = 1 << gameObject.layer;
        layerMask = ~layerMask;
    }

    private void move()
    {
        x = Random.Range(-1, 2);
        y = Random.Range(-1, 2);
    }

    private void attach()
    {
        x = 0f;
        y = 0f;

        Vector3 diff = charactor.transform.position - transform.position;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, diff, Mathf.Infinity, layerMask);

        if (hit.collider.gameObject.GetComponent<Charactor>())
        {

            
            diff.Normalize();

            aim.right = diff;
            // Tạo ra 3 viên đạn với các góc khác nhau
            Instantiate(Super_bullet, transform.position + diff * 2.0f, aim.rotation);
            Instantiate(Super_bullet, transform.position + diff *2.0f, Quaternion.Euler(0,0,30) * aim.rotation);
            Instantiate(Super_bullet, transform.position + diff * 2.0f, Quaternion.Euler(0, 0, -30) * aim.rotation);
            Instantiate(Super_bullet, transform.position + diff * 2.0f, Quaternion.Euler(0, 0, 60) * aim.rotation);
            source.pitch = 1 + Random.Range(-0.1f, 0f);
            source.volume = 0.6f + Random.Range(-0.1f, 0.1f);
            source.PlayOneShot(clip);
            Debug.DrawRay(transform.position, diff, Color.green);
        }
        else
        {
            Debug.DrawRay(transform.position, diff, Color.red);
        }
    }
    
    private void spear()
    {
        x = 0f;
        y = 0f;
    }

    public bool entre(int valor, int desde, int hasta)
    {
        return desde <= valor && valor < hasta;
    }
    private void FixedUpdate()
    
    {
        if (CurrenHP())
        {
            return;
        }

        time += 1;

        updateEst();

        if (time == 150) // thay đổi giá trị này
        {
            int Acction = Random.Range(0, 100);

            if (est == 0) // pasivo
            {
                if (entre(Acction, 1, 100)) // thay đổi điều kiện này
                {
                    attach();
                }
                if (entre(Acction, 1, 20))
                {
                    move();
                }
                
            }

            if (est == 1) // Agresivo
            {
                if (entre(Acction, 1, 100)) // thay đổi điều kiện này
                {
                    move();
                }
            }

            if (est == 2) // amistoso
            {
                if (entre(Acction, 1, 100)) // thay đổi điều kiện này
                {
                    move();
                }
            }
            time = 0;
            nextAction = 150; // thay đổi giá trị này
        }

        mover();
    }

    //    mover();
    //}


    public void currenHust(int hust)
    {
        if (CurrenHP())
        {
            return;
        }

        HP -= hust;

        if (HP <= 0)
        {
            mata();
        }
        else
        {
            anim.SetTrigger("BrokenTrigger");
        }
    }
    public void mata()
    {
        x = 0f;
        y = 0f;

        mover();

        anim.SetTrigger("DeadTrigger");
        bc.enabled = false;

        source.PlayOneShot(die);

        onDieEnemy.Invoke();
    }

    public void mover()
    {
        Vector2 direccionVelocidad = new Vector2(x, y).normalized * Speed;

        //rb.velocity = direccionVelocidad;
        rb.AddForce(direccionVelocidad);
        anim.SetFloat("Move", rb.velocity.magnitude);
    }
    public bool CurrenHP()
    {
        return HP <= 0;
    }
    public bool estaVivo()
    {
        return !CurrenHP();
    }

    public void updateEst()
    {
        if (charactor.ShowHP())
        {
            est = Vector3.Distance(transform.position, charactor.transform.position) > theshold ? 0 : 1;

        }
        else
        {
            est = 2;
        }
    }
}
