using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCWalk : MonoBehaviour
{
    //控制在x方向还是y方向移动
    public bool vertical;
    Rigidbody rigidbody;
    public float speed = 3.0f;
    public float changeTime = 3.0f;
    float timer;
    int direction = 1;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        timer = changeTime;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
        Vector3 position = rigidbody.position;
        if (vertical)
        {
            position.z = position.z + direction * speed * Time.deltaTime;
        }
        else
        {
            position.x = position.x + direction * speed * Time.deltaTime;
        }
        //使用刚体的位置来设置，刚体碰撞产生的抖动就不会出现
        rigidbody.MovePosition(position);
    }
}
