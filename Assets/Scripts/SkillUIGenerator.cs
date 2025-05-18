using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkillUIGenerator : MonoBehaviour
{
    //[SerializeField]
    //private 


    [SerializeField]
    private Transform skillHolder;

    [SerializeField]
    private List<GameObject> skills;


    public void GenerateSkill(CardTypeData data, int cardId)
    {
        //Get Skill from Pacific Code
        //List<Card> cards = Find

        //checktype
        
        foreach (var _skill in skills)
        {
            if (_skill.GetComponent<Card>().cardTypeData.cardType == data.cardType)
            {
                GameObject _skillButton = Instantiate(_skill, skillHolder);
                Card card = _skillButton.GetComponent<Card>();
                card.cardTypeData.cardType = data.cardType;
                card.cardTypeData.moveBlock = data.moveBlock;
                card.cardId = cardId;
                GetComponent<GameManager>().PushCard(card);
                if (data.cardType == CardType.Move)
                {
                    _skillButton.GetComponent<Card>().cardTypeData.moveBlock = data.moveBlock;

                    _skillButton.transform.GetChild(0).GetComponent<TMP_Text>().text = data.moveBlock.ToString();
                }
            }
        } 
    }
}
