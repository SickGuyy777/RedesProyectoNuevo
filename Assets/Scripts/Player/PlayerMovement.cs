using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float CurrentLife;
    float MaxLife=10;
    //UI del arma
    public bool isFiring;
    public Text UIBullets;
    public Text UIBulletsAllChargers;
    int BulletinCharger;
    public int MaxBulletinCharger;
    public int AllCharger = 180;
    //coldDown
    public float shootRate;
    public float ShootRateTime = 0;
    [SerializeField] Transform PosShoot;
    [SerializeField] LookCam cam;
    //[SerializeField] Animator anim;
    [Header("Movement")]
    [SerializeField] Animator animpl;
    public float speed=6f;
    public float MovementMultipler = 10f;
    public float ForceJump;
    public bool IcanJump;
    float HorizontalMovement;
    float VerticalMovement;
    Vector3 MoveDirection;
    Rigidbody rb;
    private bool LeanRightAct = false;
    private bool LeanLefttAct = false;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = GetComponent<LookCam>();
        rb.freezeRotation = true;
        CurrentLife = MaxLife;
        BulletinCharger = MaxBulletinCharger;
        IcanJump = false;
    }

    void Update()
    {
        Inputs();

        UIGun();
        if (Input.GetKeyDown(KeyCode.E))//aca tienen que hacer las animaciones de izquierda y derecha para inclinarse
        {
            LeanRightAct = !LeanRightAct;
            animpl.SetBool("Right", LeanRightAct);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            LeanLefttAct = !LeanLefttAct;
            animpl.SetBool("Left", LeanLefttAct);
        }
        if(Input.GetMouseButton(0) && !isFiring && BulletinCharger>0)
        {
            Shoot();
        }
        if (Input.GetKey(KeyCode.R) && AllCharger>0)//sistema de recarga
        {
            Reload();
        }
        if(IcanJump)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(new Vector3(0,ForceJump,0),ForceMode.Impulse);
            }
        }
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }
    #region Controls

    void MovePlayer()
    {
        rb.AddForce(MoveDirection.normalized*speed*MovementMultipler, ForceMode.Acceleration);
    }
    void Inputs()
    {
        HorizontalMovement = Input.GetAxis("Horizontal");
        VerticalMovement = Input.GetAxis("Vertical");
        MoveDirection = transform.forward * VerticalMovement + transform.right * HorizontalMovement;
    }
    void Shoot()
    {
        if (Time.time > ShootRateTime) //coldown de disparo
        {
            ShootRateTime = Time.time + shootRate;
            isFiring = true;
            spawnBullet();
            BulletinCharger--;
            isFiring = false;//aca hace falta que se intancie la bala
        }
    }
    void spawnBullet()
    {
        var b = FactoryPool.Instance.GetObj();
        b.transform.position = PosShoot.position;
        b.transform.forward = PosShoot.forward;
    }
    void Reload()
    {
        if (BulletinCharger == 0)
        {
            BulletinCharger = MaxBulletinCharger;
            AllCharger -= 30;
        }
        if (BulletinCharger > 0 && BulletinCharger < 30)
        {
            int BulletsaCharge = MaxBulletinCharger - BulletinCharger;
            BulletinCharger += BulletsaCharge;
            AllCharger -= BulletsaCharge;
        }
    }
    #endregion

    #region UI
    public void UIGun()
    {
        UIBullets.text = BulletinCharger.ToString();
        UIBulletsAllChargers.text = AllCharger.ToString();
    }

    #endregion
}
