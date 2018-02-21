using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackTrigger : MonoBehaviour {

    private Player player;
    private IKillable enemy;

    void Start()
    {
        player = this.transform.parent.gameObject.GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && collision.tag == "Enemy")
        {
            collision.GetComponent<IKillable>().TakeDamage(player.GetMeleeDamage());
        }
    }

}
