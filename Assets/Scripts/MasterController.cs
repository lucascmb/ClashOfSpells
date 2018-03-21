using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterController : MonoBehaviour {

    public enum Scene { Intro, Menu, Game };
    public static Scene scene;

    public static string actualStage;

	// Use this for initialization
	void Start () {
        actualStage = "menu";
	}
	
	// Update is called once per frame
	void Update () {
	}

    public static void ChangeScene(string name)
    {
        actualStage = name;
    }

}
