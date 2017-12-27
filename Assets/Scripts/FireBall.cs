using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : Spell, ISpell {

	// Use this for initialization
	void Start () {
        damage = 2f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public float GetDamage()
    {
        return damage;
    }

    public static void Cast(Vector3 pos, GameObject f)
    {
        GameObject fb = Instantiate(f, pos, Quaternion.identity);
        Vector3 test = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 vel = new Vector3((test.x - pos.x), (test.y - pos.y), 1);
        vel = Vector3.Normalize(vel);
        fb.transform.GetComponent<Rigidbody2D>().velocity = vel * 8;
    }
}
