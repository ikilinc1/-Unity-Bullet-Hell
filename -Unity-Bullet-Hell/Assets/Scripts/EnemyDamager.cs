using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamager : MonoBehaviour
{
    public float damageAmount;
    public float lifeTime;
    public float growSpeed = 2.5f;
    public bool shouldKnockBack;
    public bool destroyParent;
    public bool damageOverTime;
    public float timeBetweenDamage;
    public bool destroyOnImpact;
    
    private Vector3 targetSize;
    private float damageCounter;
    private List<EnemyController> enemiesInRange = new List<EnemyController>();
    
    // Start is called before the first frame update
    void Start()
    {
        
        targetSize = transform.localScale;
        transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.MoveTowards(transform.localScale, targetSize, growSpeed * Time.deltaTime);
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            targetSize = Vector3.zero;

            if (transform.localScale.x == 0f)
            {
                Destroy(gameObject);
                if (destroyParent)
                {
                    Destroy(transform.parent.gameObject);
                }
            }
        }

        if (damageOverTime)
        {
            damageCounter -= Time.deltaTime;
            if (damageCounter <= 0)
            {
                damageCounter = timeBetweenDamage;
                for (int i = 0; i < enemiesInRange.Count; i++)
                {
                    if (enemiesInRange[i] != null)
                    {
                        enemiesInRange[i].TakeDamage(damageAmount, shouldKnockBack);
                    }
                    else
                    {
                        enemiesInRange.RemoveAt(i);
                        i--;
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!damageOverTime)
        {
            if (other.CompareTag("Enemy"))
            {
                other.GetComponent<EnemyController>().TakeDamage(damageAmount, shouldKnockBack);
                if (destroyOnImpact)
                {
                    Destroy(gameObject);
                }
            }
        }
        else
        {
            if (other.CompareTag("Enemy"))
            {
                enemiesInRange.Add(other.GetComponent<EnemyController>());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (damageOverTime)
        {
            if (other.CompareTag("Enemy"))
            {
                enemiesInRange.Remove(other.GetComponent<EnemyController>());
            }
        }
    }
}
