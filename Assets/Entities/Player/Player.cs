using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : PlayerBehaviour, IKillable
{


    private float spellTime = 0f;
    private bool push = false;
    private bool dashing = false;
<<<<<<< HEAD
=======

    private float dashCoolDown;
>>>>>>> dashing
    private float dashValue;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;

    public GameObject [] spellSet;  // all the spells available in the game

    private Dictionary<string, GameObject> spells;  // all the spells that was chosen by the player

    private bool isJumping = false;
    private bool isFalling = false;

    public float autoVel;
    public float dashSpeed = 2f;

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
<<<<<<< HEAD
            if (Input.GetKeyDown(KeyCode.Joystick1Button5))
            {
                StartCoroutine("Dashing");
=======
            if (Input.GetKeyDown(KeyCode.Joystick1Button5) && Time.time > dashCoolDown)
            {
                dashCoolDown = Time.time + 2f;
                StartCoroutine(Dashing(true));
            }
            else if (Input.GetKeyDown(KeyCode.Joystick1Button4) && Time.time > dashCoolDown)
            {
                dashCoolDown = Time.time + 2f;
                StartCoroutine(Dashing(false));
>>>>>>> dashing
            }
        }
    }

<<<<<<< HEAD
    IEnumerator Dashing()
=======
    IEnumerator Dashing(bool right)
>>>>>>> dashing
    {
        if (!dashing)
        {
            dashing = true;
            dashValue = Time.time + dashSpeed;
            dashValue = dashValue - Time.time;
<<<<<<< HEAD
            if (righe)
=======
            if (right)
>>>>>>> dashing
            {
                rb.velocity = new Vector2(autoVel * dashValue * 2, rb.velocity.y);
            } else
            {
                rb.velocity = new Vector2(autoVel * dashValue * (-2), rb.velocity.y);
            }
        }
        yield return new WaitForSeconds(.1f);
        Stop();
    }

    void Stop()
    {
        if (dashing)
        {
            dashing = false;
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