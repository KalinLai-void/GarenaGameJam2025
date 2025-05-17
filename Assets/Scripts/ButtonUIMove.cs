using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonUIMove : MonoBehaviour
{
    public void ButtonPressed()
    {
        GetComponentInParent<HorizontalLayoutGroup>().enabled = false;


        RemoveOthers();
        Invoke("SelectMe", 1f);
        Invoke("SelectMe", 2f);
    }

    private void RemoveOthers()
    {
        GameObject[] skillButtons = GameObject.FindGameObjectsWithTag("SkillButtons");

        foreach(var buttons in skillButtons)
        {
            if(buttons != this.gameObject)
            {
                RectTransform rt = buttons.GetComponent<RectTransform>();

                Image skillImage = buttons.GetComponent<Image>();
                
                skillImage.DOColor(new Color((skillImage.color.r + skillImage.color.g + skillImage.color.b) / 3
                    , (skillImage.color.r + skillImage.color.g + skillImage.color.b) / 3
                    , (skillImage.color.r + skillImage.color.g + skillImage.color.b) / 3
                    ), 0.5f);
                

                rt.DOMove(new Vector2(rt.position.x, -340), 7f);
                Destroy(rt.gameObject, 7f);
            }
        }
    }

    private void SelectMe()
    {
        RectTransform rt = GetComponent<RectTransform>();

        rt.DOMove(new Vector2(284, this.transform.position.y), .7f);
    }

    private void ExecuteSkill()
    {

    }

   


}
