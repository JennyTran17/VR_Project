using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PotionCompletionChecker : MonoBehaviour
{
    [Header("Potion Settings")]
    public int totalPotionTypes = 4;         

    [Header("Events")]
    public GameObject lightBeam;
    public VideoPlayer videoPlayer;

    private HashSet<string> craftedPotionNames = new HashSet<string>();
    private bool eventTriggered = false;

    // When a potion prefab is instantiated
    public void RegisterPotion(GameObject potionObj)
    {
        if (eventTriggered) return;

        craftedPotionNames.Add(potionObj.name.Replace("(Clone)", "").Trim());

        if (craftedPotionNames.Count >= totalPotionTypes)
            TriggerEvent();
    }

    private void TriggerEvent()
    {
        eventTriggered = true;

        if (lightBeam != null)
            lightBeam.SetActive(true);

        if (videoPlayer != null)
            videoPlayer.Play();

        Debug.Log("All potions crafted. Event triggered!");
    }
}
