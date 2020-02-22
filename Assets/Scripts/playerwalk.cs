using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerwalk : MonoBehaviour
{
    Vector3 m_Movement;
    //人物转向速度
    public float turnSpeed = 20f;
    //Create and Store a Rotation 四元数 Quaternion.identity不旋转
    Quaternion m_Rotation = Quaternion.identity;

    Rigidbody m_Rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();
        //键盘引起的移动是大于0的
        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        //转向 
        //transform是实例的朝向、位置、旋转角度等基本信息 
        //四个参数 首先是原来的位置，目标位置，然后角度的变化（以弧度为单位），然后是大小的变化。
        //常用Time.deltaTime 因为不受帧率控制
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);
    }
}
