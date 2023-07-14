using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Damage;
    public int penetrationPower;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.GetComponent<Enemy>())
        {
            other.GetComponent<Enemy>().TakeDam(Damage);
            if(penetrationPower < 1) { gameObject.SetActive(false); }
            penetrationPower--;
        }   
        else if (other.GetComponent<PlayerInfo>())
        {
            other.GetComponent<PlayerInfo>().TakeDamage(Damage);
            if (penetrationPower < 1) { gameObject.SetActive(false); }
            penetrationPower--;
        }
        else { gameObject.SetActive(false); }
    }
}
