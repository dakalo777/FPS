using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWeaponController : MonoBehaviour, IWeapon
{

    [SerializeField] private float fireRate;
    [SerializeField] private float reloadTime;
    [SerializeField] private float maxRange;
    [SerializeField] private LayerMask layerMask;
    private Vector3 aimPoint;
    private float nextTimeToFire;

    [SerializeField] private ParticleSystem muzzleEffect;
    [SerializeField] private GameObject impactEffect;

    [Range(1, 100)]
    [SerializeField] private int baseDamage;
    private int damageAmmount;

    [SerializeField] private ScriptableAmmo ammo;
    [SerializeField] private int maxClipCount;
    private int clipCount;
    private int currentAmmoLeft;

    private bool canFire;
    private void Start()
    {
        damageAmmount = baseDamage;
        currentAmmoLeft = ammo.maxClipSize;
        damageAmmount = baseDamage + ammo.baseBulletDamage;
        if (currentAmmoLeft > 0)
            canFire = true;

    }
    public void Drop()
    {
        throw new System.NotImplementedException();
    }

    public void Take()
    {
        throw new System.NotImplementedException();
    }

    public void Use(Vector3 aimPosition)
    {
        aimPoint = aimPosition;
        if (Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1 / fireRate;
            if (currentAmmoLeft > 0 && canFire)
            {
                Fire();
            }
            else
            {
                StartCoroutine(Reload());
            }
        }

    }

    private void Fire()
    {
        RaycastHit hit;
        muzzleEffect.Play();
        if (Physics.Raycast(aimPoint, this.transform.forward, out hit, maxRange, layerMask))
        {
            var impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impact, 3f);

            impact.transform.parent = hit.collider.transform;

            ITakeDamage damageInterface = hit.transform.gameObject.GetComponent<ITakeDamage>();
            if (damageInterface != null)
            {
                damageInterface.TakeDamage(damageAmmount);
            }
            var hitRB = hit.transform.GetComponent<Rigidbody>();
            if (hitRB != null)
            {
                hitRB.AddForce((hit.normal * -1) * 1.5f, ForceMode.Impulse);
            }
        }
        currentAmmoLeft--;
    }

    private IEnumerator Reload()
    {
        canFire = false;
        yield return new WaitForSeconds(reloadTime);
        if (clipCount > 0)
        {
            clipCount--;
            currentAmmoLeft = ammo.maxClipSize;
            canFire = true;
            print("Loaded");
            StopAllCoroutines();
        }
    }

    public void RefillAmmo(ScriptableAmmo ammo, int clipCount)
    {
        if (this.ammo == ammo && clipCount < maxClipCount)
        {
            this.clipCount += clipCount;
        }

        else
        {
            if (clipCount < maxClipCount)
            {
                this.clipCount = clipCount;
                currentAmmoLeft = ammo.maxClipSize;
                damageAmmount = baseDamage + ammo.baseBulletDamage;
            }

        }
    }

    public void ReloadWeaponAtCommand()
    {
        print("Reloading");
        if (canFire)
        {
            StopAllCoroutines();
            StartCoroutine(Reload());
        }
   
    }
}
