using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerate : MonoBehaviour
{
    public GameObject roadPrefab;
    public Transform startPos;
    private float nextRoadStep;
    private CarMovement getScore;



    private void Start()
    {
        getScore = GameObject.Find("car_yellow_3").GetComponent<CarMovement>();
    }
    // Update is called once per frame
    void Update()
    {
        newRoadGenerate();

    }

    private void newRoadGenerate()
    {
        if (getScore.roadCounter % 4 == 0)
        {
            Debug.Log(getScore.roadCounter%2);
            getScore.roadCounter = 1;
            nextRoadStep += 8.92f;
            Instantiate(roadPrefab, new Vector3(startPos.position.x + nextRoadStep, 1f, startPos.position.z), transform.rotation);
        }
    }


}
