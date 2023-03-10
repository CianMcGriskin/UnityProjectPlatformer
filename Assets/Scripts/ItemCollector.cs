using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private TMP_Text plantsText;
    private int plants = 0;
    void OnTriggerEnter2D(Collider2D collisionObject)
    {
        if (collisionObject.gameObject.CompareTag("Collectable"))
        {
            Destroy(collisionObject.gameObject);
            plants++;
            plantsText.text = "Plants: " + plants;
        }
    }
}
