using LitMotion;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
[CreateAssetMenu]
public class TextStyle : ScriptableObject
{
    public bool hasBeenModified;
    public int hash;
    public int paletteIndex = -1; //-1 == dont use palette
    [SerializeField] Color color;
    string[] colorTag = new string[2];
    [SerializeField] bool isBold;
    string[] boldTag = new string[2];

    MotionHandle colorMotionHandle;
    public ColorPaletteMotionData motionData;
    public void SetColor(Color value)
    {
        if (colorMotionHandle.IsActive())
            colorMotionHandle.Cancel();

        DoColorMotion(value);
    }
    public void UpdateTextStyleTags()
    {
        if(!hasBeenModified)
        {
            return;
        }
        colorTag[0] = $"<color={ColorUtility.ToHtmlStringRGBA(color)}>";
        colorTag[1] = "</color>";

        boldTag[0] = isBold ? "<b>" : "";
        boldTag[1] = isBold ? "</b>" : "";

        hasBeenModified = false;
    }
    void DoColorMotion(Color value)
    {
        LMotion.Create(color, value, motionData.transitionTime)
            .WithDelay(motionData.delay)
            .WithEase(motionData.easing)
            .Bind(x =>
            {
                color = x;
                hasBeenModified = true;
            });
    }
    public string GetOpeningTextStyleTags()
    {
        string tags = "";
        tags = boldTag[0] + colorTag[0];
        return tags;
    }
    public string GetClosingTextStyleTags()
    {
        string tags = "";
        tags = boldTag[1] + colorTag[1];
        return tags;
    }
}