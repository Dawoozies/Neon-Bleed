using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;
public class TMPStyleSheetGenerator : MonoBehaviour
{
    public TMP_Text text;
    public string openingTags;
    public string closingTags;
    [ContextMenu("ReadTextStyleFromTextComponent")]
    public void ReadTextStyleFromTextComponent()
    {
        StringBuilder opening = new StringBuilder();
        StringBuilder closing = new StringBuilder();

        if((text.fontStyle & FontStyles.Bold) != 0)
        {
            opening.Append("<b>");
            closing.Append("</b>");
        }

        Color textColor = text.color;
        string textColorHex = ColorUtility.ToHtmlStringRGBA(textColor);
        opening.Append($"<color=#{textColorHex}>");
        closing.Append("</color>");



        openingTags = opening.ToString();
        closingTags = closing.ToString();
    }
}
