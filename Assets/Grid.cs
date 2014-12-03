using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour
{
    public float width = 32.0f;
    public float height = 32.0f;

    public Color color = Color.white;

	public GameObject[] GrassPrefabs;

    void OnDrawGizmos()
    {
		return;

        Vector3 pos = Camera.current.transform.position;
        Gizmos.color = color;

        for (float y = pos.y - 5.0f; y < pos.y + 5.0f; y += height)
        {
            Gizmos.DrawLine(new Vector3(-5.0f, Mathf.Floor(y / height) * height, 0.0f),
                            new Vector3(5.0f, Mathf.Floor(y / height) * height, 0.0f));
        }

        for (float x = pos.x - 5.0f; x < pos.x + 5.0f; x += width)
        {
            Gizmos.DrawLine(new Vector3(Mathf.Floor(x / width) * width, -5.0f, 0.0f),
                            new Vector3(Mathf.Floor(x / width) * width, 5.0f, 0.0f));
        }
    }
}
