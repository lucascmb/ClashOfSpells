using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IKillable
{

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;

    private PolygonCollider2D col;

    public GameObject [] spellSet;  // all the spells available in the game

    private Dictionary<string, GameObject> spells;  // all the spells that was chosen by the player

    private bool isJumping = false;

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
            if (Input.GetKey(KeyCode.D))
            {
                rb.velocity = new Vector2(autoVel, rb.velocity.y);
                anim.SetFloat("walking", 2);
                sr.flipX = false;
                setRightCollider();
            }
            else if (Input.GetKey(KeyCode.A))
            {
                rb.velocity = new Vector2(-autoVel, rb.velocity.y);
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
                if (Input.GetKeyDown(KeyCode.W))
                {
                    rb.velocity = new Vector2(this.GetComponent<Rigidbody2D>().velocity.x, 6f);
                    isJumping = true;
                }
            }

            if (Input.GetKey(KeyCode.S))
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
                anim.SetFloat("walking", 0);
                anim.SetFloat("down", 1);
                setDownCollider();
            }

            else if (Input.GetKeyUp(KeyCode.S))
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
    }

    void setLeftCollider()
    {
        this.transform.GetChild(1).gameObject.SetActive(true);
        this.transform.GetChild(0).gameObject.SetActive(false);
        this.transform.GetChild(2).gameObject.SetActive(false);
    }

    void CheckSpell()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject s;
            spells.TryGetValue("Fireball", out s);
            FireBall.Cast(this.transform.position, s);
        }
    }
}