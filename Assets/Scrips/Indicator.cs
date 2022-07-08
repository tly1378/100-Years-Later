using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    public RectTransform indicator;

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
        indicator.position = screenPos;
    }
}
