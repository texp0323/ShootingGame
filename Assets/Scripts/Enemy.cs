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
    [SerializeField] private int itemDropPer;
    [SerializeField] private bool isBoss;
    public float crashDamage;

    Vector2 originSize;
    private Color hitColor = new Color(1, 0.85f, 0.85f);

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
        spr.color = hitColor;
        Invoke(nameof(returnColor),0.05f);
        if(hp < 1)
        {
            if(gameObject.activeSelf)
            {
                playerInfo.takeExpAndScore(exp, score);
                particleManager.summonEnemyDestroyParticle(transform.position);
                if(isBoss)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        particleManager.summonEnemyDestroyParticle(transform.position + new Vector3(Random.Range(-16, 16), Random.Range(-16, 16), 0));
                    }
                }
                itemManager.SummonItem(transform.position, dropItemType, itemDropPer);
            }
            gameObject.SetActive(false);
        }
    }

    void returnColor()
    {
        transform.localScale = originSize;
        spr.color = Color.white;
    }
}
