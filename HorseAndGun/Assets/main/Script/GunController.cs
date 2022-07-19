using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public string gunName;  // 총의 이름
    public float range; // 사정거리
    public float accuracy;  // 정확도
    public float fireRate;  // 연사속도
    public float reloadTime;    // 재장전 속도

    public int damage; // 총의 데미지

    public int reloadBulletCount; // 총알 재장전 개수
    public int currentBullectCount; // 현재 탄알집에 남아있는 총알의 개수
    public int maxBulletCount;  // 최대 소유 가능 총알 개수
    public int carryBulletCount; // 현재 소유하고 있는 총알 개수

    public float retroActionForce; // 반동 세기
    public float retroActionFineSightFoce; // 정조준시의 반동 세기

    public Vector3 fineSightOriginPos;
    public Animator anim;
    public ParticleSystem muzzleFlash;

    public AudioSource audioSource;

    private float currentFireRate;

    // 레이저 충돌 받아옴
    private RaycastHit hitInfo;

    [SerializeField]
    private Camera theCam;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        GunFireRateCalc();
        TryFire();
    }

    // 총 연사속도
    private void GunFireRateCalc()
    {
        if(currentFireRate > 0)
        {
            currentFireRate -= Time.deltaTime;
        }
    }

    // 총 발사
    private void TryFire()
    {
        if(Input.GetButton("Fire1") && currentFireRate <= 0)
        {
            Fire();
        }
    }

    // 발사
    private void Fire()
    {
        currentFireRate = fireRate;
        Shoot();
    }

    // 발사
    private void Shoot()
    {
        muzzleFlash.Play();
        audioSource.Play();
        Hit();
    }

    // 맞을 때
    private void Hit()
    {
        if (Physics.Raycast(theCam.transform.position, theCam.transform.forward, out hitInfo, range))
        {
            if (hitInfo.transform.name == "WoodTarget")
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().HitTarget();
            }
            if (hitInfo.transform.name == "WoodTarget1")
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().HitTarget();
            }
            if (hitInfo.transform.name == "WoodTarget2")
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().HitTarget();
            }
        }
    }
} 
