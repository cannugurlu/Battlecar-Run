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
    public float bulletRotStabilizer;
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
                GameObject bullet = Instantiate(bulletobject, transform.position, Quaternion.Euler(0, transform.parent.eulerAngles.y -90, 0));

                bullet.GetComponent<Rigidbody>().velocity = new Vector3(playersScript.instance.touchDeltaX,0,1)* bulletSpeed;

                bullet.transform.Rotate(0, 90, 0);

                bulletScript = bullet.GetComponent<bulletManager>();

                bulletScript.bulletDamagetoBox = damageetobox;

                Destroy(bullet, bulletLifeTime);
            }

            yield return new WaitForSeconds(fireRate);
        }

    }
}