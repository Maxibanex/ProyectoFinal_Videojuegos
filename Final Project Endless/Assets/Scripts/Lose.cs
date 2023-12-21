using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lose : MonoBehaviour
{
    public GameObject LooseScreen;
    public Generator generator;
    //public int TimesDead = 0;

    private void Awake()
    {
        Trap.OnTrap += Trap_OnTrap;
  
    
    }

    private void Trap_OnTrap(trapType trapType)
    {
        //TimesDead++;
        //msg.text = $"Perdiste bro {TimesDead}";
        LooseScreen.SetActive(true);
        generator.enabled = false;
    }
}
