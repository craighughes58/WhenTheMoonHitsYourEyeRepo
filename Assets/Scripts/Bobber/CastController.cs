using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CastController : MonoBehaviour
{
    [SerializeField] float castSpeed, rotSpeed;
    BobberManager bobberManager;
    float rotDir;
    Transform transChild;
    bool rotLocked;

    public static CastController Instance;
    private void Awake()
    {
        transChild = transform.GetChild(0).transform;
        if(Instance == null)
        {
            Instance = this;
        }
    }


    public void SetPlayer(BobberManager bobber, float pointerDist = .45f)
    {
        bobberManager = bobber;
        transChild.localPosition = new Vector2(0, pointerDist);
    }

    public void OnFire(InputValue value)
    {
        if(bobberManager == null)
        {
            Destroy(gameObject);
            return;
        }
        bobberManager.transform.position = transChild.position;
        bobberManager.GetComponent<Rigidbody2D>().velocity = transform.up * castSpeed;
        bobberManager.transform.up = transform.up;
        rotLocked = false;
        Destroy(gameObject);
    }

    void OnAim(InputValue value)
    {
        if (rotLocked) return;
        rotDir = value.Get<float>();
    }

    private void Update()
    {
        if(rotDir != 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + (-rotDir * rotSpeed * Time.deltaTime));
        }
    }

    public void ForceRotDir(int dir)
    {
        rotDir = Mathf.Sign(dir);
        rotLocked = true;
    }

    public float GetCastSpeed()
    {
        return castSpeed;
    }

}
