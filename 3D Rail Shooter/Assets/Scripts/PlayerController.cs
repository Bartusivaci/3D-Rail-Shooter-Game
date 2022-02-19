using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("General Setup Settings")]
    [Tooltip("How fast ship moves based upon player input.")]
    [SerializeField] float controlSpeed = 30f;
    [Tooltip("Ship boundary on x axis.")]
    [SerializeField] float xRange = 14f;
    [Tooltip("Ship boundary on y axis.")]
    [SerializeField] float yRange = 7f;

    [Header("Laser Array")]
    [Tooltip("Drag the laser game objects.")]
    [SerializeField] GameObject[] lasers;

    [Header("Screen Position Based Tuning")]
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float positionYawFactor = 3f;

    [Header("Player Input Based Tuning")]
    [SerializeField] float controlPitchFactor = -15f;
    [SerializeField] float controlRollFactor = -40f;

    [Header("Rotation Setting")]
    [Tooltip("How fast ship rotates.")]
    [SerializeField] float rotationFactor = 5f;

    float horizontalMove, verticalMove;


    void Update()
    {
        PlayerTranslation();
        PlayerRotation();
        PlayerFiring();
    }

    void PlayerTranslation()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        float xOffset = horizontalMove * controlSpeed * Time.deltaTime;
        float xPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(xPos, -xRange, xRange);

        float yOffset = verticalMove * controlSpeed * Time.deltaTime;
        float yPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(yPos, -yRange, yRange);


        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    void PlayerRotation()
    {

        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControl = verticalMove * controlPitchFactor;

        float pitch = pitchDueToPosition + pitchDueToControl;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = horizontalMove * controlRollFactor;

        //transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
        Quaternion targetRotation = Quaternion.Euler(pitch, yaw, roll);
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, rotationFactor);
    }

    void PlayerFiring()
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

    void SetLasersActive(bool isActive)
    {
        foreach (GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }
}
