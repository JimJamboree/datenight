using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DateManager : MonoBehaviour
{
    public GameObject spawnOne;
    public GameObject spawnTwo;

    public bool spawnOneTaken;
    public bool spawnTwoTaken;
    // Start is called before the first frame update
    void Start()
    {
        PlacePlayerAtSpawn();
    }

    void PlacePlayerAtSpawn()
    {
        for(int i = 0; i < GameObject.FindGameObjectsWithTag("Player").Length; i++)
        {
            if(!spawnOneTaken && GameObject.FindGameObjectsWithTag("Player")[i].GetComponent<UserStartup>().isSeated == false)
            {
                GameObject.FindGameObjectsWithTag("Player")[i].GetComponent<UserStartup>().SeatUser(spawnOne);
                spawnOneTaken = true;
            }
            else if (!spawnTwoTaken && GameObject.FindGameObjectsWithTag("Player")[i].GetComponent<UserStartup>().isSeated == false)
            {
                GameObject.FindGameObjectsWithTag("Player")[i].GetComponent<UserStartup>().SeatUser(spawnTwo);
                spawnTwoTaken = true;
            }
        }
    }
}
