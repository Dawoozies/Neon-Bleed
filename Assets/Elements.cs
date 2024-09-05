using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using uPools;

public class Elements : MonoBehaviour
{
    public static Elements ins;
    void Awake()
    {
        ins = this;
    }

    [SerializeField] GameObject prefab;
    ElementPool pool;

    GraphicRaycaster raycaster;
    PointerEventData pointerEventData;
    EventSystem eventSystem;

    public GameObject selectedObject;
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
            selectedObject = results[0].gameObject;
        }
    }
}
public class PooledElement
{
    public Element element;
}
public sealed class ElementPool : ObjectPoolBase<PooledElement>
{
    protected override PooledElement CreateInstance()
    {
        return new PooledElement();
    }
    protected override void OnDestroy(PooledElement instance)
    {
        //do the actual destroy of the object
    }
    protected override void OnRent(PooledElement instance)
    {
        //get starting position
        //create LMotion to onscreen pos
    }
    protected override void OnReturn(PooledElement instance)
    {
        //get offscreen position
        //from current position create LMotion to offscreen
    }
}