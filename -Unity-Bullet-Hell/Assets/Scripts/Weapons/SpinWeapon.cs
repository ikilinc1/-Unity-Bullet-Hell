using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinWeapon : Weapon
{
    public float rotateSpeed;
    public Transform holder;
    public Transform fireballToSpawn;
    public float timeBetweenSpawn;
    public EnemyDamager damager;

    private float spawnCounter;
    
    // Start is called before the first frame update
    void Start()
    {
        SetStats();
    }

    // Update is called once per frame
    void Update()
    {
        holder.rotation = Quaternion.Euler(0f, 0f, holder.rotation.eulerAngles.z + (stats[weaponLevel].speed * rotateSpeed * Time.deltaTime));
    
        spawnCounter -= Time.deltaTime;
        if (spawnCounter <= 0)
        {
            spawnCounter = timeBetweenSpawn;
            for (int i = 0; i < stats[weaponLevel].amount; i++)
            {
                float rot = (360f / stats[weaponLevel].amount) * i;
                Instantiate(fireballToSpawn, fireballToSpawn.position, Quaternion.Euler(0f,0f,rot), holder).gameObject.SetActive(true);
                SFXManager.instance.PlaySFXPitched(8);
            }
        }

        if (statsUpdated)
        {
            statsUpdated = false;
            SetStats();
        }
    }

    public void SetStats()
    {
        damager.damageAmount = stats[weaponLevel].damage;
        transform.localScale = Vector3.one * stats[weaponLevel].range;
        timeBetweenSpawn = stats[weaponLevel].timeBetweenAttacks;
        damager.lifeTime = stats[weaponLevel].duration;
        spawnCounter = 0f;
    }
}
