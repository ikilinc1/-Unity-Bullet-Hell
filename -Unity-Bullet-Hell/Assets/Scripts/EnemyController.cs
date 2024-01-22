using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D theRB;
    public float moveSpeed;
    public float damage;
    public float hitWaitTime = 1f;
    public float health = 5f;
    public float knockBackTime = 0.5f;
    public int expToGive = 1;
    
    private Transform target;
    private float hitCounter;
    private float knockBackCounter;
    
    // Start is called before the first frame update
    void Start()
    {
        target = PlayerHealthController.instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.instance.gameObject.activeSelf)
        {
            if (knockBackCounter > 0)
            {
                knockBackCounter -= Time.deltaTime;
                if (moveSpeed > 0)
                {
                    moveSpeed = -moveSpeed * 2f;
                }
                if (knockBackCounter <=0)
                {
                    moveSpeed = Math.Abs(moveSpeed * 0.5f);
                }
            }
        
            theRB.velocity = (target.position - transform.position).normalized * moveSpeed;

            if (hitCounter > 0f)
            {
                hitCounter -= Time.deltaTime;
            }
        }
        else
        {
            theRB.velocity = Vector2.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && hitCounter <= 0f)
        {
           PlayerHealthController.instance.TakeDamage(damage);

           hitCounter = hitWaitTime;
        }
    }

    public void TakeDamage(float damageToTake)
    {
        health -= damageToTake;
        if (health <= 0)
        {
            Destroy(gameObject);
            ExperienceLevelController.instance.SpawnExp(transform.position, expToGive);
        }
        
        DamageNumberController.instance.SpawnDamage(damageToTake, transform.position);
    }
    
    public void TakeDamage(float damageToTake, bool shouldKnockBack)
    {
        TakeDamage(damageToTake);

        if (shouldKnockBack)
        {
            knockBackCounter = knockBackTime;
        }
    }
}
