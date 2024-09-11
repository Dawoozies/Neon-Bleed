using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using uPools;

public class ScrollingTextArea : MonoBehaviour
{
    public GameObject textMeshPrefab;
    RectTransform rectTransform;
    Rect rect => rectTransform.rect;
    public int spawnCount;
    public Vector3 endPoint => -Vector3.right * (rect.size.x) / 2f;
    public Vector3 startPoint => Vector3.right * (rect.size.x) / 2f;
    public string[] textChoices;
    public List<TextMeshProUGUI> textMeshes = new();
    public float scrollSpeed;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    void Update()
    {
        if(textMeshes.Count < spawnCount)
        {
            CreateNewText();
        }

        if(textMeshes.Count > 0)
        {
            float halfWidth = (textMeshes[0].preferredWidth * textMeshes[0].pixelsPerUnit)/2f;
            //might not work when rotated
            if (textMeshes[0].rectTransform.localPosition.x + halfWidth < endPoint.x)
            {
                RemoveAtZero();
            }
        }

        ScrollText();
    }
    int currentTextChoice;
    void CreateNewText()
    {
        GameObject newTextObject = SharedGameObjectPool.Rent(textMeshPrefab);
        TextMeshProUGUI newTextMesh = newTextObject.GetComponent<TextMeshProUGUI>();
        newTextMesh.text = textChoices[currentTextChoice];
        newTextMesh.rectTransform.SetParent(rectTransform, false);

        newTextMesh.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newTextMesh.preferredWidth * newTextMesh.pixelsPerUnit);
        newTextMesh.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, rect.size.y);

        //get previous position
        if (textMeshes.Count > 0)
        {
            Vector3 lastElementPos = textMeshes[textMeshes.Count - 1].rectTransform.localPosition;
            //float halfWidthPrevious = (textMeshes[textMeshes.Count - 1].preferredWidth * textMeshes[textMeshes.Count - 1].pixelsPerUnit)/2f;
            float halfWidthPrevious = (textMeshes[textMeshes.Count - 1].rectTransform.rect.size.x )/2f;
            //float halfWidthCurrent = (newTextMesh.preferredWidth * newTextMesh.pixelsPerUnit) / 2f;
            float halfWidthCurrent = (newTextMesh.rectTransform.rect.size.x) / 2f;
            newTextMesh.rectTransform.localPosition = lastElementPos + Vector3.right * (halfWidthPrevious + halfWidthCurrent);
        }
        else
        {
            newTextMesh.rectTransform.localPosition = endPoint;
        }

        textMeshes.Add(newTextMesh);

        currentTextChoice++;
        currentTextChoice %= textChoices.Length;
    }

    float GetWidthOfAllText()
    {
        float width = 0f;
        foreach (TextMeshProUGUI textMesh in textMeshes)
        {
            width += textMesh.preferredWidth * textMesh.pixelsPerUnit;
        }
        return width;
    }
    //if it goes off screen just return it and remake it
    void RemoveAtZero()
    {
        GameObject toReturn = textMeshes[0].gameObject;
        SharedGameObjectPool.Return(toReturn);
        textMeshes.RemoveAt(0);
    }
    void ScrollText()
    {
        foreach (var item in textMeshes)
        {
            item.rectTransform.localPosition += Vector3.right * scrollSpeed * Time.deltaTime;
        }
    }
}