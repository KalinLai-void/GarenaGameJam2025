using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Coffee.UIEffectInternal;
using Coffee.UIEffects;

public class ButtonUIMove : MonoBehaviour
{
    public float holdingTimer = 1f;
    private float selectMeTime = 1f;
    private float executeSkillTime = 1.7f;
    private float removeOtherHandsTime = 3.1f;


    public GameObject description;
    public Transform descriptionHolder;
    


    public void ButtonPressed()
    {
        GetComponentInParent<HorizontalLayoutGroup>().enabled = false;


        RemoveOthers();
        Invoke("SelectMe", selectMeTime);
        Invoke("ExecuteSkill", executeSkillTime);
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
                StartCoroutine(HideCard(rt.gameObject, 2.9f));
                Destroy(rt.gameObject, removeOtherHandsTime);
                
                
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

        //Debug.Log("Hi");
        rt.DOMove(new Vector2((float)Screen.width/2, this.transform.position.y), .7f);

        //ExecuteSkill();
    }

    private void ExecuteSkill()
    {
        RectTransform rt = this.GetComponent<RectTransform>();

        Image skillImage = this.GetComponent<Image>();

        //rt.DOMove(new Vector2(rt.position.x, (float)Screen.width/2), 1f);

        

        //rt.DOMove(new Vector2((float)Screen.width / 2, this.transform.position.y), .7f);

        GetComponent<Card>().OnButtonClick();

        Invoke("SelfDissolve", 1f);
        //RemoveMe(1.1f);
    }

    private void SelfDissolve()
    {
        Debug.Log("A");
        var effect = this.gameObject.AddComponent<UIEffect>();

        // Apply a runtime preset
        effect.LoadPreset("Transition-Burn");

        // Set the effect parameters
        effect.transitionWidth = 0.1f;
        effect.transitionColor = Color.red;
        effect.transitionAutoPlaySpeed = 0.7f;
        Invoke("RemoveMe", 1f);
    }

    private void RemoveMe()
    {
        
        GetComponentInParent<HorizontalLayoutGroup>().enabled = true;
        Destroy(this.gameObject); //�ɶ��i�H��
    }



    public void GenerateDescription()
    {
        descriptionHolder = GameObject.Find("NoteHolder").transform;
        if (descriptionHolder.childCount != 0) Destroy(descriptionHolder.GetChild(0).gameObject);
        
        GameObject _description = Instantiate(description, descriptionHolder);

        

    }

    public void RemoveDescription()
    {



        descriptionHolder = GameObject.Find("NoteHolder").transform;
        if (descriptionHolder.childCount != 0) Destroy(descriptionHolder.GetChild(0).gameObject);
    }

   


}
