using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private TMP_Text plantsText;

    public HealthBar healthbar;
    public int maxHealth = 100;
    private int plants = 0;
    void OnTriggerEnter2D(Collider2D collisionObject)
    {
        if (collisionObject.gameObject.CompareTag("Collectable"))
        {
            healthbar.SetHealth(maxHealth);
            Destroy(collisionObject.gameObject);
            plants++;
            plantsText.text = "Plants: " + plants;
        }
    }
}
