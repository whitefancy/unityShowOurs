using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    //您希望能够从任何其他脚本访问我们的UIHealthBar 脚本而无需引用。
    //将该特定设置称为Singleton的原因，因为只能存在该类型的一个对象。这正是您想要的：仅拥有一个Health bar 。
    public static UIHealthBar instance { get; private set; }

    public Image mask;
    float originalSize;
    //然后在您的Awake函数中（请记住，一旦创建对象即会调用此方法，这是游戏开始时的情况），您需要在静态实例中存储this ，这是一个特殊的C＃关键字，表示“当前对象运行该功能”
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        originalSize = mask.rectTransform.rect.width;
    }
    
    public void SetValue(float value)
    {
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }
}
