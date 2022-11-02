using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class elemBehaviour : MonoBehaviour
{
    [Header("Element1")]
    [SerializeField] public Button elem1;

    [Header("Position")]
    [SerializeField] public int[] position;
    
    [Header("Element2")]
    [SerializeField] public Button elem2;    
    
    [Header("Element3")]
    [SerializeField] public Button elem3;    
    
    [Header("Element4")]
    [SerializeField] public Button elem4;    
    
    [Header("Element5")]
    [SerializeField] public Button elem5;
    
    [Header("Element6")]
    [SerializeField] public Button elem6;
    public void Replace()
    {
        if (!elem1.transform.localPosition.Equals(new Vector2(55, 0)))
        {
            elem1.transform.localPosition = new Vector2(55, 0);
            elem2.interactable = false;
            elem3.interactable = false;
            elem4.interactable = false;
            elem5.interactable = false;
            elem6.interactable = false;
        }
        else
        {
            elem1.transform.localPosition = new Vector2(position[0], position[1]);
            elem2.interactable = true;
            elem3.interactable = true;
            elem4.interactable = true;
            elem5.interactable = true;
            elem6.interactable = true;
        }
    }

    public void ReplaceFromRight()
    {
        if (!elem1.transform.localPosition.Equals(new Vector2(105, 0)))
        {
            elem1.transform.localPosition = new Vector2(105, 0);
            elem2.interactable = false;
            elem3.interactable = false;
            elem4.interactable = false;
            elem5.interactable = false;
            elem6.interactable = false;
        }
        else
        {
            elem1.transform.localPosition = new Vector2(position[0], position[1]);
            elem2.interactable = true;
            elem3.interactable = true;
            elem4.interactable = true;
            elem5.interactable = true;
            elem6.interactable = true;
        }
    }
}
