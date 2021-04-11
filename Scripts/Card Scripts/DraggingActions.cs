﻿using UnityEngine;
using System.Collections;

public abstract class DraggingActions : MonoBehaviour {

    public abstract void OnStartDrag();

    public abstract void OnEndDrag();

    public abstract void OnDraggingInUpdate();

    
    public virtual bool CanDrag
    {
        get
        {
            return true; //GlobalSettings.Instance.CanControlThisPlayer(playerOwner);
        }
    }

    protected abstract bool DragSuccessful();
}
