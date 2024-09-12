using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uPools;

public class ScrollingImages : MonoBehaviour
{
    RectTransform rectTransform;
    public GameObject prefab;
    public RectTransform[] scrollPoints;
    public int spawnCount;
    public float lerpObjectSpacing;
    public float lerpScrollSpeed;
    public List<ScrollingObject> scrollingObjects = new();
    [System.Serializable]
    public class ScrollingObject
    {
        public RectTransform rectTransform;
        public float lerpValue;
        public void Scroll(RectTransform[] scrollPoints, float timeDelta)
        {
            lerpValue += timeDelta;
            rectTransform.position = Vector3.LerpUnclamped(scrollPoints[0].position, scrollPoints[1].position, lerpValue);
        }
    }

    Vector3 scrollLine => scrollPoints[1].position - scrollPoints[0].position;
    Vector3 scrollDir => scrollLine.normalized;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    void Update()
    {
        if(scrollingObjects.Count < spawnCount)
        {
            CreateNewObject();
        }

        if(scrollingObjects.Count > 0)
        {
            if (scrollingObjects[0].lerpValue > 1)
            {
                GameObject toReturn = scrollingObjects[0].rectTransform.gameObject;
                SharedGameObjectPool.Return(toReturn);
                scrollingObjects.RemoveAt(0);
            }
        }

        ScrollText();
    }
    void CreateNewObject()
    {
        GameObject newObject = SharedGameObjectPool.Rent(prefab);
        RectTransform newRectTransform = newObject.GetComponent<RectTransform>();
        newRectTransform.SetParent(rectTransform);

        ScrollingObject newScrollingObject = new ScrollingObject();
        newScrollingObject.rectTransform = newRectTransform;

        if(scrollingObjects.Count > 0)
        {
            newScrollingObject.lerpValue = scrollingObjects[scrollingObjects.Count - 1].lerpValue - lerpObjectSpacing;
        }
        else
        {
            newScrollingObject.lerpValue = 1f;
        }

        newRectTransform.position = ScrollingObjectPosition(newScrollingObject);

        scrollingObjects.Add(newScrollingObject);
    }
    void ScrollText()
    {
        foreach(var item in scrollingObjects)
        {
            item.Scroll(scrollPoints, lerpScrollSpeed*Time.deltaTime);
        }
    }
    Vector3 ScrollingObjectPosition(ScrollingObject scrollingObject)
    {
        return Vector3.LerpUnclamped(scrollPoints[0].position, scrollPoints[1].position, scrollingObject.lerpValue);
    }
}
