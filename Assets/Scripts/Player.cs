using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    GameManager gameManager;

    private float playerHealth;
    [SerializeField]
    private float playerMaxHealth;

    public float moveSpeed = 5f;
    public float jumpStrength = 5f;
    public float spikeDamage = 20;

    private bool isGrounded = false;

    private Rigidbody2D riBo;
    void Start()
    {
        riBo = GetComponent<Rigidbody2D>();

        playerHealth = playerMaxHealth;
    }

    void Update()
    {

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            riBo.AddForce(Vector2.up * jumpStrength);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            //Debug.Log("Ground touched");
            isGrounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            //Debug.Log("Ground untouched");
            isGrounded = false;
        }
    }

    /// <summary>
    /// Damages the player, damage amount changing depending on the source
    /// </summary>
    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.CompareTag("Spike"))
        {
            Debug.Log("Spike touched");
            takeDamage(spikeDamage);
        }
    }
    private void takeDamage(float damage)
    {
        Debug.Log("Damage taken, New HP: " + playerHealth);
        playerHealth -= damage;
        if (playerHealth <= 0)
        {
            if(gameManager != null)
            {
                gameManager.ChangeState(GameManager.GameState.GameOver);
            }
        }
    }
}
