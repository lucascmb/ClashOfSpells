using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour, IElements {

    SpriteRenderer sr;

    private bool fireballAvailable = false;
    private bool castingSpell = false;

    private float castingStartTime;

    private bool flip;
    private float time = 0f;
    public FireBall fireballPrefab;

    private Player player;

    private bool selected;


    // Use this for initialization
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        flip = false;
        selected = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!selected)
        {
            FlipAnimation();
        }
        Stay();
        CastFireball();
    }

    public string GetName()
    {
        return "Fire";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Select")
        {
            FlipActive();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Select")
        {
            FlipActive();
        }
    }

    void FlipActive()
    {
        if (!flip)
        {
            flip = true;
            //transform.localScale = new Vector3(1.3f, 1.3f, 1);
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 255f);
        }
        else
        {
            flip = false;
            //transform.localScale = new Vector3(1f, 1f, 1);
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.5f);
        }
    }

    void FlipAnimation()
    {
       if (flip && Time.time > time)
        {
            if (sr.flipX)
            {
                sr.flipX = false;
            }
            else
            {
                sr.flipX = true;
            }
            time = Time.time + 0.5f;
        }
    }

    void Stay()
    {
        if(transform.parent.tag == "Player")
        {
            selected = true;
            flip = false;
            //transform.localScale = new Vector3(1.3f, 1.3f, 1);
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 255f);
            sr.flipX = false;
        }
    }

    public void Off()
    {
        player = transform.parent.GetComponent<Player>();
        this.gameObject.SetActive(false);
    }

    public void On()
    {
        this.gameObject.SetActive(true);
    }

    public void DisableCollisions()
    {
        GetComponent<Collider2D>().enabled = false;
    }

    void CastFireball()
    {

        if (Input.GetAxisRaw("Vertical" + player.index.ToString()) <= -0.95f)
        {
            print("começou aqui");
            castingSpell = true;
            castingStartTime = Time.time;
        }

        else if ((Input.GetAxisRaw("Horizontal" + player.index.ToString()) >= 0.95f || Input.GetAxisRaw("Horizontal" + player.index.ToString()) <= -0.95f) && castingStartTime + 1f > Time.time && castingSpell)
        {
            print("chegou aqui");
            fireballAvailable = true;
        }

        else if (Input.GetButtonDown("Spell" + player.index.ToString()) && fireballAvailable && castingStartTime + 1f > Time.time)
        {
            fireballAvailable = false;
            castingSpell = false;

            Vector3 vel, extPos;

            if (player.righe)
            {
                vel = Vector2.right; extPos = Vector3.right;
                FireBall fb = Instantiate(fireballPrefab, player.transform.position + extPos, Quaternion.identity);
                fb.transform.GetComponent<Rigidbody2D>().velocity = vel * 10;
            }
            else if (!player.righe)
            {
                vel = Vector2.left; extPos = Vector3.left;
                FireBall fb = Instantiate(fireballPrefab, player.transform.position + extPos, Quaternion.Euler(0f, 0f, 180f));
                fb.transform.GetComponent<Rigidbody2D>().velocity = vel * 10;
            }
        }
    }


    public void PrepareForBattle()
    {
        this.transform.localPosition = new Vector3(-8f + player.index, -7f, 0f); 
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
