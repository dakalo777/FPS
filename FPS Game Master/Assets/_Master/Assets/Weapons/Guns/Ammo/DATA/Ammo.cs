using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour, IPickable
{
    [SerializeField] private ScriptableAmmo ammo;
    public ScriptableAmmo _Ammo
    {
        get
        {
            return ammo;
        }
    }
    [Range(1, 10)]
    [SerializeField] private int clipCount;
    public int ClipCount
    {
        get { return clipCount; }
    }

    public void Discard(Transform discardLocation)
    {
        this.gameObject.SetActive(true);
        this.transform.position = discardLocation.position;
    }

    public void Ignore()
    {
        throw new System.NotImplementedException();
    }

    public void PickUp()
    {
        this.gameObject.SetActive(false);
    }
}
