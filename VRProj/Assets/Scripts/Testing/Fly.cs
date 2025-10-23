using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class Fly : MonoBehaviour
{
    [Header("References")]
    public GameObject head;                  // The VR camera
    public Transform rightHand;              // The right hand controller transform
    public XRSocketInteractor broomSocket;   // Socket where the broom is attached
    public InputActionProperty flyTrigger;   // Trigger action for flight

    [Header("Settings")]
    public float flyingSpeed = 3.0f;         // Base flying speed
    public float verticalSpeedMultiplier = 1.0f;
    public float rotationSmoothing = 4f;     // Smooth direction change

    private bool isFlying = false;
    private bool hasBroom = false;
    private Vector3 currentDirection;

    private void Start()
    {
        flyTrigger.action.Enable();

        // Broom attach/detach
        broomSocket.selectEntered.AddListener(OnBroomAttached);
        broomSocket.selectExited.AddListener(OnBroomDetached);
    }

    private void OnDestroy()
    {
        broomSocket.selectEntered.RemoveListener(OnBroomAttached);
        broomSocket.selectExited.RemoveListener(OnBroomDetached);
    }

    private void Update()
    {
        bool triggerPressed = flyTrigger.action.ReadValue<float>() > 0.5f;

        if (hasBroom)
        {
            if (triggerPressed && !isFlying)
                StartFlying();
            else if (!triggerPressed && isFlying)
                StopFlying();

            if (isFlying)
                HandleFlying();
        }
        else
        {
            isFlying = false;
        }
    }

    private void OnBroomAttached(SelectEnterEventArgs args)
    {
        hasBroom = true;
    }

    private void OnBroomDetached(SelectExitEventArgs args)
    {
        hasBroom = false;
    }

    private void StartFlying()
    {
        isFlying = true;
        currentDirection = rightHand.forward;
    }

    private void StopFlying()
    {
        isFlying = false;
    }

    private void HandleFlying()
    {
        // Smoothly align current direction with right-hand forward direction
        currentDirection = Vector3.Slerp(currentDirection, rightHand.forward, Time.deltaTime * rotationSmoothing);

        if (currentDirection.y < -0.6f)
            currentDirection.y = -0.6f;

        // Move in the direction the broom/hand points
        Vector3 movement = currentDirection.normalized * flyingSpeed * Time.deltaTime;

        // Apply vertical control (pointing up/down controls climb/fall)
        transform.position += movement;

        // Calculate a target rotation based on direction 
        Vector3 flatDirection = currentDirection;

        if (flatDirection.sqrMagnitude > 1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(flatDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSmoothing);
        }
    }
}
