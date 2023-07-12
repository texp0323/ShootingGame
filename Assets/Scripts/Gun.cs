using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [System.Serializable]
    public struct GunStat
    {
        public float shotDelay;
        public float bulletCount;
        public float spreadAngle;
        public float bulletSpeed;
    }

    private PlayerInfo playerInfo;

    [Header("GunStat")]
    public GunStat Stat;

    [Header("Others")]
    public Transform muzzle;
    public Transform BulletBundle;
    public GameObject bullet;
    public List<GameObject> shootedBullets;
    private bool shootAble;

    void Start() 
    {
        shootedBullets = new List<GameObject>();
        shootAble = true;
        playerInfo = GetComponent<PlayerInfo>();
    }

    void Update()
    {
        Shot();
    }


    void Shot()
    {
        if(Input.GetKey(KeyCode.Z) && shootAble)
        {
            shootAble = false;
            for(float i = -Stat.bulletCount/2; i<Stat.bulletCount/2; i++)
            {
                GameObject selectBullet = null;
                foreach(GameObject Bullet in shootedBullets)
                {
                    if(!Bullet.activeSelf)
                    {
                        selectBullet = Bullet;
                        selectBullet.SetActive(true);
                        selectBullet.transform.SetPositionAndRotation(muzzle.position, Quaternion.Euler(0,0,Stat.spreadAngle / 2 +  i * Stat.spreadAngle));
                        selectBullet.GetComponent<Projectile>().Damage = playerInfo.atk;
                        selectBullet.GetComponent<Projectile>().speed = Stat.bulletSpeed;
                        break;
                    }
                }

                if(!selectBullet)
                {
                    selectBullet = Instantiate(bullet, muzzle.position, Quaternion.Euler(0,0, Stat.spreadAngle / 2 + i * Stat.spreadAngle));
                    selectBullet.transform.SetParent(BulletBundle);
                    selectBullet.GetComponent<Projectile>().Damage = playerInfo.atk;
                    selectBullet.GetComponent<Projectile>().speed = Stat.bulletSpeed;
                    shootedBullets.Add(selectBullet);
                }
            }
            Invoke(nameof(DelayEnd),Stat.shotDelay);
        }
    }

    void DelayEnd()
    {
        shootAble = true;
    }
}
