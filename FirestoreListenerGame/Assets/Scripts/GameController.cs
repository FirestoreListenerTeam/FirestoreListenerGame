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
    public float box_y_spawn_position = 5f;
    public float box_y_velocity = 0.2f;

    private bool box_spawned = false;

    public Animator box_animator;



	// Use this for initialization
	void Start () {
        // Get any component if needed
        box_animator.StopPlayback();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("1"))
        {
            print("Spawn box in chair 1");
            box.transform.position = new Vector3(chair_1.transform.position.x, chair_1.transform.position.y + box_y_spawn_position, chair_1.transform.position.z);
            box_spawned = true;
        }
        if (Input.GetKeyDown("2"))
        {
            print("Spawn box in chair 2");
            box.transform.position = new Vector3(chair_2.transform.position.x, chair_2.transform.position.y + box_y_spawn_position, chair_2.transform.position.z);
            box_spawned = true;
        }
        if (Input.GetKeyDown("3"))
        {
            print("Spawn box in chair 3");
            box.transform.position = new Vector3(chair_3.transform.position.x, chair_3.transform.position.y + box_y_spawn_position, chair_3.transform.position.z);
            box_spawned = true;
        }
        if (Input.GetKeyDown("4"))
        {
            print("Spawn box in chair 4");
            box.transform.position = new Vector3(chair_4.transform.position.x, chair_4.transform.position.y + box_y_spawn_position, chair_4.transform.position.z);
            box_spawned = true;
        }

        if(box_spawned){ // Make the box go down till the chair
            box.transform.position = new Vector3(box.transform.position.x, box.transform.position.y - box_y_velocity, box.transform.position.z);
            if (box.transform.position.y < 2f)
            {
                box_spawned = false;
                //box_animator.Play()
                box_animator.Play("box_squishy");
            }
        }

	}
}
