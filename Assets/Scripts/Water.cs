using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour, IElements {

    SpriteRenderer sr;

    private bool flip;
    private float time = 0f;

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
    }

    public string GetName()
    {
        return "Water";
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
        flip = true;
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 255f);
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
        if (transform.parent.tag == "Player")
        {
            selected = true;
            flip = false;
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 255f);
            sr.flipX = false;
        }
    }

    public void Off()
    {
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

    public void PrepareForBattle()
    {
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
