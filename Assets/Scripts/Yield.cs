using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class WaitKeyDown : CustomYieldInstruction
{
    private KeyCode key;

    public WaitKeyDown(KeyCode keyCode)
    {
        key = keyCode;
    }

    public override bool keepWaiting => Input.GetKey(key) == false && Input.GetKeyDown(key) == false;
}
