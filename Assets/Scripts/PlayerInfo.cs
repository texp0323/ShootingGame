using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private Gun playerWeapon;
    private WeaponStats weaponStats;
    private SpriteRenderer spr;
    private ScoreManager scoreManager;

    [Header("�÷��̾� ����")]
    public int level = 1;
    public float maxExp = 100;
    public float exp;
    public float maxhp = 20;
    public float hp;
    public float moveSpeed = 4;
    public float atk = 2;

    //����ġ ����
    private float takedExp;
    private bool expBarReloaded;

    //��ų ��Ÿ��
    [SerializeField] private float skillCool;
    private float curSkillCool;
    private bool skillAble;

    [Header("��ų��ƼŬ")]
    [SerializeField] ParticleSystem skillPs;

    [Header("����")]
    [SerializeField] private int equippedWeapon = 1;
    [SerializeField] private int upgradeNum;

    [Header("UI")]
    [SerializeField] private Image expBar;
    [SerializeField] private Animator bloodScreenAnim;
    [SerializeField] private Image skillUICircle;
    private Image skillUIIcon;
    [SerializeField] private Image[] weaponCaseUIs;
    [SerializeField] private Text levelText;
    [SerializeField] private Text hpText;
    private Image hpBar;

    [Header("���� ����Ʈ ������Ʈ")]
    [SerializeField] GameObject shield;

    //��Ÿ����
    Color disableColor = new Color(1,1,1,0.1f);
    private float hitTime;
    private bool invincibility;
    private float invincibilityDuration;

    [Header("Color")]
    [SerializeField] private Color[] weaponColor;


    private void Start()
    {
        //GetComponent
        scoreManager = GetComponent<ScoreManager>();
        playerMovement = GetComponent<PlayerMovement>();
        playerWeapon = GetComponent<Gun>();
        weaponStats = GetComponent<WeaponStats>();
        spr = GetComponent<SpriteRenderer>();

        //�� �ʱ�ȭ
        hp = maxhp;
        curSkillCool = skillCool;
        skillAble = true;

        //UI Set
        skillUIIcon = skillUICircle.transform.GetChild(0).GetComponent<Image>();
        hpBar = hpText.transform.GetChild(1).GetComponent<Image>();
        levelToUI();
        HpToUI();
    }

    private void Update()
    {
        //����ġ UI ����ȭ �� ������ ����
        if(exp != takedExp)
            ExpToUI();
        //��ų���
        if (Input.GetKeyDown(KeyCode.E) && skillAble)
        {
            UseSkill();
        }
        //��ų Ÿ�̸� ����
        SkillCoolTimer();
        //��ųUI ����ȭ
        SkillCoolToUI();
        //hitTime ����
        if (hitTime > 0)
            HitRedEffectTimer();
        if (invincibilityDuration > 0)
            InvincibilityTimer();
    }

    //ü��ȸ��
    public void Heal(float healPercent)
    {
        hp += maxhp * healPercent;
        if(hp > maxhp)
            hp = maxhp;
        HpToUI();
    }

    //���� ������
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

    //��ų
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
        }
        else
        {
            skillUICircle.color = disableColor;
            skillUIIcon.color = disableColor;
        }
    }

    //������ �ޱ�
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


    //����
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
    //���� UI
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

    //������
    public void takeExpAndScore(float takingExp, int takedScore)
    {
        scoreManager.ScoreUp(takedScore);
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

    //�� �� �浹 ��
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            TakeDamage(collision.transform.GetComponent<Enemy>().crashDamage);
            playerMovement.Crash();
        }
    }

    //����Լ�
    private float Abs(float a)
    {
        if (a < 0)
            return -a;
        else
            return a;
    }
}
