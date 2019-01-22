using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player
{
    public bool active = true;
    public uint rotations = 0;

    public enum CurrentCamera { a, b, c, d, e };
    public CurrentCamera currentCamera = CurrentCamera.a;

    public enum CurrentPlayer { p1, p2, p3, p4 };
    public CurrentPlayer currentPlayer = CurrentPlayer.p1;
}

public class Game : MonoBehaviour
{
    //UI

    public GameObject manivela_UI;
    public GameObject joystick_UI;
    public GameObject choose_lbl;
    public GameObject choose_timer;
    public GameObject rb;
    public GameObject lb;
    Animator rb_anim;
    Animator lb_anim;
    Animator choose_anim2;
    Animator choose_anim;
    Animator joystick_amimator;
    Animator manivela_animator;
    Animator clown_animator;

    public AudioManager managerAudio;

    public GameObject choose_color_lbl;
    public GameObject choose_color_timer_lbl;
    float choose_color_timer = 10.0f;

    public CameraController cameraController = null;
    public LightsController lightsController = null;
    public GameController gameController = null;
    public Box box = null;

    public Player[] players = null;
    public Player currentPlayer = null;
    public Player nextPlayer = null;
    public Player lastPlayer = null;

    public ParticleSystem ps1 = null;
    public ParticleSystem ps2 = null;
    public ParticleSystem ps3 = null;
    public ParticleSystem ps4 = null;
    public ParticleSystem sps1 = null;
    public ParticleSystem sps2 = null;
    public ParticleSystem sps3 = null;
    public ParticleSystem sps4 = null;

    public GameObject chair1 = null;
    public GameObject chair2 = null;
    public GameObject chair3 = null;
    public GameObject chair4 = null;

    public enum GameState { chooseColour, randomLights, oneLight, play };
    public GameState gameState = GameState.chooseColour;

    public enum PlayState
    {
        waitLightOn, lightOn,
        waitDropBox, dropBox,
        interactBox,
        waitDie, die,
        waitLightOff, lightOff,
        waitMoveCamera, moveCamera,
        toEndScreen
    };
    public PlayState playState = PlayState.dropBox;

    public uint numPlayers = 0;

    private float timer = 0.0f;

    // Random lights
    public float lightsSeconds = 0.0f;

    // One light
    public float lightSeconds = 0.0f;

    // Wait light on
    public float waitLightOnSeconds = 0.0f;

    // Wait drop box
    public float waitDropBoxSeconds = 0.0f;

    // Wait light off
    public float waitLightOffSeconds = 0.0f;

    // Wait move camera
    public float waitMoveCameraSeconds = 0.0f;

    void Start()
    {
        manivela_animator = manivela_UI.GetComponent<Animator>();
        joystick_amimator = joystick_UI.GetComponent<Animator>();
        choose_anim = choose_lbl.GetComponent<Animator>();
        choose_anim2 = choose_timer.GetComponent<Animator>();
        rb_anim = rb.GetComponent<Animator>();
        lb_anim = lb.GetComponent<Animator>(); 

        players = new Player[4];

        for (uint i = 0; i < 4; ++i)
            players[i] = new Player();

        players[0].currentPlayer = Player.CurrentPlayer.p1;
        players[0].currentCamera = Player.CurrentCamera.a;
        players[1].currentPlayer = Player.CurrentPlayer.p2;
        players[1].currentCamera = Player.CurrentCamera.a;
        players[2].currentPlayer = Player.CurrentPlayer.p3;
        players[2].currentCamera = Player.CurrentCamera.a;
        players[3].currentPlayer = Player.CurrentPlayer.p4;
        players[3].currentCamera = Player.CurrentCamera.a;

        currentPlayer = nextPlayer = players[0];

        numPlayers = 4; // TODO: set num players

        managerAudio.backgroundTerror.Play(); // TODO PAUSE THIS MUSIC
    }

    void Update()
    {
        if (cameraController.can)
        {
            rb_anim.SetBool("rb_in", true);
            lb_anim.SetBool("lb_in", true);
        }
        else
        {
            rb_anim.SetBool("rb_in", false);
            lb_anim.SetBool("lb_in", false);
        }
        if (box.can)
        {
            joystick_amimator.SetBool("joystick_in", true);
            manivela_animator.SetBool("fade_in_manivela", true);
        }
        else
        {
            joystick_amimator.SetBool("joystick_in", false);
            manivela_animator.SetBool("fade_in_manivela", false);
        }

        choose_color_timer -= Time.deltaTime;

        switch (gameState)
        {
            case GameState.chooseColour:
               
                choose_anim.SetBool("choose_in", true);
                choose_anim2.SetBool("choose_in", true);

                timer += Time.deltaTime;

                choose_color_timer_lbl.GetComponent<Text>().text = "You have " + choose_color_timer.ToString("F0") + " seconds left";

                if (choose_color_timer <= 0.0f)
                {
                    choose_anim.SetBool("choose_in", false);
                    choose_anim2.SetBool("choose_in", false);

                    timer = 0.0f;
                    lightsController.LightsOn();

                    managerAudio.PlayDrumRoll();

                    gameState = GameState.randomLights;
                }

                break;

            case GameState.randomLights:

                timer += Time.deltaTime;

                if (timer >= lightsSeconds) // TODO: adjust random lights time
                {
                    timer = 0.0f;

                    lightsController.LightsOff();

                    int random = Random.Range(0, 4);
                    switch (random)
                    {
                        case 0:
                            currentPlayer = players[0];
                            break;
                        case 1:
                            currentPlayer = players[1];
                            break;
                        case 2:
                            currentPlayer = players[2];
                            break;
                        case 3:
                            currentPlayer = players[3];
                            break;
                    }

                    lightsController.LightOn(currentPlayer.currentPlayer);

                    gameState = GameState.oneLight;
                }

                break;

            case GameState.oneLight:

                timer += Time.deltaTime;

                if (timer >= lightSeconds)
                {
                    timer = 0.0f;

                    cameraController.animator.SetBool("toScene", false);

                    switch (currentPlayer.currentPlayer)
                    {
                        case Player.CurrentPlayer.p1:
                            cameraController.animator.SetBool("to1", true);
                            break;
                        case Player.CurrentPlayer.p2:
                            cameraController.animator.SetBool("to2", true);
                            break;
                        case Player.CurrentPlayer.p3:
                            cameraController.animator.SetBool("to3", true);
                            break;
                        case Player.CurrentPlayer.p4:
                            cameraController.animator.SetBool("to4", true);
                            break;
                    }

                    box.beat = true;
                    gameState = GameState.play;
                    playState = PlayState.waitLightOn;
                }

                break;

            case GameState.play:

                Play();

                break;
        }
    }

    void Play()
    {
        switch (playState)
        {
            case PlayState.waitLightOn:

                timer += Time.deltaTime;

                if (timer >= waitLightOnSeconds)
                {
                    if (!currentPlayer.active)
                    {
                        Debug.Log("The player has died. The camera will move to the next player...");

                        switch (currentPlayer.currentPlayer)
                        {
                            case Player.CurrentPlayer.p1:
                                if (lastPlayer.currentPlayer == Player.CurrentPlayer.p4)
                                    nextPlayer = players[1];
                                else if (lastPlayer.currentPlayer == Player.CurrentPlayer.p2)
                                    nextPlayer = players[3];
                                break;
                            case Player.CurrentPlayer.p2:
                                if (lastPlayer.currentPlayer == Player.CurrentPlayer.p3)
                                    nextPlayer = players[0];
                                else if (lastPlayer.currentPlayer == Player.CurrentPlayer.p1)
                                    nextPlayer = players[2];
                                break;
                            case Player.CurrentPlayer.p3:
                                if (lastPlayer.currentPlayer == Player.CurrentPlayer.p4)
                                    nextPlayer = players[1];
                                else if (lastPlayer.currentPlayer == Player.CurrentPlayer.p2)
                                    nextPlayer = players[3];
                                break;
                            case Player.CurrentPlayer.p4:
                                if (lastPlayer.currentPlayer == Player.CurrentPlayer.p3)
                                    nextPlayer = players[0];
                                else if (lastPlayer.currentPlayer == Player.CurrentPlayer.p1)
                                    nextPlayer = players[2];
                                break;
                        }

                        timer = 0.0f;

                        playState = PlayState.moveCamera;

                        break;
                    }

                    timer = 0.0f;

                    playState = PlayState.lightOn;
                }

                break;

            case PlayState.lightOn:

                lightsController.LightOn(currentPlayer.currentPlayer);

                playState = PlayState.waitDropBox;

                break;

            case PlayState.waitDropBox:

                timer += Time.deltaTime;

                if (timer >= waitDropBoxSeconds)
                {
                    timer = 0.0f;

                    playState = PlayState.dropBox;
                }

                break;

            case PlayState.dropBox:

                switch (currentPlayer.currentPlayer)
                {
                    case Player.CurrentPlayer.p1:
                        gameController.SpawnBoxInChair1();
                        break;
                    case Player.CurrentPlayer.p2:
                        gameController.SpawnBoxInChair2();
                        break;
                    case Player.CurrentPlayer.p3:
                        gameController.SpawnBoxInChair3();
                        break;
                    case Player.CurrentPlayer.p4:
                        gameController.SpawnBoxInChair4();
                        break;
                }

                playState = PlayState.interactBox;
                Debug.Log("You can interact with the BOX now");

                break;

            case PlayState.interactBox:

                // If the box is spawned...
                if (!gameController.box_spawned)
                {
                    // Can rotate box
                    box.can = true;

                    if (currentPlayer.rotations > 0)
                        // Can move camera
                        cameraController.can = true;
                }

                break;

            // Despawn box

            case PlayState.waitDie:

                switch (currentPlayer.currentPlayer)
                {
                    case Player.CurrentPlayer.p1:
                        ps1.Play();
                        chair1.SetActive(false);
                        break;
                    case Player.CurrentPlayer.p2:
                        ps2.Play();
                        chair2.SetActive(false);
                        break;
                    case Player.CurrentPlayer.p3:
                        ps3.Play();
                        chair3.SetActive(false);
                        break;
                    case Player.CurrentPlayer.p4:
                        ps4.Play();
                        chair4.SetActive(false);
                        break;
                }

                box.SetToDefault();
                managerAudio.PlayExplosion();
                clown_animator.SetBool("open", true);

                playState = PlayState.die;

                break;

            case PlayState.die:

                timer += Time.deltaTime;

                if (timer >= 2.0f)
                {
                    timer = 0.0f;

                    clown_animator.SetBool("open", false); // TODO: adjust clown time

                    switch (currentPlayer.currentPlayer)
                    {
                        case Player.CurrentPlayer.p1:
                            sps1.Play();
                            break;
                        case Player.CurrentPlayer.p2:
                            sps2.Play();
                            break;
                        case Player.CurrentPlayer.p3:
                            sps3.Play();
                            break;
                        case Player.CurrentPlayer.p4:
                            sps4.Play();
                            break;
                    }

                    playState = PlayState.waitLightOff;
                }

                break;

            case PlayState.waitLightOff:

                Debug.Log("waitLightOff");

                box.StopVibration();
                box.StopShake();

                if (!gameController.box_despawned)
                {
                    timer += Time.deltaTime;

                    if (timer >= waitLightOffSeconds)
                    {
                        timer = 0.0f;

                        playState = PlayState.lightOff;
                    }
                }

                break;

            case PlayState.lightOff:

                Debug.Log("lightOff");

                lightsController.LightOff(currentPlayer.currentPlayer);

                playState = PlayState.waitMoveCamera;

                break;

            case PlayState.waitMoveCamera:

                timer += Time.deltaTime;

                if (timer >= waitMoveCameraSeconds)
                {
                    timer = 0.0f;

                    // Play again?
                    if (AllPlayersDead())
                    {
                        Debug.Log("All players died...");
                        Knowledge.Winner = (int)GetWinner();

                        playState = PlayState.toEndScreen;
                        break;
                    }

                    playState = PlayState.moveCamera;
                }

                break;

            case PlayState.moveCamera:

                Debug.Log("moveCamera");

                lastPlayer = currentPlayer;
                currentPlayer = nextPlayer;

                cameraController.ResetPlayerCamera(lastPlayer.currentPlayer);

                switch (currentPlayer.currentPlayer)
                {
                    case Player.CurrentPlayer.p1:
                        cameraController.animator.SetBool("to1", true);
                        Debug.Log("Current player = 1");
                        break;
                    case Player.CurrentPlayer.p2:
                        cameraController.animator.SetBool("to2", true);
                        Debug.Log("Current player = 2");
                        break;
                    case Player.CurrentPlayer.p3:
                        cameraController.animator.SetBool("to3", true);
                        Debug.Log("Current player = 3");
                        break;
                    case Player.CurrentPlayer.p4:
                        cameraController.animator.SetBool("to4", true);
                        Debug.Log("Current player = 4");
                        break;
                }

                playState = PlayState.waitLightOn;

                break;

            case PlayState.toEndScreen:

                // Do nothing else

                break;
        }
    }

    public bool AllPlayersDead()
    {
        uint count = 0;

        for (uint i = 0; i < 4; ++i)
        {
            if (!players[i].active)
                count++;
        }

        return count == numPlayers - 1;
    }

    public uint GetWinner()
    {
        uint winner = 0;

        for (uint i = 0; i < 4; ++i)
        {
            if (players[i].active)
            {
                switch (players[i].currentPlayer) // 1 - blue 2 - red 3 - yellow 4 - green
                {
                    case Player.CurrentPlayer.p1:
                        winner = 4;
                        break;
                    case Player.CurrentPlayer.p2:
                        winner = 2;
                        break;
                    case Player.CurrentPlayer.p3:
                        winner = 3;
                        break;
                    case Player.CurrentPlayer.p4:
                        winner = 1;
                        break;
                }
            }
        }

        return winner;
    }
}