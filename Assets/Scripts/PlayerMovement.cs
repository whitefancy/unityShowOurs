using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // The m_ part of the non-public member variables comes from them being 'member' variables.
    Vector3 m_Movement;
    //动作
    Animator m_Animator;
    //人物转向速度
    public float turnSpeed = 20f;
    //Create and Store a Rotation 四元数 Quaternion.identity不旋转
    Quaternion m_Rotation = Quaternion.identity;
    public int maxHealth = 5;
    int currentHealth;
    Rigidbody m_Rigidbody;
    Vector3 lookDirection = new Vector3(1, 0,0);
    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    //自定义组件的 Update 方法被调用后，会在屏幕上渲染新图像。这些更新的长度会有所不同，具体取决于计算和渲染的复杂程度。但是，还有另一个单独的循环可以运行所有物理操作。此循环不会改变更新的频率，因此称为 FixedUpdate。
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

        m_Animator.SetBool("IsWalking", isWalking);
        if (isWalking)
        {
            lookDirection.Set(m_Movement.x,m_Movement.y, m_Movement.z);
        }
        //转向 
        //transform是实例的朝向、位置、旋转角度等基本信息 
        //四个参数 首先是原来的位置，目标位置，然后角度的变化（以弧度为单位），然后是大小的变化。
        //常用Time.deltaTime 因为不受帧率控制
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);
        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit[] hit = Physics.RaycastAll(m_Rigidbody.position + Vector3.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit!=null&&hit.Length>0)
            {
                NonPlayerCharacter character = hit[0].collider.GetComponent<NonPlayerCharacter>();
                if (character != null)
                {
                    character.DisplayDialog();
                    UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
                    currentHealth--;
                }
            }
        }

    }
    private void OnAnimatorMove()
    {
        //m_Animator.deltaPosition 会根据帧率调整实际的大小
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation(m_Rotation);
    }
}
