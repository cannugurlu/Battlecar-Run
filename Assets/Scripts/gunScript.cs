using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    [SerializeField] GameObject bulletobject;
    [SerializeField] float bulletSpeed;
    public float bulletLifeTime=1; // menzil
    public float fireRate=1; // atýþ sýklýðý
    public float power;
    public float minFireRate;
    public float minBulletLifeTime;
    //public static GunScript instance;

    private void Awake()
    {
        //instance = this;
    }
    private void Start()
    {
        StartAtesEt();
    }

    private void Update()
    {
        //print("fireRate " + fireRate);
        //print("mermi lifetime " + bulletLifeTime);
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

                Destroy(bullet, bulletLifeTime);
            }

            yield return new WaitForSeconds(fireRate);
        }

    }
}