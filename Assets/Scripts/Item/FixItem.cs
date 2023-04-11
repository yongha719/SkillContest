using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixItem : Item
{
    protected override void ItemEffect(Player player)
    {
        player.Hp += 10f;
    }
}
