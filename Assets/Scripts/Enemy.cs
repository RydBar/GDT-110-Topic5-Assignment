using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    private float enemyHealth = 40f;

    [SerializeField]
    private float enemySpeed = 2.5f;

    [SerializeField]
    private Rigidbody2D ribo;

    [SerializeField]
    private GameObject playerObject;

    [SerializeField]
    private GameManager gameManager;

    private Vector2 relativePoint;

    void Start()
    {

        //get enemy's own rigidbody if not assigned
        if (ribo == null)
        {
            ribo = GetComponent<Rigidbody2D>();
        }

        playerObject = GameObject.FindGameObjectWithTag("Player");

        gameManager = FindFirstObjectByType<GameManager>();
        if (gameManager == null)
        {
            Debug.Log("GAME MANAGER IS NULL IN ENEMY");
        }
    }

    void Update()
    {
        if (playerObject != null)
        {
            relativePoint = playerObject.transform.InverseTransformPoint(transform.position);
        }

        if (relativePoint.x < 0)
        {
            transform.Translate(Vector2.right * enemySpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * enemySpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.tag == "Bullet")
        {
            enemyHealth -= 20;
            if (enemyHealth <= 0)
            {
                Destroy(gameObject);
                gameManager.IncreaseScore();
            }
        }

    }
}
