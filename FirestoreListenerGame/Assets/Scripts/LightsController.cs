using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsController : MonoBehaviour
{
    public Game game = null;

    public int poolSize = 10;
    public GameObject lightPrefab = null;

    public GameObject light1 = null;
    public GameObject light2 = null;
    public GameObject light3 = null;
    public GameObject light4 = null;

    private GameObject[] lights = null;

    void Start()
    {
        lights = new GameObject[poolSize];

        for (uint i = 0; i < poolSize; ++i)
        {
            lights[i] = Instantiate(lightPrefab, Vector3.zero, Quaternion.identity) as GameObject;
            lights[i].transform.rotation *= Quaternion.AngleAxis(90.0f, Vector3.right);
            lights[i].SetActive(false);
        }

        light1.SetActive(false);
        light2.SetActive(false);
        light3.SetActive(false);
        light4.SetActive(false);
    }

    public void LightsOn()
    {
        bool g = false;
        bool r = false;
        bool y = false;
        bool b = false;

        for (uint i = 0; i < poolSize; ++i)
        {
            lights[i].SetActive(true);

            LightMover lightMover = lights[i].GetComponent<LightMover>();
            float posX = Random.Range(lightMover.minX, lightMover.maxX);
            float posZ = Random.Range(lightMover.minZ, lightMover.maxZ);
            Vector3 position = new Vector3(posX, lightMover.defaultY, posZ);
            lights[i].transform.position = position;

            Color color = Color.white;
            int random = Random.Range(0, 4);
            switch (random)
            {
                case 0:
                    color = Color.green;
                    g = true;
                    break;
                case 1:
                    color = Color.red;
                    r = true;
                    break;
                case 2:
                    color = Color.yellow;
                    y = true;
                    break;
                case 3:
                    color = Color.blue;
                    b = true;
                    break;
            }

            if (i >= poolSize - 4)
            {
                if (!g)
                {
                    color = Color.green;
                    g = true;
                }
                else if (!r)
                {
                    color = Color.red;
                    r = true;
                }
                else if (!y)
                {
                    color = Color.yellow;
                    y = true;
                }
                else if (!b)
                {
                    color = Color.blue;
                    b = true;
                }
            }

            lights[i].GetComponent<Light>().color = color;
        }
    }

    public void LightsOff()
    {
        for (uint i = 0; i < poolSize; ++i)
        {
            lights[i].SetActive(false);
        }
    }

    public void LightOn(Player.CurrentPlayer currentPlayer)
    {
        switch (currentPlayer)
        {
            case Player.CurrentPlayer.p1:
                light1.SetActive(true);
                break;
            case Player.CurrentPlayer.p2:
                light2.SetActive(true);
                break;
            case Player.CurrentPlayer.p3:
                light3.SetActive(true);
                break;
            case Player.CurrentPlayer.p4:
                light4.SetActive(true);
                break;
        }
    }

    public void LightOff(Player.CurrentPlayer currentPlayer)
    {
        switch (currentPlayer)
        {
            case Player.CurrentPlayer.p1:
                light1.SetActive(false);
                break;
            case Player.CurrentPlayer.p2:
                light2.SetActive(false);
                break;
            case Player.CurrentPlayer.p3:
                light3.SetActive(false);
                break;
            case Player.CurrentPlayer.p4:
                light4.SetActive(false);
                break;
        }
    }
}