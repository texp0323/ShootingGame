using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    private WaveManager waveManager;
    private PlayerMovement playerMovement;
    private Gun playerWeapon;
    private WeaponStats weaponStats;
    private SpriteRenderer spr;
    private ScoreManager scoreManager;

    [Header("플레이어 스탯")]
    public int level = 1;
    public float maxExp = 100;
    public float exp;
    public float maxhp = 20;
    public float hp;
    public float moveSpeed = 4;
    public float atk = 2;

    //경험치 관련
    private float takedExp;
    private bool expBarReloaded;

    //스킬 쿨타임
    [SerializeField] private float skillCool;
    private float curSkillCool;
    private bool skillAble;

    [Header("스킬파티클")]
    [SerializeField] ParticleSystem skillPs;

    [Header("무기")]
    [SerializeField] private int equippedWeapon = 1;
    [SerializeField] private int upgradeNum;

    [Header("UI")]
    [SerializeField] private Image expBar;
    [SerializeField] private Animator bloodScreenAnim;
    [SerializeField] private Image skillUICircle;
    private Image skillUIIcon;
    private Text skillUIText;
    [SerializeField] private Image[] weaponCaseUIs;
    [SerializeField] private Text levelText;
    [SerializeField] private Text hpText;
    private Image hpBar;

    [Header("무적 이펙트 오브젝트")]
    [SerializeField] GameObject shield;

    //기타변수
    Color disableColor = new Color(1,1,1,0.1f);
    private float hitTime;
    private bool invincibility;
    private float invincibilityDuration;

    [Header("Color")]
    [SerializeField] private Color[] weaponColor;


    private void Start()
    {
        //GetComponent
        waveManager = GameObject.FindWithTag("WaveManager").GetComponent<WaveManager>();
        scoreManager = GetComponent<ScoreManager>();
        playerMovement = GetComponent<PlayerMovement>();
        playerWeapon = GetComponent<Gun>();
        weaponStats = GetComponent<WeaponStats>();
        spr = GetComponent<SpriteRenderer>();

        //UI Set
        skillUIIcon = skillUICircle.transform.GetChild(0).GetComponent<Image>();
        skillUIText = skillUICircle.transform.GetChild(1).GetComponent<Text>();
        hpBar = hpText.transform.GetChild(1).GetComponent<Image>();

        //값 초기화
        ResetPlayer();
    }

    private void Update()
    {
        //경험치 UI 동가화 및 레벨업 판정
        if(exp != takedExp)
            ExpToUI();
        //스킬사용
        if (Input.GetKeyDown(KeyCode.X) && skillAble)
        {
            UseSkill();
        }
        //스킬 타이머 진행
        SkillCoolTimer();
        //스킬UI 동기화
        SkillCoolToUI();
        //hitTime 진행
        if (hitTime > 0)
            HitRedEffectTimer();
        if (invincibilityDuration > 0)
            InvincibilityTimer();
    }

    //플레이어 초기화
    public void ResetPlayer()
    {
        level = 1;
        maxExp = 10;
        takedExp = 0;
        exp = 0;
        maxhp = 20;
        hp = maxhp;
        atk = 5;
        int temp = equippedWeapon;
        if (temp > 1) { takeWeapon(1); }
        else { takeWeapon(2); }
        takeWeapon(temp);
        HpToUI();
        levelToUI();
        ExpToUI();
        curSkillCool = skillCool;
        skillAble = true;
    }

    //체력회복
    public void Heal(float healPercent)
    {
        hp += maxhp * healPercent;
        if(hp > maxhp)
            hp = maxhp;
        HpToUI();
    }

    //무적 아이템
    public void UseInvincibility()
    {
        invincibility = true;
        shield.SetActive(true);
        hpBar.color = Color.yellow;
        invincibilityDuration = 5;
    }

    private void InvincibilityTimer()
    {
        invincibilityDuration -= Time.deltaTime;
        if (invincibilityDuration < 0)
        {
            invincibility = false;
            shield.SetActive(false);
            hpBar.color = Color.white;
        }
    }

    //스킬
    private void UseSkill()
    {
        skillAble = false;
        curSkillCool = 0;
        skillPs.Play();
        GameObject[] Eprojectiles = GameObject.FindGameObjectsWithTag("EnemyProjectile");
        foreach (GameObject eprojectile in Eprojectiles)
        {
            eprojectile.SetActive(false);
        }
    }

    private void SkillCoolTimer()
    {
        if (curSkillCool < skillCool)
            curSkillCool += Time.deltaTime;
        else
            skillAble = true;
    }

    private void  SkillCoolToUI()
    {
        skillUICircle.fillAmount = curSkillCool / skillCool;
        if(skillAble)
        {
            skillUICircle.color = Color.white;
            skillUIIcon.color = Color.white;
            skillUIText.color = Color.white;
        }
        else
        {
            skillUICircle.color = disableColor;
            skillUIIcon.color = disableColor;
            skillUIText.color = disableColor;
        }
    }

    //데미지 받기
    public void TakeDamage(float damage)
    {
        if(!invincibility)
        {
            hp -= damage;
            if (!bloodScreenAnim.gameObject.activeSelf) { bloodScreenAnim.gameObject.SetActive(true); }
            HitRedEffect();
            if (hp < 1)
            {
                hp = 0;
                gameObject.SetActive(false);
            }
            HpToUI();
        }
    }
    private void HpToUI()
    {
        hpText.text = ("HP " + hp + "/" + maxhp);
        hpBar.fillAmount = hp / maxhp;
    }

    private void HitRedEffect()
    {
        spr.color = Color.red;
        hitTime = 0.05f;
        bloodScreenAnim.Play("bloodScreen", -1, 0f);
    }

    private void HitRedEffectTimer()
    {
        hitTime -= Time.deltaTime;
        if (hitTime <= 0)
            spr.color = Color.white;
    }


    //무기
    public void takeWeapon(int weaponNum)
    {
        if (equippedWeapon != weaponNum)
        {
            upgradeNum = 0;
            equippedWeapon = weaponNum;
            playerWeapon.selectBulletSprite = playerWeapon.bulletSprites[weaponNum - 1];
            weaponReload(equippedWeapon);
            weaponToUI();
        }
        else if (upgradeNum < 3) 
        {
            upgradeNum++;
            weaponToUI();
        }

        playerWeapon.Stat = weaponStats.weaponStatReload(equippedWeapon, upgradeNum);
    }
    private void weaponReload(int weaponNum)
    {
        for (int i = 1; i < 4; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
            if (weaponNum == i)
                transform.GetChild(i).gameObject.SetActive(true);
        }
        for (int i = 0; i < 4; i++)
        {
            transform.GetChild(weaponNum).GetChild(i).gameObject.SetActive(false);
            if (level-1 > i)
                transform.GetChild(weaponNum).GetChild(i).gameObject.SetActive(true);
        }
    }
    //무기 UI
    private void weaponToUI()
    {
        for (int i = 0; i < 4; i++)
        {
            weaponCaseUIs[i].transform.GetComponent<Image>().enabled = false;
            weaponCaseUIs[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
            if (upgradeNum >= i)
            {
                weaponCaseUIs[i].color = weaponColor[equippedWeapon-1];
                weaponCaseUIs[i].transform.GetComponent<Image>().enabled = true;
                weaponCaseUIs[i].transform.GetChild(0).GetComponent<Image>().enabled = true;
            }
            if(upgradeNum == i)
                weaponCaseUIs[i].transform.GetComponent<Animator>().Play("WeaponIcon", -1, 0f);
        }
    }

    //레벨업
    public void takeExpAndScore(float takingExp, int takedScore)
    {
        scoreManager.ScoreUp(takedScore);
        waveManager.KillEnemy();
        if(level < 5)
        {
            takedExp = takedExp + takingExp;
            expBarReloaded = false;
        }
    }
    private void ExpToUI()
    {
        if (Abs(takedExp - exp) < 0.01f)
        {
            if (takedExp != 0)
                expBarReload();
            else if(!expBarReloaded)
            {
                expBarReloaded = true;
                Invoke(nameof(expBarReload), 0.1f);
            }
        }
        exp = Mathf.Lerp(exp, takedExp, Time.deltaTime * 5);
        expBar.fillAmount = exp / maxExp;
        if (exp >= maxExp) { LevelUp(); }
    }
    private void expBarReload()
    {
        exp = takedExp;
    }
    private void LevelUp()
    {
        takedExp = 0;
        maxExp = maxExp * 1.5f;
        level++;
        maxhp = maxhp + 5;
        hp = maxhp;
        atk = atk + 5;
        weaponReload(equippedWeapon);
        levelToUI();
        HpToUI();
    }
    private void levelToUI()
    {
        levelText.text = ("LV. " + level);
        if(level > 4)
            levelText.text = ("LV. MAX");
    }

    //적 과 충돌 시
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            TakeDamage(collision.transform.GetComponent<Enemy>().crashDamage);
            playerMovement.Crash();
        }
    }

    //계산함수
    private float Abs(float a)
    {
        if (a < 0)
            return -a;
        else
            return a;
    }
}
