using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Aeroplane;


public class Plane : MonoBehaviour
{
    Animator anim;
   
    GameController gc;
    Rigidbody rb;
    public FloatingJoystick joystick;


    float colorLerpSpeed = 1f;
    public Color startColor;
    public Color endColor;
    float startTime;


    public GameObject explodeVFX;
    public GameObject smokeVFX;

    //movement related vars
    private CharacterController controller;
    private AeroplaneController aeroplaneController;

    bool brakes;
    //bool lerpColors;

    float baseSpeed = 10f;
    float rotSpeedX = 3f;
    float rotSpeedY = 1.5f;

    Dictionary<int, Vector2> activeTouches = new Dictionary<int, Vector2>();


    private void Start()
    {

        //lerpColors = false;
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        gc = FindObjectOfType<GameController>();
        controller = GetComponent<CharacterController>();

        //aeroplaneController = FindObjectOfType<AeroplaneController>();

        // disable mesh collider let the box collider(which is triggers) stay active

    }

    private void FixedUpdate()
    {
        if (gc.isFlying)
        {
            
            anim.enabled = false;
            return;
            //aeroplaneController.Move(joystick.Horizontal + Input.GetAxis("Horizontal"), joystick.Vertical + Input.GetAxis("Vertical"),
            //    0, 0.5f, false);

        }
    }

    private void Update()
    {
        if (gc.isFlying)
        {
            rb.isKinematic = false;
            
            //rb.AddForce(Vector3.right*10f, ForceMode.Force);
            //Debug.Log("AA beleev I can flaiii, I beleve I can philakitai");
        }
        

        //if (lerpColors)
        //{
        //    ColorLerp();
        //}
      
        /* bad code
            //        //forward velocity
            //        Vector3 moveVector = transform.forward * baseSpeed;

            //        //playerInput
            //        Vector3 inputs = GetPlayerInput();

            //        //get deltaDir
            //        Vector3 yaw = inputs.x * transform.right * rotSpeedX * Time.deltaTime;
            //        Vector3 pitch = inputs.y * transform.up * rotSpeedY * Time.deltaTime;
            //        Vector3 dir = yaw + pitch;

            //        //make sure we prevent player from doing loopdeloops
            //        float maxX = Quaternion.LookRotation(moveVector + dir).eulerAngles.x;

            //        // if hes not going too far up/down
            //        if(maxX<90 && maxX>70 || maxX>270 && maxX < 290)
            //        {
            //            // too far dont do anything
            //        }
            //        else
            //        {
            //            //add direction to current movevector
            //            moveVector += dir;

            //            //have the player face where hes going
            //            transform.rotation = Quaternion.LookRotation(moveVector);
            //        }

            //        // move player
            //        controller.Move(moveVector * Time.deltaTime);

            //    }
            //}

            //private Vector3 GetPlayerInput()
            //{
            //    //read all touches
            //    Vector3 r = Vector3.zero;
            //    foreach (Touch touch in Input.touches)
            //    {
            //        // if we just started touch
            //        if(touch.phase == TouchPhase.Began)
            //        {
            //            activeTouches.Add(touch.fingerId, touch.position);
            //        }

            //        //if we remove our finger
            //        else if(touch.phase == TouchPhase.Ended)
            //        {
            //            if (activeTouches.ContainsKey(touch.fingerId))
            //            {
            //                activeTouches.Remove(touch.fingerId);
            //            }
            //        }

            //        //finger is moving or holding then use delta
            //        else
            //        {
            //            float mag = 0;
            //            r = (touch.position - activeTouches[touch.fingerId]);
            //            mag = r.magnitude / 360;
            //            r = r.normalized * mag;
            //        }

            //    }
            //    return r;
            //} */
    }
        void ColorLerp()
        {
            float t = (Time.time - startTime) * colorLerpSpeed;
            GetComponent<Renderer>().material.color = Color.Lerp(startColor, endColor, t);
        }

        public void Explode()
        {
            if (!gc.exploded)
            {
                anim.SetTrigger("Explode");
                gc.exploded = true;
            }
        }

        public void ExplodeFX()
        {
            GameObject explod = Instantiate(explodeVFX, transform.position, Quaternion.identity) as GameObject;
            GameObject smoke = Instantiate(smokeVFX, transform.position, Quaternion.identity) as GameObject;
            //Destroy(smoke, 2f);
        } 
}
