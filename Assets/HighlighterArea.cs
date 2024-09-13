using LitMotion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlighterArea : Highlighter
{
    //if selected object within rect of an area swap to that one
    public GameObject highlightObject;
    public RectTransform[] areas;
    int activeArea;
    protected override void Start()
    {
        rectTransform = highlightObject.GetComponent<RectTransform>();
        //Elements.ins.RegisterSelectedObjectChangeCallback(OnSelectedObjectChanged, 0);
    }
    public override void OnSetReference(GameObject previousRef, GameObject newRef)
    {
        int areaObjectIsIn = 0;
        RectTransform targetRect;
        if(newRef.TryGetComponent(out targetRect))
        {
            Vector2 selectedObjectPos = targetRect.position;
            for (int i = 0; i < areas.Length; i++)
            {
                Vector2 center = areas[i].position;
                Vector2 size = areas[i].rect.size;
                Vector2 xBounds = Vector2.zero;
                xBounds.x = center.x - size.x / 2f;
                xBounds.y = center.x + size.x / 2f;
                Vector2 yBounds = Vector2.zero;
                yBounds.x = center.y - size.y / 2f;
                yBounds.y = center.y + size.x / 2f;
                bool inBounds = 
                    (xBounds.x < selectedObjectPos.x && selectedObjectPos.x < xBounds.y)
                    && (yBounds.x < selectedObjectPos.y && selectedObjectPos.y < yBounds.y);

                if(inBounds)
                {
                    Debug.Log($"RECT AREA {areas[i].gameObject.name} contains {newRef.name}");
                    areaObjectIsIn = i;
                    if(activeArea != areaObjectIsIn)
                    {
                        DoPositionMotion(areas[areaObjectIsIn]);
                        DoSizeMotion(areas[areaObjectIsIn]);
                        activeArea = areaObjectIsIn;
                    }
                    break;
                }
            }
        }
    }
}
