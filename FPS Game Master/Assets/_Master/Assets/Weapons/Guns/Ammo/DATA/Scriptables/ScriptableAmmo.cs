using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableAmmo : ScriptableObject
{
    public GameObject cartidgePrefab;
    public int maxClipSize;
    [Range(1,10)]
    public int baseBulletDamage;
}
