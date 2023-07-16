using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    private ParticleManager particleManager;

    [Header("GunStat")]
    public GunStat Stat;
    public float damage;

    [Header("Others")]
    public Transform[] muzzle;
    public Transform BulletBundle;
    public GameObject bulletPrefab;
    public List<GameObject> shootedBullets;
    public Sprite selectBulletSprite;

    private bool shootAble;
    private float curShotDelay;


    void Start()
    {
        shootedBullets = new List<GameObject>();
        shootAble = true;
        BulletBundle = GameObject.FindWithTag("BulletBundle").transform;
        particleManager = GameObject.FindWithTag("ParticleManager").GetComponent<ParticleManager>();
    }

    void Update()
    {
        Shot();
        if (!shootAble)
        {
            curShotDelay -= Time.deltaTime;
            if (curShotDelay <= 0)
                shootAble = true;
        }

    }


    void Shot()
    {
        if (shootAble)
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
                            Projectile bulletProjectile = selectBullet.GetComponent<Projectile>();
                            Rigidbody2D bulletRigid = selectBullet.GetComponent<Rigidbody2D>();
                            bulletProjectile.Damage = damage * Stat.damageMultipiler;
                            bulletProjectile.penetrationPower = Stat.penetrationPower;
                            selectBullet.GetComponent<SpriteRenderer>().sprite = selectBulletSprite;
                            selectBullet.SetActive(true);
                            bulletRigid.velocity = Vector2.zero;
                            bulletRigid.AddForce(selectBullet.transform.up * -1 * Stat.bulletSpeed, ForceMode2D.Impulse);
                            break;
                        }
                    }

                    if (!selectBullet)
                    {
                        selectBullet = Instantiate(bulletPrefab, muzzle[j].position, Quaternion.Euler(0, 0, Stat.spreadAngle / 2 + i * Stat.spreadAngle));
                        selectBullet.transform.SetParent(BulletBundle);
                        shootedBullets.Add(selectBullet);
                        Projectile bulletProjectile = selectBullet.GetComponent<Projectile>();
                        Rigidbody2D bulletRigid = selectBullet.GetComponent<Rigidbody2D>();
                        bulletProjectile.Damage = damage * Stat.damageMultipiler;
                        bulletProjectile.penetrationPower = Stat.penetrationPower;
                        bulletProjectile.particleManager = particleManager;
                        selectBullet.GetComponent<SpriteRenderer>().sprite = selectBulletSprite;
                        bulletRigid.AddForce(selectBullet.transform.up * -1 * Stat.bulletSpeed, ForceMode2D.Impulse);
                    }
                }
            }
            curShotDelay = Stat.shotDelay;
        }
    }
}
