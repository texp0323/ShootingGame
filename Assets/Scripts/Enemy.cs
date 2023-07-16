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
    [SerializeField] private int score;
    [SerializeField] private int dropItemType;
    [SerializeField] private float itemDropPer;
    public float crashDamage;

    Vector2 originSize;

    private PlayerInfo playerInfo;
    private ParticleManager particleManager;
    private ItemManager itemManager;

    void Start() 
    {
        originSize = transform.localScale;
        hp = maxHp;
        playerInfo = GameObject.FindWithTag("Player").GetComponent<PlayerInfo>();
        particleManager = GameObject.FindWithTag("ParticleManager").GetComponent<ParticleManager>();
        itemManager = GameObject.FindWithTag("ItemManager").GetComponent<ItemManager>();
        spr = GetComponent<SpriteRenderer>();
    }

    
    public void TakeDam(float Damage)
    {
        hp -= Damage;
        transform.localScale = originSize + new Vector2(0.1f, 0.1f);
        Invoke(nameof(returnColor),0.05f);
        if(hp < 1)
        {
            if(gameObject.activeSelf)
            {
                playerInfo.takeExpAndScore(exp, score);
                particleManager.summonEnemyDestroyParticle(transform.position);
                itemManager.SummonItem(transform.position, dropItemType, itemDropPer);
            }
            gameObject.SetActive(false);
        }
    }

    void returnColor()
    {
        transform.localScale = originSize;
    }
}
