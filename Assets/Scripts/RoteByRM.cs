using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoteByRM : MonoBehaviour
{
    public float rotateSpeed = 100;       //设置旋转的速度
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            float nor1 = Input.GetAxis("Mouse X");//获取鼠标的偏移量
            float nor2 = Input.GetAxis("Mouse Y");//获取鼠标的偏移量
            transform.Rotate( Vector3.up, Time.deltaTime * rotateSpeed * nor1);//每帧旋转空物体，相机也跟随旋转
            transform.Rotate(Vector3.forward, Time.deltaTime * rotateSpeed * nor2);//每帧旋转空物体，相机也跟随旋转s
        }
    }
}
