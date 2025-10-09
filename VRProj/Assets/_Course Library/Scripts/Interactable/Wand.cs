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

    private LineRenderer currentDrawing;
    private int index;
    private int currentColorIndex;
    private bool isDrawing;

    private void Start()
    {
        currentColorIndex = 0;
        if (tipMaterial != null)
            tipMaterial.color = wandColors[currentColorIndex];

        drawButtonAction.action.Enable();
    }

    private void Update()
    {
        bool isPressed = drawButtonAction.action.ReadValue<float>() > 0.5f;

        if (isPressed && !isDrawing)
            StartDrawing();
        else if (!isPressed && isDrawing)
            StopDrawing();

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
}
