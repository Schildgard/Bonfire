using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IElectrilizable 
{

    public void Electrify(Material _material);

    public void Electrify(Vector3 _position);
    
}
