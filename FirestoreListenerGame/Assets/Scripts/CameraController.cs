using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class CameraController : MonoBehaviour
{
    public Game game = null;
    public GameController gameController = null;
    public Box box = null;
    public Animator animator = null;

    public bool can = false;

    // Controller
    bool playerIndexSet = false;
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;

    void Start()
    {
        animator = GetComponent<Animator>();
	}
	
	void Update()
    {
        // Find a PlayerIndex, for a single player game
        // Will find the first controller that is connected ans use it
        if (!playerIndexSet || !prevState.IsConnected)
        {
            for (int i = 0; i < 4; ++i)
            {
                PlayerIndex testPlayerIndex = (PlayerIndex)i;
                GamePadState testState = GamePad.GetState(testPlayerIndex);
                if (testState.IsConnected)
                {
                    //Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
                    playerIndex = testPlayerIndex;
                    playerIndexSet = true;
                }
            }
        }

        prevState = state;
        state = GamePad.GetState(playerIndex);

        if (can)
        {
            // Detect if a button was pressed this frame
            if (prevState.Buttons.LeftShoulder == ButtonState.Released && state.Buttons.LeftShoulder == ButtonState.Pressed)
            {
                switch (game.currentPlayer.currentPlayer)
                {
                    case Player.CurrentPlayer.p1:
                        game.nextPlayer = game.players[3];
                        break;
                    case Player.CurrentPlayer.p2:
                        game.nextPlayer = game.players[0];
                        break;
                    case Player.CurrentPlayer.p3:
                        game.nextPlayer = game.players[1];
                        break;
                    case Player.CurrentPlayer.p4:
                        game.nextPlayer = game.players[2];
                        break;
                }

                // Reset variables
                can = false;
                box.can = false;
                gameController.DespawnBox();
                game.currentPlayer.rotations = 0;
                game.playState = Game.PlayState.waitLightOff;
            }
            else if (prevState.Buttons.RightShoulder == ButtonState.Released && state.Buttons.RightShoulder == ButtonState.Pressed)
            {
                switch (game.currentPlayer.currentPlayer)
                {
                    case Player.CurrentPlayer.p1:
                        game.nextPlayer = game.players[1];
                        break;
                    case Player.CurrentPlayer.p2:
                        game.nextPlayer = game.players[2];
                        break;
                    case Player.CurrentPlayer.p3:
                        game.nextPlayer = game.players[3];
                        break;
                    case Player.CurrentPlayer.p4:
                        game.nextPlayer = game.players[0];
                        break;
                }

                // Reset variables
                can = false;
                box.can = false;
                gameController.DespawnBox();
                game.currentPlayer.rotations = 0;
                game.playState = Game.PlayState.waitLightOff;
            }
        }
    }
}