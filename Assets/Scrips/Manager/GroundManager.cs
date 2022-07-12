using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundManager : MonoBehaviour
{
    public int space;

    Vector2 last;
    private void Update()
    {
        Vector2 position = Ceil(Camera.main.transform.position / space);
        if (position != last)
        {
            Vector2 change = position - last;
            Move(change);
            last = position;
        }

    }

    Vector2 Ceil(Vector2 position)
    {
        return new Vector2(Mathf.Ceil(position.x), Mathf.Ceil(position.y));
    }

    void Move(Vector2 change)
    {
        foreach(Transform transform in transform)
        {
            transform.position = (Vector3)change * space + transform.position;
        }
    }
}
