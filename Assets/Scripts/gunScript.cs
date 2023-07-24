using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public int gunLevel;
    [SerializeField] GameObject bulletobject;
    [SerializeField] float bulletSpeed;
    public float bulletLifeTime=1; // menzil
    public float fireRate=1; // at�� s�kl���
    public int damageetobox,damagetogate;
    public float minFireRate;
    public float minBulletLifeTime;
    public static bulletManager bulletScript;
    //public static GunScript instance;

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
                GameObject bullet = Instantiate(bulletobject, transform.position, Quaternion.identity);

                bullet.GetComponent<Rigidbody>().velocity = Vector3.forward * bulletSpeed;

                bullet.transform.Rotate(0, 90, 0);

                bulletScript = bullet.GetComponent<bulletManager>();

                bulletScript.bulletDamagetoBox = damageetobox;

                Destroy(bullet, bulletLifeTime);
            }

            yield return new WaitForSeconds(fireRate);
        }

    }
}