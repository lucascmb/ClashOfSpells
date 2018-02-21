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

    public void Recoil(Vector2 force)
    {
        StartCoroutine(PushBack(force));
    }

    IEnumerator PushBack(Vector2 force)
    {
        this.GetComponent<Rigidbody2D>().AddForce(force);
        yield return new WaitForSeconds(.2f);
        Stop();
    }

    private void Stop()
    {
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, this.GetComponent<Rigidbody2D>().velocity.y);
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
