using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select : MonoBehaviour {

    GameObject go;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        go = collision.gameObject;
    }

    public void SetTransform(Player player)
    {
        go.transform.SetParent(player.transform);
        go.GetComponent<IElements>().DisableCollisions();
    }
}
