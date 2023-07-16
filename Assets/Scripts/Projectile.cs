using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Damage;
    public int penetrationPower;
    
    public ParticleManager particleManager;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Enemy")) 
        {
            other.GetComponent<Enemy>().TakeDam(Damage);
            particleManager.summonProjectileHitParticle(transform.position);
            if (penetrationPower < 1) { gameObject.SetActive(false); }
            penetrationPower--;
        }   
        else if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerInfo>().TakeDamage(Damage);
            particleManager.summonProjectileHitParticle(transform.position);
            if (penetrationPower < 1) { gameObject.SetActive(false); }
            penetrationPower--;
        }
        else { gameObject.SetActive(false); }
    }
}
