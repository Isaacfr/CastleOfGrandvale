using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverTest : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject CardPreview;
    public GameObject OriginalCard;

    public Vector3 previewPosition;
    public float previewScale;

    public bool TurnOnPreviewing = false;

    public bool PreviewEnabled;
    /*Instantiate a new Card Preview of the same card
    Set the original card to disappear
    Set the position of preview
    Set the scale of preview
    Turn off the previewing when dragging cards
    */

    private void Awake()
    {
        CardPreview.SetActive(false);
    }

    private void OnMouseEnter()
    {
        TurnOnPreviewing = true;
        PreviewEnabled = true;
        CreateNewPreview();
    }

    private void OnMouseButtonDown()
    {
        DisablePreviews();
        PreviewEnabled = false;
        
    }

    private void OnMouseExit()
    {
        TurnOnPreviewing = false;
        CardPreview.transform.localScale = Vector3.one;
        CardPreview.SetActive(false);
        OriginalCard.SetActive(true);
        PreviewEnabled = false;
    }

    public void CreateNewPreview()
    {
        if (TurnOnPreviewing == true)
        {
            CardPreview.SetActive(true);
            //CardPreview.transform.SetParent(.transform);
            CardPreview.transform.localPosition = new Vector3(0, 2.5f, 0);
            CardPreview.transform.localScale = new Vector3(2.5f, 2.5f, 0);

            OriginalCard.SetActive(false);
        }
        else
        {
            DisablePreviews();
        }
    }

    public void DisablePreviews()
    {
        TurnOnPreviewing = false;
        CardPreview.SetActive(false);
        OriginalCard.SetActive(true);
    }

    private static bool CardIsPreviewed()
    {
        HoverTest[] allHoverTests = GameObject.FindObjectsOfType<HoverTest>();
        while (!Input.GetMouseButtonDown(0))
        {
            foreach (HoverTest ht in allHoverTests)
            {
                if(ht.TurnOnPreviewing)
                return true;
                //ht.gameObject.SetActive(false);
                //ht.transform.localPosition = new Vector3(0, 0, 5);
            }
        }

        return false;
    }

}
