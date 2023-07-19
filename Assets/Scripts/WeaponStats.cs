using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    [SerializeField] GunStat[] weaponStat;

    public GunStat WeaponStatReload(int weaponNum, int upgradeNum)
    {
        int weaponStatNum = 0;
        weaponStatNum = (weaponNum - 1) * 4;
        for (int i = 0;i < 4;i++)
        {
            if (upgradeNum == i) { return weaponStat[weaponStatNum + i]; }
        }
        return null;
    }
}