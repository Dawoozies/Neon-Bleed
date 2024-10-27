using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BloodlustDisplay : MonoBehaviour
{
    public ObservedFloat ObservedPlayerBloodlust;
    public TMP_Text text;
    private void Update()
    {
        text.text = $"BLOODLUST {Mathf.RoundToInt(ObservedPlayerBloodlust.GetReference()*100f)}%";
    }
}
