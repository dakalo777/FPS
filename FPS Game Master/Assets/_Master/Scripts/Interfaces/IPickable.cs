using UnityEngine;
public interface IPickable 
{
     void PickUp();
     void Ignore();
     void Discard(Transform discardLocation);


}
