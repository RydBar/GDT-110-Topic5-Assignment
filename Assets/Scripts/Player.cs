using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    GameManager gameManager;
    [SerializeField]
    private GameObject bulletPrefab;

    private float playerHealth;
    [SerializeField]
    private float playerMaxHealth;

    public float moveSpeed = 5f;
    public float jumpStrength = 5f;
    public float enemyDamage = 20;
    public float fireRate = 0.2f;

    private float fireDelay = 0.5f;
    private bool isGrounded = false;
    private bool lastMoveDir = false; //true = left, false = right
    private float bulletSpawnOffset = 1.4f;

    private Rigidbody2D riBo;
    void Start()
    {
        riBo = GetComponent<Rigidbody2D>();
        fireDelay = fireRate;
    }

    void Update()
    {
        if (fireDelay > 0)
        {
            fireDelay -= Time.deltaTime;
        }
        
        //Movement
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            lastMoveDir = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            lastMoveDir = false;
        }
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            riBo.AddForce(Vector2.up * jumpStrength);
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && fireDelay <= 0)
        {
            if (lastMoveDir) //if last move direction was left
            {
                Vector3 bulletSpawnLocation = new Vector3(transform.position.x  + bulletSpawnOffset, transform.position.y, 0);
                GameObject bullet = Instantiate(bulletPrefab, bulletSpawnLocation, transform.rotation);
                bullet.GetComponent<Bullet>().setDirection(lastMoveDir); //changes bullet direction & sets force based on last direction moved
                fireDelay = fireRate;
            }
            else //if last move direction was right
            {
                Vector3 bulletSpawnLocation = new Vector3(transform.position.x  - bulletSpawnOffset, transform.position.y, 0);
                GameObject bullet = Instantiate(bulletPrefab, bulletSpawnLocation, transform.rotation);
                bullet.GetComponent<Bullet>().setDirection(lastMoveDir); //changes bullet direction & sets force based on last direction moved
                fireDelay = fireRate;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            //Debug.Log("Ground touched");
            isGrounded = true;
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Damage taken by player");
            takeDamage(enemyDamage);
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
        if (trigger.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy touched");
            takeDamage(enemyDamage);
        }
    }

    public void resetHealth()
    {
        playerHealth = playerMaxHealth;
    }

    /// <summary>
    /// Damages the player by the given amount
    /// </summary>
    /// <param name="damage"></param>
    private void takeDamage(float damage)
    {
        playerHealth -= damage;
        Debug.Log("Damage taken, New HP: " + playerHealth);
        if (playerHealth <= 0)
        {
            if(gameManager != null)
            {
                gameManager.ChangeState(GameManager.GameState.GameOver);
            }
        }
    }
}
