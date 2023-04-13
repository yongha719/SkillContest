using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    private bool IsVisible;

    private void Update()
    {
        transform.Translate(Vector3.back * 8 * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Bullet bullet))
            Destroy(bullet.gameObject);
    }

    private void OnBecameVisible()
    {
        IsVisible = true;
    }

    private void OnBecameInvisible()
    {
        if (IsVisible)
            Destroy(gameObject);
    }
}
