using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponInputHandler : MonoBehaviour
{
    private IWeapon weapon;
    private Camera playerCamera;

    private FireWeaponController fireWeapon;
    private void Start()
    {
        weapon = GetComponentInChildren<IWeapon>();
        playerCamera = GetComponentInChildren<Camera>();
        fireWeapon = GetComponentInChildren<FireWeaponController>();
        if (fireWeapon != null)
        {
            PlayerInfo.PlayerAmmo = fireWeapon.CurrentAmmoLeft;
            PlayerInfo.PlayerMaxAmmo = fireWeapon.MaxAmmo;
        }
    }

    private void Update()
    {
        if (Input.GetButton("Fire1") && weapon != null)
        {
            weapon.Use(playerCamera.transform.position);         
        }
        if (Input.GetButtonDown("Reload"))
        {

            var fireWeapon = GetComponentInChildren<FireWeaponController>();
            if (fireWeapon != null)
            {
                fireWeapon.ReloadWeaponAtCommand();               
            }
        }

        PlayerInfo.PlayerAmmo = fireWeapon.CurrentAmmoLeft;
        PlayerInfo.PlayerMaxAmmo = fireWeapon.MaxAmmo;
    }
}
