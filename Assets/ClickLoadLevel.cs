using UnityEngine;
using System.Collections;

public class ClickLoadLevel : MonoBehaviour
{
    public string LevelName = "Game";

    public void Click()
    {
        if (!string.IsNullOrEmpty(LevelName))
            Application.LoadLevel(LevelName);
    }
}
