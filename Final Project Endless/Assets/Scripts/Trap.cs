using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public static event TrapDelegate OnTrap;
    public trapType trapType=trapType.impact;


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            Debug.Log("Mueito");
        }
    }
    private void OnDestroy()
    {
        OnTrap= null;

    }

}

public delegate void TrapDelegate(trapType trapType);

public enum trapType { 
impact,water
}

