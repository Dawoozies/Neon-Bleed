using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Elements : MonoBehaviour
{
    public static Elements ins;
    void Awake()
    {
        ins = this;
    }

    [SerializeField] GameObject prefab;

    GraphicRaycaster raycaster;
    PointerEventData pointerEventData;
    EventSystem eventSystem;

    public ObservedGameObject ObservedSelectedObject;
    void Start()
    {
        raycaster = GetComponent<GraphicRaycaster>();
        eventSystem = GetComponent<EventSystem>();
    }
    void Update()
    {
        pointerEventData = new PointerEventData(eventSystem);
        pointerEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(pointerEventData, results);


        if(results.Count > 0)
        {
            if(ObservedSelectedObject.GetReference() != results[0].gameObject)
            {
                ObservedSelectedObject.SetReference(results[0].gameObject);
            }
        }
    }
}