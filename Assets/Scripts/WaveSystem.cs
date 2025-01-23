using UnityEngine;
using System.Collections;

public class WaveSystem : MonoBehaviour
{

    Manager manager;

    public float xDistance = 10;
    public float minSpread = 5;
    public float maxSpread = 8;
    public Transform PlayerTransform;

    //public Transform[] Obstacles;

    public Transform[] BlackObstaclesSet1;
    public Transform[] WhiteObstaclesSet1;

    public Transform[] BlackObstaclesSet2;
    public Transform[] WhiteObstaclesSet2;

    GameObject[] InstatiatedBlackObstaclesSet1;
    GameObject[] InstatiatedWhiteObstaclesSet1;

    GameObject[] InstatiatedBlackObstaclesSet2;
    GameObject[] InstatiatedWhiteObstaclesSet2;

    int IndexOfSpawnedBlackObstacle;
    int IndexOfSpawnedWhiteObstacle;

    int IndexOfLastSpawnedBlackObstacle;
    int IndexOfLastSpawnedWhiteObstacle;

	int SetInUse = 1; 

    float xSpread;
    float lastXPos = 10;

    public void Begin()
    {
        lastXPos = Mathf.NegativeInfinity;       
    }

    void Start()
    {
        manager = GetComponent<Manager>();
        InstantiateAllObstacles();

        InstatiatedBlackObstaclesSet1 = GameObject.FindGameObjectsWithTag("Black Obstacle Set1");
        InstatiatedWhiteObstaclesSet1 = GameObject.FindGameObjectsWithTag("White Obstacle Set1");

        InstatiatedBlackObstaclesSet2 = GameObject.FindGameObjectsWithTag("Black Obstacle Set2");
        InstatiatedWhiteObstaclesSet2 = GameObject.FindGameObjectsWithTag("White Obstacle Set2");
    }
    
    void Update()
    {
        PoolObstacles();
        if (manager.GameStarted && maxSpread >= 6 )
        {
            minSpread -= 0.001f;
            maxSpread -= 0.001f;
            xDistance -= 0.001f;
        }
    }

    void InstantiateAllObstacles()
    {
        // Note that BlackObstacles.length = WhiteObstacles.length
        for (int a = 0; a< 5; a++)
        {
            Instantiate(BlackObstaclesSet1[a], new Vector3(-100, -100, 0), Quaternion.identity);
			Instantiate(WhiteObstaclesSet1[a], new Vector3(-100, -100, 0), Quaternion.identity);

            Instantiate(BlackObstaclesSet2[a], new Vector3(-100, -100, 0), Quaternion.identity);
            Instantiate(WhiteObstaclesSet2[a], new Vector3(-100, -100, 0), Quaternion.identity);
        }
    }

    void PoolObstacles()//GameObject WhiteObstacle, GameObject BlackObstacle)
    {
        IndexOfSpawnedBlackObstacle = Random.Range(0, BlackObstaclesSet1.Length);
        IndexOfSpawnedWhiteObstacle = Random.Range(0, BlackObstaclesSet1.Length);
		
			ProcessObstacleSelection();
			xSpread = Random.Range(minSpread, maxSpread);
			
		
		
        if (PlayerTransform.position.x - lastXPos >= xSpread )
        {
            if (SetInUse == 1)
            {
                InstatiatedBlackObstaclesSet1[IndexOfSpawnedBlackObstacle].transform.position = new Vector2(PlayerTransform.position.x + xDistance, 0);

                InstatiatedWhiteObstaclesSet1[IndexOfSpawnedWhiteObstacle].transform.position = new Vector2(PlayerTransform.position.x + xDistance, 0);

                Debug.Log("Index Of Spawned Black Obstacle = " + IndexOfSpawnedBlackObstacle + "| Index Of Spawned White Obstacle = " + IndexOfSpawnedWhiteObstacle);
                //Instantiate(BlackObstacles[IndexOfSpawnedBlackObstacle], new Vector2(PlayerTransform.position.x + xDistance * 2, 0), Quaternion.identity);
                //Instantiate(WhiteObstacles[Random.Range(1, WhiteObstacles.Length)], new Vector2(PlayerTransform.position.x + xDistance * 2, 0), Quaternion.identity);

                lastXPos = PlayerTransform.position.x;
                SetInUse = 2;
            }
            else
            {
                InstatiatedBlackObstaclesSet2[IndexOfSpawnedBlackObstacle].transform.position = new Vector2(PlayerTransform.position.x + xDistance, 0);

                InstatiatedWhiteObstaclesSet2[IndexOfSpawnedWhiteObstacle].transform.position = new Vector2(PlayerTransform.position.x + xDistance, 0);
            		
                Debug.Log("Index Of Spawned Black Obstacle = "+ IndexOfSpawnedBlackObstacle + "| Index Of Spawned White Obstacle = "+ IndexOfSpawnedWhiteObstacle);
                //Instantiate(BlackObstacles[IndexOfSpawnedBlackObstacle], new Vector2(PlayerTransform.position.x + xDistance * 2, 0), Quaternion.identity);
               //Instantiate(WhiteObstacles[Random.Range(1, WhiteObstacles.Length)], new Vector2(PlayerTransform.position.x + xDistance * 2, 0), Quaternion.identity);

			    lastXPos = PlayerTransform.position.x;
                SetInUse = 1;
            }


        }
		
    }
	
	void ProcessObstacleSelection()
	{
		
        //now I have a pair (x,y) while 0<=x<=4 and 0<=y<=4

        while(IndexOfSpawnedBlackObstacle == IndexOfLastSpawnedBlackObstacle)
        {
            IndexOfSpawnedBlackObstacle = Random.Range(0, BlackObstaclesSet1.Length);
        }

        while(IndexOfSpawnedWhiteObstacle == IndexOfLastSpawnedWhiteObstacle)
        {
            IndexOfSpawnedWhiteObstacle = Random.Range(0, BlackObstaclesSet1.Length);
        }

        IndexOfLastSpawnedBlackObstacle = IndexOfSpawnedBlackObstacle;
        IndexOfLastSpawnedWhiteObstacle = IndexOfSpawnedBlackObstacle;
		

        if (IndexOfSpawnedBlackObstacle == 0 && IndexOfSpawnedWhiteObstacle == 0)
        {
            IndexOfSpawnedWhiteObstacle = Random.Range(1, BlackObstaclesSet1.Length);
        }
        //now I have a pair (x,y) while 0<=x<=4 , 0<=y<=4 and (x,y) != (0,0)

        if (IndexOfSpawnedBlackObstacle == 4 && IndexOfSpawnedWhiteObstacle == 4)
        {
            IndexOfSpawnedWhiteObstacle = Random.Range(0, BlackObstaclesSet1.Length-1);
        }
            //now I have a pair (x,y) while 0<=x<=4 , 0<=y<=4 , (x,y) != (0,0) and (x,y) != (4,4)
            //fewwwwwwwwwwwwwwww!!
            // now I can pool objects
	}
	
}
