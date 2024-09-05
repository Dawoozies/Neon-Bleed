using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Element : MonoBehaviour
{
    protected GameObject SelectedObject => Elements.ins.selectedObject;
    protected bool isSelected;
    protected RectTransform rectTransform;
    protected virtual void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    protected virtual void Update()
    {
        if(gameObject == SelectedObject)
        {
            if(!isSelected)
            {
                //first frame this has been selected
                OnMouseOverEnter();
                isSelected = true;
            }
            else
            {
                OnMouseOverUpdate();
            }
        }

        if(gameObject != SelectedObject)
        {
            if(isSelected)
            {
                //first frame this has been not selected
                OnMouseOverExit();
                isSelected = false;
            }
        }
    }
    protected virtual void OnMouseOverEnter()
    {
        Debug.Log($"name = {gameObject.name} OnMouseOverEnter");
    }
    protected virtual void OnMouseOverUpdate()
    {
        Debug.Log($"name = {gameObject.name} OnMouseOverUpdate");
    }
    protected virtual void OnMouseOverExit()
    {
        Debug.Log($"name = {gameObject.name} OnMouseOverExit");
    }
}