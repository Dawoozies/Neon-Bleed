using LitMotion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ElementMotions : MonoBehaviour
{
    [ReorderableList] public ElementMotionData[] motions;
    MotionHandle currentMotion;
    float t;
    Image image;
    private void Start()
    {
        image = GetComponent<Image>();
    }
    public void PlayMotion(int index)
    {
        if (index >= motions.Length || index < 0)
            return;

        if(currentMotion.IsActive())
        {
            currentMotion.Cancel();
        }

        t = 0f;
        currentMotion = LMotion.Create(transform.localScale, motions[index].scale, motions[index].transitionTime)
            .WithEase(motions[index].easing)
            .Bind(x =>
            {
                t += Time.deltaTime;
                image.color = motions[index].colorOverTransition.Evaluate(t / motions[index].transitionTime);
                transform.localScale = x;
            }
            );
    }
}
