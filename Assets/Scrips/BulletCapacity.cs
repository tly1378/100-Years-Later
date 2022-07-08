using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCapacity : MonoBehaviour
{
    public TMPro.TMP_Text text;
    private Gun weapon;
    private void Start()
    {
        weapon = Player.player.weapon as Gun;
    }
    private void Update()
    {
        text.text = $"{weapon.bulletRemaining}/{weapon.bulletCapacity}";
    }
}
