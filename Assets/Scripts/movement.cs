using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    [Header("Translation Settings")]
    [Tooltip("The speed moving horizontal and vertical")]
    [SerializeField] float controlSpeed = 10f;
    [Tooltip("Horizontal range in which the plane and traverse within")]
    [SerializeField] float xRange = 5f;
    [Tooltip("Vertical range in which the plane can traverse within")]
    [SerializeField] float yRange = 5f;

    [Header("Screen position based tuning")]
    [Tooltip("How much to change the pitch by depending on position")]
    [SerializeField] float positionPitchFactor = -2f;
    [Tooltip("How much to change the yaw by depending on position")]
    [SerializeField] float positionYawFactor = 2f;

    [Header("Player input based tuning")]
    [Tooltip("How much to change the pitch by depending on player input")]
    [SerializeField] float controlPitchFactor = -10f;
    [Tooltip("How much to change the roll by depending on player input")]
    [SerializeField] float controlRollFactor = -10f;

    [Header("Particle Systems")]
    [Tooltip("List of bullet particle systems")]
    [SerializeField] GameObject[] bullets;

    float xThrow, yThrow;

    private void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    private void ProcessFiring()
    {
        if (Input.GetButton("Fire1")) {
            ActiveBullets();
        } else {
            DeactivateBullets();
        }
    }

    private void DeactivateBullets()
    {
        foreach(GameObject bullet in bullets) {
            var emissionModule = bullet.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = false;
        }
    }

    private void ActiveBullets()
    {
        foreach(GameObject bullet in bullets) {
            var emissionModule = bullet.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = true;
        }
    }

    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControlThrow;


        float yaw = transform.localPosition.x * positionYawFactor;

        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessTranslation()
    {
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");

        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);


        float yOffset = yThrow * Time.deltaTime * controlSpeed;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }
}
