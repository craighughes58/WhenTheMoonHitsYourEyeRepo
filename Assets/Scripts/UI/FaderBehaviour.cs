using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaderBehaviour : MonoBehaviour
{
    #region Private Variables

    #region Serialized Variables
    [Tooltip("The reference to the sprite renderer on the vignette")]
    [SerializeField] private SpriteRenderer _vignette;
    [Tooltip("How fast the vignette fades in and out")]
    [SerializeField] private float _fadingSpeed;
    [Tooltip("How much darker/lighter the vignette gets over time")]
    [SerializeField] private float _fadingAmount;
    [Tooltip("Represents if this is a menu scene")]
    [SerializeField] private bool _isMenu;
    [Header("CUTSCENES")]
    [Tooltip("Represents if this just a scene meant to be watched and then moved on from")]
    [SerializeField] private bool _isCutScene;
    [Tooltip("The name of the scene the cutscene switches to")]
    [SerializeField] private string _sceneAfterCutscene;
    [Tooltip("How much time the cutscene lasts")]
    [SerializeField] private float _sceneLength;
    [Tooltip("The reference to the menu controller in the scene")]
    [SerializeField] private MainMenuController _menCon;


    public static FaderBehaviour Instance;
    #endregion
    #endregion


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (!_isMenu)
        {
            ChangeVignette();
        }
        if (_isCutScene)
        {
            StartCoroutine(LeaveCutscene());
        }
    }


    /// <summary>
    /// 
    /// </summary>
    public void ChangeVignette()
    {
        if (_vignette.color.a <= 0)
        {
            StartCoroutine(FadeOut());
        }
        else
        {
            StartCoroutine(FadeIn());
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private IEnumerator FadeIn()
    {
        while (_vignette.color.a > 0)
        {
            _vignette.color = new Color(_vignette.color.r, _vignette.color.g, _vignette.color.b, _vignette.color.a - _fadingAmount);
            yield return new WaitForSeconds(_fadingSpeed);
        }

    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private IEnumerator FadeOut()
    {
        while (_vignette.color.a < 1)
        {
            _vignette.color = new Color(_vignette.color.r, _vignette.color.g, _vignette.color.b, _vignette.color.a + _fadingAmount);
            yield return new WaitForSeconds(_fadingSpeed);
        }
    }

    private IEnumerator LeaveCutscene()
    {
        yield return new WaitForSeconds(_sceneLength);
        _menCon.MoveScene(_sceneAfterCutscene);
    }
}
