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
    private AudioSource audioSourcePlantBomb;
    private AudioSource audioSourceBombFuse;
    private AudioSource audioSourceExplosion;
    private AudioSource audioSourceDebris;
    [SerializeField] private AudioClip plantBombClip = null;
    [SerializeField] private AudioClip bombFuseClip = null;
    [SerializeField] private AudioClip debrisFallingClip = null;

    private bool exploding = false;

    private void Awake()
    {
        audioSourceExplosion = GetComponent<AudioSource>();

        audioSourcePlantBomb = gameObject.AddComponent<AudioSource>();
        audioSourcePlantBomb.playOnAwake = false;
        audioSourcePlantBomb.clip = plantBombClip;

        audioSourceBombFuse = gameObject.AddComponent<AudioSource>();
        audioSourceBombFuse.playOnAwake = false;
        audioSourceBombFuse.clip = bombFuseClip;

        audioSourceDebris = gameObject.AddComponent<AudioSource>();
        audioSourceDebris.playOnAwake = false;
        audioSourceDebris.clip = debrisFallingClip;

        notGlowingDynamite.SetActive(false);
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
        audioSourcePlantBomb.Play();
        audioSourceBombFuse.Play();

        yield return new WaitForSeconds(countdownTime);

        notGlowingDynamite.SetActive(false);
        explosion.SetActive(true);
        explosion.GetComponent<ParticleSystem>().Play();
        audioSourceExplosion.pitch = Random.Range(.9f, 1.1f);
        audioSourceExplosion.Play();
        audioSourceDebris.pitch = Random.Range(.9f, 1.1f);
        audioSourceDebris.Play();

        pillarModel.SetActive(false);
        pillarModelBroken1.SetActive(true);
        pillarModelBroken2.SetActive(true);

        pillarModelBroken1.GetComponent<Rigidbody>().AddExplosionForce(600f, pillarModelBroken1.transform.position + new Vector3(-2f, 5f, 2f), 10f, 10f, ForceMode.Impulse);
        pillarModelBroken2.GetComponent<Rigidbody>().AddExplosionForce(600f, pillarModelBroken1.transform.position - new Vector3(2f, 5f, 2f), 10f, 10f, ForceMode.Impulse);

        PlayerManager.instance.camFX.HighShake();

        if (Vector3.Distance(transform.position, PlayerManager.instance.transform.position) < 15f)
        {
            Debug.Log("Player caught in Explosion");
            PlayerManager.instance.CaughtInExplosion(transform.position);
        }

        yield return new WaitForSeconds(.25f);

        explosion.GetComponent<Light>().enabled = false;

        GameManager.instance.ExplodedPillar();
    }
}
