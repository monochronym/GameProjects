using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewScene : MonoBehaviour
{
    public string Name;
    public Button button;
    public void GetNextRoom(string name)
    { 
        if (Class1.Check())
            SceneManager.LoadScene(Name);
    }
    public void Update()
    {
        if (Class1.Check()) button.enabled = true;
    }
}
