using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRender : MonoBehaviour
{
    public ParticleSystem particle;
    private LineRenderer lineRenderer;
    private float counter;
    private float dist;
    private GameObject activeNode;

    public Transform origin;
    public Transform destination;
    public bool hooked;

    private float lineDrawSpeed = 10;

    public TrailRenderer leftTireMark;
    public TrailRenderer rightTireMark;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, origin.position);
        leftTireMark.emitting = false;
        rightTireMark.emitting = false;
        particle.Stop();

    }
    void OnDisable()
    {
        CarMovement.act -= GetNode;
    }

    void OnEnable()
    {

        CarMovement.act += GetNode;
    }
    private void Update()
    {
        
        if (Input.GetMouseButton(0))
        {
            lineRendererComponents();
            createLine();
            leftTireMark.emitting = true;
            rightTireMark.emitting = true;
            hooked = true;
            if (!particle.isPlaying)
            {
                particle.Play();
            }
            
        }
        else if (Input.GetMouseButtonUp(0))
        {


            leftTireMark.emitting = false;
            rightTireMark.emitting = false;
            lineRenderer.SetPosition(1, origin.position);
            counter = 0;
            hooked = false;
            if (particle.isPlaying)
            {
                particle.Stop();

            }



        }



    }
    private void GetNode(GameObject node)
    {
        activeNode = node;
    }
    void lineRendererComponents()
    {
        dist = Vector3.Distance(origin.position, activeNode.transform.position );
        lineRenderer.SetPosition(0, origin.position);

    }
    void createLine()
    {
    
            counter += .1f / lineDrawSpeed;

            float x = Mathf.Lerp(0, dist, counter);

            Vector3 pointA = origin.position;
            Vector3 pointB = activeNode.transform.position;

            Vector3 pointAlongLine = x * Vector3.Normalize(pointB - pointA) + pointA;
            lineRenderer.SetPosition(1, pointAlongLine);
            lineRenderer.sortingOrder = 2; 

        
    }

  
}
