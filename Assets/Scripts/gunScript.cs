using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    [SerializeField] GameObject bulletobject;
    [SerializeField] float bulletSpeed; //mermi hizi
    private float minbulletSpeed = 20f;
    private float maxbulletSpeed = 100f;

    public float fireRate = 1f; // ates sikligi
    private float minFireRate = 0.17f;
    private float maxFireRate = 1f;

    public float bulletLifeTime = 1f; // menzil
    private float minBulletLifeTime = 0.10f;
    private float maxBulletLifeTime = 2f;

    public int damagetobox,damagetogate;
    public static bulletManager bulletScript;
    public float bulletRotStabilizer;
    public float initialFireRate;
    public float initialBulletLifeTime;

    private void Start()
    {
        StartAtesEt();
        initialBulletLifeTime = bulletLifeTime;
        initialFireRate = fireRate;
    }
    private void Update() 
    {
        fireRate = Mathf.Clamp(fireRate, minFireRate, maxFireRate);
        bulletLifeTime = Mathf.Clamp(bulletLifeTime, minBulletLifeTime, maxBulletLifeTime);
        bulletSpeed = Mathf.Clamp(bulletSpeed, minbulletSpeed, maxbulletSpeed);
    }

    public void StartAtesEt()
    {
        StartCoroutine(AtesEt());
    }
    public IEnumerator AtesEt()
    {
        while (true)
        {
            if (Time.timeScale > 0)
            {
                GameObject bullet = Instantiate(bulletobject, transform.position, transform.rotation);
                bullet.GetComponent<bulletManager>().bulletDamagetoBox = damagetobox;
                bullet.GetComponent<bulletManager>().bulletDamagetoGate = damagetogate;

                bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;

                bulletScript = bullet.GetComponent<bulletManager>();
                bulletScript.bulletDamagetoBox = damagetobox;

                Destroy(bullet, bulletLifeTime);
            }

            yield return new WaitForSeconds(fireRate);
        }

    }
}