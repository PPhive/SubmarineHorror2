using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    [SerializeField] private Image blackOverlay = null;
    [SerializeField] private Text titleText1 = null;
    [SerializeField] private Text titleText2 = null;
    [SerializeField] private Text titleText3 = null;
    [SerializeField] private Text subTitleText = null;
    [SerializeField] private Text startButtonText = null;
    private AudioSource audioSourceButton = null;
    [SerializeField] private AudioSource audioSourceAmbiance = null;
    private bool fading = false;

    private void Start()
    {
        audioSourceButton = GetComponent<AudioSource>();
        blackOverlay.color = new Color(0f, 0f, 0f, 0f);
        StartCoroutine(StartText());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            StartGame();
    }

    private IEnumerator StartText()
    {
        titleText1.color = new Color(1f, 1f, 1f, 0f);
        titleText2.color = new Color(1f, 1f, 1f, 0f);
        titleText3.color = new Color(1f, 1f, 1f, 0f);
        subTitleText.color = new Color(1f, 1f, 1f, 0f);
        startButtonText.color = new Color(1f, 1f, 1f, 0f);

        yield return new WaitForSeconds(5f);

        float elapsedTime = 0f;
        float duration = 2f;
        Color initColor = new Color(1f, 1f, 1f, 0f);
        Color finalColor = new Color(1f, 1f, 1f, 1f);
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            titleText1.color = Color.Lerp(initColor, finalColor, elapsedTime / duration);

            yield return null;
        }
        titleText1.color = finalColor;

        elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            titleText2.color = Color.Lerp(initColor, finalColor, elapsedTime / duration);

            yield return null;
        }
        titleText2.color = finalColor;

        elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            titleText3.color = Color.Lerp(initColor, finalColor, elapsedTime / duration);

            yield return null;
        }
        titleText3.color = finalColor;

        yield return new WaitForSeconds(2f);

        elapsedTime = 0f;
        duration = 4f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            subTitleText.color = Color.Lerp(initColor, finalColor, elapsedTime / duration);

            yield return null;
        }
        subTitleText.color = finalColor;

        elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            startButtonText.color = Color.Lerp(initColor, finalColor, elapsedTime / duration);

            yield return null;
        }
        startButtonText.color = finalColor;
    }

    public void StartGame()
    {
        Debug.Log("clicked");
        if (!fading)
            StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        Debug.Log("working");
        audioSourceButton.Play();
        fading = true;
        blackOverlay.color = new Color(0f, 0f, 0f, 0f);
        float elapsedTime = 0f;
        float duration = 2f;
        Color initColor = new Color(0f, 0f, 0f, 0f);
        float initVol = audioSourceAmbiance.volume;
        Color finalColor = new Color(0f, 0f, 0f, 1f);
        float finalVol = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            blackOverlay.color = Color.Lerp(initColor, finalColor, elapsedTime / duration);
            audioSourceAmbiance.volume = Mathf.Lerp(initVol, finalVol, elapsedTime / duration);
            yield return null;
        }
        blackOverlay.color = finalColor;
        audioSourceAmbiance.volume = 0;
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);

    }
}
