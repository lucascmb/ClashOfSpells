using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : Spell, ISpell {

	void Start () {
        damage = 10f;
	}

    public new float GetDamage()
    {
        return damage;
    }

    public string GetName()
    {
        return "FireBall";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector2 force;
        int playerLayer = 9;
        int layer = 1 << playerLayer;
        if (collision.IsTouchingLayers(layer))
        {
            collision.GetComponentInParent<IKillable>().TakeDamage(damage);
            if (this.transform.eulerAngles.z == 180f)
            {
                force = new Vector2(-200f, 0);
            }else
            {
                force = new Vector2(200f, 0);
            }
            collision.GetComponentInParent<IKillable>().Recoil(force);
            Destroy(this.gameObject);
        }else
        {
            //
        }
    }
}
