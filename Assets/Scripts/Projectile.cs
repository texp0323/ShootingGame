using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Damage;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.GetComponent<Enemy>())
        {
            other.GetComponent<Enemy>().TakeDam(Damage);
        }
        gameObject.SetActive(false);
    }
}
