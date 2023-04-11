using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleItem : Item
{
    protected override void ItemEffect(Player player)
    {
        player.Invisible();
    }
}
