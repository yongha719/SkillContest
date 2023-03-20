using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;
    public float Speed
    {
        get
        {
            return speed;
        }

        set
        {
            speed = value;
        }
    }

    void Start()
    {

    }

    void FixedUpdate()
    {
        transform.Translate(new Vector2(Input.GetAxisRaw("Horizontal"), 0) * Time.deltaTime * speed);
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -5, 5), -4);
    }

    private void Reset()
    {
        speed = 10f;
    }
}
