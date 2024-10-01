using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public GameObject prefabArrow;
    public float speedMove;

    private bool isRage;


    void Update()
    {
        Move();
        Check();
    }

    public void Active(bool isRage)
    {
        this.isRage = isRage;
    }

    void Move()
    {
        float speed = isRage ? speedMove : (speedMove * 2f);
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    void Check()
    {
        // Lấy vị trí của đối tượng trên viewport (vị trí tương đối với màn hình)
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);

        // Nếu đối tượng ra khỏi màn hình theo bất kỳ trục nào
        if (viewPos.x < 0 || viewPos.x > 1 || viewPos.y < 0 || viewPos.y > 1)
        {
            Destroy(gameObject);  // Hủy đối tượng khi nó ra khỏi màn hình
        }
    }
}
