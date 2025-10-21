using UnityEngine;

[CreateAssetMenu(fileName = "NewPotionData", menuName = "Potion System/Potion Data")]
public class PotionData : ScriptableObject
{
    public PotionType potionType;
    [TextArea] public string description;
    public string[] ingredients;
}