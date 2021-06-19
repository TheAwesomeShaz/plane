using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArcadeJets;

public class Stopper : MonoBehaviour
{
    GameController gc;

    private void Start()
    {
        gc = FindObjectOfType<GameController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<StickInput>())
        {
            other.GetComponentInParent<StickInput>().isSlowing = true;
            gc.Win();
        }
    }

}
