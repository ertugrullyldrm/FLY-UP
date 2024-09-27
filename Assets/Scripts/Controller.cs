using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour

{
    [Header("Particles")]
    [SerializeField] GameObject[] Breath;
    [Header("General Setup Settings")]
    [Tooltip("How fast ship moves up and down")]
    [SerializeField] float MovementSpeed = 1f;
    [Header("Movement Range")]
    [Tooltip("Maximum range the player can travel on x axis")]
    [SerializeField] float xRange = 8f;
    [Tooltip("Maximum range the player can travel on y axis")]
    [SerializeField] float yRange = 8f;

    [Header("Tune Rotation According to Position")]
    [Tooltip("Pitch = x axis rotation,Position = Rotation effect according to position")]
    [SerializeField] float positionPitchFactor = 2f;
    [Tooltip("Yaw = y axis Rotation, Position = Rotation effect according to position")]
    [SerializeField] float positionYawFactor = -2f;
    
    [Header("Tune Rotation According to Input")]
    [Tooltip("Pitch = x axis rotation, Control = Rotation effect according to input")]
    [SerializeField] float controlPitchFactor = -20f;
    [Tooltip("Roll = z axis Rotation, Control = Rotation effect according to input")]
    [SerializeField] float controlRollFactor = -20f;
    
    float xMovement, yMovement;
    bool isActive = true;
    
    

    void Update()
    {
        ProcessMovement();
        ProcessRotation();
        ProcessFiring();
    }

   
    void ProcessMovement()
    {
        xMovement = Input.GetAxis("Horizontal");
        float xOffset = xMovement * Time.deltaTime * MovementSpeed;
        float Xpos = transform.localPosition.x + xOffset;
        float clampedXpos = Mathf.Clamp(Xpos, -xRange, xRange);

        yMovement = Input.GetAxis("Vertical");
        float yOffset = yMovement * Time.deltaTime * MovementSpeed;
        float Ypos = transform.localPosition.y + yOffset;
        float clampedYpos = Mathf.Clamp(Ypos, -yRange, yRange);

        float zPos = transform.localPosition.z + 0;



        transform.localPosition = new Vector3(clampedXpos, clampedYpos, zPos);
    }
    void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControl = yMovement * controlPitchFactor;
        
        float pitch = pitchDueToPosition + pitchDueToControl;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xMovement * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch,yaw,roll);
    }
    void ProcessFiring()
    {
        if  (Input.GetButton("Fire1"))
        {
            SetFireballActive(true);
        }
        else 
        {
            SetFireballActive(false);
        }    
    }
    void SetFireballActive(bool isActive)
    {
        foreach (GameObject Fireball in Breath)
        {
           var emissionModule =  Fireball.GetComponent<ParticleSystem>().emission;
           emissionModule.enabled = isActive;
        }
    }
}

