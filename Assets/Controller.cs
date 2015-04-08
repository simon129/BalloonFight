using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour
{
    private float force = 0.1f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var inputPos = Input.mousePosition;
            inputPos.z = 10;

            var worldPos = Camera.main.ScreenToWorldPoint(inputPos);
            rigidbody2D.AddForce((worldPos - transform.position) * force, ForceMode2D.Impulse);

            //Debug.LogWarning(Input.mousePosition + ", (" + worldPos.x + "," + worldPos.y + "), (" + transform.position.x + "," + transform.position.y + ")");
            //Debug.Log(worldPos.x > transform.position.x + ", " + worldPos.y > transform.position.y);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("OnCollisionEnter");
        rigidbody2D.AddForce((collision.transform.position - transform.position) * force * 5, ForceMode2D.Impulse);
    }
}