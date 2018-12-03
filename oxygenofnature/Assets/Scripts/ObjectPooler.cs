using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {

    public GameObject Object;

    private GameObject ObjectPool;

    public List<GameObject> ObjectHolder;

    private int MaxObj;

    private SpawnZombies Spawner;

	// Use this for initialization
	void Start () {
        ObjectHolder = new List<GameObject>();

        ObjectPool = GameObject.Find("ObjectPool");

        MaxObj = 60;

        Spawner = GameObject.Find("ObjectPool").GetComponent<SpawnZombies>();

        for (int i = 0; i < MaxObj; i++)
        {
            GameObject temp = (GameObject)Instantiate(Object);

            temp.SetActive(false);

            ObjectHolder.Add(temp);

            temp.transform.parent = ObjectPool.transform;

        }
        InvokeRepeating("Spawn", 1, 10f);
	}

    public GameObject GetObject()
    {
        for (int i = 0; i < ObjectHolder.Count; i++)
        {
            if (!ObjectHolder[i].activeInHierarchy)
            {
                return ObjectHolder[i];
            }
        }
        GameObject temp = (GameObject)Instantiate(Object);
        temp.SetActive(false);
        ObjectHolder.Add(temp);
        temp.transform.parent = ObjectPool.transform;

        return temp;
    }

    private void Spawn()
    {
        Spawner.SpawnZombie();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
