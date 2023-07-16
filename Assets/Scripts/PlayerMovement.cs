using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rigid;
    PlayerInfo playerInfo;

    private float lastH;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        playerInfo = GetComponent<PlayerInfo>();
    }

    //적과 충돌시
    public void Crash()
    {
        rigid.velocity = Vector2.zero;
        rigid.AddForce(Vector2.down * 100,ForceMode2D.Impulse);
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector2 moveDirection = new Vector2(h * 2, v);

        rigid.AddForce(moveDirection.normalized * Time.deltaTime * playerInfo.moveSpeed * 5000);

        if(h < 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 13), Time.deltaTime * 10);
        }
        if(h > 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, -13), Time.deltaTime * 10);
        }

        if (lastH != h)
        {
            if (h == 0)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * 10);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                lastH = h;
            }
        }
    }
}
