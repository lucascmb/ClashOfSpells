using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerMethods : MonoBehaviour {

    public float life;
    public bool righe = false;
    protected Animator anim; // player's animator

    protected bool animated = false; // a boolean to check if the player is on an animation

    Transform menuPosition;

    // Use this for initialization
    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected void setDownCollider()
    {
        this.transform.GetChild(1).gameObject.SetActive(false);
        this.transform.GetChild(0).gameObject.SetActive(false);
        this.transform.GetChild(2).gameObject.SetActive(true);
    }

    protected void setRightCollider()
    {
        this.transform.GetChild(1).gameObject.SetActive(false);
        this.transform.GetChild(0).gameObject.SetActive(true);
        this.transform.GetChild(2).gameObject.SetActive(false);
        righe = true;
    }

    protected void setLeftCollider()
    {
        this.transform.GetChild(1).gameObject.SetActive(true);
        this.transform.GetChild(0).gameObject.SetActive(false);
        this.transform.GetChild(2).gameObject.SetActive(false);
        righe = false;
    }

    protected void setAllCollidersOff()
    {
        this.transform.GetChild(1).gameObject.SetActive(false);
        this.transform.GetChild(0).gameObject.SetActive(false);
        this.transform.GetChild(2).gameObject.SetActive(false);
    }

    public void On()
    {
        gameObject.SetActive(true);
    }

    protected void Off()
    {
        gameObject.SetActive(false);
    }

    public void RestrictPosition(float min, float max)  // Used in the menu scene to limitate the player's movement
    {
        //print("Posição atual do Player : " + transform.position.x + " | Posição min / max : " + min + " / " + max);
        this.transform.position = new Vector3(Mathf.Clamp(transform.position.x, min, max), transform.position.y, transform.position.z);
    }

    public void StartDrain(Transform pos)
    {
        animated = true;
        menuPosition = pos;
    }

    public void Draining()
    {
        this.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        
        if (transform.position.x > menuPosition.position.x - 0.1f && transform.position.x < menuPosition.position.x + 0.1f)
        {
            transform.position = menuPosition.position;
            anim.SetBool("drained", true);
        }

        else
        {
            Vector3 direction = menuPosition.position - transform.position;
            direction.Normalize();
            transform.position += new Vector3(direction.x * 4 * Time.deltaTime, direction.y * 4 * Time.deltaTime, 0);
        }
    }
}
