using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpItem : Item
{
    protected override void ItemEffect(Player player)
    {
        player.PowerUp();
    }
}
