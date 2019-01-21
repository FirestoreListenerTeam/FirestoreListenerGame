using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {

    private GameObject mainMenu;
    private GameObject playerSubMenu;

    private void Start()
    {
        mainMenu = transform.Find("MainMenu").gameObject;
        playerSubMenu = transform.Find("PlayersMenu").gameObject;
    }

    public void OnPlayClicked()
    {
        mainMenu.SetActive(false);
        playerSubMenu.SetActive(true);
    }

    public void OnCreditsClicked()
    {

    }

    public void OnExitClicked()
    {

    }
}
