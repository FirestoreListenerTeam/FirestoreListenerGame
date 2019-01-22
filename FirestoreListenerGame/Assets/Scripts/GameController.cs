using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject box;
    public GameObject chair_1;
    public GameObject chair_2;
    public GameObject chair_3;
    public GameObject chair_4;
    public AudioManager audios;

    //defines
    public float box_y_spawn_position = 5f;
    public float box_y_velocity = 0.2f;

    public bool box_spawned = false;
    public bool box_despawned = false;

    public Animator box_animator;



	// Use this for initialization
	void Start () {
        // Get any component if needed
	}
	
	// Update is called once per frame
	void Update () {
        if(box_spawned){ // Make the box go down till the chair
            box.transform.position = new Vector3(box.transform.position.x, box.transform.position.y - box_y_velocity, box.transform.position.z);
            if (box.transform.position.y < 1.477f)
            {
                box_spawned = false;
                box_animator.SetBool("squishy", true);
                audios.PlaySquishy();
            }
        }

        if (box_despawned)
        { // Make the box go down till the chair
            box.transform.position = new Vector3(box.transform.position.x, box.transform.position.y + box_y_velocity, box.transform.position.z);
            if (box.transform.position.y > 10f)
            {
                box_despawned = false;
                // TODO: Add despawn animation ...
                //box_animator.SetBool("squishy", true);
            }
        }
	}

    public void SpawnBoxInChair1(){
        print("Spawn box in chair 1");
        box_animator.SetBool("squishy", false);
        box.transform.position = new Vector3(chair_1.transform.position.x, chair_1.transform.position.y + box_y_spawn_position, chair_1.transform.position.z);
        box.transform.rotation = chair_1.transform.rotation;
        box_spawned = true;
    }

    public void SpawnBoxInChair2()
    {
        print("Spawn box in chair 2");
        box_animator.SetBool("squishy", false);
        box.transform.position = new Vector3(chair_2.transform.position.x, chair_2.transform.position.y + box_y_spawn_position, chair_2.transform.position.z);
        box.transform.rotation = chair_2.transform.rotation;
        box_spawned = true;
    }

    public void SpawnBoxInChair3()
    {
        print("Spawn box in chair 3");
        box_animator.SetBool("squishy", false);
        box.transform.position = new Vector3(chair_3.transform.position.x, chair_3.transform.position.y + box_y_spawn_position, chair_3.transform.position.z);
        box.transform.rotation = chair_3.transform.rotation;
        box_spawned = true;
    }

    public void SpawnBoxInChair4()
    {
        print("Spawn box in chair 4");
        box_animator.SetBool("squishy", false);
        box.transform.position = new Vector3(chair_4.transform.position.x, chair_4.transform.position.y + box_y_spawn_position, chair_4.transform.position.z);
        box.transform.rotation = chair_4.transform.rotation;
        box_spawned = true;
    }

    public void DespawnBox(){
        print("Despawning box");
        // TODO: Add despawn animation ...
        //box_animator.SetBool("squishy", false);
        //box.transform.position = new Vector3(chair_4.transform.position.x, chair_4.transform.position.y  box_y_spawn_position, chair_4.transform.position.z);
        box_despawned = true;
    }

}
