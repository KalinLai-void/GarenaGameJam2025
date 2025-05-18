using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class NoteSystem : MonoBehaviour
{

    public Image icon;
    public TMP_Text title;
    public TMP_Text subTitle;
    public TMP_Text description;


    public List<Skills> skillContainer;

    public void SetUp(CardTypeData data)
    {

    }

}
[System.Serializable]
public class Skills
{
    public CardType skillType;

    public Sprite skillIcon;
    public string title;
    public int hpCost;
    public string descriptionText;

}
