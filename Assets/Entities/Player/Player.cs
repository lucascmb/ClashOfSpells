using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : PlayerMethods, IKillable
{
    public int index; // the players' index for joystick management
    private bool active; // identify if the players is available for movements and actions or not
    private bool readyForBattle = false; // identify if the players is available for the battle or not
    private bool attacking = false; // a boolean to check if the player is attacking or not
    private bool dashing = false; // a boolean to check if the player is dashing or not
    private bool isJumping = false; // a boolean to check if the player is jumping or not
    private bool isFalling = false; // a boolean to check if the player is falling or not

    private float dashCoolDown; // the cooldown's value for the dash
    private float dashValue; // the dash speed value

    private Rigidbody2D rb; // player's rigidibody
    private SpriteRenderer sr; // player's sprite rendering

    public IElements[] elementsSet;  // the element's choosen by the player

    public float autoVel; // the maximum velocity that the player can reach just walking
    public float dashSpeed = 2f; // the multiplier from dash

    public float meleeDamage = 2f; // the damage that the player do when it is attacking with its sword;

    private Vector3 posForAnimation;
    private Position menuPosition;

    void Start()
    {
        active = false;

        rb = this.GetComponent<Rigidbody2D>();
        sr = this.GetComponent<SpriteRenderer>();
        anim = this.GetComponent<Animator>();

        index = MenuController.GetIndex();
        print(index);
        elementsSet = new IElements[2];
    }

    void Update()
    {
        if (active && !animated)
        {
            HorizontalMovement();
            VerticalMovement();
            Attack();
        }
        else if (animated)
        {
            Draining();
        }
    }

    void HorizontalMovement()
    {

        if (anim.GetFloat("down") <= 0)
        {
            if (Input.GetAxisRaw("Horizontal" + index.ToString()) > 0.2f && !dashing)
            {
                rb.velocity = new Vector2(autoVel * Input.GetAxisRaw("Horizontal" + index.ToString()), rb.velocity.y);
                anim.SetFloat("walking", 2);
                sr.flipX = false;
                setRightCollider();
            }
            else if (Input.GetAxisRaw("Horizontal" + index.ToString()) < -0.2f && !dashing)
            {
                rb.velocity = new Vector2(autoVel * Input.GetAxisRaw("Horizontal" + index.ToString()), rb.velocity.y);
                anim.SetFloat("walking", 4);
                sr.flipX = true;
                setLeftCollider();
            }
            else if (!dashing)
            {
                StopDashing();
            }
            if ((Input.GetButtonDown("Dash" + index.ToString())) &&
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

            Vector2 mov = new Vector2(Input.GetAxisRaw("Horizontal" + index.ToString()), Input.GetAxisRaw("Vertical" + index.ToString()));
            mov.Normalize();
            rb.velocity = mov * dashValue * autoVel;
        }
        yield return new WaitForSeconds(.4f);
        StopDashing();
    }

    void StopDashing()
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
            if (anim.GetFloat("down") <= 0)
            {
                if (Input.GetButtonDown("Jump" + index.ToString()))
                {
                    rb.velocity = new Vector2(this.GetComponent<Rigidbody2D>().velocity.x, 8f);
                    anim.SetBool("jumping", true);
                    anim.SetBool("falling", false);
                    isJumping = true;
                    isFalling = false;
                }
            }

            if (Input.GetAxisRaw("Vertical" + index.ToString()) < -0.95f)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
                anim.SetFloat("walking", 0);
                anim.SetFloat("down", 1);
                setDownCollider();
            }

            else if (Input.GetAxisRaw("Vertical" + index.ToString()) > -0.95f)
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
        if (isFalling)
        {
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

    void Attack()
    {
        if (Input.GetButtonDown("Attack" + index.ToString()) && anim.GetFloat("down") <= 0 && !dashing && !attacking && !anim.GetBool("attacking"))
        {
            attacking = true;
            anim.SetBool("attacking", true);
            if (righe)
            {
                this.transform.GetChild(3).gameObject.SetActive(true);
            }
            else
            {
                this.transform.GetChild(4).gameObject.SetActive(true);
            }
        }
    }

    void SetAttackOff()
    {
        anim.SetBool("attacking", false);

        if (this.transform.GetChild(3).gameObject.activeSelf)
        {
            this.transform.GetChild(3).gameObject.SetActive(false);

        }

        else if (this.transform.GetChild(4).gameObject.activeSelf)
        {
            this.transform.GetChild(4).gameObject.SetActive(false);
        }

        attacking = false;
    }

    public void Death()
    {
        if (life <= 0)
        {
            //
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

    public void Active() // Active or Desactive the player for actions and movements
    {
        if (!active)
        {
            active = true;
        }
        else
        {
            active = false;
        }
    }

    // All the methods below are just for animations 

    public void ChangeAniamtion(string name, bool value)
    {
        anim.SetBool(name, value);
    }

    public void HideElements()
    {
        elementsSet = transform.GetComponentsInChildren<IElements>();
        foreach (IElements element in elementsSet)
        {
            element.Off();
        }
    }

    public void ShowElements()
    {
        foreach (IElements element in elementsSet)
        {
            element.PrepareForBattle();
            element.On();
        }
    }

    public void SetAvailable()
    {
        MenuController.AddPlayersAvailable();
        anim.SetBool("drained", false);
        active = true;
        animated = false;
        setRightCollider();
    }

    public void PrepareForBattle()
    {
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        foreach (Transform child in transform)
        {
            child.gameObject.layer = (16);

        }

        gameObject.layer = 16;

        On();
    }

    public void ChangeLayer(int num)
    {
        Transform parent = transform;
        int layer = 15 + num;

        foreach (Transform child in parent)
        {
            child.gameObject.layer = (layer);
        }

        gameObject.layer = layer;
    }
}