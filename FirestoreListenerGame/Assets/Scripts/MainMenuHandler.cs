﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class MainMenuHandler : MonoBehaviour {

    public GameObject start_btn;
    public GameObject credits_btn;
    public GameObject how_to_play_btn;
    public GameObject exit_btn;

    Animator credits_animator;
    Animator how_to_play_animator;
    Animator start_animator;
    Animator exit_animator;

    // XInput stuff
    bool playerIndexSet = false;
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;

	// Use this for initialization
	void Start () {
        credits_animator = credits_btn.GetComponent<Animator>();
        how_to_play_animator = how_to_play_btn.GetComponent<Animator>();
        start_animator = start_btn.GetComponent<Animator>();
        exit_animator = exit_btn.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        
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
            print("A PRESSED");
        }

        if (prevState.Buttons.B == ButtonState.Released && state.Buttons.B == ButtonState.Pressed)
        {
            // B Pressed
            if(credits_animator.GetBool("credits_in") || how_to_play_animator.GetBool("how_to_play_in")){ // Any option opened
                credits_animator.SetBool("credits_in", false);
                how_to_play_animator.SetBool("how_to_play_in", false);
            }
        }

        if (prevState.Buttons.X == ButtonState.Released && state.Buttons.X == ButtonState.Pressed)
        {
            // X Pressed
            credits_btn.transform.SetAsLastSibling();
            credits_animator.SetBool("credits_in", true);
        }

        if (prevState.Buttons.Y == ButtonState.Released && state.Buttons.Y == ButtonState.Pressed)
        {
            // Y Pressed
            how_to_play_btn.transform.SetAsLastSibling();
            how_to_play_animator.SetBool("how_to_play_in", true);
        }
        /*
        if (Input.GetKeyDown("1"))
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
        }*/
	}
}
