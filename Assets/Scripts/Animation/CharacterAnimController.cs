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

    public void SetIsAttacking(bool bIsAttacking)
    {
        _animator.SetBool("isAttacking", bIsAttacking);
    }

    public void SetIsMoveing(bool bIsMoveing)
    {
        _animator.SetBool("isMoving", bIsMoveing);
    }
}
