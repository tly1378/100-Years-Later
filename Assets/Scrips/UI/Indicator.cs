using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Indicator : MonoBehaviour
{
    public RectTransform indicator;
    public Image image;

    private void Update()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPos.x < 0)
            screenPos.x = 0;
        if (screenPos.x > Screen.width)
            screenPos.x = Screen.width;
        if (screenPos.y < 0)
            screenPos.y = 0;
        if (screenPos.y > Screen.height)
            screenPos.y = Screen.height;

        if (screenPos.x > 0 && screenPos.x < Screen.width && screenPos.y > 0 && screenPos.y < Screen.height)
            image.enabled = false;
        else
            image.enabled = true;

        indicator.position = screenPos;
    }
}
