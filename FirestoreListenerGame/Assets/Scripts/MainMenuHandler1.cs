using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XInputDotNetPure;

public class MainMenuHandler1 : MonoBehaviour {

    public GameObject start_btn;
    public GameObject exit_btn;

    public GameObject winner_lbl;

    Animator start_animator;
    Animator exit_animator;

    // XInput stuff
    bool playerIndexSet = false;
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;

    //timers stuff
    float start_timer = 3.0f;
    float exit_timer = 3.0f;

	// Use this for initialization
	void Start () {
        start_animator = start_btn.GetComponent<Animator>();
        exit_animator = exit_btn.GetComponent<Animator>();

        winner_lbl.GetComponent<Text>().text = "I WIN";
        winner_lbl.GetComponent<Text>().color = Color.gray;

        switch(Knowledge.Winner){
            case 1:
                winner_lbl.GetComponent<Text>().text = "Blue wins";
                winner_lbl.GetComponent<Text>().color = Color.blue;
                break;
            case 2:
                winner_lbl.GetComponent<Text>().text = "Red wins";
                winner_lbl.GetComponent<Text>().color = Color.red;
                break;
            case 3:
                winner_lbl.GetComponent<Text>().text = "Yellow wins";
                winner_lbl.GetComponent<Text>().color = Color.yellow;
                break;
            case 4:
                winner_lbl.GetComponent<Text>().text = "Green wins";
                winner_lbl.GetComponent<Text>().color = Color.green;
                break;
            default:
                break;
        }

	}
	
	// Update is called once per frame
	void Update () {

        if(start_animator.GetBool("play_in")){
            start_timer -= Time.deltaTime;
            if(start_timer < 0.0f){
                // START GAME
                Application.LoadLevel("RealMainScene");
            }
        }

        if (exit_animator.GetBool("exit_in"))
        {
            exit_timer -= Time.deltaTime;
            if (exit_timer < 0.0f)
            {
                // END GAME
                Application.LoadLevel("MainMenu");
            }
        }

        if (!playerIndexSet || !prevState.IsConnected)
        {
            for (int i = 0; i < 4; ++i)
            {
                PlayerIndex testPlayerIndex = (PlayerIndex)i;
                GamePadState testState = GamePad.GetState(testPlayerIndex);
                if (testState.IsConnected)
                {
                    Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
                    playerIndex = testPlayerIndex;
                    playerIndexSet = true;
                }
            }
        }

        prevState = state;
        state = GamePad.GetState(playerIndex);

        // Detect if a button was pressed this frame


        if (prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed)
        {
            // A Pressed

            {
                print("A PRESSED");
                start_btn.transform.SetAsLastSibling();
                start_animator.SetBool("play_in", true);
            }
        }

        if (prevState.Buttons.B == ButtonState.Released && state.Buttons.B == ButtonState.Pressed)
        {
            // B Pressed
            {
                exit_btn.transform.SetAsLastSibling();
                exit_animator.SetBool("exit_in", true);
            }
        }


        // MAC TEST

        /*if (Input.GetKeyDown("1"))
        {
            // Like A Pressed
            print("X PRESSED");
            credits_btn.transform.SetAsLastSibling();
            credits_animator.SetBool("credits_in", true);
        }
        if (Input.GetKeyDown("2"))
        {
            credits_animator.SetBool("credits_in", false);
        }

        if (Input.GetKeyDown("3"))
        {
            // Like A Pressed
            print("Y PRESSED");
            how_to_play_btn.transform.SetAsLastSibling();
            how_to_play_animator.SetBool("how_to_play_in", true);
        }
        if (Input.GetKeyDown("4"))
        {
            how_to_play_animator.SetBool("how_to_play_in", false);
        }

        if (Input.GetKeyDown("5"))
        {
            start_btn.transform.SetAsLastSibling();
            start_animator.SetBool("play_in", true);
        }
        if (Input.GetKeyDown("6"))
        {
            exit_btn.transform.SetAsLastSibling();
            exit_animator.SetBool("exit_in", true);
        }*/
	}
}
