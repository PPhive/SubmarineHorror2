using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightLerp : MonoBehaviour
{
    [SerializeField] private Color light1 = new Color(255, 193, 90);
    [SerializeField] private Color light2 = new Color(255, 163, 90);
    [SerializeField] private int delay = 1;
    private Light thisLight;

    private void Start()
    {
        thisLight = GetComponent<Light>();
        StartCoroutine(LightLerpEnum());
    }

    private IEnumerator LightLerpEnum()
    {
        while (true)
        {
            float duration = 1f;
            float elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                for (int i = 0; i < delay; i++)
                {
                    yield return null;
                    elapsedTime += Time.deltaTime;
                }

                thisLight.color = Color.Lerp(light1, light2, elapsedTime / duration);
            }

            elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                for (int i = 0; i < delay; i++)
                {
                    yield return null;
                    elapsedTime += Time.deltaTime;
                }

                thisLight.color = Color.Lerp(light2, light1, elapsedTime / duration);
            }
        }

    }
}
