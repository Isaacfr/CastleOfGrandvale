using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggingTest : MonoBehaviour
{
    public bool pointerDisplacement = true;

    private bool dragging = false;

    private Vector3 vectorPointerDisplacement = Vector3.zero;

    private float zAxisDisplacement;

    HoverTest hoverTest;

    private void Awake()
    {
         hoverTest = new HoverTest();
    }

    private void OnMouseDown()
    {
        dragging = true;
        hoverTest.DisablePreviews();
    
    zAxisDisplacement = -Camera.main.transform.position.z + transform.position.z;
        if (pointerDisplacement)
        {
            vectorPointerDisplacement = -transform.position + MouseInWorldCoords();
        }
        else
        {
            vectorPointerDisplacement = Vector3.zero;
        }
       
    }

    private void Update()
    {
        if(dragging)
        {
            Vector3 mousePos = MouseInWorldCoords();

            transform.position = new Vector3( mousePos.x - vectorPointerDisplacement.x, mousePos.y - vectorPointerDisplacement.y, transform.position.z);

        }

    }

    private void OnMouseUp()
    {
        if (dragging)
        {
            dragging = false;
        }


    }
    private Vector3 MouseInWorldCoords()
    {
        var screenMousePos = Input.mousePosition;

        screenMousePos.z = zAxisDisplacement;

        return Camera.main.ScreenToWorldPoint(screenMousePos);
    }
}
