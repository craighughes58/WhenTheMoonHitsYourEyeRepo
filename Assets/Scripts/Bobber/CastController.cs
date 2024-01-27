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

    private void Awake()
    {
        transChild = transform.GetChild(0).transform;
    }


    public void SetPlayer(BobberManager bobber, float pointerDist = .45f)
    {
        bobberManager = bobber;
        transChild.localPosition = new Vector2(0, pointerDist);
    }

    public void OnFire(InputValue value)
    {
        bobberManager.transform.position = transChild.position;
        bobberManager.GetComponent<Rigidbody2D>().velocity = transform.up * castSpeed;
        Destroy(gameObject);
    }

    void OnAim(InputValue value)
    {
        rotDir = value.Get<float>();
       
    }

    private void Update()
    {
        if(rotDir != 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + (-rotDir * rotSpeed * Time.deltaTime));
        }
    }
}
