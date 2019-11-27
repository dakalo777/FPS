using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickablesHandler : MonoBehaviour
{
    IPickable currentItemPicked;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Item"))
        {
            var item = other.transform.GetComponent<IPickable>();
            item.PickUp();
            currentItemPicked = item;
            var ammo = other.transform.GetComponent<Ammo>();
            if (ammo != null)
            {
                var fireWeapon = GetComponentInChildren<FireWeaponController>();
                if (fireWeapon!= null)
                {
                    fireWeapon.RefillAmmo(ammo._Ammo, ammo.ClipCount);                    
                }
            }

        }
    }
}
