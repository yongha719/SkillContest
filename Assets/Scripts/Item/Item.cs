using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    private bool IsVisible;

    void Update()
    {
        transform.Translate(Vector3.back * 8 * Time.deltaTime, Space.World);
    }

    protected abstract void ItemEffect(Player player);

    private void OnTriggerEnter(Collider other)
    {

        if (other.TryGetComponent(out Player player))
        {
            ItemEffect(player);

            GameManager.Instance.Score += 20;

            Destroy(gameObject);
        }
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
