using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YTween : MonoBehaviour
{
    public static YTween Instance { get; private set; }


    public static void SmoothMove(Transform transform, Vector3 current, Vector3 target, float time)
        => Instance.StartCoroutine(SmoothCoroutine(transform, current, target, time));

    private static IEnumerator SmoothCoroutine(Transform transform, Vector3 current, Vector3 target, float time)
    {
        Vector3 velocity = Vector3.zero;

        transform.position = current;

        for (float offset = 0.01f; target.y + offset <= transform.position.y; offset = 0.01f)
        {
            transform.position
               = Vector3.SmoothDamp(transform.position, target, ref velocity, time);

            yield return null;
        }
        transform.position = target;

        yield return null;
    }
}
