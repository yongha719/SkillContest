using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f;

    void Start()
    {

    }

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.unscaledDeltaTime);
    }
}
