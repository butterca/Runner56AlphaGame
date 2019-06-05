using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class jetpackActivate : MonoBehaviour
{

    public IGotBools bs;

    public GameObject Player;


    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Iam checking triger" + bs.hasJetpack);
        //GameObject player = GameObject.FindGameObjectsWithTag("nightflyer");
        if (other.gameObject == Player)
        {
            Debug.Log("adding jetpack to player");
            //Screen.lockCursor = false;
            //SceneManager.LoadScene(2);
            bs.hasJetpack = true;
            


            Debug.Log("adding jetpack to player" + bs.hasJetpack);


        }
    }
}