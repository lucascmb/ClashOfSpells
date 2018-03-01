using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBehaviour : MonoBehaviour {

    public float life;
    protected bool righe = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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

    void On()
    {
        gameObject.SetActive(true);
    }

    void Off()
    {
        gameObject.SetActive(false);
    }

    public void SetAvailable()
    {
        MenuController.AddPlayersAvailable();
    }

}
