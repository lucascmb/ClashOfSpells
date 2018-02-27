using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    Image[] playersImage;

	// Use this for initialization
	void Start () {
		playersImage = new Image[5];
        playersImage = FindObjectsOfType<Image>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.X))
        {
            InstantiateCard(1);
        }
	}

    void InstantiateCard(int index)
    {
        //playersImage[index] = FindObjectOfType<Image>();
        //playersImage[index].gameObject.SetActive(true);
        print(playersImage[0]);
    }
}
