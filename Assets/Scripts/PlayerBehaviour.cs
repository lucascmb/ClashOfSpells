using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Map")
        {
            Transform parent = transform;

            foreach(Transform child in parent)
            {
                child.gameObject.layer = 13;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Transform parent = transform;

        foreach (Transform child in parent)
        {
            child.gameObject.layer = 12;
        }
    }

}
