using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fliper : MonoBehaviour
{
    //public SpriteRenderer spriteRenderer;
    Vector3 scale;
    void Start()
    {
        scale = transform.localScale;
    }

    void Update()
    {
        if (transform.rotation.eulerAngles.z > 0 && transform.rotation.eulerAngles.z < 180)
        {
            scale.x = -1;
            transform.localScale = scale;
            //spriteRenderer.flipY = true;
        }
        else
        {
            scale.x = 1;
            transform.localScale = scale;
            //spriteRenderer.flipY = false;
        }
    }
}
