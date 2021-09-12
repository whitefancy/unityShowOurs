using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginPage
{
    public static LoginPage Instance;
    public static LoginPage getInstance()
    {
        if (Instance == null)
        {
            Instance = new LoginPage();
        }
        return Instance;
    }
    private bool Inited;
    internal void init()
    {
        GameObject Prefab = (GameObject)Resources.Load("LoginCanvas");
        GameObject loginPanel = MainEntrance.Instantiate(Prefab);
        Button button = loginPanel.transform.Find("enter").GetComponent<Button>();
        button.onClick.AddListener(enter);
        Inited = true;
    }
    public void show()
    {
        if (!Inited)
        {
            init();
        }

    }

    public void ClickHandler(string tag)
    {
    } 
    private void enter()
    {
        GamePage.getInstance().init();
    }
}
