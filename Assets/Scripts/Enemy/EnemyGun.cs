using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    private ParticleManager particleManager;
    private EnemyBulletManager enemyBulletManager;

    [Header("GunStat")]
    public GunStat Stat;
    public float damage;

    [Header("Others")]
    public Transform[] muzzle;
    public Transform BulletBundle;
    public GameObject bulletPrefab;
    [SerializeField] private int bulletType;
    [SerializeField] private float spawnDealy;
    private bool spawnEnd = false;

    private bool shootAble;
    private float curShotDelay;


    void Start()
    {
        particleManager = GameObject.FindWithTag("ParticleManager").GetComponent<ParticleManager>();
        enemyBulletManager = GameObject.FindWithTag("EnemyBulletManager").GetComponent<EnemyBulletManager>();
        Invoke(nameof(EnableSpawnDealy), spawnDealy);
        shootAble = true;
        BulletBundle = GameObject.FindWithTag("BulletBundle").transform;
    }

    private void EnableSpawnDealy()
    {
        spawnEnd = true;
    }

    void Update()
    {
        if(spawnEnd)
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
                    foreach (GameObject Bullet in enemyBulletManager.EnemyShootedBullets[bulletType])
                    {
                        if (!Bullet.activeSelf)
                        {
                            selectBullet = Bullet;
                            selectBullet.transform.SetPositionAndRotation(muzzle[j].position, Quaternion.Euler(0, 0, muzzle[j].localEulerAngles.z + Stat.spreadAngle / 2 + i * Stat.spreadAngle));
                            Projectile bulletProjectile = selectBullet.GetComponent<Projectile>();
                            Rigidbody2D bulletRigid = selectBullet.GetComponent<Rigidbody2D>();
                            bulletProjectile.Damage = damage * Stat.damageMultipiler;
                            selectBullet.SetActive(true);
                            bulletRigid.velocity = Vector2.zero;
                            bulletRigid.AddForce(selectBullet.transform.up * -1 * Stat.bulletSpeed, ForceMode2D.Impulse);
                            break;
                        }
                    }

                    if (!selectBullet)
                    {
                        selectBullet = Instantiate(bulletPrefab, muzzle[j].position, Quaternion.Euler(0, 0, muzzle[j].localEulerAngles.z + Stat.spreadAngle / 2 + i * Stat.spreadAngle));
                        selectBullet.transform.SetParent(BulletBundle);
                        enemyBulletManager.EnemyShootedBullets[bulletType].Add(selectBullet);
                        Projectile bulletProjectile = selectBullet.GetComponent<Projectile>();
                        Rigidbody2D bulletRigid = selectBullet.GetComponent<Rigidbody2D>();
                        bulletProjectile.Damage = damage * Stat.damageMultipiler;
                        bulletProjectile.particleManager = particleManager;
                        bulletRigid.AddForce(selectBullet.transform.up * -1 * Stat.bulletSpeed, ForceMode2D.Impulse);
                    }
                }
            }
            curShotDelay = Stat.shotDelay;
        }
    }
}
