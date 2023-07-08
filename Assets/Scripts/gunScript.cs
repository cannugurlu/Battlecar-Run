using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    [SerializeField] GameObject bulletobject;
    [SerializeField] float bulletSpeed;
    public float bulletLifeTime;
    public float fireSpeed;
    public float power;
    public float minFireSpeed;
    public float minBulletLifeTime;
    private void Start()
    {
        StartAtesEt();
    }

    public void StartAtesEt()
    {
        StartCoroutine(AtesEt());
    }
    private IEnumerator AtesEt()
    {
        while (true)
        {
            GameObject bullet = Instantiate(bulletobject, transform.position, Quaternion.identity);

            bullet.GetComponent<Rigidbody>().velocity = Vector3.forward * bulletSpeed;

            bullet.transform.Rotate(0, 90, 0);

            Destroy(bullet, bulletLifeTime);

            yield return new WaitForSeconds(fireSpeed);
        }

    }
}