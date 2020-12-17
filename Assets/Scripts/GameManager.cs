using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;

    [SerializeField] private Image blackOverlay = null;
    [SerializeField] private Text deathText1 = null;
    [SerializeField] private Text deathText2 = null;
    public bool dead = false;

    public int numPillars = 5;

    private void Awake()
    {
        // Ensure that there is only one instance of the GameManager.
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        StartCoroutine(BlackOverlayFadeOut(2f));
        deathText1.color = new Color(1f, 1f, 1f, 0f);
        deathText2.color = new Color(1f, 1f, 1f, 0f);
    }


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (dead)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    private IEnumerator BlackOverlayFadeOut(float duration)
    {
        blackOverlay.color = new Color(0f, 0f, 0f, 1f);
        float elapsedTime = 0f;
        Color initColor = new Color(0f, 0f, 0f, 1f);
        Color finalColor = new Color(0f, 0f, 0f, 0f);
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            blackOverlay.color = Color.Lerp(initColor, finalColor, elapsedTime / duration);

            yield return null;
        }
        blackOverlay.color = finalColor;
    }

    private IEnumerator BlackOverlayFadeIn(float duration)
    {
        blackOverlay.color = new Color(0f, 0f, 0f, 0f);
        float elapsedTime = 0f;
        Color initColor = new Color(0f, 0f, 0f, 0f);
        Color finalColor = new Color(0f, 0f, 0f, 1f);
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            blackOverlay.color = Color.Lerp(initColor, finalColor, elapsedTime / duration);

            yield return null;
        }
        blackOverlay.color = finalColor;
    }

    public void ExplodedPillar()
    {
        numPillars--;
    }

    public void DeathSequence()
    {
        StartCoroutine(DeathSequenceEnum());
    }

    private IEnumerator DeathSequenceEnum()
    {
        // Fade in deathText
        dead = true;
        StartCoroutine(BlackOverlayFadeIn(5f));
        yield return new WaitForSeconds(4f);

        deathText1.color = new Color(1f, 1f, 1f, 0f);
        deathText2.color = new Color(1f, 1f, 1f, 0f);
        float duration = 4f;
        float elapsedTime = 0f;
        Color initColor = new Color(1f, 1f, 1f, 0f);
        Color initColorRed = new Color(1f, 0f, 0f, 0f);
        Color finalColor = new Color(1f, 1f, 1f, 1f);
        Color finalColorRed = new Color(1f, 0f, 0f, 1f);
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            
            deathText1.color = Color.Lerp(initColorRed, finalColorRed, elapsedTime / duration);
            deathText2.color = Color.Lerp(initColor, finalColor, elapsedTime / duration);

            yield return null;
        }
        deathText1.color = finalColorRed;
        deathText2.color = finalColor;
    }
}
