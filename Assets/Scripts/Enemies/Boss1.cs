using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : Boss
{
    protected override float BasicAttackDelay => 3f;

    protected override float CircleAttackDelay => 5f;

    protected override float Damage => 4f;

}
