using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Charactor : MonoBehaviour
{
    private Rigidbody2D rb;
    private CircleCollider2D cc;
    private Animator anim;

    public static int maxhp = 100;
    public int HP;
    public float Speed;

    AudioSource source;
    public AudioClip step;
    public AudioClip die;

    public static UnityEvent OnHit = new UnityEvent();
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CircleCollider2D>();
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        if (CurrenHP())
        {
            return;
        }

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 direccionVelocidad = new Vector2(x, y).normalized * Speed;
        rb.AddForce(direccionVelocidad);

        if (rb.velocity.magnitude > 0.5)
        {
            if (!source.isPlaying && (x != 0 || y != 0))
            {
                source.PlayOneShot(step);
            }
        }
        
        anim.SetFloat("Move", rb.velocity.magnitude);

        // espejo el personaje en funcion del mouse
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        int flip = transform.position.x > mousePos.x ? 180 : 0;
        transform.localRotation = Quaternion.Euler(0, flip, 0);
    }

    IEnumerator DieCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene("GameOverMenu");
    }
    public void currenHust(int hust)
    {
        if (CurrenHP())
        {
            return;
        }

        HP -= hust;

        string hitTrigger;

        if (ShowHP())
        {
            hitTrigger = "BrokenTrigger";
        } else
        {
            hitTrigger = "DeadTrigger";
            cc.enabled = false;
            rb.velocity = Vector2.zero;
            source.PlayOneShot(die);

            StartCoroutine(DieCoroutine(die.length + 0.3f));
        }

        anim.SetTrigger(hitTrigger);
        OnHit.Invoke();
    }

    public void Currren(int cur)
    {
        HP += cur;
    }
    public bool CurrenHP()
    {
        return HP <= 0;
    }

    public bool ShowHP()
    {
        return !CurrenHP();
    }
}
