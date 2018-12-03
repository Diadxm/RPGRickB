using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZombies : MonoBehaviour {
    private ObjectPooler ObjPool;
    public List<GameObject> SpawnPoints;
    private int MaxSpawns;

    public int MaxZombies;


	// Use this for initialization
	void Start () {
        ObjPool = GameObject.Find("ObjectPool").GetComponent<ObjectPooler>();

        MaxSpawns = 6;
	}

    public void SpawnZombie()
    {
        for (int i = 0; i < MaxSpawns; i++)
        {
            if (i == MaxSpawns)
            {
                i = 0;
            }if (MaxZombies != 60)
            {
                ObjPool.GetObject().transform.position = SpawnPoints[i].transform.position;
                ObjPool.GetObject().SetActive(true);
            }
            MaxZombies++;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
