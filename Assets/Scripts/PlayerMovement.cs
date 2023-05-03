using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private TMP_Text plantsText;
    public Rigidbody2D rb;
    private BoxCollider2D coll;
    public HealthBar healthbar;
    [SerializeField] public float moveSpeed = 5f;
    [SerializeField] public float jumpSpeed = 15f;
    [SerializeField] private LayerMask jumpableGround;
    public GameObject hempSmokeSprite;
    public float initialSize = 1f;

    public int maxHealth = 100;
    public float currentHealth;
    void Start()
    {
        InvokeRepeating("TakeDamageWrapper", 1.0f, 1.0f);
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
        hempSmokeSprite.SetActive(false);
        initialSize = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.rotation.eulerAngles.y < 180)
            if (Input.GetKeyDown(KeyCode.A))
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 180f, transform.rotation.eulerAngles.z);

        if (transform.rotation.eulerAngles.y >= 180)
            if (Input.GetKeyDown(KeyCode.D))
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 180f, transform.rotation.eulerAngles.z);

        float dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if(Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }

    }

    void TakeDamageWrapper() {
        int damage = 1;
        TakeDamage(damage);
    }

    void TakeDamage(int damage) {
        currentHealth -= damage;
        healthbar.SetHealth(currentHealth);

        if (healthbar.GetHealth() <= 0)
            Die();
    }

    public Vector2 respawnPosition;
    private void Die()
    {
        // Disable the player game object
        gameObject.SetActive(false);

        // Reset the player's health
        currentHealth = maxHealth;

        // Respawn the player at the specified position
        transform.position = respawnPosition;

        // Enable the player game object
        gameObject.SetActive(true);
    }

    private bool IsGrounded(){
        //Returns if boxcast is on ground or not, used for logic of jumping and to stop infinite jumping
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    public void Shrink(float scale)
    {
        transform.localScale = new Vector3(scale, scale, scale);
    }

    private void RestoreSize()
    {
        transform.localScale = new Vector3(initialSize, initialSize, initialSize);
    }

    private int plants = 0;
    public int plantsNeededToShowSprite = 3;
    
    void OnTriggerEnter2D(Collider2D collisionObject)
    {
        if (collisionObject.gameObject.CompareTag("Collectable"))
        {
            currentHealth = maxHealth;
            healthbar.SetHealth(maxHealth);
            Destroy(collisionObject.gameObject);
            plants++;
            plantsText.text = "Plants: " + plants;

            if (plants >= plantsNeededToShowSprite)
                hempSmokeSprite.SetActive(true);
            
            if(collisionObject.gameObject.name == "SpecialPlant")
                jumpSpeed = 13f;
            
            if(collisionObject.gameObject.name == "BadPlant")
            {
                Debug.Log("test");
                Shrink(0.3f);
                jumpSpeed = 6f;
            }

            if(collisionObject.gameObject.name == "RegrowPlant")
            {
                RestoreSize();
                jumpSpeed = 14f;
            }

            if(collisionObject.gameObject.name == "JumpPlant")
            {
                jumpSpeed = 18f;
            }
        }

        if (collisionObject.gameObject.CompareTag("Enemy"))
        {
            Die();
        }
        
    }

    
}
