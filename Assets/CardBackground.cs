using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBackground : MonoBehaviour {

    public Color[] colors;

    private SpriteRenderer mySpriteRenderer;
    public GameObject arrow;
    private SpriteRenderer arrow1SpriteRenderer, arrow2SpriteRenderer;

    // Use this for initialization
    void Start () {
        mySpriteRenderer = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update () {
		
	}

    public void ChangeColor(int index)
    {
        if(index == 0) {
            arrow1SpriteRenderer.color = colors[1];
            arrow2SpriteRenderer.color = colors[3];
        }
        else if(index == 3)
        {
            arrow1SpriteRenderer.color = colors[0];
            arrow2SpriteRenderer.color = colors[2];
        }
        else
        {
            arrow1SpriteRenderer.color = colors[index + 1];
            arrow2SpriteRenderer.color = colors[index - 1];
        }
        mySpriteRenderer.color = colors[index];
    }

    public void InstantiateArrow()
    {
        GameObject arrow1 = Instantiate(arrow, new Vector3(transform.position.x - 2.5f, transform.position.y + 0.8f, 0f), Quaternion.identity, transform);
        arrow1SpriteRenderer = arrow1.GetComponent<SpriteRenderer>();
        GameObject arrow2 = Instantiate(arrow, new Vector3(transform.position.x - 2.5f, transform.position.y - 0.8f, 0f), Quaternion.identity, transform);
        arrow2SpriteRenderer = arrow2.GetComponent<SpriteRenderer>();
        arrow2.GetComponent<SpriteRenderer>().flipY = true;
        arrow1SpriteRenderer.color = colors[1];
        arrow2SpriteRenderer.color = colors[3];
    }
}
