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
    public InputActionProperty drawButtonAction;      // A/B or X/Y
    public InputActionProperty sparkleButtonAction;   // B/Y
    public InputActionProperty magicButtonAction;   // trigger pressed

    [Header("Sparkle Effect")]
    public GameObject sparklePrefab;                  // Wand projectile
    public GameObject otherMagicPrefab;               
    public float sparkleCooldown = 0.2f;

    // Internal state
    private LineRenderer currentDrawing;
    private int index;
    private int currentColorIndex;
    private bool isDrawing;
    private float lastSparkleTime;

    // XR Grab detection
    private XRGrabInteractable grabInteractable;
    private bool isHeld = false;

    // Hand transform for free-hand magic
    public Transform handTransform;
    public GameObject magicCircle;

    private void Start()
    {
        currentColorIndex = 0;
        if (tipMaterial != null)
            tipMaterial.color = wandColors[currentColorIndex];

        drawButtonAction.action.Enable();
        sparkleButtonAction.action.Enable();
        magicButtonAction.action.Enable();

        grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrab);
            grabInteractable.selectExited.AddListener(OnRelease);
            if (grabInteractable.isSelected)
                isHeld = true;
        }
    }

    private void Update()
    {
        bool sparklePressed = sparkleButtonAction.action.WasPressedThisFrame();
        bool magicPressed = magicButtonAction.action.WasPressedThisFrame();

        if (isHeld)
        {
            HandleDrawing();

            // Shoot wand projectile
            if (sparklePressed && Time.time - lastSparkleTime > sparkleCooldown)
            {
                ShootSparkle();
                lastSparkleTime = Time.time;
            }
        }
        else
        {
            return;

        }

        if (magicPressed && otherMagicPrefab != null && handTransform != null)
        {
            Transform spawnPoint = handTransform;

            XRController controller = handTransform.GetComponentInParent<XRController>();
            

            if (controller != null && controller.modelParent != null)
            {
                spawnPoint = handTransform;
               
            }

            GameObject magicObj = Instantiate(otherMagicPrefab, spawnPoint.position, spawnPoint.rotation);

            magicObj.transform.SetParent(spawnPoint, true);

            lastSparkleTime = Time.time;
            Destroy(magicObj, 3);
        }
       
       
    }

    private void HandleDrawing()
    {
        bool isPressed = drawButtonAction.action.ReadValue<float>() > 0.5f;

        if (isPressed && !isDrawing)
        {
            magicCircle.SetActive(true);
            StartDrawing();
        }    
        else if (!isPressed && isDrawing)
        {
            magicCircle.SetActive(false);
            StopDrawing();
        }
            

        if (isDrawing)
            Draw();
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

    private void ShootSparkle()
    {
        if (sparklePrefab != null && tip != null)
        {
            GameObject sparkle = Instantiate(sparklePrefab, tip.position, tip.rotation);
            SparkleProjectile projectile = sparkle.GetComponent<SparkleProjectile>();
            if (projectile != null)
                projectile.Launch(tip.forward);
        }
    }

    // Grab events
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
