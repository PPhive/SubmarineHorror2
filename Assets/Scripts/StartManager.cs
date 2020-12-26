using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    [SerializeField] private Image blackBackground = null;
    [SerializeField] private Image blackOverlay = null;
    [SerializeField] private Text titleText = null;
    [SerializeField] private Text subTitleText = null;
    [SerializeField] private Text startButtonText = null;
    private AudioSource audioSourceButton = null;
    private bool fading = false;

    private void Start()
    {
        audioSourceButton = GetComponent<AudioSource>();
        blackBackground.color = new Color(0f, 0f, 0f, 1f);
        blackOverlay.color = new Color(0f, 0f, 0f, 0f);
        StartCoroutine(StartText());
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            StartGame();
        else if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    private IEnumerator StartText()
    {

        titleText.color = new Color(1f, 1f, 1f, 0f);
        subTitleText.color = new Color(1f, 1f, 1f, 0f);
        startButtonText.color = new Color(1f, 1f, 1f, 0f);
        yield return new WaitForSeconds(2f);

        float elapsedTime = 0f;
        float duration = 6f;
        Color initColor = new Color(0f, 0f, 0f, 1f);
        Color finalColor = new Color(0f, 0f, 0f, 0f);
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            blackBackground.color = Color.Lerp(initColor, finalColor, elapsedTime / duration);

            yield return null;
        }
        blackBackground.color = finalColor;

        yield return new WaitForSeconds(4f);

        elapsedTime = 0f;
        duration = 4f;
        initColor = new Color(1f, 1f, 1f, 0f);
        finalColor = new Color(1f, 1f, 1f, 1f);
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            titleText.color = Color.Lerp(initColor, finalColor, elapsedTime / duration);

            yield return null;
        }
        titleText.color = finalColor;

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
        yield return new WaitForSeconds(2f);

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
        Color finalColor = new Color(0f, 0f, 0f, 1f);
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            blackOverlay.color = Color.Lerp(initColor, finalColor, elapsedTime / duration);
            yield return null;
        }
        blackOverlay.color = finalColor;
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);

    }
}
