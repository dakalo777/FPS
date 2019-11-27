using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthPack : MonoBehaviour, IPickable
{
    [SerializeField] private int healAmmount;
    [SerializeField] private bool isConsumed;

    public static event Action<int> OnHeal = delegate { };
    public void Discard(Transform discardLocation)
    {
        throw new System.NotImplementedException();
    }

    public void Ignore()
    {
        throw new System.NotImplementedException();
    }

    public void PickUp()
    {
        OnHeal(healAmmount);
        if (isConsumed)
        {
            this.gameObject.SetActive(false);
        }
    }
}

