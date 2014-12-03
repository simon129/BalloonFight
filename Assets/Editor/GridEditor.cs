using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

[CustomEditor(typeof(Grid))]
public class GridEditor : Editor
{
	Grid grid;

	public void OnEnable()
	{
		grid = (Grid)target;
	}

	void OnSceneGUI()
	{
		Vector3 pos = Camera.current.transform.position;
		var height = grid.height;
		var width = grid.width;

		for (float y = pos.y - 5.0f; y < pos.y + 5.0f; y += height)
		{
			Handles.DrawLine(new Vector3(-5.0f, Mathf.Floor(y / height) * height, 0.0f),
							new Vector3(5.0f, Mathf.Floor(y / height) * height, 0.0f));
		}

		for (float x = pos.x - 5.0f; x < pos.x + 5.0f; x += width)
		{
			Handles.DrawLine(new Vector3(Mathf.Floor(x / width) * width, -5.0f, 0.0f),
							new Vector3(Mathf.Floor(x / width) * width, 5.0f, 0.0f));
		}

		int controlId = GUIUtility.GetControlID(FocusType.Passive);
		Event e = Event.current;

		if (e.isMouse && (e.type == EventType.MouseDown || e.type == EventType.MouseDrag))
		{
			GUIUtility.hotControl = controlId;
			e.Use();

			Ray ray = Camera.current.ScreenPointToRay(new Vector3(e.mousePosition.x, Camera.current.pixelHeight - e.mousePosition.y));
			Vector3 mousePos = ray.origin;

			int x = Mathf.FloorToInt(mousePos.x / grid.width);
			int y = Mathf.FloorToInt(mousePos.y / grid.height);
			var blockName = GetBlockName(x, y);

			if (e.button == 0)
			{
				var block = grid.transform.Find(blockName);
				if (block == null)
				{
					Undo.IncrementCurrentGroup();

					var prefab = GetGrass();
					block = ((GameObject)PrefabUtility.InstantiatePrefab(prefab)).transform;
					block.SetParent(grid.transform);
					block.name = blockName;
					block.position = new Vector3(x * grid.width + grid.width / 2f, y * grid.height + grid.height / 2f);

					Undo.RegisterCreatedObjectUndo(block.gameObject, "Create " + block.name);
				}
			}
			else if (e.button == 1)
			{
				Transform t = grid.transform.Find(blockName);
				while (t != null)
				{
					Undo.DestroyObjectImmediate(t.gameObject);
					t = grid.transform.Find(blockName);
				}
			}
		}

		if (e.isMouse && e.type == EventType.MouseUp)
			GUIUtility.hotControl = 0;
	}

	private string GetBlockName(int x, int y)
	{
		return string.Format("Block_{0}-{1}", x, y);
	}

	private GameObject GetGrass()
	{
		return grid.GrassPrefabs[Random.Range(0, grid.GrassPrefabs.Length)];
	}
}