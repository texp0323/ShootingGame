using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    SpriteRenderer spr;
    
    public float maxHp;
    public float hp;
    [SerializeField] private float exp;

    private PlayerInfo playerInfo;

    void Start() 
    {
        hp = maxHp;
        playerInfo = GameObject.FindWithTag("Player").GetComponent<PlayerInfo>();
        spr = GetComponent<SpriteRenderer>();
    }

    
    public void TakeDam(float Damage)
    {
        hp -= Damage;
        spr.color = Color.red;
        Invoke(nameof(returnColor),0.05f);
        if(hp < 1)
        {
            if(gameObject.activeSelf)
                playerInfo.takeExp(exp);
            gameObject.SetActive(false);
        }
    }

    void returnColor()
    {
        spr.color = Color.white;
    }
}
