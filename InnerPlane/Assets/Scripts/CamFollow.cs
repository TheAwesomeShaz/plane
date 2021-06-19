using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Vector3 offset;
    public GameObject FPSTarget;
    public GameObject target;
    public Transform topDownCamPos;
    public float smoothSpeed = 0.125f;
    public float rotationSpeed = 1f;

    [SerializeField] Vector3 camRotTD;

    

    [SerializeField] bool fps = false; // i.e camera is in fps mode

    GameController gc;

    [Tooltip ("Wait Time for  camToFPS")]
    [SerializeField] float camWaitTime = 2.5f;

    [Tooltip("Wait Time for plane to enter flight mode i.e touch causes plane to move forward")]
    [SerializeField] float flyingWaitTime = 0.6f;

    private void Start()
    {
        gc = FindObjectOfType<GameController>();
    }

    private void Update()
    {
        if (fps && !gc.exploded && !gc.hasWon)
        {
            CamShiftToFPS();
            StartCoroutine(FlyingTrueAfterTime(flyingWaitTime)); 
        }

        if (gc.isFlying && !gc.hasWon)
        {
            transform.position = target.transform.position + offset;
            //transform.rotation = transform.parent.rotation;

            //The line below was for third person camera
            //transform.LookAt(target.transform.position);

            //this is for first person camera
            transform.LookAt(FPSTarget.transform.position);
        }



        if (gc.hasWon)
        {
            CamShiftToTopDown();
            
            gc.DisableJoystick();
        }
    }

    IEnumerator FlyingTrueAfterTime(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        gc.isFlying = true;
        gc.EnableJoystick();
        
    }

    public void FlightCam()
    {
        Dood[] doods = FindObjectsOfType<Dood>();

        gc.EnableJoystick();

        foreach ( var dood in doods)
        {
            dood.move = false;

            dood.ResetDestination();
        }
        
        StartCoroutine(MoveCamAfterTime(camWaitTime));
    }

    public IEnumerator MoveCamAfterTime(float waitTime)
    {

        if (!gc.exploded)
        {
            gc.FeedBack();

            yield return new WaitForSeconds(waitTime);
            fps = true;
        }
    }

   

    private void CamShiftToFPS()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 90f, 0f),
           Time.deltaTime * rotationSpeed);

        Vector3 desiredPos = target.transform.position + offset;
        Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPos;
        //transform.LookAt(target.transform, Vector3.forward);
       
    }

    private void CamShiftToTopDown()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(camRotTD),
           Time.deltaTime * rotationSpeed);

        Vector3 desiredPos = topDownCamPos.position + offset;
        Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPos;
    }

}
