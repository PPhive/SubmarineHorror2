using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;

    [SerializeField] private Image overlay = null;
    [SerializeField] private Text deathText1 = null;
    [SerializeField] private Text deathText2 = null;
    [SerializeField] private Text winText1 = null;
    [SerializeField] private Text winText2 = null;
    public bool dead = false;
    public bool won = false;
    public bool endSequence = false;

    public int numPillars = 5;

    [SerializeField] private Transform secondarySpawn = null;

    [SerializeField] private EndSequenceManager endSequenceManager = null;


    private void Awake()
    {
        // Ensure that there is only one instance of the GameManager.ger
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        StartCoroutine(BlackOverlayFadeOut(2f));
        deathText1.color = new Color(1f, 1f, 1f, 0f);
        deathText2.color = new Color(1f, 1f, 1f, 0f);

        winText1.color = new Color(1f, 1f, 1f, 0f);
        winText2.color = new Color(1f, 1f, 1f, 0f);

        endSequenceManager = FindObjectOfType<EndSequenceManager>();
    }


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        numPillars = FindObjectsOfType<DestroyPillar>().Length;

        // add in way to spawn in at bottom of volcano.
        if (dead)
        {
            PlayerManager.instance.transform.position = secondarySpawn.position;
        }
    }

    private void Update()
    {
        if (dead)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(3);
            }
        }
        else if (won)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    private IEnumerator BlackOverlayFadeOut(float duration)
    {
        overlay.color = new Color(0f, 0f, 0f, 1f);
        float elapsedTime = 0f;
        Color initColor = new Color(0f, 0f, 0f, 1f);
        Color finalColor = new Color(0f, 0f, 0f, 0f);
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            overlay.color = Color.Lerp(initColor, finalColor, elapsedTime / duration);

            yield return null;
        }
        overlay.color = finalColor;
    }

    private IEnumerator BlackOverlayFadeIn(float duration)
    {
        overlay.color = new Color(0f, 0f, 0f, 0f);
        float elapsedTime = 0f;
        Color initColor = new Color(0f, 0f, 0f, 0f);
        Color finalColor = new Color(0f, 0f, 0f, 1f);
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            overlay.color = Color.Lerp(initColor, finalColor, elapsedTime / duration);

            yield return null;
        }
        overlay.color = finalColor;
    }

    private IEnumerator WhiteOverlayFadeIn(float duration)
    {
        overlay.color = new Color(1f, 1f, 1f, 0f);
        float elapsedTime = 0f;
        Color initColor = new Color(1f, 1f, 1f, 0f);
        Color finalColor = new Color(1f, 1f, 1f, 1f);
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            overlay.color = Color.Lerp(initColor, finalColor, elapsedTime / duration);

            yield return null;
        }
        overlay.color = finalColor;
    }

    public void ExplodedPillar()
    {
        numPillars--;

        if (numPillars <= 0)
        {
            endSequenceManager.StartEndSequence();
        }
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

        Color finalColorRed2 = new Color(.8f, 0f, 0f, 1f);
        while (true)
        {
            duration = 2f;
            elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;

                deathText1.color = Color.Lerp(finalColorRed, finalColorRed2, elapsedTime / duration);

                yield return null;
            }
            elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;

                deathText1.color = Color.Lerp(finalColorRed2, finalColorRed, elapsedTime / duration);

                yield return null;
            }
        }
    }

    public void WinSequence()
    {
        PlayerManager.instance.WinSequence();
        StartCoroutine(WinSequenceEnum());
    }

    private IEnumerator WinSequenceEnum()
    {
        // Fade in winText
        won = true;
        StartCoroutine(BlackOverlayFadeIn(4f));
        yield return new WaitForSeconds(4f);

        winText1.color = new Color(0f, 1f, 1f, 0f);
        winText2.color = new Color(1f, 1f, 1f, 0f);
        float duration = 4f;
        float elapsedTime = 0f;
        Color initColor1 = new Color(0f, 1f, 1f, 0f);
        Color initColor2 = new Color(1f, 1f, 1f, 0f);
        Color finalColor1 = new Color(0f, 1f, 1f, 1f);
        Color finalColor2 = new Color(1f, 1f, 1f, 1f);
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            winText1.color = Color.Lerp(initColor1, finalColor1, elapsedTime / duration);
            winText2.color = Color.Lerp(initColor2, finalColor2, elapsedTime / duration);

            yield return null;
        }
        winText1.color = finalColor1;
        winText2.color = finalColor2;

        Color finalColor12 = new Color(0f, .5f, 1f, 1f);
        while (true)
        {
            duration = 2f;
            elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;

                winText1.color = Color.Lerp(finalColor1, finalColor12, elapsedTime / duration);

                yield return null;
            }
            elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;

                winText1.color = Color.Lerp(finalColor12, finalColor1, elapsedTime / duration);

                yield return null;
            }
        }
    }
}
