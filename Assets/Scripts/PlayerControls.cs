using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("General Setup settings")]
    [Tooltip("How fast ship moves on X (left-right)")] [SerializeField] float playerXSpeedMultiplier = .15f;
    [Tooltip("How fast ship moves on Y (up-down)")] [SerializeField] float playerYSpeedMultiplier = .12f;
    [Tooltip("How far ship can move to corners X (left-right)")] [SerializeField] float xRange = 16f;
    [Tooltip("How far ship can move to corners Y (up-down)")] [SerializeField] float yRange = 9f;

    [Header("Lasers in here")]
    [Tooltip("Add all player lasers here")] [SerializeField] GameObject[] lasers;

    [Header("Screen position based tuning")]
    [Tooltip("Rotation: Position Pitch * Factor")] [SerializeField] float positionPitchFactor = -3f;
    [Tooltip("Rotation: Position Yaw * Factor")] [SerializeField] float positionYawFactor = 2.5f;

    [Header("Player input based tuning")]
    [Tooltip("Rotation: Control Pitch * Factor")] [SerializeField] float controlPitchFactor = -13f;
    [Tooltip("Rotation: Control Roll * Factor")] [SerializeField] float controlRollFactor = -25f;

    [Header("Damage")]
    [Tooltip("How much dmg the player deals per shot")] [SerializeField] public int playerdamage = 15;
    float xThrow, yThrow;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    private void ProcessFiring()
    {
        if (Input.GetButton("Fire1"))
        {
            SetLasersActive(true);
        }
        else
        {
            SetLasersActive(false);
        }
    }

    void SetLasersActive(bool status)
    {
        foreach (GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = status;
        }
    }

    void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;

        float pitch = pitchDueToPosition + pitchDueToControlThrow; // Due to position and control - schnauze hoch runter
        float yaw = transform.localPosition.x * positionYawFactor; // Only due to position - schussrichtung
        float roll = xThrow * controlRollFactor; // Only due to Control - drehung
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void ProcessTranslation()
    {
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");

        float xOffset = xThrow * playerXSpeedMultiplier;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange); // maximum setzen, dass keiner rausfliegen kann

        float yOffset = yThrow * playerYSpeedMultiplier;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }
}
