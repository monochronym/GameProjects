using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Check : MonoBehaviour
{
    [Header("Buttons1")]
    [SerializeField] public Button[] buttons1 = new Button[6];
    
    [Header("Buttons2")]
    [SerializeField] public Button[] buttons2 = new Button[6];

    [Header("Texts1")]
    [SerializeField] public Text[] texts1 = new Text[6];
    
    [Header("Texts2")]
    [SerializeField] public Text[] texts2 = new Text[6];

    [Header("SpecialText")]
    [SerializeField] public Text textResult;

    [Header("ElementName")]
    [SerializeField] public Text nameElem;

    [Header("GreenPosion")]
    [SerializeField] public GameObject[] positions = new GameObject[10];

    [Header("Finish")]
    [SerializeField] public GameObject finish;

    [Header("Canvas1")]
    [SerializeField] public GameObject canvas1;

    [Header("Canvas2")]
    [SerializeField] public GameObject canvas2;
    private int index = 0;
    private Dictionary<string, string[]> possibleElements = new Dictionary<string, string[]>();
    private List<string> possiblePart1 = new List<string>();
    private List<string> possiblePart2 = new List<string>();
    private int[] positionsElems = new int[2] { 0, 0 };
    private List<int> usedIndexElems = new List<int>() { 0 };
    public void CheckElements()
    {
        if (possibleElements.Count == 0) GetPossibleElements();
        var elem1 = GetActiveButton(buttons1, texts1);
        var elem2 = GetActiveButton(buttons2, texts2);
        if (elem1 == null || elem2 == null)
        {
            textResult.text = "�������� ��������";
        }
        else if (elem1.text == possibleElements[nameElem.text][0] && elem2.text == possibleElements[nameElem.text][1])
        {
            textResult.text = "�����";
            Return();
            Change();
            positions[index].gameObject.SetActive(true);
            index++;
            if (index >= 10)
            {
                finish.gameObject.SetActive(true);
                canvas1.gameObject.SetActive(false);
                canvas2.gameObject.SetActive(false);
            }
        }
        else
        {
            textResult.text = "�������";
            Return();
        }
    }
    private void Change()
    {
        if (possibleElements.Count == 0) GetPossibleElements();
        if (possiblePart1.Count == 0) GetPossiblePart1();
        if (possiblePart2.Count == 0) GetPossiblePart2();
        var elems1 = texts1.Select(text => text.text = "").ToList();
        var elems2 = texts2.Select(text => text.text = "").ToList();
        var elems = possibleElements.Keys.ToList();
        var indexElem = UnityEngine.Random.Range(0, elems.Count());
        while (usedIndexElems.Contains(indexElem))
            indexElem = UnityEngine.Random.Range(0, elems.Count());
        usedIndexElems.Add(indexElem);
        nameElem.text = elems[indexElem];
        positionsElems[0] = UnityEngine.Random.Range(0, texts1.Length);
        positionsElems[1] = UnityEngine.Random.Range(0, texts2.Length);
        for (int i = 0; i < texts1.Length; i++)
        {
            if (i == positionsElems[0])
            {
                texts1[i].text = possibleElements[nameElem.text][0];
                elems1.Add(possibleElements[nameElem.text][0]);
            }
            else
            {
                var j = UnityEngine.Random.Range(0, possiblePart1.Count);
                while (elems1.Contains(possiblePart1[j]) || possiblePart1[j].Equals(possibleElements[nameElem.text][0]))
                    j = UnityEngine.Random.Range(0, possiblePart1.Count);
                elems1.Add(possiblePart1[j]);
                texts1[i].text = possiblePart1[j];
            }
            if (i == positionsElems[1])
            {
                texts2[i].text = possibleElements[nameElem.text][1];
                elems2.Add(possibleElements[nameElem.text][1]);
            }
            else
            {
                var j = UnityEngine.Random.Range(0, possiblePart2.Count);
                while (elems2.Contains(possiblePart2[j]) || possiblePart2[j].Equals(possibleElements[nameElem.text][1]))
                    j = UnityEngine.Random.Range(0, possiblePart2.Count);
                elems2.Add(possiblePart2[j]);
                texts2[i].text = possiblePart2[j];
            }
        }
    }

    private Text GetActiveButton(Button[] buttons, Text[] texts)
    {
        if (buttons.Where(b => b.interactable == true).Count() != 1) return null;
        var index = Array.IndexOf(buttons, buttons.Where(b => b.interactable == true).FirstOrDefault());
        return texts[index];
    }

    private void Return()
    {
        var positions = GetPositions();
        for (int i = 0; i < buttons1.Length; i++)
        {
            buttons1[i].transform.localPosition = positions[0][i];
            buttons1[i].interactable = true;
            buttons2[i].transform.localPosition = positions[1][i];
            buttons2[i].interactable = true;
        }
    }

    private Vector2[][] GetPositions()
    {
        var positions = new Vector2[2][];
        positions[0] = new Vector2[6]
        {
            new Vector2(-195, 40),
            new Vector2(-110, 80),
            new Vector2(-25, 40),
            new Vector2(-195, -80),
            new Vector2(-110, -40),
            new Vector2(-25, -80)
        };
        positions[1] = new Vector2[6]
        {
            new Vector2(180, 40),
            new Vector2(265, 80),
            new Vector2(350, 40),
            new Vector2(180, -80),
            new Vector2(265, -40),
            new Vector2(350, -80)
        };
        return positions;
    }

    private void GetPossibleElements()
    {
        possibleElements["����� ��������/����"] = new string[] { "H2", "O" };
        possibleElements["������"] = new string[] { "N", "H3" };
        possibleElements["������ ������"] = new string[] { "Na", "H" };
        possibleElements["������ �������"] = new string[] { "Ca", "H2" };
        possibleElements["������� ��������/���������� ���"] = new string[] { "C", "O2" };
        possibleElements["����� ��������"] = new string[] { "Al2", "O3" };
        possibleElements["����� ������ (lll)"] = new string[] { "Fe2", "O3" };
        possibleElements["����� ������� (lV)"] = new string[] { "Si", "O2" };
        possibleElements["��������� ������"] = new string[] { "Na", "OH" };
        possibleElements["��������� �����"] = new string[] { "K", "OH" };
        possibleElements["��������� �����"] = new string[] { "Li", "OH" };
        possibleElements["��������� ������"] = new string[] { "Mg", "(OH)2" };
        possibleElements["��������� �������"] = new string[] { "Ca", "(OH)2" };
        possibleElements["��������� ������"] = new string[] { "Fe", "(OH)3" };
        possibleElements["�������/��������������� �������"] = new string[] { "H", "Cl" };
        possibleElements["��������������� �������"] = new string[] { "H", "F" };
        possibleElements["��������������� �������"] = new string[] { "H", "Br" };
        possibleElements["�������������� �������"] = new string[] { "H", "I" };
        possibleElements["�������������� �������"] = new string[] { "H2", "S" };
        possibleElements["������� �������"] = new string[] { "H", "NO3" };
        possibleElements["�������� �������"] = new string[] { "H2", "CO3" };
        possibleElements["���������� �������"] = new string[] { "H2", "SiO3" };
        possibleElements["������ �������"] = new string[] { "H2", "SO4" };
        possibleElements["��������� �������"] = new string[] { "H3", "PO4" };
        possibleElements["������ ������"] = new string[] { "Na", "Cl" };
        possibleElements["������� ������"] = new string[] { "Mg", "S" };
        possibleElements["������ ������"] = new string[] { "Na", "NO3" };
        possibleElements["�������� �������"] = new string[] { "Ca", "CO3" };
        possibleElements["������� �����"] = new string[] { "K2", "SO4" };
        possibleElements["������ ��������"] = new string[] { "Al", "PO4" };
    }

    private void GetPossiblePart1()
    {
        possiblePart1.Add("H");
        possiblePart1.Add("H2");
        possiblePart1.Add("H3");
        possiblePart1.Add("N");
        possiblePart1.Add("C");
        possiblePart1.Add("Al");
        possiblePart1.Add("Al2");
        possiblePart1.Add("Na");
        possiblePart1.Add("Ca");
        possiblePart1.Add("K");
        possiblePart1.Add("K2");
        possiblePart1.Add("Si");
        possiblePart1.Add("Fe");
        possiblePart1.Add("Fe2");
        possiblePart1.Add("Mg");
        possiblePart1.Add("Li");
    }

    private void GetPossiblePart2()
    {
        possiblePart2.Add("Cl");
        possiblePart2.Add("F");
        possiblePart2.Add("Br");
        possiblePart2.Add("I");
        possiblePart2.Add("S");
        possiblePart2.Add("H");
        possiblePart2.Add("H2");
        possiblePart2.Add("H3");
        possiblePart2.Add("O");
        possiblePart2.Add("O2");
        possiblePart2.Add("O3");
        possiblePart2.Add("OH");
        possiblePart2.Add("(OH)2");
        possiblePart2.Add("(OH)3");
        possiblePart2.Add("NO3");
        possiblePart2.Add("CO3");
        possiblePart2.Add("SiO3");
        possiblePart2.Add("SO4");
        possiblePart2.Add("PO4");
    }
}
