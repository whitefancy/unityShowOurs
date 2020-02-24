using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//这里GameManger继承了单例，而单例继承了MonoBehavior，所以还是可以实现MonoBehavior的行为
public class GameManager : Singleton<GameManager>
{
    //keep track of the game state
    //需要被GameManger捕捉的预制
    public GameObject[] SystemPrefabs;
    //由上面的预制实例化出的对象实例序列
    private List<GameObject> _instancedSystemPrefabs;
    //单例模式
    private static GameManager instance;
    //当前关卡的名字
    private string _currentLevelName = string.Empty;
    //存放已经加载的关卡
    List<AsyncOperation> _loadOperations;
    //private void awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //    }else
    //    {
    //        destroy(gameobject);
    //        debug.logerror("bad things happen");
    //    }
    //}
    private void Start()
    {
        //当前对象不能被任何进程销毁，保护GameManager
        DontDestroyOnLoad(gameObject);
        //初始化保存scene的list
        _loadOperations = new List<AsyncOperation>();
        //实例化系统对象
        InstantiateSystemPrefabs();
        //加载第一个场景
        LoadLevel("Main");
    }
    void OnLoadOperationComplete(AsyncOperation ao)
    {
        if (_loadOperations.Contains(ao))
        {
            _loadOperations.Remove(ao);
            //dispatch message
            //transition between scenes
        }
        Debug.Log("Load Complete.");
    }
    void OnUnloadOperationComplete(AsyncOperation ao)
    {
        Debug.Log("Unload Complete.");
    }

    //generate other persistent systems
    void InstantiateSystemPrefabs()
    {
        _instancedSystemPrefabs = new List<GameObject>();
        GameObject prefabInstance;
        for(int i = 0; i < SystemPrefabs.Length; ++i)
        {
            //实例化
           prefabInstance= Instantiate(SystemPrefabs[i]);
           _instancedSystemPrefabs.Add(prefabInstance);
        }
    }
    public void LoadLevel(string levelName)
    {
        AsyncOperation ao=SceneManager.LoadSceneAsync(levelName,LoadSceneMode.Additive);
        if (ao == null)
        {
            Debug.LogError("[GameManager] Unable to load level" + levelName);
            return;
        }
        _loadOperations.Add(ao);
        ao.completed += OnLoadOperationComplete;//这是什么用法？
        _currentLevelName = levelName;
    }

    public void UnloadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.UnloadSceneAsync(levelName);
        if (ao == null)
        {
            Debug.LogError("[GameManager] Unable to unload level" + levelName);
            return;
        }
        ao.completed += OnUnloadOperationComplete;
    }
    //C#中的override不是写在@里，而是在域类型的后面
    protected override void OnDestroy()
    {
        //C#需要自己管理指针和内存
        base.OnDestroy();
        for(int i=0;i<_instancedSystemPrefabs.Count;++i)
        {
            Destroy(_instancedSystemPrefabs[i]);
        }
        _instancedSystemPrefabs.Clear();
    }
}
