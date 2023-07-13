using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public float Damage;
    public float curLifeTime = 0;

    void LateUpdate()
    {
        transform.Translate(Vector2.up * Time.deltaTime * speed);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.GetComponent<Enemy>())
        {
            other.GetComponent<Enemy>().TakeDam(Damage);
        }
        gameObject.SetActive(false);
    }
}
