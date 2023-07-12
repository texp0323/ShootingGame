using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField] int sans;   

    [Header("플레이어 스탯")]
    public int level = 1;
    public float maxExp = 100;
    public float exp;
    public int maxhp = 20;
    public int hp;
    public float moveSpeed = 4;
    public int atk = 2;

    private float takedExp;
    private bool expBarReloaded;

    [Header("무기")]
    [SerializeField] private int equippedWeapon = 1;
    [SerializeField] private int upgradeNum;


    [Header("UI")]
    [SerializeField] private Image expBar;

    private void Update()
    {
        if(exp != takedExp)
            ExpToUI();
        if (Input.GetKeyDown(KeyCode.R))
        {
            takeExp(5);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            takeWeapon(sans);
        }
    }


    //무기
    public void takeWeapon(int weaponNum)
    {
        if(equippedWeapon != weaponNum)
        {
            upgradeNum = 0;
            equippedWeapon = weaponNum;
            weaponReload(equippedWeapon);
        }
        else if(upgradeNum < 10) { upgradeNum++;}
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

    //레벨업
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
        atk = atk + 2;
        weaponReload(equippedWeapon);
    }

    private float Abs(float a)
    {
        if (a < 0)
            return -a;
        else
            return a;
    }
}
