using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public int coinAmount = 1;
    public float moveSpeed;
    public float timeBetweenChecks = 0.2f;
    
    private bool movingToPlayer;
    private float checkCounter;
    private PlayerController player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = PlayerController.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (movingToPlayer)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                player.transform.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            checkCounter -= Time.deltaTime;
            if (checkCounter<= 0)
            {
                checkCounter = timeBetweenChecks;
                if (Vector3.Distance(transform.position, player.transform.position) < player.pickupRange)
                {
                    movingToPlayer = true;
                    moveSpeed += player.moveSpeed;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CoinController.instance.AddCoins(coinAmount);
            Destroy(gameObject);
        }
    }
}
