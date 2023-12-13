using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IchigoAnimation : MonoBehaviour
{
    [SerializeField] Animator animator = null;
    [SerializeField] Ichigo player = null;

    private void Awake()
    {
        player.OnMove += UpdateForwardAnimation;
        player.OnMove += UpdateForwardAnimationWithBomb;
        player.OnShoot += UpdateShoot;
        player.OnThrow += UpdateThrow;
        player.OnDrop += UpdateDrop;
    }

    void UpdateForwardAnimation(float _axis)
    {
        animator.SetFloat(AnimatorParams.SPEED_PARAM, _axis, .2f, Time.deltaTime);
    }

    void UpdateForwardAnimationWithBomb(float _axis)
    {
        animator.SetBool(AnimatorParams.HASBOMB_PARAM, player.HasBomb);
        animator.SetFloat(AnimatorParams.SPEED_PARAM, _axis, .2f, Time.deltaTime); 
    }

    void UpdateShoot(bool  _shoot)
    {
        animator.SetBool(AnimatorParams.SHOOT_PARAM, _shoot);
    }

    void UpdateThrow(bool _shoot)
    {
        animator.SetBool(AnimatorParams.THROW_PARAM, _shoot);

    }

    void UpdateDrop(bool _shoot)
    {
        animator.SetBool(AnimatorParams.DROP_PARAM, _shoot);

    }
}
