using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitKeyDown : CustomYieldInstruction
{
    private KeyCode key;

    public WaitKeyDown(KeyCode keyCode)
    {
        key = keyCode;
    }

    public override bool keepWaiting => Input.GetKeyDown(key) == false || Input.GetKey(key) == false;
}
