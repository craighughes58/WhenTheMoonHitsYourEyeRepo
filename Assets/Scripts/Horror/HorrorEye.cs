using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorrorEye : MonoBehaviour, IHookable
{
    #region Serialized Variable
    [Tooltip("The visual that the eye is hit")]
    [SerializeField] private Sprite _closed;

    #endregion
    #region Private Variable
    //
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    #endregion


    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();

    }

    public void Hook(BobberManager bobber)
    {
        if(_spriteRenderer.sprite != _closed)
        {
            bobber.GetComponent<Collider2D>().enabled = false;
            _animator.SetBool("isHurt", true);
            //_spriteRenderer.sprite = _closed;
            
            HorrorBehaviour.Instance.LoseEye();
        }
    }


}
