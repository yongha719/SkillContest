using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WarningText : MonoBehaviour
{
    private TextMeshProUGUI text;
    float t;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        StartCoroutine(FadeAlpha());
    }

    private IEnumerator FadeAlpha()
    {
        while (text.alpha != 0)
        {
            t += Time.deltaTime / 2f;
            text.alpha = Mathf.Lerp(1, 0, t);
            yield return null;
        }

        text.alpha = 1f;
        t = 0f;
        gameObject.SetActive(false);
    }
}
