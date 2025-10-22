using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using UnityEngine;

public class Fly : MonoBehaviour
{
    public GameObject head;
    public GameObject leftHand;
    private float flyingSpeed = 0.8f;
    private bool isFlying;

    public InputActionProperty flyingMode;

    private void Start()
    {
        flyingMode.action.Enable();
    }

    private void Update()
    {
        CheckIfFlying();
        FlyIfFlying();
    }

    void CheckIfFlying()
    {
        bool isPressed = flyingMode.action.ReadValue<float>() > 0.5f;

        if (isPressed && !isFlying)
            isFlying = true;
        else if (!isPressed && isFlying)
            isFlying = false;
    }

    void FlyIfFlying()
    {
       if(isFlying)
        {
            Vector3 flyDirection = leftHand.transform.position - head.transform.position;
            transform.position += flyDirection.normalized * flyingSpeed;
        }
    }
}
