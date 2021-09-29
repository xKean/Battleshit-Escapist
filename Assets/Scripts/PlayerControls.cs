using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] float playerXSpeedMultiplier = .15f;
    [SerializeField] float playerYSpeedMultiplier = .12f;
    [SerializeField] float xRange = 16f;
    [SerializeField] float yRange = 9f;

    [SerializeField] float positionPitchFactor = -3f;
    [SerializeField] float positionYawFactor = 2.5f;
    [SerializeField] float controlPitchFactor = -13f;
    [SerializeField] float controlRollFactor = -25f;

    float xThrow, yThrow;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        ProcessTranslation();
        ProcessRotation();



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
