using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puppet : MonoBehaviour, IKillable
{

    public float health = 100f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(health <= 0)
        {
            Death();
        }
	}

    public void Death()
    {
        Destroy(this.gameObject);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}
