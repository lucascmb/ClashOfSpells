using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour {

    enum Stage {preparing, choosingElement1, choosingElement2, elementChoosen};
    Stage stage;

    IElements[] elementsAvailable;

    public Player playerPrefab;

    private Vector3 pos;

    private IElements thisElement;

    private int currentElement = 0;
    public bool fliping = false;
    public bool disfliping = false;
    private bool righe = false;

    private bool animating = false;

    private CardBackground cardBg;

    public Fire fireElement;
    public Land landElement;
    public Water waterElement;
    public Air airElement;
    public Select selectPrefab;

    public int index;
    private Players players;
    private Animator cardAnimator;

    Player player;
    private Fire fire;
    private Land land;
    private Air air;
    private Water water;
    Select select;

    private int mostLeft;
    private int mostRight;

    private LayerMask mask;

	// Use this for initialization
	void Start () {

        cardAnimator = GetComponent<Animator>();
        players = FindObjectOfType<Players>();
        cardBg = GetComponentInChildren<CardBackground>();

        stage = Stage.preparing;
        index = MenuController.GetIndex();

        mask = 2048;

        transform.localPosition = new Vector3(0f, 0f, 0f);
        transform.rotation = Quaternion.Euler(0, 0, 90);
        transform.localScale = new Vector3(0f, 0f, 1f);

        elementsAvailable = new IElements[4];

        mostRight = elementsAvailable.Length - 1;
        mostLeft = 0;
    }
	
	// Update is called once per frame
	void Update () {
        ChoiceElement();
    }

    void SetReady()
    {
        cardAnimator.SetBool("ready", true);
    }

    void ChoiceElement()
    {
        if(stage == Stage.preparing)
        {
            transform.localScale = transform.localScale + new Vector3(Time.deltaTime * 0.8f, Time.deltaTime * 0.8f, 1f);
            if(transform.localScale.x >= 0.8f && transform.localScale.y >= 0.8f)
            {
                stage = Stage.choosingElement1;
                transform.localScale = new Vector3(0.8f, 0.8f, 1f);

                player = Instantiate(playerPrefab, this.transform.position, Quaternion.identity, this.transform);
                player.ChangeLayer(index);

                cardBg.InstantiateArrow();

                fire = Instantiate(fireElement, new Vector3(cardBg.transform.position.x - 2.5f, cardBg.transform.position.y, 0f), Quaternion.identity, this.transform);
                elementsAvailable[0] = fire;
                cardBg.ChangeColor(0);

                land = Instantiate(landElement, new Vector3(cardBg.transform.position.x - 2.5f, cardBg.transform.position.y, 0f), Quaternion.Euler(270, 0, 0), this.transform);
                elementsAvailable[1] = land;
                elementsAvailable[1].Off();

                water = Instantiate(waterElement, new Vector3(cardBg.transform.position.x - 2.5f, cardBg.transform.position.y, 0f), Quaternion.Euler(270, 0, 0), this.transform);
                elementsAvailable[2] = water;
                elementsAvailable[2].Off();

                air = Instantiate(airElement, new Vector3(cardBg.transform.position.x - 2.5f, cardBg.transform.position.y, 0f), Quaternion.Euler(270, 0, 0), this.transform);
                elementsAvailable[3] = air;
                elementsAvailable[3].Off();

                select = Instantiate(selectPrefab, new Vector3(cardBg.transform.position.x - 2.5f, cardBg.transform.position.y, 0f), Quaternion.identity, this.transform);
            }
        }

        if (stage == Stage.choosingElement1 || stage == Stage.choosingElement2)
        {
            if (Input.GetAxisRaw("Vertical" + index.ToString()) >= 0.95f && !fliping && !disfliping && !animating) {
                fliping = true;
                righe = true;
            }
            else if (Input.GetAxisRaw("Vertical" + index.ToString()) <= -0.95f && !fliping && !disfliping && !animating)
            {
                fliping = true;
                righe = false;
            }

            else if (fliping)
            {
                if ((elementsAvailable[currentElement].GetTransform().localEulerAngles.y > 270 || elementsAvailable[currentElement].GetTransform().localEulerAngles.y == 0))
                {
                    elementsAvailable[currentElement].GetTransform().Rotate((Time.deltaTime * 400) + elementsAvailable[currentElement].GetTransform().localEulerAngles.x, 0, 0);
                }

                if (elementsAvailable[currentElement].GetTransform().localEulerAngles.y <= 270 && elementsAvailable[currentElement].GetTransform().localEulerAngles.y != 0)
                {
                    elementsAvailable[currentElement].GetTransform().localRotation = Quaternion.Euler(0, 90, -90f);
                    elementsAvailable[currentElement].Off();
                    if (currentElement == mostRight && righe)
                    {
                        currentElement = mostLeft;
                    }
                    else if (currentElement == mostLeft && !righe)
                    {
                        currentElement = mostRight;
                    }
                    else
                    {
                        if (righe)
                        {
                            currentElement++;
                        }
                        else
                        {
                            currentElement--;
                        }
                    }
                    elementsAvailable[currentElement].On();
                    cardBg.ChangeColor(currentElement);
                    disfliping = true;
                    fliping = false;
                }
            }

            else if (disfliping)
            {
                elementsAvailable[currentElement].GetTransform().Rotate((Time.deltaTime * 400) + elementsAvailable[currentElement].GetTransform().localEulerAngles.x, 0, 0);

                if (elementsAvailable[currentElement].GetTransform().localEulerAngles.y >= 180)
                {
                    elementsAvailable[currentElement].GetTransform().rotation = Quaternion.Euler(0f, 0f, 0);

                    disfliping = false;
                    fliping = false;
                }
            }

            else if (Input.GetButtonDown("Jump" + index.ToString()) && !fliping && !disfliping && !animating)
            {
                select.SetTransform(player);

                if (stage == Stage.choosingElement1)
                {
                    thisElement = elementsAvailable[currentElement];

                    for (int i = currentElement; i < mostRight; i++)
                    {
                        elementsAvailable[i] = elementsAvailable[i + 1];
                        cardBg.colors[i] = cardBg.colors[i + 1];
                    }

                    mostRight--;
                    currentElement = mostRight;

                    fliping = righe = true;

                    thisElement.Off();

                    elementsAvailable[0].On();

                    stage = Stage.choosingElement2;
                }

                else if (stage == Stage.choosingElement2)
                {
                    stage = Stage.elementChoosen;
                    Destroy(select);
                    player.HideElements();
                    player.transform.SetParent(players.transform);
                    animating = true;
                }
            }
        }

        else if (stage == Stage.elementChoosen)
        {

            if (animating)
            {
                transform.localScale -= new Vector3(0, transform.localScale.y * Time.deltaTime * 5, 0);
                cardBg.transform.localScale -= new Vector3(0, cardBg.transform.localScale.y * Time.deltaTime * 5, 0);

                if (transform.localScale.y <= 0.1f || cardBg.transform.localScale.y <= 0.01f)
                {
                    MenuController.PreparePlayer(index - 1, player);
                    Destroy(cardBg.gameObject);
                    Destroy(this.gameObject);
                    player.Active();
                }
            }
        }
    }
}
