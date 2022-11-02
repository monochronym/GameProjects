using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GetNewScene : MonoBehaviour
{
    public GameObject animation;
    public float time;
    public string scene;
    public int sceneIndex;
    public void SceneChanger(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        sceneIndex--;
        if (sceneIndex >=0 ) Class1.bools[sceneIndex] = true;
    }
    private void SetTime()
    {
        if (animation != null)
        {
            if (animation.active && time == 0)
            {
                time = Time.time;
            }
        }
    }
    public void Update()
    {
        SetTime();
        if ((time != 0 ) && (Time.time > time + 4))
            SceneManager.LoadScene(scene);
    }
}
