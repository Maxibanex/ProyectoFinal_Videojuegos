using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public AudioClip clip;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Scores.Instance.current.Gems++;
            AudioHolder.Instance.Play(clip);
            gameObject.SetActive(false);
        }
    }
}
