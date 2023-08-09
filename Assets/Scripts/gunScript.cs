using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    [SerializeField] GameObject bulletobject;
    [SerializeField] float bulletSpeed;
    public float bulletLifeTime=1; // menzil
    public float fireRate=1; // at�� s�kl���
    public int damageetobox,damagetogate;
    public float minFireRate;
    public float minBulletLifeTime;
    public static bulletManager bulletScript;
    public float bulletRotStabilizer;

    private void Start()
    {
        StartAtesEt();
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
                bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;

                bulletScript = bullet.GetComponent<bulletManager>();
                bulletScript.bulletDamagetoBox = damageetobox;

                Destroy(bullet, bulletLifeTime);
            }

            yield return new WaitForSeconds(fireRate);
        }

    }
}