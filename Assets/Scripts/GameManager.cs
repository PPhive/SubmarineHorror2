using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;

    public int numPillars = 5;

    private void Awake()
    {
        // Ensure that there is only one instance of the GameManager.
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            CameraFX.instance.LowShake();
        if (Input.GetKeyDown(KeyCode.Alpha2))
            CameraFX.instance.MedShake();
        if (Input.GetKeyDown(KeyCode.Alpha3))
            CameraFX.instance.HighShake();
    }

    public void ExplodedPillar()
    {

    }
}
