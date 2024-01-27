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
    #endregion


    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Hook(BobberManager boober)
    {
        if(_spriteRenderer.sprite != _closed)
        {
            _spriteRenderer.sprite = _closed;
            HorrorBehaviour.Instance.LoseEye();
        }
    }


}
