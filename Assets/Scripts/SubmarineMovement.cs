using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Creator: Nate Smith
 * Date: 12/4/2020
 * Description: Primary player movement manager. Most action will occur in Update and FixedUpdate.
 */

public class SubmarineMovement : MonoBehaviour
{
    public bool canMove = true;

    // Movement parameters
    [SerializeField] private float forwardMovePower = 10f;
    [SerializeField] private float backwardMoveMultiplier = .5f;
    [SerializeField] private float horizontalMovePower = 5f;
    [SerializeField] private float sprintMultiplier = 2f;

    // Rotation parameters
    [SerializeField] private float vertMouseSensitivity = 100f;
    [SerializeField] private float horiMouseSensitivity = 100f;
    [SerializeField] private float rotSensitivity = 1f;

    // Rotation parameters
    [SerializeField] private float bossForce = 10000f;
    [SerializeField] private float bossTorque = 1000f;

    private Quaternion intendedRot = Quaternion.identity;

    private Rigidbody rb;

    private float moveZ; // Forward and backward movement
    private float moveX; // Horizontal movement
    private float rotH; // Horizontal rotation
    private float rotV; // Vertical rotation
    private float rotR; // Rotational (Z axis) rotation
    private float shift; // Sprint?

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        moveZ = Input.GetAxis("Vertical");
        moveX = Input.GetAxis("Horizontal");
        rotH = Input.GetAxis("Mouse X"); // moving the mouse left/right should rotate player around y axis
        rotV = Input.GetAxis("Mouse Y"); // moving the mouse up/down should rotate player around x axis
        rotR = Input.GetAxis("Rotate"); // moving the mouse up/down should rotate player around x axis
        shift = Input.GetAxis("Sprint");
        //transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0f);
        /*
        if (transform.eulerAngles.x > 180f)
        {
            Debug.Log("Rot too high");
            transform.eulerAngles = new Vector3(90f, transform.eulerAngles.y, 0f);
        }
        else if (transform.eulerAngles.x < 0f)
        {
            Debug.Log("Rot too low");
            transform.eulerAngles = new Vector3(-90f, transform.eulerAngles.y, 0f);
        }*/

    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            if (moveZ > .1f || moveZ < -.1f || moveX > .1f || moveX < -.1f)
            {
                PlayerManager.instance.Moving();
            }
            else
            {
                PlayerManager.instance.NotMoving();
            }

            // Handle spacial movement
            Vector3 forwardForce;
            forwardForce = Vector3.forward * forwardMovePower * moveZ;


            if (moveZ > 0 && shift > 0)
            {
                forwardForce *= sprintMultiplier;
                PlayerManager.instance.MovingFast();
            }
            else if (moveZ < 0)
            {
                forwardForce *= backwardMoveMultiplier;
            }

            Vector3 horizontalForce;
            horizontalForce = Vector3.right * horizontalMovePower * moveX;

            Vector3 totalForce = forwardForce + horizontalForce;

            rb.AddRelativeForce(totalForce, ForceMode.Force);

            // Handle rotational movement

            // Retrieve mouse input.
            float intendedRotChangeH = rotH * horiMouseSensitivity;
            float intendedRotChangeV = rotV * vertMouseSensitivity;
            float intendedRotChangeR = rotR * rotSensitivity;

            // if you are not pressing a rotation button, try to right the sub
            if (rotR < .01f && rotR > -.01f)
            {
                float zRot = transform.localEulerAngles.z % 360f;
                // if rotated too far to right, rotate left
                if (zRot < 180f)
                {
                    intendedRotChangeR = rotSensitivity * .5f;
                } // if rotated too far to right, rotate left
                else if (zRot > 180f)
                {
                    intendedRotChangeR = rotSensitivity * -.5f;
                }
            }

            /*

            float intendedRotH = transform.localEulerAngles.y;
            float intendedRotV = transform.localEulerAngles.x;

            intendedRotH += intendedRotChangeH;
            intendedRotV -= intendedRotChangeV;

            intendedRotV = Mathf.Clamp(intendedRotV, -90, 90);

            intendedRot = Quaternion.Euler(new Vector3(intendedRotV, intendedRotH, 0f));

            Quaternion finalRot = Quaternion.Lerp(transform.localRotation, intendedRot, Time.fixedDeltaTime * mouseMovementSmoothing);

            //rb.MoveRotation(finalRot);
            */

            Vector3 change = new Vector3(-intendedRotChangeV, intendedRotChangeH, -intendedRotChangeR);
            rb.AddRelativeTorque(change, ForceMode.Force);



            //transform.localEulerAngles = new Vector3(Mathf.Clamp(transform.localEulerAngles.x, -90, 90), transform.localEulerAngles.y, 0f);
        }
    }

    public void BossPush(Vector3 BossPos)
    {
        Vector3 forceDir = (transform.position - BossPos).normalized;

        rb.AddForce(forceDir * bossForce, ForceMode.Impulse);
        rb.AddTorque(transform.right * bossTorque, ForceMode.Impulse);
    }

    public void CaughtInExplosion(Vector3 ExplosionPos)
    {
        Vector3 forceDir = (transform.position - ExplosionPos).normalized;

        rb.AddForce(forceDir * 1000f, ForceMode.Impulse);
        rb.AddTorque(transform.right * 200f, ForceMode.Impulse);
    }

    public void DeathSequence()
    {
        StartCoroutine(DeathSequenceEnum());
    }

    private IEnumerator DeathSequenceEnum()
    {
        while (true)
        {
            rb.AddForce(Vector3.down * 1f, ForceMode.Impulse);
            rb.AddTorque(transform.right * .1f, ForceMode.Impulse);
            rb.AddTorque(transform.forward * .1f, ForceMode.Impulse);
            yield return new WaitForFixedUpdate();
        }
    }
}
