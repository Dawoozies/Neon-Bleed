using LitMotion;
using UnityEngine;
[CreateAssetMenu(fileName = "ColorPaletteMotionData", menuName = "ScriptableObjects/ColorPaletteMotionData", order = 2)]
public class ColorPaletteMotionData : ScriptableObject
{
    public float delay;
    public float transitionTime;
    public Ease easing;
}
