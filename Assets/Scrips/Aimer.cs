using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aimer : MonoBehaviour
{
    public Transform target;
    private void Update()
    {
        if (Time.timeScale==0)
            return;
#if UNITY_ANDROID
        FollowJoy(transform);
#else
        LookAtMouse(target);
#endif
    }

    void LookAtMouse(Transform target = null)
    {
        if(target == null)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.up = mousePosition - transform.position;
        }
        else
        {
            transform.up = target.position - transform.position;
        }
    }

    void FollowJoy(Transform transform)
    {
        Vector2 vector2 = Player.player.fire_joystick.joystick;
        if (vector2.sqrMagnitude>0)
            transform.up = vector2;
    }

    void FollowMouse(Transform transform)
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
