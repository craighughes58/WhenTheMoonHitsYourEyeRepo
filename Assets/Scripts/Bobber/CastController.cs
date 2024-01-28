using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CastController : MonoBehaviour
{
    [HideInInspector]
    public AudioClip castSound;

    [SerializeField] float castSpeed, fixedRotSpeed, torque, maxSpeed;
    BobberManager bobberManager;
    float rotDir;
    Transform transChild;
    bool rotLocked;
    Rigidbody2D rb;

    public static CastController Instance;
    public static bool shootLock;
    private void Awake()
    {
        transChild = transform.GetChild(0).transform;
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    public void SetPlayer(BobberManager bobber, float pointerDist = .45f)
    {
        bobberManager = bobber;
        transChild.localPosition = new Vector2(0, pointerDist);
    }

    public void OnFire(InputValue value)
    {
        if (shootLock) return;
        if(bobberManager == null)
        {
            Debug.Log("Bobber was null :(", gameObject);
            Debug.Break();
            Destroy(gameObject);
            return;
        }

        AudioManager.Instance.PlayClip2D(castSound);

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

    private void FixedUpdate()
    {
        if(rotLocked)
        {
            transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + (-rotDir * fixedRotSpeed * Time.fixedDeltaTime));
            return;
        }
        if(rotDir != 0)
        {
            rb.AddTorque(-rotDir * torque * Time.fixedDeltaTime);
            rb.angularVelocity = Mathf.Clamp(rb.angularVelocity, -maxSpeed, maxSpeed);
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
