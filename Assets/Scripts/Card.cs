using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour {

    enum Stage {preparing, choosingElement1, choosingElement2, elementChoosen, go, ready };
    Stage stage;

    enum Element { Fire, Land, Air, Water }
    Element selectable;

    public Player playerPrefab;

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

	// Use this for initialization
	void Start () {

        fire = Instantiate(fireElement, new Vector3 (this.transform.position.x - 0.8f, (7.1f - 10f), 0f), Quaternion.identity, this.transform);
        land = Instantiate(landElement, new Vector3 (this.transform.position.x + 0.8f, (7.1f - 10f), 0f), Quaternion.identity, this.transform);
        air = Instantiate(airElement, new Vector3(this.transform.position.x - 0.8f, (6.1f - 10f), 0f), Quaternion.identity, this.transform);
        water = Instantiate(waterElement, new Vector3(this.transform.position.x + 0.8f, (6.1f - 10f), 0f), Quaternion.identity, this.transform);

        select = Instantiate(selectPrefab, new Vector3(this.transform.position.x - 0.8f, (7.1f - 10f), 0f), Quaternion.identity, this.transform);

        cardAnimator = GetComponent<Animator>();
        players = FindObjectOfType<Players>();

        stage = Stage.preparing;
        selectable = Element.Fire;
        index = MenuController.GetIndex();
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
            if(transform.localScale.x == 1 && transform.localScale.y == 1)
            {
                transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
                stage = Stage.choosingElement1;
                player = Instantiate(playerPrefab, this.transform.position, Quaternion.identity, this.transform);
            }
        }

        if (stage == Stage.choosingElement1)
        {
            if (Input.GetAxisRaw("Horizontal" + index.ToString()) >= 0.95f && selectable != Element.Land && selectable != Element.Water)
            {
                selectable = selectable + 1;
                select.transform.position = new Vector3(select.transform.position.x + 1.6f, select.transform.position.y, select.transform.position.z);
            }
            else if (Input.GetAxisRaw("Horizontal" + index.ToString()) <= -0.95f && selectable != Element.Fire && selectable != Element.Air)
            {
                selectable = selectable - 1;
                select.transform.position = new Vector3(select.transform.position.x - 1.6f, select.transform.position.y, select.transform.position.z);
            }
            else if (Input.GetAxisRaw("Vertical" + index.ToString()) >= 0.95f && selectable != Element.Fire && selectable != Element.Land)
            {
                selectable = selectable - 2;
                select.transform.position = new Vector3(select.transform.position.x, select.transform.position.y + 1f, select.transform.position.z);
            }
            else if (Input.GetAxisRaw("Vertical" + index.ToString()) <= -0.95f && selectable != Element.Air && selectable != Element.Water)
            {
                selectable = selectable + 2;
                select.transform.position = new Vector3(select.transform.position.x, select.transform.position.y - 1f, select.transform.position.z);
            }
            else if (Input.GetButtonDown("Jump" + index.ToString()))
            {
                select.SetTransform(player);
                stage = Stage.choosingElement2;
            }
        }

        else if (stage == Stage.choosingElement2)
        {
            if (Input.GetAxisRaw("Horizontal" + index.ToString()) >= 0.95f && selectable != Element.Land && selectable != Element.Water)
            {
                selectable = selectable + 1;
                select.transform.position = new Vector3(select.transform.position.x + 1.6f, select.transform.position.y, select.transform.position.z);
            }
            else if (Input.GetAxisRaw("Horizontal" + index.ToString()) <= -0.95f && selectable != Element.Fire && selectable != Element.Air)
            {
                selectable = selectable - 1;
                select.transform.position = new Vector3(select.transform.position.x - 1.6f, select.transform.position.y, select.transform.position.z);
            }
            else if (Input.GetAxisRaw("Vertical" + index.ToString()) >= 0.95f && selectable != Element.Fire && selectable != Element.Land)
            {
                selectable = selectable - 2;
                select.transform.position = new Vector3(select.transform.position.x, select.transform.position.y + 1f, select.transform.position.z);
            }
            else if (Input.GetAxisRaw("Vertical" + index.ToString()) <= -0.95f && selectable != Element.Air && selectable != Element.Water)
            {
                selectable = selectable + 2;
                select.transform.position = new Vector3(select.transform.position.x, select.transform.position.y - 1f, select.transform.position.z);
            }
            else if (Input.GetButtonDown("Jump" + index.ToString()))
            {
                select.SetTransform(player);
                stage = Stage.elementChoosen;
            }
        }
        else if (stage == Stage.elementChoosen)
        {
            Destroy(select);
            player.Active();

            if (Input.GetButtonDown("Start" + index.ToString()))
            {
                stage = Stage.go;
                player.HideElements();
            }
        }
        else if (stage == Stage.go)
        {
            player.Portal();
            player.transform.SetParent(players.transform);
            transform.position += new Vector3(0f, -8 * Time.deltaTime, 0f);

            if (transform.position.y <= -10)
            {
                transform.position = new Vector3(transform.position.x, -10f, transform.position.y);
                stage = Stage.ready;
            }
        }

        else if(stage == Stage.ready)
        {
            Deactivate();
        }
    }

    void Deactivate()
    {
        this.gameObject.SetActive(false);
    }
}
