﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject box;
    public GameObject chair_1;
    public GameObject chair_2;
    public GameObject chair_3;
    public GameObject chair_4;

    //defines
    public int box_y_spawn = 5;

    static 

	// Use this for initialization
	void Start () {
		// Get any component if needed
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("1"))
        {
            print("Spawn box in chair 1");
            box.transform.position = new Vector3(chair_1.transform.position.x, chair_1.transform.position.y + box_y_spawn, chair_1.transform.position.z);
        }
        if (Input.GetKeyDown("2"))
        {
            print("Spawn box in chair 2");
            box.transform.position = new Vector3(chair_2.transform.position.x, chair_2.transform.position.y + box_y_spawn, chair_2.transform.position.z);
        }
        if (Input.GetKeyDown("3"))
        {
            print("Spawn box in chair 3");
            box.transform.position = new Vector3(chair_3.transform.position.x, chair_3.transform.position.y + box_y_spawn, chair_3.transform.position.z);
        }
        if (Input.GetKeyDown("4"))
        {
            print("Spawn box in chair 4");
            box.transform.position = new Vector3(chair_4.transform.position.x, chair_4.transform.position.y + box_y_spawn, chair_4.transform.position.z);
        }
	}
}
