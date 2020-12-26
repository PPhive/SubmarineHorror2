using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFX : MonoBehaviour
{
    public static CameraFX instance = null;

    [SerializeField] private float ambientShakeIntensity = .03f;
    [SerializeField] private float lowShakeIntensity = .1f;
    [SerializeField] private float medShakeIntensity = .2f;
    [SerializeField] private float highShakeIntensity = .3f;
    [SerializeField] private float lowShakeDuration = .25f;
    [SerializeField] private float medShakeDuration = .5f;
    [SerializeField] private float highShakeDuration = 1f;

    private bool timedShaking = false;
    public bool ambientRumbling = false;

    private Vector3 originalLocalPos;
    private Vector3 originalLocalRot;

    private void Awake()
    {
        // Ensure that there is only one instance of the CameraFX.
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        originalLocalPos = transform.localPosition;
        originalLocalRot = transform.localEulerAngles;
    }

    private void Update()
    {
        if ((ambientRumbling || GameManager.instance.endSequence) && !timedShaking)
        {
            transform.localPosition = originalLocalPos + (new Vector3(Random.Range(-.1f, .1f), Random.Range(-.1f, .1f), 0f) * ambientShakeIntensity);
            transform.localEulerAngles = originalLocalRot + (new Vector3(Random.Range(-10f, 10f), Random.Range(-50f, 50f), 0f) * ambientShakeIntensity);
        }
    }

    public void LowShake()
    {
        StartCoroutine(LowShakeEnum());
    }

    private IEnumerator LowShakeEnum()
    {
        timedShaking = true;
        float elapsedTime = 0;
        while (elapsedTime < lowShakeDuration)
        {
            elapsedTime += Time.deltaTime;
            transform.localPosition = originalLocalPos + (new Vector3(Random.Range(-.1f, .1f), Random.Range(-.1f, .1f), 0f) * lowShakeIntensity * (1 - elapsedTime / lowShakeDuration));
            transform.localEulerAngles = originalLocalRot + (new Vector3(Random.Range(-10f, 10f), Random.Range(-50f, 50f), 0f) * lowShakeIntensity * (1 - elapsedTime / lowShakeDuration));
            yield return null;
        }
        transform.localPosition = originalLocalPos;
        transform.localEulerAngles = originalLocalRot;
        timedShaking = false;
    }

    public void MedShake()
    {
        StartCoroutine(MedShakeEnum());
    }

    private IEnumerator MedShakeEnum()
    {
        timedShaking = true;
        float elapsedTime = 0;
        while (elapsedTime < medShakeDuration)
        {
            elapsedTime += Time.deltaTime;
            transform.localPosition = originalLocalPos + (new Vector3(Random.Range(-.1f, .1f), Random.Range(-.1f, .1f), 0f) * medShakeIntensity * (1 - elapsedTime / medShakeDuration));
            transform.localEulerAngles = originalLocalRot + (new Vector3(Random.Range(-10f, 10f), Random.Range(-50f, 50f), 0f) * medShakeIntensity * (1 - elapsedTime / medShakeDuration));
            yield return null;
        }
        transform.localPosition = originalLocalPos;
        transform.localEulerAngles = originalLocalRot;
        timedShaking = false;
    }

    public void HighShake()
    {
        StartCoroutine(HighShakeEnum());
    }

    private IEnumerator HighShakeEnum()
    {
        timedShaking = true;
        float elapsedTime = 0;
        while (elapsedTime < highShakeDuration)
        {
            elapsedTime += Time.deltaTime;
            transform.localPosition = originalLocalPos + (new Vector3(Random.Range(-.1f, .1f), Random.Range(-.1f, .1f), 0f) * highShakeIntensity * (1 - elapsedTime / highShakeDuration));
            transform.localEulerAngles = originalLocalRot + (new Vector3(Random.Range(-10f, 10f), Random.Range(-50f, 50f), 0f) * highShakeIntensity * (1 - elapsedTime / highShakeDuration));
            yield return null;
        }
        transform.localPosition = originalLocalPos;
        transform.localEulerAngles = originalLocalRot;
        timedShaking = false;
    }


}
