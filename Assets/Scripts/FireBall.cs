﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : Spell, ISpell {

	void Start () {
        damage = 2f;
	}

    public float GetDamage()
    {
        return damage;
    }

    public static void Cast(Vector3 pos, float axiX, float axiY, bool righe, GameObject f)
    {
        Vector2 vel = new Vector2((axiX), (axiY));
        Vector3 extPos = new Vector3(axiX, axiY, 0);

        if (righe) {
            vel = Vector2.right; extPos = Vector3.right;
            GameObject fb = Instantiate(f, pos + extPos, Quaternion.identity);
            fb.transform.GetComponent<Rigidbody2D>().velocity = vel * 10;
        }
        else if (!righe) {
            vel = Vector2.left; extPos = Vector3.left;
            GameObject fb = Instantiate(f, pos + extPos, Quaternion.Euler(0f, 0f, 180f));
            fb.transform.GetComponent<Rigidbody2D>().velocity = vel * 10;
        }       
    }

   /* private void OnTriggerEnter2D(Collider2D collision)
    {
        int playerLayer = 9;
        int layer = 1 << playerLayer;
        if (collision.IsTouchingLayers(layer))
        {
            collision.GetComponentInParent<Player>().TakeDamage(damage);
            //Destroy(this);
        }
    }*/
}