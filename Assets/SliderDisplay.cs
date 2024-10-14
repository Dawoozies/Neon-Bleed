using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SliderDisplay : MonoBehaviour
{
    public ObservedFloat ObservedMaxValue;
    public ObservedFloat ObservedValue;
    Slider slider;
    private void Start()
    {
        slider = GetComponent<Slider>();
    }
    private void Update()
    {
        slider.maxValue = ObservedMaxValue.GetReference();
        slider.value = ObservedValue.GetReference();
    }
}
