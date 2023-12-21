using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lose : MonoBehaviour
{
    public TMPro.TextMeshPro msg;
    public int TimesDead = 0;

    private void Awake()
    {
        Trap.OnTrap+=Trap_OnTrap;
  
    
    }

    private void Trap_OnTrap(trapType traptype)
    {
        TimesDead++;
        msg.text = $"Perdiste bro {TimesDead}";
    }
}
