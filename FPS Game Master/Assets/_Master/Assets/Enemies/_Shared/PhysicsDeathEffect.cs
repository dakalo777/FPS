using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsDeathEffect : MonoBehaviour, IDie
{
    [SerializeField] private GameObject activePart;
    [SerializeField] private GameObject deathEffectGameObject;
    [SerializeField] private GameObject particleEffect;

    private int intensity;

    private bool isDeath;
    public void Die(int intensity)
    {
        this.intensity = intensity;
        if (!isDeath)
        {
            ActivateDeathEffect();
        }
    }

    public void Revive()
    {
        if (isDeath)
        {
            RebuildElement();
        }
    }

    private void ActivateDeathEffect()
    {
        isDeath = true;
        activePart.SetActive(false);
        deathEffectGameObject.SetActive(true);
        var particle = Instantiate(particleEffect, this.transform.position, Quaternion.identity);
        Destroy(particle, 3f);
        var child = deathEffectGameObject.transform.childCount;
        int[] children = new int[child];
        for (int i = 0; i < children.Length; i++)
        {
            var currentChild = deathEffectGameObject.transform.GetChild(i);
            var currentRB = currentChild.GetComponent<Rigidbody>();
            if (currentRB != null)
            {
                currentRB.AddExplosionForce(intensity * 3, this.transform.position, intensity);                    
            }
        }
    }

    private void RebuildElement()
    {
        activePart.SetActive(true);
        deathEffectGameObject.SetActive(false);
        var child = deathEffectGameObject.transform.childCount;
        int[] children = new int[child];
        for (int i = 0; i < children.Length; i++)
        {
            var currentChild = deathEffectGameObject.transform.GetChild(i);
            currentChild.transform.position = Vector3.zero;
            currentChild.transform.rotation = Quaternion.identity;
        }
        isDeath = false;
    }
}
