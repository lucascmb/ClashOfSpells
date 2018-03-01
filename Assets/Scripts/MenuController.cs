using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    private static int playersAvailable = 0;
    private static int playersOn = 0;
    private static bool active;
    private static int actualIndice;

    private bool [] playersActive;
    
    private Players players;

    private Cards activeCardsObject;

    public Cards cardsPrefab;

    Image[] playersImage;

    void Start ()
    {
        activeCardsObject = Instantiate(cardsPrefab, new Vector3(-9f, 5f, -5f), Quaternion.identity);

        playersActive = new bool[4];

        players = FindObjectOfType<Players>();
	}
	
	void Update ()
    {
        InstantiateCard();
        CanStart();
	}

    void InstantiateCard()
    {
        if(true)
        {
            if (Input.GetButtonDown("Start1") && playersActive[0] == false)
            {
                playersActive[0] = true;
                actualIndice = 0;
                activeCardsObject.InstantiateCard(0);
                playersOn++;
            }
            else if (Input.GetButtonDown("Start2") && playersActive[1] == false)
            {
                playersActive[1] = true;
                actualIndice = 1;
                activeCardsObject.InstantiateCard(1);
                playersOn++;
            }
            else if (Input.GetButtonDown("Jump3") && playersActive[2] == false)
            {
                playersActive[2] = true;
                actualIndice = 2;
                activeCardsObject.InstantiateCard(2);
                playersOn++;
            }
            else if (Input.GetButtonDown("Jump4") && playersActive[3] == false)
            {
                playersActive[3] = true;
                actualIndice = 3;
                activeCardsObject.InstantiateCard(3);
                playersOn++;
            }
        }
    }

    public static int GetIndex()
    {
        return actualIndice + 1;
    }

    public static int GetPlayersAvailable()
    {
        return playersAvailable;
    }

    public static void AddPlayersAvailable()
    {
        playersAvailable = playersAvailable + 1;
    }

    private void CanStart()
    {
        if(playersAvailable >= 2 && playersAvailable == playersOn)
        {
            playersAvailable = 0;
            playersOn = 0;

            DontDestroyOnLoad(players);
            SceneManager.LoadScene("Level_01");

        }
    }
}
