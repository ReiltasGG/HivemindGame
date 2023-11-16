using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonManager : MonoBehaviour
{
    public int damagePerTick = 5;
    public float tickRateInSeconds = 1.0f;
    public float poisonDurationInSeconds = 5.0f;

    private Color PoisonEffectColor = Color.green;

    private Dictionary<GameObject, float> PoisonedObjectsAndTimeLeft;
    GameTimer timer;

    private void Start()
    {
        PoisonedObjectsAndTimeLeft = new Dictionary<GameObject, float>();
    }

    // If you want to poison something, use this function
    public void ApplyPoison(GameObject poisonedObject)
    {
        if (PoisonedObjectsAndTimeLeft.ContainsKey(poisonedObject))
        {
            PoisonedObjectsAndTimeLeft[poisonedObject] = poisonDurationInSeconds;
        }
        else
        {
            TurnPoisonColor(poisonedObject);
            PoisonedObjectsAndTimeLeft.Add(poisonedObject, poisonDurationInSeconds);
            TurnPoisonColor(poisonedObject);
        }

        if (!IsInvoking(nameof(PoisonDamagePoisonedObjects)))
        {
            StartCoroutine(PoisonDamagePoisonedObjects());
        }
    }

    private IEnumerator PoisonDamagePoisonedObjects()
    {
        while (PoisonedObjectsAndTimeLeft.Count > 0)
        {
            yield return new WaitForSeconds(tickRateInSeconds);

            List<GameObject> objectsToRemove = new List<GameObject>();

            foreach (var entry in new Dictionary<GameObject, float>(PoisonedObjectsAndTimeLeft))
            {
                GameObject poisonedObject = entry.Key;
                float timeLeft = entry.Value;

                timeLeft -= Time.deltaTime;

                if (poisonedObject == null) // object was destroyed and is now null
                {
                    objectsToRemove.Add(poisonedObject);
                    continue;
                }

                if (timeLeft <= 0) // poison ends
                {
                    objectsToRemove.Add(poisonedObject);

                    ResetColorToDefault(poisonedObject);

                }
                else // do poison effects
                {
                    PoisonedObjectsAndTimeLeft[poisonedObject] = timeLeft - tickRateInSeconds;
                    Damage(poisonedObject);
                }
            }

            foreach (var obj in objectsToRemove)
            {
                PoisonedObjectsAndTimeLeft.Remove(obj);
            }

        }
    }

    private void TurnPoisonColor(GameObject poisonedObject)
    {
        AttributesManager attributesManager = GetAttributesManager(poisonedObject);
        attributesManager.SetSpriteDefaultColor(PoisonEffectColor);

        SpriteRenderer sprite = GetSprite(poisonedObject);
        sprite.color = PoisonEffectColor;
    }
    private void ResetColorToDefault(GameObject poisonedObject)
    {
        AttributesManager attributesManager = GetAttributesManager(poisonedObject);

        attributesManager.ResetSpriteDefaultColor();
        attributesManager.ResetColorToDefault();
        
    }
    private SpriteRenderer GetSprite(GameObject poisonedObject)
    {
        AttributesManager attributesManager = GetAttributesManager(poisonedObject);

        SpriteRenderer sprite = attributesManager.GetSprite();
        if (sprite == null)
            throw new System.Exception("No sprite found");

        return sprite;
    }

    private void Damage(GameObject poisenedObject)
    {
        AttributesManager attributesManager = GetAttributesManager(poisenedObject);

        attributesManager.takeDamage(damagePerTick);
    }

    private AttributesManager GetAttributesManager(GameObject gameObject) {
        AttributesManager attributesManager = gameObject.GetComponent<AttributesManager>();

        if (attributesManager == null)
            throw new System.Exception("No attribute manager found");

        return attributesManager;
    }
}
