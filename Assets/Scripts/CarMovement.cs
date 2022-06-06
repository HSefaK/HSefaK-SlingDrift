using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarMovement : MonoBehaviour
{
    public static Action<GameObject> act;

    public Text scoreText;

    public int roadCounter = 1;

    public GameObject GameOver;

    private int score = 0;
    
    private GameObject rightNode;
    private GameObject leftNode;
    private GameObject road;

   
    [SerializeField]
    Rigidbody2D carRigidBody;



    void Start()
    {
    }

    void Update()
    {
        rightNode = GameObject.FindGameObjectWithTag("RightNode");
        leftNode = GameObject.FindGameObjectWithTag("LeftRode");
        road = GameObject.FindGameObjectWithTag("Road");
        DriftFunction();
        scoreText.text = score.ToString();
    }

    private void DriftFunction()
    {

        Vector2 speed = transform.up;
        carRigidBody.velocity = speed * 2;

        float direction = Vector2.Dot(carRigidBody.velocity, carRigidBody.GetRelativeVector(Vector2.up));




        float driftForce = Vector2.Dot(carRigidBody.velocity, carRigidBody.GetRelativeVector(Vector2.left)) * 2.0f;

        Vector2 relativeForce = Vector2.right * driftForce;

        carRigidBody.AddForce(carRigidBody.GetRelativeVector(relativeForce));


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "RightNode" || collision.tag == "LeftRode")
        {
            act?.Invoke(collision.gameObject);
        }


        if (collision.tag == "Road")
        {

            GameOver.SetActive(true);
            Time.timeScale = 0.0f;

        }



    }
    

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "RightNode")
        {
            score += 1;
            roadCounter += 1;
            carRigidBody.rotation = -90f*score;

        }

        if (collision.tag == "LeftRode")
        {
            score += 1;
            roadCounter += 1;
            carRigidBody.rotation = -90f * score;

        }
    }
    void OnTriggerStay2D(Collider2D other)
    {

        if (other.tag == "RightNode")
        {
            if (Input.GetMouseButton(0))
            {

                carRigidBody.rotation -= 4f  * (carRigidBody.velocity.magnitude / 3);

            }
        }

        if (other.tag == "LeftRode")
        {
            if (Input.GetMouseButton(0))
            {
                carRigidBody.rotation += 4f  * (carRigidBody.velocity.magnitude / 3);


            }
        }
    }
}
