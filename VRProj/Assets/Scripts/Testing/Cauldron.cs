using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    public List<string> currentIngredients = new List<string>();
    public PotionsManager potionsManager;

    public Transform spawnPoint;

    public GameObject healingPrefab;
    public GameObject manaPrefab;
    public GameObject strengthPrefab;
    public GameObject speedPrefab;

    private void OnCollisionEnter(Collision other)
    {
        Ingredient ingredient = other.gameObject.GetComponent<Ingredient>();
        if (ingredient != null)
        {
            string ingredientName = ingredient.ingredientName;
            Debug.Log($"Ingredient {ingredientName} added to cauldron.");
            currentIngredients.Add(ingredientName);

            // Optionally destroy the ingredient object (simulate being consumed)
            Destroy(other.gameObject);

            CheckPotion();
        }
    }

    public void AddIngredient(string ingredient)
    {
        currentIngredients.Add(ingredient);
        CheckPotion();
    }

    private void CheckPotion()
    {
        foreach (var potion in potionsManager.allPotions)
        {
            if (IsMatchingRecipe(potion.ingredients, currentIngredients))
            {
                Debug.Log($"Created potion: {potion.potionType}");
                SpawnPotion(potion.potionType);
                currentIngredients.Clear();
                return;
            }
        }
    }

    private bool IsMatchingRecipe(string[] recipe, List<string> addedIngredients)
    {
        if (recipe.Length != addedIngredients.Count) return false;

        var recipeSet = new HashSet<string>(recipe);
        var addedSet = new HashSet<string>(addedIngredients);

        return recipeSet.SetEquals(addedSet); // order-independent match
    }

    private void SpawnPotion(PotionType type)
    {
        GameObject prefab = null;

        switch (type)
        {
            case PotionType.Healing:
                prefab = healingPrefab;
                break;
            case PotionType.Mana:
                prefab = manaPrefab;
                break;
            case PotionType.Strength:
                prefab = strengthPrefab;
                break;
            case PotionType.Speed:
                prefab = speedPrefab;
                break;
        }

        if (prefab != null)
        {
            Instantiate(prefab, spawnPoint.position, Quaternion.identity);
        }
    }
}
