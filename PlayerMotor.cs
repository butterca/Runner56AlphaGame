﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour {


    [SerializeField]
    private Camera cam;

    [SerializeField]
    private bool isJumping;

    public Transform camPos1;

    public Transform camPos2;

    public float zoomTime = 5.0f;

    public float startTime;

    public bool inFlight = false;

    public GameObject characterBody;
   
   

    public CapsuleCollider characterCollider;




    //[SerializeField]
    //private Camera cam1;

    //[SerializeField]
    //private Camera cam2;


    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private float cameraRotationX = 0f;
    private float currentCameraRotationX = 0f;
    private float check = 1f;

    private Vector3 jetpackForce = Vector3.zero;


    private Vector3 gravityForce = new Vector3 (0f, -1000f, 0f);


    [SerializeField]
    private float cameraRotationLimit = 85;

    private Rigidbody rb;





    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
    }


    //Set Velocity
    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    //Set Rotation
    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    //Set Camera Rotation
    public void RotateCamera(float _cameraRotationX)
    {
        cameraRotationX = _cameraRotationX;


    }


    // Change from 1st to 3rd Person
    public void ChangeViewToFly()
    {


        Vector3 center = (camPos1.position + camPos2.position) * 0.5f;

        center += new Vector3(0, 1, 0);

        // Interpolate over the arc relative to center
        Vector3 riseRelCenter = camPos1.position - center;
        Vector3 setRelCenter = camPos2.position - center;

        // The fraction of the animation that has happened so far is
        // equal to the elapsed time divided by the desired time for
        // the total journey.
        float fracComplete = (Time.time -startTime) / zoomTime;
        if (inFlight == false)
        {
            cam.transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, fracComplete);
            cam.transform.position += center;



            //transform.Rotate(90, 0, 0);
            //characterBody.transform.rotation = Quaternion.Euler(new Vector3(characterBody.transform.rotation.x + 90, transform.rotation.y, transform.rotation.z - 180));

            //characterBody.transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x + 90, transform.rotation.y, transform.rotation.z));
            //characterBody.transform.rotation = 
            //rotating character model

            //Putting the character in constant motion
            //rb.MovePosition(transform.position + (new Vector3(10,0,0)));


            //Somehow need to only add a constant velocity to the direction that the character is looking
            //Vector3 velo = new Vector3(0, 0, - 10);
            //rb.MovePosition(transform.position + velo * Time.fixedDeltaTime);
            //rb.velocity = new Vector3(0, 0, -10);
            //rb.AddRelativeForce(Vector3.forward * 10);

            //rb.velocity = (rb.velocity.normalized) * 10;

            //transform.position += Vector3.forward * Time.deltaTime * 10f;

            //rb.MovePosition(transform.position + velo * Time.fixedDeltaTime);

            Debug.Log("trying to put player in constant motion without gravity");
            inFlight = true;
        }
        else
        {
            cam.transform.position = camPos1.position;


            //characterBody.transform.rotation = Quaternion.Euler(new Vector3(characterBody.transform.rotation.x, characterBody.transform.rotation.y, characterBody.transform.rotation.z));

            //characterBody.transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y,transform.rotation.z));
            //transform.Rotate(-90, 0, 0);

            //trying to remove constant velo
            //rb.velocity = new Vector3(0, 0, 0);
            //rb.MovePosition(transform.position + velocity * Time.fixedDeltaTime);

            inFlight = false;
        }
        //cam.transform


        //putting character into flying pose
        //rb.transform.SetPositionAndRotation(new Vector3(rb.position.x, rb.position.y , rb.position.z), Quaternion.Euler(new Vector3(rb.rotation.x - 90, rb.rotation.y, rb.rotation.z)));
        //putting camera into 3rd person

        //cam.transform.SetPositionAndRotation(new Vector3(rb.position.x, rb.position.y + 1, rb.position.z + 1), Quaternion.Euler(rotation));

        //cam.transform.SetPositionAndRotation(new Vector3(rb.position.x, rb.position.y - 1, rb.position.z - 1), Quaternion.Euler(rotation));
        //check = 1f;
        //Debug.Log("change back to normal");
    }


    public void ChangeViewToRun()
    {
        //putting character into flying pose
        //rb.transform.SetPositionAndRotation(new Vector3(rb.position.x, rb.position.y , rb.position.z), Quaternion.Euler(new Vector3(rb.rotation.x - 90, rb.rotation.y, rb.rotation.z)));
        //putting camera into 3rd person

        //cam.transform.SetPositionAndRotation(new Vector3(rb.position.x, rb.position.y, rb.position.z), Quaternion.Euler(rotation));

        //cam.transform.SetPositionAndRotation(new Vector3(rb.position.x, rb.position.y - 1, rb.position.z - 1), Quaternion.Euler(rotation));
        //check = 1f;
        //Debug.Log("change back to normal");
    }

    public void AddJumpForce(Vector3 _rotation)
    {
        //const float velo
        float veli = 10f;
        //const Vector 3 velo
        Debug.Log("current rotation " + _rotation);
        Vector3 velic = new Vector3(0,200f,0);
        if (!isJumping)
        {
            isJumping = true;
            if ((rb.velocity.y == 0) || (rb.velocity.y < 0))
            {
                rb.AddForce(velic, ForceMode.Acceleration);

            }
            Invoke("resetIsJumping", 1.5f);
        }


        //Quaternion autoRotate = Quaternion.LookRotation(relativePos, Vector3.up);

    }

    public void resetIsJumping()
    {
        isJumping = false;
    }


    // Update is called once per frame
    void Update () {
        PerformMovement();
        PerformRotation();
	}


    //Moves player with velocity
    void PerformMovement()
    {
        if(velocity != Vector3.zero)
        {
            //Debug.Log("velociy = " + velocity);
            rb.MovePosition(transform.position + velocity * Time.fixedDeltaTime);
        }

        //adds jetpack force
        if(jetpackForce != Vector3.zero)
        {
            rb.AddForce(jetpackForce * Time.fixedDeltaTime, ForceMode.Acceleration);
        }
    }


    //Rotates player with the roation and camera rotation
    void PerformRotation()
    {
        rb.MoveRotation(transform.rotation * Quaternion.Euler(rotation));
        if(cam != null)
        {
            //Set Rotation and clamp it
            currentCameraRotationX -= cameraRotationX;
            currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

            //Apply rotation to transform of camera
            cam.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0, 0);
        }
    }

    //Apply Jetpack Force
    public void ApplyJetpackForce(Vector3 _jetpackForce)
    {
        jetpackForce = _jetpackForce;
    }

    public void ApplyGravity()
    {
        jetpackForce = new Vector3(0f, 200f, 0f);
        //rb.AddForce(gravityForce * Time.deltaTime, ForceMode.Acceleration);
    }

}