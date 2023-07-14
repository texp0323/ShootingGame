using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    private Gun playerWeapon;
    private WeaponStats weaponStats;

    [Header("�÷��̾� ����")]
    public int level = 1;
    public float maxExp = 100;
    public float exp;
    public float maxhp = 20;
    public float hp;
    public float moveSpeed = 4;
    public float atk = 2;

    private float takedExp;
    private bool expBarReloaded;

    [SerializeField] private float skillCool;
    private float curSkillCool;
    private bool skillAble;

    [Header("����")]
    [SerializeField] private int equippedWeapon = 1;
    [SerializeField] private int upgradeNum;


    [Header("UI")]
    [SerializeField] private Image expBar;
    [SerializeField] private Animator bloodScreenAnim;
    [SerializeField] private Image skillUI;


    private void Start()
    {
        playerWeapon = GetComponent<Gun>();
        weaponStats = GetComponent<WeaponStats>();
        hp = maxhp;
    }

    private void Update()
    {
        if(exp != takedExp)
            ExpToUI();
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject[] projectiles = GameObject.FindGameObjectsWithTag("EnemyProjectile");

        }
    }

    //��ų
    private void skill()
    {

    }

    //������ �ޱ�
    public void TakeDamage(float damage)
    {
        hp -= damage;
        if (!bloodScreenAnim.gameObject.activeSelf) { bloodScreenAnim.gameObject.SetActive(true); }
        bloodScreenAnim.Play("bloodScreen", -1, 0f);
        if (hp < 1)
            Time.timeScale = 0;
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
        }
        else if (upgradeNum < 3) { upgradeNum++; }
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

    //������
    public void takeExp(float takingExp)
    {
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
        maxhp = maxhp + 20;
        hp = maxhp;
        atk = atk + 5;
        weaponReload(equippedWeapon);
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
