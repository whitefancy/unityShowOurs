using UnityEngine;

public class MainEntrance : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        LoginPage.getInstance().show();
    }
}
