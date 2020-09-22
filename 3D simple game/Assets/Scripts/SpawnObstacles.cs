using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;


public class SpawnObstacles : MonoBehaviour
{
    public Camera cam;
    public Text ArrivalPointText;
    public GameObject ObstaclePrefab, ArrivalPoint, PlayerPrefab;
    public int ObstaclesNumber;
    public List<Vector3> Positions;
    public bool GameStartedFlag = false;
    private string ArrivalPointSelected = null;
    // Start is called before the first frame update
    void Start()
    {
        ObstacleSpawn(); //initialize the board
        GameStartedFlag = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //if a click on board is detected and an arrival point is selected from GUI the designer can change the position of each arrival point
        {
            HandleArrivalPoint(); //set the position of arrival point
        }

    }

    public void ObstacleSpawn()
    {
        GameObject Particles = GameObject.FindGameObjectWithTag("Particle"); //destroy any particles if any in the scene
        Destroy(Particles);
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        Destroy(Player);
        Positions.Clear();
        GameObject[] ObstaclesInScene = GameObject.FindGameObjectsWithTag("Obstacle"); //since the obstacles are about to be reset destroy all of the existing ones
        foreach (GameObject obstacle in ObstaclesInScene)
            Destroy(obstacle);



        if (GameStartedFlag == true) //same for arrival points
        {
            GameObject ArrivalPoint1 = GameObject.FindGameObjectWithTag("Arrival0");
            GameObject ArrivalPoint2 = GameObject.FindGameObjectWithTag("Arrival1");
            GameObject ArrivalPoint3 = GameObject.FindGameObjectWithTag("Arrival2");
            Destroy(ArrivalPoint1);
            Destroy(ArrivalPoint2);
            Destroy(ArrivalPoint3);
        }
        



        for (int i = 0; i < ObstaclesNumber; i++) //initialization for the obstacles in a 15x15 board in random positions
        {
            bool posFound = false;
            while(posFound == false)
            {
                Vector3 RandomPosition = new Vector3(Random.Range(-15, 16), 0.15f, Random.Range(-15, 16)); //random position
                if (Positions.Contains(RandomPosition) || RandomPosition == new Vector3(0, 0.15f, 0)) //dont spawn anything at (0, 0.15f, 0) this is where the player spawns
                {
                    continue;
                }
                else
                {
                    Instantiate(ObstaclePrefab, RandomPosition, Quaternion.identity);
                    Positions.Add(RandomPosition);
                    posFound = true;
                }
                
            }
            
        }

        
        ArrivalSpawn();//spawn 3 random arrival points

    }

    public void ArrivalSpawn()
    {
        for(int i = 0; i < 3; i++)
        {
            bool posFound = false;
            while (posFound == false)
            {
                Vector3 RandomPosition = new Vector3(Random.Range(-15, 16), 0.15f, Random.Range(-15, 16));
                if (Positions.Contains(RandomPosition) || RandomPosition == new Vector3(0, 0.15f, 0)) //dont spawn anything at (0, 0.15f, 0) this is where the player spawns
                {
                    continue;
                }
                else
                {
                    Instantiate(ArrivalPoint, RandomPosition, Quaternion.identity).tag = "Arrival"+i; //also set some tags to the arrival points to identify them easier                   
                    Positions.Add(RandomPosition);
                    posFound = true;
                }

            }
        }
    }

    public void SetArrivalPoint(string name) //the selected arrival target points through the UI
    {
        ArrivalPointSelected = name;
        switch (name)
        {
            case "Arrival0": //assign the names of the arrival points 
                ArrivalPointText.text = "Arrival Point 1";
                break;
            case "Arrival1":
                ArrivalPointText.text = "Arrival Point 2";
                break;
            case "Arrival2":
                ArrivalPointText.text = "Arrival Point 3";
                break;
        }
        
        //Debug.Log("Arrival point is "+name);
    }

    public void HandleArrivalPoint()
    {
        if (ArrivalPointSelected != null)
        {
            Debug.Log(ArrivalPointSelected);
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Ground"))
                {
                    GameObject ArrivalPoint = GameObject.FindGameObjectWithTag(ArrivalPointSelected);
                    ArrivalPoint.transform.position = hit.point;
                }
            }
        }
        else
        {
            ArrivalPointText.text = "Nothing selected!";
        }
        
    }

    public void SpawnPlayer()
    {
        GameObject Particles = GameObject.FindGameObjectWithTag("Particle");
        Destroy(Particles);
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        Destroy(Player);
        Instantiate(PlayerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }

    public void EndCurrentPlayer()
    {
        GetComponent<AudioSource>().Play();
    }
}
