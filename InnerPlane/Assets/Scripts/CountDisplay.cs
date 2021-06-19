using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountDisplay : MonoBehaviour
{
    GameController gc;
    public TextMeshProUGUI counterText;
    [SerializeField] Vector3 offset;
    public Animator textAnim;

    float maxTimer = 5f;// time to go back to normal text size 
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        gc = FindObjectOfType<GameController>();
        timer = maxTimer;
        textAnim = counterText.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        counterText.text = gc.currentPassengers.ToString() + "/" + gc.maxPassengers.ToString();
        
        if (!gc.exploded)
        {
            transform.position = FindObjectOfType<Plane>().transform.position + offset;
        }
    }

    public void TextPop()
    {
        textAnim.SetTrigger("pop");
    }

}
