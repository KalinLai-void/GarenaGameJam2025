using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimController : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void TriggerAttacking()
    {
        _animator.SetTrigger("isAttacking");
    }

    public void SetIsMoveing(bool bIsMoveing)
    {
        _animator.SetBool("isMoving", bIsMoveing);
    }
}
