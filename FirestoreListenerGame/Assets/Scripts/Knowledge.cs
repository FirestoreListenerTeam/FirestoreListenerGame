using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knowledge : MonoBehaviour {
    
    private static int winner; // 1 - blue 2 - red 3 - yellow 4 - green

    public static int Winner
    {
        get
        {
            return winner;
        }
        set
        {
            winner = value;
        }
    }

}
