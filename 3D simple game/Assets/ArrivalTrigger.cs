using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrivalTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) //every Arrival Point has this script. Based on its number it directs the player object to its next destination
    {
        if (other.CompareTag("Player"))
        {
            switch (gameObject.tag)
            {
                case "Arrival0":
                    if(other.GetComponent<PlayerHandler>().TravellingTo == 0)
                    {
                        other.GetComponent<PlayerHandler>().TravellingTo++;
                    }
                    break;
                case "Arrival1":
                    if (other.GetComponent<PlayerHandler>().TravellingTo == 1)
                    {
                        other.GetComponent<PlayerHandler>().TravellingTo++;
                    }
                    break;
                case "Arrival2":
                    if (other.GetComponent<PlayerHandler>().TravellingTo == 2)
                    {
                        other.GetComponent<PlayerHandler>().TravellingTo++;
                    }
                    break;

            }
            ;
        }
    }

}
