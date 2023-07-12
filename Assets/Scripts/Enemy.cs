using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    SpriteRenderer spr;
    
    public float maxHp;
    public float hp;

    void Start() 
    {
        hp = maxHp;
        spr = GetComponent<SpriteRenderer>();
    }

    
    public void TakeDam(float Damage)
    {
        hp -= Damage;
        spr.color = Color.red;
        Invoke(nameof(returnColor),0.05f);
    }

    void returnColor()
    {
        spr.color = Color.white;
    }
}
