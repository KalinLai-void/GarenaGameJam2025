using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUIGenerator : MonoBehaviour
{
    //[SerializeField]
    //private 


    [SerializeField]
    private Transform skillHolder;

    [SerializeField]
    private List<GameObject> skills;


    public void GenerateSkill(CardTypeData data )
    {
        //Get Skill from Pacific Code
        //List<Card> cards = Find

        //checktype

        foreach(var _skill in skills) 
        {
            if(_skill.GetComponent<Card>().cardTypeData.cardType == data.cardType)
            {
                 GameObject _skillButton = Instantiate(_skill , skillHolder);
                 if(data.cardType == CardType.Move)
                 {
                     _skillButton.GetComponent<Card>().cardTypeData.moveBlock = data.moveBlock;
                 }
            }    
        } 
    }
}
