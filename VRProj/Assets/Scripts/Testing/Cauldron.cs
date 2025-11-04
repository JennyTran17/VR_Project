using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    [Header("Cauldron State")]
    public List<string> currentIngredients = new List<string>();
    public PotionsManager potionsManager;
    public Transform spawnPoint;

    [Header("Potion Prefabs")]
    public GameObject healingPrefab;
    public GameObject manaPrefab;
    public GameObject strengthPrefab;
    public GameObject lovePrefab;

    [Header("Explosion Effects")]
    [Tooltip("List of explosion or magic burst prefabs to play before potion spawns")]
    public List<GameObject> explosionEffects = new List<GameObject>();

    private GameObject prefab;

    private void OnCollisionEnter(Collision other)
    {
        Ingredient ingredient = other.gameObject.GetComponent<Ingredient>();
        if (ingredient != null)
        {
            string ingredientName = ingredient.ingredientName;
            Debug.Log($"Ingredient {ingredientName} added to cauldron.");
            currentIngredients.Add(ingredientName);

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
        return recipeSet.SetEquals(addedSet);
    }

    private void SpawnPotion(PotionType type)
    {
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
            case PotionType.Love:
                prefab = lovePrefab;
                break;
        }

        if (prefab != null)
        {
            StartCoroutine(SpawnSequence(1f));
        }
    }

    private IEnumerator SpawnSequence(float delay)
    {
        // Pick a random explosion effect from the list
        GameObject chosenExplosion = null;
        if (explosionEffects != null && explosionEffects.Count > 0)
        {
            int randomIndex = Random.Range(0, explosionEffects.Count);
            chosenExplosion = explosionEffects[randomIndex];
        }

        // Instantiate explosion if available
        GameObject explosionInstance = null;
        if (chosenExplosion != null)
        {
            explosionInstance = Instantiate(chosenExplosion, spawnPoint.position, Quaternion.identity);
        }

        // Wait before potion spawns
        yield return new WaitForSeconds(delay);

        // Spawn the potion
        Instantiate(prefab, spawnPoint.position, Quaternion.identity);

        // Destroy explosion after a short time
        if (explosionInstance != null)
        {
            yield return new WaitForSeconds(0.5f);
            Destroy(explosionInstance);
        }
    }
}
