using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public float Damage;
    private float curLifeTime;

    void OnEnable()
    {
        curLifeTime = 0;
    }

    void Update()
    {
        transform.Translate(Vector2.up * Time.deltaTime * speed);
        if(curLifeTime < lifeTime)
        {
            curLifeTime += Time.deltaTime;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.GetComponent<Enemy>())
        {
            other.GetComponent<Enemy>().TakeDam(Damage);
        }
        gameObject.SetActive(false);
    }
}
