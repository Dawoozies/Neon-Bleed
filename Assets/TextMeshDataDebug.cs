using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextMeshDataDebug : MonoBehaviour
{
    TextMeshProUGUI textMesh;
    public float widthTakenUpByText;
    private void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        widthTakenUpByText = textMesh.preferredWidth * textMesh.pixelsPerUnit;
    }
}
