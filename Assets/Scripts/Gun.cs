using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [System.Serializable]
    public class GunStat
    {
        public float shotDelay;
        public float bulletCount;
        public float spreadAngle;
        public float bulletSpeed;
        public float muzzleCount;
    }

    private PlayerInfo playerInfo;

    [Header("GunStat")]
    public GunStat Stat;

    [Header("Others")]
    public Transform[] muzzle;
    public Transform BulletBundle;
    public GameObject bulletPrefab;
    public List<GameObject> shootedBullets;
    public Sprite[] bulletSprites;
    public Sprite selectBulletSprite;

    private bool shootAble;
    private float curShotDelay;


    void Start()
    {
        shootedBullets = new List<GameObject>();
        shootAble = true;
        playerInfo = GetComponent<PlayerInfo>();
    }

    void Update()
    {
        Shot();
        if(!shootAble)
        {
            curShotDelay -= Time.deltaTime;
            if (curShotDelay < 0)
                shootAble = true;
        }

    }


    void Shot()
    {
        if(Input.GetKey(KeyCode.Z) && shootAble)
        {
            shootAble = false;
            for (int j = 0; j < Stat.muzzleCount; j++)
            {
                for (float i = -Stat.bulletCount / 2; i < Stat.bulletCount / 2; i++)
                {
                    GameObject selectBullet = null;
                    foreach (GameObject Bullet in shootedBullets)
                    {
                        if (!Bullet.activeSelf)
                        {
                            selectBullet = Bullet;
                            selectBullet.transform.SetPositionAndRotation(muzzle[j].position, Quaternion.Euler(0, 0, Stat.spreadAngle / 2 + i * Stat.spreadAngle));
                            selectBullet.GetComponent<Projectile>().Damage = playerInfo.atk;
                            selectBullet.GetComponent<SpriteRenderer>().sprite = selectBulletSprite;
                            selectBullet.SetActive(true);
                            selectBullet.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                            selectBullet.GetComponent<Rigidbody2D>().AddForce(selectBullet.transform.up * Stat.bulletSpeed,ForceMode2D.Impulse);
                            break;
                        }
                    }

                    if (!selectBullet)
                    {
                        selectBullet = Instantiate(bulletPrefab, muzzle[j].position, Quaternion.Euler(0, 0, Stat.spreadAngle / 2 + i * Stat.spreadAngle));
                        selectBullet.transform.SetParent(BulletBundle);
                        shootedBullets.Add(selectBullet);
                        selectBullet.GetComponent<Projectile>().Damage = playerInfo.atk;
                        selectBullet.GetComponent<SpriteRenderer>().sprite = selectBulletSprite;
                        selectBullet.GetComponent<Rigidbody2D>().AddForce(selectBullet.transform.up * Stat.bulletSpeed, ForceMode2D.Impulse);
                    }
                }
            }
            curShotDelay = Stat.shotDelay;
        }
    }
}
