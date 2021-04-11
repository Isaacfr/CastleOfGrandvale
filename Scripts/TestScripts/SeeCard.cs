using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeCard : MonoBehaviour
{
    public GameObject Canvas;

    private GameObject zoomCard;

    public void Awake()
    {
        Canvas = GameObject.Find("Main Canvas");
    }

    public void OnHoverEnter()
    {
        //instantiate a card in position of the mouse
        zoomCard = Instantiate(gameObject, new Vector2(Input.mousePosition.x, 400), Quaternion.identity);
        zoomCard.transform.SetParent(Canvas.transform, true);
        zoomCard.layer = LayerMask.NameToLayer("Zoom");

        RectTransform rectTransform = zoomCard.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(500, 600);
        //rectTransform.position = new Vector2(Input.mousePosition.x, 300);
    }

    public void OnHoverExit()
    {
        Destroy(zoomCard);
    }
}
