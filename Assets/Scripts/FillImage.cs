using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillImage : MonoBehaviour
{
    [SerializeField]
    private Image fillImage;

    Coroutine FillCoroutine;

    public void StartFill(float time)
    {
        FillCoroutine = StartCoroutine(EStartFill(time));
    }

    private IEnumerator EStartFill(float time)
    {
        float t = 0f;

        while (fillImage.fillAmount != 1)
        {
            t += Time.deltaTime / time;
            fillImage.fillAmount = Mathf.Lerp(0, 1, t);
            yield return null;
        }

        fillImage.fillAmount = 0f;
        FillCoroutine = null;
    }


    public void StopFill()
    {
        if (FillCoroutine != null)
            StopCoroutine(FillCoroutine);

        fillImage.fillAmount = 0f;
    }
}
