using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StaticData : MonoBehaviour
{
    public static StaticData ins;
    void Awake()
    {
        ins = this;
    }
    public ColorPalette ActiveColorPalette;
}