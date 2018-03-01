using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Air : MonoBehaviour, IElements {

    SpriteRenderer sr;

    private bool flip;
    private float time = 0f;


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
        return "Air";
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
            transform.localScale = new Vector3(1.3f, 1.3f, 1);
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 255f);
        }
        else
        {
            flip = false;
            transform.localScale = new Vector3(1f, 1f, 1);
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
        if (transform.parent.tag == "Player")
        {
            selected = true;
            flip = false;
            transform.localScale = new Vector3(1.3f, 1.3f, 1);
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 255f);
            sr.flipX = false;
        }
    }

    public void Off()
    {
        this.gameObject.SetActive(false);
    }
}
