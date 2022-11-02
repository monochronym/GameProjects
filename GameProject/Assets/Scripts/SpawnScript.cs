using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using TMPro;

public class SpawnScript : MonoBehaviour
{
    public Object gameObject;
    public Transform spawnPoint;
    public TextMeshPro ElementText;
    public TextMeshPro panelText;
    public static int count;
    private System.Random random = new System.Random();
    public static void GetCircleText(TextMeshPro myText,System.Random random) => myText.text = EnumsAndRandomiser.elements[random.Next(0,EnumsAndRandomiser.elements.Count)];
    public static void GetPanelText(TextMeshPro panelText, System.Random random) => panelText.text = EnumsAndRandomiser.elementsInGame[random.Next(0,EnumsAndRandomiser.elementsInGame.Count-1)];
    public static void PlusPoint(TextMeshPro pointsText)
    {
        count++;
        pointsText.text = "Очки: " + count;
    }
    public void Start()
    {
        var startSpawn = spawnPoint.position;
        for (int i = 0; i < 5; i++)
        {
            startSpawn.x += 10;
            startSpawn.y += 10;
            GetCircleText(ElementText, random);
            EnumsAndRandomiser.elementsInGame.Add(EnumsAndRandomiser.elementsDictionary[ElementText.text]);
            Instantiate(gameObject, startSpawn, spawnPoint.rotation);
        }
        GetPanelText(panelText, random);
    }
    //public static void WrongAnswer(Object gameObject,TextMeshPro panelText,System.Random random,Transform spawnPoint,TextMeshPro myText)
    //{
    //    SpawnScript.GetPanelText(panelText, random);
    //    Instantiate(gameObject, spawnPoint.position, spawnPoint.rotation);
    //    SpawnScript.GetCircleText(myText, random);
    //}
}
