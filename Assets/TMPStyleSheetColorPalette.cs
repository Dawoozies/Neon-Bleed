using LitMotion;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class TMPStyleSheetColorPalette : MonoBehaviour, IObserver<ColorPalette>
{
    public TMP_StyleSheet mainStyleSheet;
    //highlight
    //normal text
    //selectable
    public TextStyle highlightStyle;
    public TextStyle selectableStyle;
    public ObservedColorPalette ObservedActiveColorPalette;
    public int OrderPriority => 0;
    private void OnEnable()
    {
        if (ObservedActiveColorPalette != null)
            ObservedActiveColorPalette.RegisterObserver(this);
    }
    void OnDisable()
    {
        if (ObservedActiveColorPalette != null)
            ObservedActiveColorPalette.UnregsiterObserver(this);
    }
    public void OnSetReference(ColorPalette previousRef, ColorPalette newRef)
    {

    }
    private void Start()
    {
    }
}