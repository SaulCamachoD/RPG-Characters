using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField] protected float velocidad = 5f;
    [SerializeField] protected float velocidadAtaque = 1f;
    [SerializeField] private float sensibilidadRotacion = 600f;
    [SerializeField] private float impulso = 5f;
    [SerializeField] private Animator animator;
    [SerializeField] protected float velX, velY;
    [SerializeField] private ParticleSystem DirtFoots;
    private bool isAction;

    private Rigidbody rb;
    public float lastDodgeTime = -1f;
    public float doubleTap = 0.2f;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        lastDodgeTime = -1f;
        isAction = false;
        DirtFoots.Stop();
    }

    protected virtual void FixedUpdate()
    {
        velX = Input.GetAxis("Horizontal");
        velY = Input.GetAxis("Vertical");
        Movement(velocidad);
    }

    private void Update()
    {
        AttacckAnimations();
        if (!isAction)
        {
            HandleDodge();
            WalkAnimations();
            DisrtyFoots();
        }
    }


    protected virtual void Movement(float velocidad)
    {
        if (!isAction)
        {

            Vector3 movimiento = new Vector3(velX, 0f, velY).normalized;
            if (velY < 0)
            {
                rb.MovePosition(rb.position + transform.TransformDirection(movimiento) * velocidad / 2 * Time.fixedDeltaTime);
            }
            else
            {
                rb.MovePosition(rb.position + transform.TransformDirection(movimiento) * velocidad * Time.fixedDeltaTime);
            }

            float rotacion = velX * sensibilidadRotacion * Time.deltaTime * 0.5f;
            transform.Rotate(0f, rotacion, 0f);

        }
    }
    private void WalkAnimations()
    {
        animator.SetFloat("VelX", velY);
        animator.SetFloat("VelY", velX);
    }

    private void AttacckAnimations()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isAction = true;
            animator.SetTrigger("Attack3");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isAction = true;
            animator.SetTrigger("BlockAttack");
        }

        if (Input.GetMouseButtonDown(0))
        {
            isAction = true;
            animator.SetTrigger("Attack1");
        }

        if (Input.GetMouseButtonDown(1))
        {
            isAction = true;
            animator.SetTrigger("Attack2");
        }

        if ( Input.GetKey(KeyCode.Q))
        {
            animator.SetBool("Block", true);
            isAction = true;
        }

        else
        {
            animator.SetBool("Block", false);
            isAction = false;
        }
    }
    private void HandleDodge()
    {
        if (Input.GetKeyUp(KeyCode.S))
        {
            if (Time.time - lastDodgeTime < doubleTap)
            {
                ImpulseBackDodge();
                animator.SetTrigger("DodgeAft");
                lastDodgeTime = Time.time;
            }
            else
            {
                lastDodgeTime = Time.time;
            }
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            if (Time.time - lastDodgeTime < doubleTap)
            {
                ImpulseLeftDodge();
                animator.SetTrigger("DodgeLeft");
                lastDodgeTime = Time.time;
            }
            else
            {
                lastDodgeTime = Time.time;
            }
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            if (Time.time - lastDodgeTime < doubleTap)
            {
                ImpulseRightDodge();
                animator.SetTrigger("DodgeRight");
                lastDodgeTime = Time.time;
            }
            else
            {
                lastDodgeTime = Time.time;
            }
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            if (Time.time - lastDodgeTime < doubleTap)
            {
                ImpulseForwardDodge();
                animator.SetTrigger("DodgeFwd");
                lastDodgeTime = Time.time;
            }
            else
            {
                lastDodgeTime = Time.time;
            }
        }
    }

    public void ImpulseBackDodge()
    {
        rb.AddForce(-transform.forward * impulso, ForceMode.Impulse);
    }

    public void ImpulseRightDodge()
    {
        rb.AddForce(transform.right * impulso, ForceMode.Impulse);
    }

    public void ImpulseLeftDodge()
    {
        rb.AddForce(-transform.right * impulso, ForceMode.Impulse);
    }

    public void ImpulseForwardDodge()
    {
        rb.AddForce(transform.forward * impulso, ForceMode.Impulse);
    }

    public void ResetIsAction()
    {
        isAction = false;
    }

    public void DisrtyFoots()
    {
        if (velX < -0.6 || velX > 0.6 || velY < -0.6 || velY > 0.6)
        {
            DirtFoots.Play();
        }
    }
}
