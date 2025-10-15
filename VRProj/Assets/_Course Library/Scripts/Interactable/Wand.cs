using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class Wand : MonoBehaviour
{
    [Header("Wand Properties")]
    public Transform tip;
    public Material drawingMaterial;
    public Material tipMaterial;
    [Range(0.01f, 0.1f)] public float wandWidth = 0.01f;
    public Color[] wandColors;

    [Header("XR Input")]
    [Tooltip("Assign either LeftHand Controller’s PrimaryButton or RightHand Controller’s PrimaryButton")]
    public InputActionProperty drawButtonAction; // A/B or X/Y

    [Tooltip("Assign either LeftHand Controller’s SecondaryButton or RightHand Controller’s SecondaryButton")]
    public InputActionProperty sparkleButtonAction; // B/Y

    [Header("Sparkle Effect")]
    public GameObject sparklePrefab;
    public float sparkleCooldown = 0.5f;

    // Internal state
    private LineRenderer currentDrawing;
    private int index;
    private int currentColorIndex;
    private bool isDrawing;
    private float lastSparkleTime;

    // XR Grab detection
    private XRGrabInteractable grabInteractable;
    private bool isHeld = false;

    private void Start()
    {
        currentColorIndex = 0;
        if (tipMaterial != null)
            tipMaterial.color = wandColors[currentColorIndex];

        drawButtonAction.action.Enable();
        sparkleButtonAction.action.Enable();

        // Get grab interactable and set up events
        grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrab);
            grabInteractable.selectExited.AddListener(OnRelease);
            if (grabInteractable.isSelected)
            {
                isHeld = true;
            }
        }
    }

    private void Update()
    {
        if (!isHeld) return; //  Only act if the wand is held

        // Drawing
        bool isPressed = drawButtonAction.action.ReadValue<float>() > 0.5f;

        if (isPressed && !isDrawing)
            StartDrawing();
        else if (!isPressed && isDrawing)
            StopDrawing();

        if (isDrawing)
            Draw();

        // Sparkle shooting
        bool sparklePressed = sparkleButtonAction.action.WasPressedThisFrame();
        if (sparklePressed && Time.time - lastSparkleTime > sparkleCooldown)
        {
            ShootSparkle();
            lastSparkleTime = Time.time;
        }
    }

    private void StartDrawing()
    {
        isDrawing = true;

        GameObject lineObj = new GameObject("DrawingLine");
        currentDrawing = lineObj.AddComponent<LineRenderer>();
        currentDrawing.material = drawingMaterial;
        currentDrawing.startColor = currentDrawing.endColor = wandColors[currentColorIndex];
        currentDrawing.startWidth = currentDrawing.endWidth = wandWidth;
        currentDrawing.positionCount = 1;
        currentDrawing.SetPosition(0, tip.position);
        index = 0;
    }

    private void StopDrawing()
    {
        isDrawing = false;
        currentDrawing = null;
    }

    private void Draw()
    {
        if (currentDrawing == null) return;

        Vector3 currentPos = currentDrawing.GetPosition(index);
        if (Vector3.Distance(currentPos, tip.position) > 0.01f)
        {
            index++;
            currentDrawing.positionCount = index + 1;
            currentDrawing.SetPosition(index, tip.position);
        }
    }

    //  Shoot sparkle particle
    private void ShootSparkle()
    {
        if (sparklePrefab != null && tip != null)
        {
            GameObject sparkle = Instantiate(sparklePrefab, tip.position, tip.rotation);
            ParticleSystem ps = sparkle.GetComponent<ParticleSystem>();
            if (ps != null)
                ps.Play();

            Destroy(sparkle, 2f); // Clean up
        }
    }

    // Grab handlers
    private void OnGrab(SelectEnterEventArgs args)
    {
        isHeld = true;
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        isHeld = false;
        StopDrawing();
    }
}
