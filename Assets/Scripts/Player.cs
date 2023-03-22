using System.Collections;
using UnityEngine;

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
        transform.Translate(new Vector2(Input.GetAxisRaw("Horizontal"), 0) * Time.unscaledDeltaTime * speed);
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -5.5f, 5.5f), -3);
    }

    // 도망가는 스킬
    IEnumerator Run()
    {
        for (float Zangle = 0f; Zangle != 1f; Zangle += 0.01f)
        {
            Camera.main.transform.Rotate(0, 0, Zangle);
        }

        yield return new WaitForSeconds(10f);



    }

    private void Reset()
    {
        speed = 10f;
    }
}
