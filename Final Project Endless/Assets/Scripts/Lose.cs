using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Lose : MonoBehaviour
{
    public GameObject LooseScreen;
    public Generator generator;
    public TextMeshProUGUI texto;
    public TextMeshProUGUI ButtonText;
    //public int TimesDead = 0;


    public TMPro.TextMeshProUGUI scoreText;

    private void Awake()
    {
        Trap.OnTrap += Trap_OnTrap;
    
    
    }

    private void Trap_OnTrap(trapType trapType)
    {
        //TimesDead++;
        //msg.text = $"Perdiste bro {TimesDead}";
        scoreText.text = Scores.Instance.current.ToString();
        Scores.Instance.Save();
        LooseScreen.SetActive(true);
        texto.text ="Peldite";
        ButtonText.text = "Reiniciar";
        generator.enabled = false;

    }
}
