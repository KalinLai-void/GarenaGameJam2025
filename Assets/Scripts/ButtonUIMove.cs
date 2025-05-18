using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonUIMove : MonoBehaviour
{
    public float holdingTimer = 1f;


    public void ButtonPressed()
    {
        GetComponentInParent<HorizontalLayoutGroup>().enabled = false;


        RemoveOthers();
        Invoke("SelectMe", 1f);
        Invoke("ExecuteSkill", 2.7f);
        //Invoke("SelectMe", 2f);
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
                
                rt.DOMove(new Vector2(rt.position.x, -140), 3f);
                StartCoroutine(HideCard(rt.gameObject, 3f));
                Destroy(rt.gameObject, 3.1f);
                
                
            }
        }
    }

    private IEnumerator HideCard(GameObject obj, float secs)
    {
        yield return new WaitForSeconds(secs);
        obj.SetActive(false);
    }

    private void SelectMe()
    {
        RectTransform rt = GetComponent<RectTransform>();

        rt.DOMove(new Vector2((float)Screen.width/2, this.transform.position.y), .7f);

        ExecuteSkill();
    }

    private void ExecuteSkill()
    {
        RectTransform rt = this.GetComponent<RectTransform>();

        Image skillImage = this.GetComponent<Image>();

        rt.DOMove(new Vector2(rt.position.x, (float)Screen.width/2), 1f);

        GetComponent<Card>().OnButtonClick();
        RemoveMe(1.1f);
    }

    private void RemoveMe(float destroyTime = 1f)
    {
        GetComponentInParent<HorizontalLayoutGroup>().enabled = true;
        Destroy(this.gameObject, destroyTime); //�ɶ��i�H��
    }

   


}
