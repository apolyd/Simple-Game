using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    public List<GameObject> ArrivalPoints;
    public GameObject Particles;
    public float speed = 1f;
    public int TravellingTo;
    // Start is called before the first frame update
    void Start()
    {
        GameObject ArrivalPoint1 = GameObject.FindGameObjectWithTag("Arrival0"); //detect the arrival points in the scene
        GameObject ArrivalPoint2 = GameObject.FindGameObjectWithTag("Arrival1");
        GameObject ArrivalPoint3 = GameObject.FindGameObjectWithTag("Arrival2");
        ArrivalPoints.Add(ArrivalPoint1);
        ArrivalPoints.Add(ArrivalPoint2);
        ArrivalPoints.Add(ArrivalPoint3);
        TravellingTo = 0; //set the first arrival point
    }

    // Update is called once per frame
    void Update()
    {
        
        if (TravellingTo == 3) //reached the last arrival point
        {
            SelfDestruct();
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, ArrivalPoints[TravellingTo].transform.position, speed * Time.deltaTime); //travel to arrival point
        }
    }

    public void SelfDestruct() //if there is a collision or the player reached the end destroy this object
    {
        GameObject Manager = GameObject.FindGameObjectWithTag("GameManager"); //find the game manager to signal that the game ended
        Manager.GetComponent<SpawnObstacles>().EndCurrentPlayer();
        Instantiate(Particles, transform.position, Quaternion.Euler(90, 0, 0)); // spiral particle effect
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other) //if the player collides with something
    {
        if (other.CompareTag("Obstacle")) //call selfdestruct only if the player hits an obstacle
        {
            SelfDestruct();
        }
    }
}
