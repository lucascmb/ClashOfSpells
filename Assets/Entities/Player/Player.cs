using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : PlayerBehaviour, IKillable
{


    private bool attacking = false; // a boolean to check if the player is attacking or not
    private bool dashing = false; // a boolean to check if the player is dashing or not
    private bool isJumping = false; // a boolean to check if the player is jumping or not
    private bool isFalling = false; // a boolean to check if the player is falling or not
    private bool push = false; // a boolean to check if the player is in time to next action in order to cast the fireball

    private float dashCoolDown; // the cooldown's value for the dash
    private float dashValue; // the dash speed value
    private float spellTime = 0f; // the time that the player has end the spell casting from the beginning of it casts;

    private Rigidbody2D rb; // player's rigidibody
    private SpriteRenderer sr; // player's sprite rendering
    private Animator anim; // player's animator

    public GameObject [] spellSet;  // all the spells available in the game
    private Dictionary<string, GameObject> spells;  // all the spells that was chosen by the player

    public float autoVel; // the maximum velocity that the player can reach just walking
    public float dashSpeed = 2f; // the multiplier from dash

    public float meleeDamage = 2f; // the damage that the player do when it is attacking with its sword;

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
    }

    void Update()
    {
        HorizontalMovement();
        VerticalMovement();
        CheckSpell();
        Attack();
    }

    void HorizontalMovement()
    {

        if (anim.GetFloat("down") <= 0)
        {
            if (Input.GetAxisRaw("Horizontal") > 0.2f && !dashing)
            {
                rb.velocity = new Vector2(autoVel * Input.GetAxisRaw("Horizontal"), rb.velocity.y);
                anim.SetFloat("walking", 2);
                sr.flipX = false;
                setRightCollider();
            }
            else if (Input.GetAxisRaw("Horizontal") < -0.2f && !dashing)
            {
                rb.velocity = new Vector2(autoVel * Input.GetAxisRaw("Horizontal"), rb.velocity.y);
                anim.SetFloat("walking", 4);
                sr.flipX = true;
                setLeftCollider();
            }
            else if (!dashing)
            {
                Stop();
            }
            if ((Input.GetKeyDown(KeyCode.Joystick1Button5) || Input.GetKeyDown(KeyCode.Joystick1Button4)) && 
                Time.time > dashCoolDown && 
                !attacking)
            {
                dashCoolDown = Time.time + 2f;
                anim.SetBool("dashing", true);
                StartCoroutine(Dashing());
            }
        }
    }


    IEnumerator Dashing()
    {
        if (!dashing)
        {
            dashing = true;
            dashValue = Time.time + dashSpeed;
            dashValue = dashValue - Time.time;

            Vector2 mov = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            mov.Normalize();
            rb.velocity = mov * dashValue * autoVel;
        }
        yield return new WaitForSeconds(.4f);
        Stop();
    }

    void Stop()
    {
        if (dashing)
        {
            dashing = false;
            anim.SetBool("dashing", false);
            rb.velocity = new Vector2(0, 0);
        }
        rb.velocity = new Vector2(0, rb.velocity.y);
        anim.SetFloat("walking", 0);
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
                    anim.SetBool("jumping", true);
                    anim.SetBool("falling", false);
                    isJumping = true;
                    isFalling = false;
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
        if (!isJumping)
        {
            if (rb.velocity.y < -2f)
            {
                isFalling = true;
                anim.SetBool("falling", true);
            }
        }
        else
        {
            if (rb.velocity.y < -0.2f)
            {
                isFalling = true;
                anim.SetBool("falling", true);
            }
        }
        if (isFalling) { 
            if (rb.velocity.y > -0.2f)
            {
                isFalling = false;
                isJumping = false;
                anim.SetBool("falling", false);
                anim.SetBool("jumping", false);
            }
        }
    }

    void setFalling()
    {
        anim.SetBool("falling", true);
    }

    void CheckSpell()
    {
        if (Input.GetAxis("Vertical") < -0.95f)
        {
            if(spellTime < Time.time)
            {
                spellTime = Time.time + 0.3f;
            }
        }
        if (Input.GetAxis("Horizontal") > 0.95f || Input.GetAxis("Horizontal") < -0.95f)
        {
            if(spellTime > Time.time)
            {
                push = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button1) && spellTime > Time.time && push && !attacking)
        {
            GameObject s;
            spells.TryGetValue("Fireball", out s);
            push = false;
            spellTime = 0f;

            FireBall.Cast(this.transform.position, Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), righe, s);
        }
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button2) && anim.GetFloat("down") <= 0 && !dashing)
        {
            attacking = true;
            anim.SetBool("attacking", true);
            if (righe)
            {
                this.transform.GetChild(3).gameObject.SetActive(true);
            }else
            {
                this.transform.GetChild(4).gameObject.SetActive(true);
            }
        }
    }

    void SetAttackOff()
    {
        attacking = false;
        anim.SetBool("attacking", false);
        if (this.transform.GetChild(3).gameObject.activeSelf)
        {
            this.transform.GetChild(3).gameObject.SetActive(false);
        } else if (this.transform.GetChild(4).gameObject.activeSelf)
        {
            this.transform.GetChild(4).gameObject.SetActive(false);
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

    public float GetMeleeDamage()
    {
        return meleeDamage;
    }

    public void Recoil(Vector2 force)
    {
        //
    }
}