using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
    public static float EnemyBulletScaledTime = 1f;
    public static float EnemyScaledTime = 1f;

    public static float EnemyBulletScaledDeltaTime => Time.deltaTime * EnemyBulletScaledTime;
    public static float EnemyScaledDeltaTime => Time.deltaTime * EnemyScaledTime;

    public static Player Player;

    public static float GetPlayerAngle(Vector3 mypos)
    {
        return Vector3.Angle(mypos, Player.transform.position);
    }

}
