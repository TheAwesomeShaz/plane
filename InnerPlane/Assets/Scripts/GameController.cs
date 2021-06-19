using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class GameController : MonoBehaviour
{
    Dood[] doods;
    Plane plane;

    int currentSceneIndex;

    public bool isFlying;
    public bool exploded;
    public bool passengerChanged;
    public bool hasWon;
    

    public int maxPassengers = 20;
    public int currentPassengers;

    [Tooltip("the wait before activating retry canvas")]
    public float waitTime = 2f; // the wait before activating retry canvas

    public GameObject retryCanvas;
    public GameObject winCanvas;
    public GameObject feedBackPanel;
    public GameObject ringFeedback;
    public GameObject confetti;
    public GameObject joystick;
    public GameObject doodPrefab;
    public Transform doodSpawnPoint;
    public Transform doodOutPoint;

    [Tooltip("non joystick panel")]
    public GameObject touchPanel;

    String[] feedbacks = { "AWESOME!", "NICE!", "WOW!", "PERFECT!", "AWESOME!"};

    public TextMeshProUGUI feedback;

    // Start is called before the first frame update
    void Start()
    {
        winCanvas.SetActive(false);
        ringFeedback.SetActive(false);
        touchPanel.SetActive(true);
        joystick.SetActive(false);
        feedBackPanel.SetActive(false);
        retryCanvas.SetActive(false);

        isFlying = false;
        exploded = false;
        hasWon = false;

        plane = FindObjectOfType<Plane>();
        doods = FindObjectsOfType<Dood>();

        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void FeedBack()
    {
        feedBackPanel.SetActive(true);
        DeactivateAfterTime(feedBackPanel, 2f);
        //feedback.gameObject.SetActive(true);
        GameObject fx = Instantiate(confetti, feedback.transform.position, Quaternion.identity);
        Destroy(fx, 2f);
    }

    public void FeedbackRing()
    {
        touchPanel.SetActive(true);
        ringFeedback.SetActive(true);
        ringFeedback.gameObject.GetComponent<TextMeshProUGUI>().text = feedbacks[UnityEngine.Random.Range(0, 4)];
        ConfettiFX();
        StartCoroutine(DeactivateAfterTime(ringFeedback, 0.5f));
        //StartCoroutine(DeactivateAfterTime(touchPanel, 0.5f));
        //feedback.gameObject.SetActive(true);
       
    }

    // Calls respective functions for passengers before takeoff and after landing
    public void OnCanvasHold()
    {
        if (!hasWon)
        {
            GotoPlane();
        }
        else if (hasWon)
        {
            GetOutOfPlane();
        }
        
    }

    //Passengers get out of plane
    private void GetOutOfPlane()
    {
        for (int i = 0; i < currentPassengers; i++)
        {
            StartCoroutine(SpawnAfterIntervals(0.2f));
        }
        
    }

    

    private IEnumerator SpawnAfterIntervals(float timeInterval)
    {
        yield return new WaitForSeconds(timeInterval);
        GameObject dude = Instantiate(doodPrefab, doodSpawnPoint.position, Quaternion.identity) as GameObject;
        dude.GetComponent<Dood>().GoOutofPlane(doodOutPoint);
    }

    //Passengers move towards the plane
    public void GotoPlane()
    {
        if (!isFlying && !exploded)
        {
            foreach (var dood in doods)
            {
                dood.PlaneGo();
            }
            
        }
    } 

    public void AddPassenger()
    {
        currentPassengers++;
        FindObjectOfType<CountDisplay>().TextPop();

        if (currentPassengers > maxPassengers)
        {
            feedback.gameObject.SetActive(false);
            plane.Explode();
            exploded = true;
            DisableJoystick();
            StartCoroutine(SetActiveAfterTime(retryCanvas, waitTime));
            
        }
    }

    IEnumerator SetActiveAfterTime(GameObject Canvas, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Canvas.SetActive(true);
    }
    IEnumerator DeactivateAfterTime(GameObject Canvas, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Canvas.SetActive(false);
    }

    public void EnableJoystick()
    {
        joystick.SetActive(true);
       // touchPanel.SetActive(false);
    }

    public void DisableJoystick()
    {
        joystick.SetActive(false);
    }

    public void Retry()
    {
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void Win()
    {
       
        DisableJoystick();
        if (currentPassengers == 0)
        {
            StartCoroutine(SetWinActiveAfterTime(1f));
        }
    }

    IEnumerator SetWinActiveAfterTime(float waitTime)
    {
        hasWon = true;
        yield return new WaitForSeconds(waitTime);
        

        if (currentPassengers == 0)
        {
            winCanvas.SetActive(true);

            ConfettiFX(10);
            ConfettiFX(20);
            ConfettiFX(25);
            ConfettiFX(15);
            ConfettiFX(12);
            ConfettiFX(22);
            ConfettiFX(28);
            ConfettiFX(30);
            ConfettiFX(32);
        }

    }

    private void ConfettiFX(float xValue = 70)
    {
        GameObject fx = Instantiate(confetti, Camera.main.transform.position + new Vector3(xValue, 0f, 0f), Quaternion.Euler(0f, 90f, 0f));
        fx.transform.localScale = new Vector3(5f, 5f, 5f);
        fx.layer = 5;
        Destroy(fx, 1.2f);
    }
}
