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
    private static int [] playersReady;
    private static Player [] playersForDraining;
    
    private Players players;

    private Cards activeCardsObject;
    private Transform[] pos;

    public Cards cardsPrefab;

    Image[] playersImage;

    void Start ()
    {
        activeCardsObject = Instantiate(cardsPrefab, new Vector3(0f,0f,0f), Quaternion.identity);
        pos = new Transform[4];

        for (int i = 0; i < 4; i++)
        {
            pos[i] = activeCardsObject.transform.GetChild(i);
        }

        playersActive = new bool[4];
        playersReady = new int[4];
        playersForDraining = new Player[4];

        for (int i = 0; i < 4; i++)
        {
            playersReady[i] = 0;
        }

        players = FindObjectOfType<Players>();
	}
	
	void Update ()
    {
        if(MasterController.actualStage == "menu")
        {
            InstantiateCard();
            RestrictPlayer();
            PreparePlayer();
            CanStart();
        }
	}

    void InstantiateCard()
    {
        if (Input.GetButtonDown("Start1") && playersActive[0] == false)
        {
            print("Player 1 Criado");
            playersActive[0] = true;
            actualIndice = 0;
            activeCardsObject.InstantiateCard(0);
            playersOn++;
        }
        else if (Input.GetButtonDown("Start2") && playersActive[1] == false)
        {
            print("Player 2 Criado");
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

    public static void PreparePlayer(int index, Player player)
    {
        playersForDraining[index] = player;
        playersReady[index] = 1;
        print("playersReady[index] = " + playersReady[index] + " | playersForDraining[index] = " + playersForDraining[index] + " | index = " + index);
    }

    void PreparePlayer()
    {
        if (Input.GetButtonDown("Start1") && playersReady[0] == 1)
        {
            playersReady[0] = 2;
            Drain();
        }
        if (Input.GetButtonDown("Start2") && playersReady[1] == 1)
        {
            playersReady[1] = 2;
            Drain();
        }
        if (Input.GetButtonDown("Jump3") && playersReady[2] == 1)
        {
            playersReady[2] = 2;
            Drain();
        }
        if (Input.GetButtonDown("Jump4") && playersReady[3] == 1)
        {
            playersReady[3] = 2;
            Drain();
        }
    }

    void Drain()
    {
        for(int i = 0; i < 4; i++)
        {
            if (playersReady[i] == 2)
            {
                playersForDraining[i].StartDrain(pos[i]);
            }
        }
    }

    private void CanStart()
    {
        if(playersAvailable >= 1 && playersAvailable == playersOn)
        {
            playersAvailable = 0;
            playersOn = 0;

            players.transform.Rotate(0, 0, -90);
            DontDestroyOnLoad(players);
            DontDestroyOnLoad(FindObjectOfType<GameController>());
            MasterController.ChangeScene("game");

            SceneManager.LoadScene("Level_01");
        }
    }

    void RestrictPlayer()
    {
        int i = 0;

        foreach(Transform child in players.transform)
        {
            int index;
            index = child.GetComponent<Player>().index;
            child.GetComponent<Player>().RestrictPosition(pos[index - 1].position.x - 6f, pos[index - 1].position.x + 6f);
            i++;
        }

        i = 0;
    }
}
