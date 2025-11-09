using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    [SerializeField] TMP_Text hpText;
    [SerializeField]  TMP_Text strengthText;
    [SerializeField] TMP_Text powerText;

    int hp, strength;
    string power;

    public static HUDManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(instance);
    }

    public void addHP()
    {
        hp += 20;
        hpText.text = "HP: " + hp;
    }

    public void addStrength()
    {
        strength += 25;
        strengthText.text = "Strength: " + strength + "%";
    }

    public void addPower(string name)
    {
        powerText.text = "Ultimate power: " + name;
    }
}
