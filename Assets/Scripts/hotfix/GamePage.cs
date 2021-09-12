
using UnityEngine;
using UnityEngine.UI;

public class GamePage 
{
    public static GamePage Instance;
    public static GamePage getInstance()
    {
        if (Instance == null)
        {
            Instance = new GamePage();
        }
        return Instance;
    }
    internal void init()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scene2");
        GameObject Prefab = (GameObject)Resources.Load("GameCanvas");
        GameObject loginPanel = MainEntrance.Instantiate(Prefab);
        Button button = loginPanel.transform.Find("Exit").GetComponent<Button>();
        button.onClick.AddListener(ExistGame);
    }

    public void ExistGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scene1");
    }
}
