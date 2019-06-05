using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ConfigurableJoint))]
[RequireComponent(typeof(PlayerMotor))]
[RequireComponent(typeof(Camera))]
public class PlayerController : MonoBehaviour
{
    public AudioSource jetPack;
    public AudioSource collisionSound1;
    public AudioSource collisionSound2;
    public AudioSource collisionSound3;

    public IGotBools bs;

    //Create Variables
    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private float lookSens = 3f;

    [SerializeField]
    private float jetpackForce = 1000f;

    [SerializeField]
    private bool isFlying = false;

    private PlayerMotor motor;
    private ConfigurableJoint joint;

    private Camera camP;


    // Use this for initialization
    void Start()
    {
        Screen.lockCursor = true;

        motor = GetComponent<PlayerMotor>();
        joint = GetComponent<ConfigurableJoint>();


    }

    //Play collision sound when hit with objects
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "floor")
        {
            collisionSound1.Play();
            Debug.Log("Collision floor");
        }
        if (collision.gameObject.tag == "wall")
        {
            collisionSound2.Play();
            Debug.Log("Collision wall");
        }
        if (collision.gameObject.tag == "hurt")
        {
            collisionSound3.Play();
            Debug.Log("hurt");
        }
    }

    // Update is called once per frame
    void Update()
    {

        //Calc Movement Velocity
        float _xMov = Input.GetAxisRaw("Horizontal");
        float _zMov = Input.GetAxisRaw("Vertical");

        Vector3 _moveHorizontal = transform.right * _xMov;
        Vector3 _moveVertical = transform.forward * _zMov;

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * speed;

        //Apply Movement Velocity
        motor.Move(_velocity);

        //Calculate Rotation
        float _yRot = Input.GetAxisRaw("Mouse X");

        Vector3 _rotation = new Vector3(0f, _yRot, 0f) * lookSens;

        //Apply Rotation
        motor.Rotate(_rotation);

        //Calculate Camera Rotation
        float _xRot = Input.GetAxisRaw("Mouse Y");

        float _cameraRotationX = _xRot * lookSens;

        //Apply Camera Rotation
        motor.RotateCamera(_cameraRotationX);


        //Unlock Cursor
        if (Input.GetButton("Escape"))
        {
            Screen.lockCursor = false;
        }


        //Adding a jumping jet pack
        Vector3 _jetpackForce = Vector3.zero;

        //Play jetpack sound when press the jump button
        if (Input.GetButtonDown("Jump") && (bs.hasJetpack))
        {
            jetPack.Play();
            Debug.Log("Play JetPack Sound");
        }

        //TEST MODE===========================================
        if (Input.GetButtonDown("Jump") &&(bs.hasJetpack == true))
        //if(Input.GetButtonDown("Jump"))
        {

            _jetpackForce = Vector3.up * jetpackForce;
            Debug.Log("jumping");

            //Apply Jetpack Force
            motor.ApplyJetpackForce(_jetpackForce);
            Debug.Log("trying to jump with jetpack" + _jetpackForce);

            speed = 1f;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            _jetpackForce = Vector3.zero;
            motor.ApplyGravity();

            speed = 1f;

            //Stop playing jetpack SFX when release the jump button
            jetPack.Stop();
            Debug.Log("Stop playing JetPack Sound");
            Debug.Log("trying to come down with gravity" + _jetpackForce);
        }

        speed = 5f;

        //Adding A JUMP AND TEST CODE

        if (Input.GetButtonDown("Jump") && (bs.hasJetpack == false))
        {
            Debug.Log("hit jump button");
            
            //    isFlying = true;
            //float consy = 10f;

            //Vector3 _posone = transform.right * consy;
            //Vector3 _postwo = transform.forward * consy;

            //Vector3 _velo = (_posone + _postwo).normalized * speed;
            motor.AddJumpForce(_rotation);

            //rb.MovePosition(transform.position + velocity * Time.fixedDeltaTime)

            //Apply Movement Velocity
            //motor.Move(_velocity);



            //motor.ChangeViewToFly();

            //add constant forward motion

            //motor.Move(new Vector3(5, 0, 0));

            //    Debug.Log("changed to flying view");
            //}
            //else
            //{
            //    isFlying = false;
            //    motor.ChangeViewToRun();
            //    Debug.Log("changed to running view");
            //}




            // The center of the arc
            //Vector3 center = (cam.position + sunset.position) * 0.5F;

            //// move the center a bit downwards to make the arc vertical
            //center -= new Vector3(0, 1, 0);

            //// Interpolate over the arc relative to center
            //Vector3 riseRelCenter = sunrise.position - center;
            //Vector3 setRelCenter = sunset.position - center;

            //// The fraction of the animation that has happened so far is
            //// equal to the elapsed time divided by the desired time for
            //// the total journey.
            //float fracComplete = (Time.time - startTime) / journeyTime;

            //transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, fracComplete);
            //transform.position += center;

        }

        if (Input.GetButtonDown("Fly"))
        {
            motor.ChangeViewToFly();
        }




    }

}