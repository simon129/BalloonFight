using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float force = 0.01f;

    Vector3 direction;
    public Vector3 width, height;
    public float w, h;
    public Vector3 one, zero;

	private Controller Player;
    void Start()
    {
        one = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, Camera.main.nearClipPlane));
        zero = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        w = one.x - zero.x;
        h = one.y - zero.y;
        width = Vector3.right * (one.x - zero.x);
        height = Vector3.up * (one.y - zero.y);
        StartCoroutine(Run());
    }

    IEnumerator Run()
    {
        while (true)
        {
            Vector3 screenVeiwport = Camera.main.WorldToViewportPoint(transform.position);
            Vector3 center = Camera.main.ViewportToWorldPoint(Vector3.one * 0.5f);
            center.z = transform.position.z;


            if (screenVeiwport.x > 1)
            {
                rigidbody2D.velocity = Vector3.zero;
                transform.position = transform.position - width;
            }
            else if (screenVeiwport.y > 1)
            {
                rigidbody2D.velocity = Vector3.zero;
                transform.position = transform.position - height;
            }
            else if (screenVeiwport.x < 0)
            {
                rigidbody2D.velocity = Vector3.zero;
                transform.position = transform.position + width;
            }
            else if (screenVeiwport.y < 0)
            {
                rigidbody2D.velocity = Vector3.zero;
                transform.position = transform.position + height;
            }
            else
            {
				if (Player == null)
					Player = GameObject.FindObjectOfType<Controller>();

				if (Player != null)
				{
					direction = Player.transform.position - transform.position;
					direction.z = 0;
				}
				else
				{
					direction = Random.insideUnitCircle;
				}

                rigidbody2D.AddForce(direction.normalized * force, ForceMode2D.Impulse);
                Debug.DrawLine(transform.position, transform.position + direction.normalized * 10, Color.white, 0.5f);
            }

            Debug.Log(transform.position + ", " + screenVeiwport + ", " + center + ", " + direction);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
