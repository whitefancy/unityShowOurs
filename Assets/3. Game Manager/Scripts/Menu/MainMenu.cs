using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Properly transition into the Main Scene.
public class MainMenu : MonoBehaviour
{
    //Track the Animation Component
    //Track the AnimationClips for fade in/out
    //[SerializeField] 自己可以设置，但是别的类看不到
    [SerializeField] private Animation _mainMenuAnimator;
    [SerializeField] private AnimationClip _fadeOutAnimation;
    [SerializeField] private AnimationClip _fadeInAnimation;

    //Function that can receive animation events 调用加载函数
    public void OnFadeOutComplete()
    {
        Debug.LogWarning("FadeOut Complete.");
    }
    public void OnFadeInComplete()
    {
        Debug.LogWarning("FadeIn Complete.");
        //菜单的摄像机激活，看向菜单
        UIManager.Instance.SetDummyCameraActive(true);
    }

    //实现淡入淡出开头的函数
    public void FadeIn()
    {
        _mainMenuAnimator.Stop();
        _mainMenuAnimator.clip = _fadeInAnimation;
        _mainMenuAnimator.Play();
    }
    public void FadeOut()
    {
        _mainMenuAnimator.Stop();
        _mainMenuAnimator.clip = _fadeOutAnimation;
        _mainMenuAnimator.Play();
        //菜单的摄像机消失，出现主界面
        UIManager.Instance.SetDummyCameraActive(false);
    }
}
