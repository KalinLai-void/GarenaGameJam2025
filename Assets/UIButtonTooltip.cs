using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //[SerializeField] private string tooltipText;
    //private Tooltip tooltip;

    void Start()
    {
        //tooltip = FindObjectOfType<Tooltip>();   // �����@�Τ@�� Tooltip
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<ButtonUIMove>().GenerateDescription();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<ButtonUIMove>().RemoveDescription();
    }
}
