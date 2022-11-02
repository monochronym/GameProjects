using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using System.Linq;

public class chIm : MonoBehaviour
{
    [Header("Sprite")]
    [SerializeField] public SpriteRenderer sprIm;

    [Header("Sprites")] 
    [SerializeField] public SpriteRenderer[] Sprites;

    [Header("Fields")]
    [SerializeField] public InputField[] inputFields;

    [Header("Result")]
    [SerializeField] public Text result;

    [Header("Score")]
    [SerializeField] public Text score;

    [Header("Image")]
    [SerializeField] private Image img;

    [Header("Buttons")]
    [SerializeField] public Button[] buttons;
    
    [Header("Ramps")]
    [SerializeField] public Image[] ramps;

    [Header("Final")]
    [SerializeField] public Image final;

    private List<int> elementsWas = new List<int>();

    public void Check()
    {
        var dict = new Dictionary<string, int[]>();
        dict.Add("H", new int[3] { 1, 0, 1 });
        dict.Add("He", new int[3] { 2, 2, 2 });
        dict.Add("Li", new int[3] { 3, 4, 3 });
        dict.Add("Be", new int[3] { 4, 5, 4 });
        dict.Add("B", new int[3] { 5, 6, 5 });
        dict.Add("C", new int[3] { 6, 6, 6 });
        dict.Add("N", new int[3] { 7, 7, 7 });
        dict.Add("O", new int[3] { 8, 8, 8 });
        dict.Add("F", new int[3] { 9, 10, 9 });
        dict.Add("Ne", new int[3] { 10, 10, 10 });
        dict.Add("Na", new int[3] { 11, 12, 11 });
        dict.Add("Mg", new int[3] { 12, 12, 12 });
        dict.Add("Al", new int[3] { 13, 14, 13 });
        dict.Add("Si", new int[3] { 14, 14, 14 });
        dict.Add("P", new int[3] { 15, 16, 15 });
        dict.Add("S", new int[3] { 16, 16, 16 });
        dict.Add("Cl", new int[3] { 17, 18, 17 });
        dict.Add("Ar", new int[3] { 18, 22, 18 });
        dict.Add("K", new int[3] { 19, 20, 19 });
        dict.Add("Ca", new int[3] { 20, 20, 20 });
        dict.Add("Sc", new int[3] { 21, 24, 21 });
        dict.Add("Ti", new int[3] { 22, 26, 22 });
        dict.Add("V", new int[3] { 23, 28, 23 });
        dict.Add("Cr", new int[3] { 24, 28, 24 });
        dict.Add("Mn", new int[3] { 25, 30, 25 });
        dict.Add("Fe", new int[3] { 26, 30, 26 });
        dict.Add("Co", new int[3] { 27, 32, 27 });
        dict.Add("Ni", new int[3] { 28, 31, 28 });
        dict.Add("Cu", new int[3] { 29, 35, 29 });
        dict.Add("Zn", new int[3] { 30, 35, 30 });
        dict.Add("Ga", new int[3] { 31, 39, 31 });
        dict.Add("Ge", new int[3] { 32, 41, 32 });
        dict.Add("As", new int[3] { 33, 42, 33 });
        dict.Add("Se", new int[3] { 34, 45, 34 });
        dict.Add("Br", new int[3] { 35, 45, 35 });
        dict.Add("Kr", new int[3] { 36, 48, 36 });

        if (!int.TryParse(inputFields[0].text, out int protons)) 
        {
            result.text = "Введите ещё раз";
            return;
        }
        if (!int.TryParse(inputFields[1].text, out int neutrons))
        {
            result.text = "Введите ещё раз";
            return;
        }
        if (!int.TryParse(inputFields[2].text, out int electrons))
        {
            result.text = "Введите ещё раз";
            return;
        }
        string elem = sprIm.name;
        var elems = new int[3] { protons, neutrons, electrons };
        for (int i = 0; i < 3; i++)
        {
            if (dict[elem][i] != elems[i])
            {
                ramps[i].gameObject.SetActive(true);
                buttons[i].gameObject.SetActive(true);
            }
            else
            {
                ramps[i].gameObject.SetActive(false);
                buttons[i].gameObject.SetActive(false);
            }
        }
        if (dict[elem][0] == protons && dict[elem][1] == neutrons && dict[elem][2] == electrons)
        {
            result.text = "Верно!";
            score.text = (int.Parse(score.text) + 1).ToString();
            if (int.Parse(score.text) == 10)
            {
                final.gameObject.SetActive(true);
                return;
            }
            Thread.Sleep(300);
            ChangeSprite();
            inputFields[0].text = "";
            inputFields[1].text = "";
            inputFields[2].text = "";
            result.text = "";
            return;
        }
        result.text = "Подумайте ещё...";
    }

    public void ChangeSprite()
    {
        if (!elementsWas.Contains(1)) elementsWas.Add(1);
        var index = UnityEngine.Random.Range(0, Sprites.Length);
        while (elementsWas.Contains(index))
            index = UnityEngine.Random.Range(0, Sprites.Length);
        elementsWas.Add(index);
        sprIm = Sprites[index];
        img.sprite = Sprites[index].sprite;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }
}
