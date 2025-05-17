using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableGroupHolder : MonoBehaviour
{
    [SerializeField]
    private HorizontalLayoutGroup holder;

    public void DisableHolder()
    {
        holder.enabled = false;
    }
    
}
