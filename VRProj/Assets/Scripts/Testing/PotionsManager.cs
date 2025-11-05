using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum PotionType
{
    Healing,
    Mana,
    Strength,
    Love
}
public class PotionsManager : MonoBehaviour
{
    [Header("Potion Data")]
    public PotionData[] allPotions;

    [Header("UI Elements")]
    public TMP_Text potionNameText;
    public TMP_Text ingredientsText;
    public TMP_Text descriptionText;

    private PotionData currentPotion;

    private void Start()
    {
        // Default potion
        SelectPotion(PotionType.Healing);
    }

    public void SelectPotion(PotionType type)
    {
        foreach (var potion in allPotions)
        {
            if (potion.potionType == type)
            {
                currentPotion = potion;
                UpdateUI();
                return;
            }
        }

        Debug.LogWarning($"Potion type {type} not found in allPotions!");
    }

    public void SetPotionHealing()
    {
        SelectPotion(PotionType.Healing);
    }

    public void SetPotionMana()
    {
        SelectPotion(PotionType.Mana);
    }

    public void SetPotionStrength()
    {
        SelectPotion(PotionType.Strength);
    }

    public void SetPotionLove()
    {
        SelectPotion(PotionType.Love);
    }

    private void UpdateUI()
    {
        if (currentPotion == null) return;

        potionNameText.text = currentPotion.potionType.ToString();
        descriptionText.text = currentPotion.description;

        ingredientsText.text = string.Join("\n", currentPotion.ingredients);
    }
}