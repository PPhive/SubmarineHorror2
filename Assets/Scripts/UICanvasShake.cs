using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICanvasShake : MonoBehaviour
{
    [SerializeField] private float posIntensity = .5f;
    [SerializeField] private float scaleIntensity = .005f;
    private Vector3 originalPos;
    private RectTransform uiObj;

    private void Start()
    {
        uiObj = gameObject.GetComponent<RectTransform>();

        originalPos = uiObj.position;

        StartCoroutine(ShakeUIEnum());
    }

    private IEnumerator ShakeUIEnum()
    {
        while (true)
        {
            uiObj.position = originalPos + new Vector3(Random.Range(-posIntensity, posIntensity), Random.Range(-posIntensity, posIntensity), Random.Range(-posIntensity, posIntensity));
            uiObj.localScale = new Vector3(Random.Range(.8f - scaleIntensity, .8f + scaleIntensity), Random.Range(.8f - scaleIntensity, .8f + scaleIntensity), 1f);
            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();
            //yield return new WaitForSeconds(Random.Range(.05f, .2f));
        }
    }
}
