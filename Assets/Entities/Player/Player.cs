using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IKillable
{

    public float life;

    private float spellTime = 0f;
    private bool push = false;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;

    private PolygonCollider2D col;

    public GameObject [] spellSet;  // all the spells available in the game

    private Dictionary<string, GameObject> spells;  // all the spells that was chosen by the player

    private bool isJumping = false;
    private bool righe = false;

    public float autoVel;

    void Start()
    {
        spells = new Dictionary<string, GameObject>();
        for (int i = 0; i < spellSet.Length; i++)
        {
            try
            {
                spells.Add(spellSet[i].name, spellSet[i]);
            }
            catch (System.NullReferenceException)
            {
                Debug.Log("FAILLED");
            }
        }
        rb = this.GetComponent<Rigidbody2D>();
        sr = this.GetComponent<SpriteRenderer>();
        anim = this.GetComponent<Animator>();
        col = this.GetComponent<PolygonCollider2D>();
    }

    void Update()
    {
        HorizontalMovement();
        VerticalMovement();
        CheckSpell();
    }

    void HorizontalMovement() {

        if (anim.GetFloat("down") <= 0)
        {
            if (Input.GetAxisRaw("Horizontal") > 0.2f)
            {
                rb.velocity = new Vector2(autoVel * Input.GetAxisRaw("Horizontal"), rb.velocity.y);
                anim.SetFloat("walking", 2);
                sr.flipX = false;
                setRightCollider();
            }
            else if (Input.GetAxisRaw("Horizontal") < -0.2f)
            {
                rb.velocity = new Vector2(autoVel * Input.GetAxisRaw("Horizontal"), rb.velocity.y);
                anim.SetFloat("walking", 4);
                sr.flipX = true;
                setLeftCollider();
            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
                anim.SetFloat("walking", 0);
            }
        }
    }

    void VerticalMovement()
    {
        if (!isJumping)
        {
            if(anim.GetFloat("down") <= 0)
            {
                if (Input.GetKeyDown(KeyCode.Joystick1Button0))
                {
                    rb.velocity = new Vector2(this.GetComponent<Rigidbody2D>().velocity.x, 6f);
                    isJumping = true;
                }
            }

            if (Input.GetAxisRaw("Vertical") < -0.95f)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
                anim.SetFloat("walking", 0);
                anim.SetFloat("down", 1);
                setDownCollider();
            }

            else if (Input.GetAxisRaw("Vertical") > -0.95f)
            {
                anim.SetFloat("down", -1);
                if (sr.flipX == false) { setRightCollider(); }
                else { setLeftCollider(); }
            }
        }
        if (rb.velocity.y == 0)
        {
            isJumping = false;
        }
    }

    void setDownCollider()
    {
        this.transform.GetChild(1).gameObject.SetActive(false);
        this.transform.GetChild(0).gameObject.SetActive(false);
        this.transform.GetChild(2).gameObject.SetActive(true);
    }

    void setRightCollider()
    {
        this.transform.GetChild(1).gameObject.SetActive(false);
        this.transform.GetChild(0).gameObject.SetActive(true);
        this.transform.GetChild(2).gameObject.SetActive(false);
        righe = true;
    }

    void setLeftCollider()
    {
        this.transform.GetChild(1).gameObject.SetActive(true);
        this.transform.GetChild(0).gameObject.SetActive(false);
        this.transform.GetChild(2).gameObject.SetActive(false);
        righe = false;
    }

    void CheckSpell()
    {
        if (Input.GetAxis("Vertical") < -0.9f)
        {
            if(spellTime < Time.time)
            {
                spellTime = Time.time + 1f;
            }
        }
        if (Input.GetAxis("Horizontal") > 0.9f || Input.GetAxis("Horizontal") < -0.9f)
        {
            if(spellTime > Time.time)
            {
                push = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button1) && spellTime > Time.time && push)
        {
            GameObject s;
            spells.TryGetValue("Fireball", out s);
            push = false;
            spellTime = 0f;

            FireBall.Cast(this.transform.position, Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), righe, s);
        }
    }

    public void Death()
    {
        if(life <= 0)
        {
            print("Death");
        }
    }

    public void TakeDamage(float damage)
    {
        life -= damage;
    }
}