using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//T是模板类 可以直接用 例如当做GameManager
//这里，就是创建一个T类型的单例
// extends 继承MonoBehaviour
//要求这个T 必须是Singleton类型的 这可以防止把某些不是单例的类创建为单例的错误
public class Singleton<T> : MonoBehaviour where T:Singleton<T>
{
    private static T instance;
    public static T Instance
    {
        get { return instance; }
        //没有set方法，单例
    }
    public static bool IsInitialized
    {
        get { return instance != null; }
    }
    //可以被子类看到的虚函数，虚函数只在子类的实例中实现，不能被重载
    protected virtual void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("[Singleton]Tring to instantiate a second instance of a singleton class.");
        }else
        {
            instance = (T)this;//确保是T类型
        }
    }
    //单例的销毁方法也是私有的
    protected virtual void OnDestroy()
    {
        if(instance==this)
        {
            instance = null;
        }
    }
}
 