using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLightManager : MonoBehaviour
{
    public bool CanControlLights = true;

    [SerializeField] private Light indoorLight = null;

    [SerializeField] private Light bossSightLightLow = null;
    [SerializeField] private Light bossSightLightHigh = null;

    [SerializeField] private Light flashlightDim = null;
    [SerializeField] private Light flashlightBright = null;

    // Start is called before the first frame update
    void Start()
    {
        bossSightLightLow.enabled = false;
        bossSightLightHigh.enabled = false;

        flashlightDim.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && CanControlLights)
        {
            if (!flashlightDim.enabled && !flashlightBright.enabled)
            {
                flashlightDim.enabled = true;
            }
            else if (flashlightDim.enabled && !flashlightBright.enabled)
            {
                flashlightBright.enabled = true;
                flashlightDim.enabled = false;
            }
            else
            {
                flashlightBright.enabled = false;
                flashlightDim.enabled = false;
            }
        }
    }

    public void BossSawPlayer()
    {
        bossSightLightLow.enabled = true;
        bossSightLightHigh.enabled = false;
    }

    public void BossLostPlayer()
    {
        bossSightLightLow.enabled = false;
        bossSightLightHigh.enabled = false;

    }

    public void BossSeenPlayerTooLong()
    {
        StartCoroutine(BossSeenPlayerTooLongEnum());
    }

    private IEnumerator BossSeenPlayerTooLongEnum()
    {
        bossSightLightLow.enabled = false;
        bossSightLightHigh.enabled = true;
        yield return new WaitForSeconds(1.5f);
        bossSightLightLow.enabled = false;
        bossSightLightHigh.enabled = false;
    }

    public void DeathSequence()
    {
        CanControlLights = false;
        StartCoroutine(DeathSequenceEnum1());
        //StartCoroutine(DeathSequenceEnum2());
    }

    private IEnumerator DeathSequenceEnum1()
    {
        flashlightBright.enabled = false;
        flashlightDim.enabled = false;
        yield return new WaitForSeconds(.1f);
        flashlightDim.enabled = true;
        yield return new WaitForSeconds(.5f);
        flashlightDim.enabled = false;
        yield return new WaitForSeconds(.1f);
        flashlightDim.enabled = true;
        yield return new WaitForSeconds(.4f);
        flashlightDim.enabled = false;
        yield return new WaitForSeconds(.1f);
        flashlightDim.enabled = true;
        yield return new WaitForSeconds(.3f);
        flashlightDim.enabled = false;
        yield return new WaitForSeconds(.1f);
        flashlightDim.enabled = true;
        yield return new WaitForSeconds(.1f);
        flashlightDim.enabled = false;
        yield return new WaitForSeconds(.1f);
        flashlightDim.enabled = true;
        yield return new WaitForSeconds(.1f);
        flashlightDim.enabled = false;
        yield return new WaitForSeconds(.1f);
        flashlightDim.enabled = true;
        yield return new WaitForSeconds(.1f);
        flashlightDim.enabled = false;
        indoorLight.enabled = false;
        // 1.7 seconds total
    }

    private IEnumerator DeathSequenceEnum2()
    {
        yield return new WaitForSeconds(1f);
        while (true)
        {
            bossSightLightLow.enabled = false;
            yield return new WaitForSeconds(.5f);
            bossSightLightLow.enabled = true;
            yield return new WaitForSeconds(.5f);
        }
    }
}
