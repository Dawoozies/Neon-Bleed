using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SliderIntDisplay : MonoBehaviour
{
    public ObservedInt ObservedMaxValue;
    public ObservedInt ObservedValue;
    Slider slider;
    private void Start()
    {
        slider = GetComponent<Slider>();
    }
    private void Update()
    {
        slider.maxValue = (float)ObservedMaxValue.GetReference();
        slider.value = (float)ObservedValue.GetReference();
    }
}