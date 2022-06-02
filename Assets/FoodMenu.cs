using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodMenu : MonoBehaviour
{
    public GameObject fMenu;
    public bool menuOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        fMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Menu();
        }
    }

    void Menu()
    {
        if (menuOpen == false)
        {
            menuOpen = true;
            fMenu.SetActive(true);
        } else
        {
            menuOpen = false;
            fMenu.SetActive(false);
        }
        
    }
}
