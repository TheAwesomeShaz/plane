using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArcadeJets;

public class FeedBackTrigger : MonoBehaviour
{
    GameController gc;

    // Start is called before the first frame update
    void Start()
    {
        gc = FindObjectOfType<GameController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<StickInput>())
        {
            gc.FeedbackRing();
            Debug.Log(other.transform.name);
        }
    }
}
