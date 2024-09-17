using LitMotion;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
[CreateAssetMenu]
public class TextStyle : ScriptableObject
{
    public TMP_StyleSheet styleSheet;
    public int hash;
    [SerializeField] int paletteIndex = -1; //-1 == dont use palette
    [SerializeField] Color color;
    string[] colorTag = new string[2];
    [SerializeField] bool isBold;
    string[] boldTag = new string[2];
    public void UpdateTextStyle()
    {
        colorTag[0] = $"<color={ColorUtility.ToHtmlStringRGBA(color)}>";
        colorTag[1] = "</color>";

        boldTag[0] = isBold ? "<b>" : "";
        boldTag[1] = isBold ? "</b>" : "";
    }
    void DoColorMotion()
    {
        Color targetColor = color;
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