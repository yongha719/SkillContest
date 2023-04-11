using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelItem : Item
{
    protected override void ItemEffect(Player player)
    {
        player.Fuel += 15f;
    }
}
