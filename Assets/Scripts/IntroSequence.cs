using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroSequence : MonoBehaviour
{
    [SerializeField] private Image blackOverlay = null;
    [SerializeField] private float textSpeed = 10f;
    [SerializeField] private AudioSource audioSourceAmbiance1 = null;
    [SerializeField] private AudioSource audioSourceAmbiance2 = null;
    [SerializeField] private AudioSource audioSourceButton = null;
    private Text text;
    private bool fading = false;

    private void Awake()
    {
        text = GetComponentInChildren<Text>();
        blackOverlay.color = new Color(0f, 0f, 0f, 0f);
    }

    private void Start()
    {
        StartCoroutine(NextSceneAfterOneMin());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !fading)
        {
            StartCoroutine(FadeOut());
        }
        text.rectTransform.position = text.rectTransform.position + new Vector3(0f, textSpeed * Time.deltaTime, 0f);
    }

    private IEnumerator NextSceneAfterOneMin()
    {
        yield return new WaitForSeconds(60f);
        if (!fading)
            StartCoroutine(FadeOut());
    }

        private IEnumerator FadeOut()
    {
        audioSourceButton.Play();
        fading = true;
        blackOverlay.color = new Color(0f, 0f, 0f, 0f);
        float elapsedTime = 0f;
        float duration = 2f;
        Color initColor = new Color(0f, 0f, 0f, 0f);
        float initVol1 = audioSourceAmbiance1.volume;
        float initVol2 = audioSourceAmbiance2.volume;
        Color finalColor = new Color(0f, 0f, 0f, 1f);
        float finalVol = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            blackOverlay.color = Color.Lerp(initColor, finalColor, elapsedTime / duration);
            audioSourceAmbiance1.volume = Mathf.Lerp(initVol1, finalVol, elapsedTime / duration);
            audioSourceAmbiance2.volume = Mathf.Lerp(initVol2, finalVol, elapsedTime / duration);
            yield return null;
        }
        blackOverlay.color = finalColor;
        audioSourceAmbiance1.volume = 0;
        audioSourceAmbiance2.volume = 0;
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(2);
    }
}
