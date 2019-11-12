using UnityEngine;
public interface IWeapon 
{
    void Use(Vector3 aimPosition);
    void Take();
    void Drop();   
    
}
