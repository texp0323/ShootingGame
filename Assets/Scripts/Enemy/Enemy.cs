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
    public float crashDamage;
    [SerializeField] private bool isBoss;
    [SerializeField] int bossNum;
    Image bossBar;

    Vector2 originSize;
    private Color hitColor = new Color(1, 0.85f, 0.85f);
    [HideInInspector] public bool hitColorAble= true;

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

        if(isBoss)
        {
            if(bossNum == 1)
                bossBar = GameObject.FindWithTag("Canvas").transform.GetChild(0).GetComponent<Image>();
            if (bossNum == 2)
                bossBar = GameObject.FindWithTag("Canvas").transform.GetChild(1).GetComponent<Image>();
            bossBar.gameObject.SetActive(true);
        }
    }

    
    public void TakeDam(float Damage)
    {
        hp -= Damage;
        if (isBoss)
            bossBar.fillAmount = hp / maxHp;
        transform.localScale = originSize + new Vector2(0.1f, 0.1f);
        Invoke(nameof(ReturnSize), 0.05f);
        if (hitColorAble)
        {
            spr.color = hitColor;
            Invoke(nameof(ReturnColor), 0.05f);
        }
        if(hp < 1)
        {
            if(gameObject.activeSelf)
            {
                if(isBoss)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        particleManager.SummonEnemyDestroyParticle(transform.position + new Vector3(Random.Range(-16, 16), Random.Range(-16, 16), 0));
                        bossBar.gameObject.SetActive(false);
                        GameObject.FindWithTag("WaveManager").GetComponent<WaveManager>().SetEnemyCount(1);
                        if(bossNum == 1)
                        {
                            Destroy(GetComponent<BossKnight>().summonedWave);
                        }
                        if (bossNum == 2)
                        {
                            Destroy(GetComponent<BossLord>().summonedWave);
                        }
                    }
                }
                playerInfo.TakeExpAndScore(exp, score);
                particleManager.SummonEnemyDestroyParticle(transform.position);
                itemManager.SummonItem(transform.position, dropItemType, itemDropPer);
            }
            gameObject.SetActive(false);
        }
    }

    void ReturnColor()
    {
        spr.color = Color.white;
    }
    void ReturnSize()
    {
        transform.localScale = originSize;
    }
}
