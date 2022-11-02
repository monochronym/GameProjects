using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CircleDelete : MonoBehaviour
{
    public GameObject circle;
    public TextMeshPro panelText;
    public Transform spawnPoint;
    public System.Random random = new System.Random();
    public TextMeshPro pointsText;
    public GameObject endPanel;
    private void OnMouseDown()
    {
        TextMeshPro myText = gameObject.GetComponentInChildren<TextMeshPro>();
        if (EnumsAndRandomiser.elementsDictionary[myText.text].Equals(panelText.text))
        {
            EnumsAndRandomiser.elementsInGame.Remove(EnumsAndRandomiser.elementsDictionary[myText.text]);
            SpawnScript.PlusPoint(pointsText);
            Destroy(gameObject);
            try { SpawnScript.GetPanelText(panelText, random); }
            catch 
            {
                endPanel.SetActive(true);
            }
        }
        else
        {
            SpawnScript.GetPanelText(panelText, random);
            SpawnScript.GetCircleText(circle.GetComponentInChildren<TextMeshPro>(), random);
            EnumsAndRandomiser.elementsInGame.Add(EnumsAndRandomiser.elementsDictionary[circle.GetComponentInChildren<TextMeshPro>().text]);
            Instantiate(circle, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
