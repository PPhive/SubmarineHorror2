using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPillar : MonoBehaviour
{

    [SerializeField] private GameObject pillarModel = null;
    [SerializeField] private GameObject pillarModelBroken1 = null;
    [SerializeField] private GameObject pillarModelBroken2 = null;
    [SerializeField] private GameObject glowingDynamite = null;
    [SerializeField] private GameObject notGlowingDynamite = null;
    [SerializeField] private GameObject explosion = null; 

    [SerializeField] private float countdownTime = 5f;
    private AudioSource audioSource;

    private bool exploding = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void TriggerExplosionCountdown()
    {
        if (!exploding)
            StartCoroutine(ExplosionCountdownEnum());
    }

    private IEnumerator ExplosionCountdownEnum()
    {
        glowingDynamite.SetActive(false);
        notGlowingDynamite.SetActive(true);

        yield return new WaitForSeconds(countdownTime);

        notGlowingDynamite.SetActive(false);
        explosion.SetActive(true);
        explosion.GetComponent<ParticleSystem>().Play();
        audioSource.Play();

        pillarModel.SetActive(false);
        pillarModelBroken1.SetActive(true);
        pillarModelBroken2.SetActive(true);

        pillarModelBroken1.GetComponent<Rigidbody>().AddExplosionForce(600f, pillarModelBroken1.transform.position + new Vector3(-2f, 5f, 2f), 10f, 10f, ForceMode.Impulse);
        pillarModelBroken2.GetComponent<Rigidbody>().AddExplosionForce(600f, pillarModelBroken1.transform.position - new Vector3(2f, 5f, 2f), 10f, 10f, ForceMode.Impulse);

        PlayerManager.instance.camFX.HighShake();

        yield return new WaitForSeconds(.25f);

        explosion.GetComponent<Light>().enabled = false;

        GameManager.instance.ExplodedPillar();
    }
}
