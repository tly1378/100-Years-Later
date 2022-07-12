using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public List<Transform> followers;
    public List<Transform> leavers;
    
    public float minimumSpeed;
    public float proportionalSpeed;

    private void FixedUpdate()
    {
        foreach (Transform follower in followers)
        {
            //距离
            Vector2 total = transform.position - follower.position;
            //移动方向
            Vector2 direction = total.normalized;
            //基础位移
            Vector2 minimumDisplacement = direction * minimumSpeed;
            //渐增位移
            Vector2 proportionalDisplacement = direction * total.sqrMagnitude * proportionalSpeed;
            //位移
            Vector2 displacement = minimumDisplacement + proportionalDisplacement;
            //单位位移
            displacement *= Time.deltaTime;
            //检查是否过度
            if (displacement.sqrMagnitude > total.sqrMagnitude)
            {
                follower.position = transform.position;
            }
            else
            {
                follower.Translate(displacement);
            }
        }
    }

    public void Follow(params Transform[] followers)
    {
        this.followers.AddRange(followers);
        leavers.RemoveAll((x) => followers.ToList().Contains(x));
    }
}
