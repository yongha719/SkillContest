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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {

        }
    }

    void FixedUpdate()
    {
        transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")) * Time.deltaTime * speed, Space.World);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -5.5f, 5.5f), 0, Mathf.Clamp(transform.position.z, -1.5f, 3f));
    }

    void CircleAttack()
    {
        for (int i = 0; i < 20; i++)
        {
            float angle = i * Mathf.PI * 2 / 20;
            float x = Mathf.Cos(angle) * 5f;
            float y = Mathf.Sin(angle) * 5f;
        }
    }

    // 도망가는 스킬
    IEnumerator Run()
    {
        for (float Zangle = 0f; Zangle != 1f; Zangle += 0.01f)
        {
            Camera.main.transform.Rotate(0, 0, Zangle);
            yield return null;
        }

        yield return new WaitForSeconds(10f);
    }

    private void Reset()
    {
        speed = 10f;
    }
}
