using UnityEngine;  //필요없는 using은 지워도댐
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    // power , distance
    public float power = 10f;
    public float range = 100f;
    public Camera fpsCam;

    // 파티클&물리 관련 변수
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public float impactForce = 60f;

    // 연사모드에 쓰일 변수
    public  float fireRate = 15f;
    private float nextTimeToFire = 0f;

    // 라인랜더러&십자선
    public LineRenderer lineLaser;
    public GameObject crosshair;

    // 리로드 관련 변수
    public Image reloadImg;
    bool bReload = false;
    float reloadCurTime = 0f;
    float reloadMaxTime = 0f;


    void Update()
    {
        crosshair.SetActive(true);
        RaycastHit laserHit;
        Ray laserRay = new Ray(lineLaser.transform.position, lineLaser.transform.forward);
        if(Physics.Raycast(laserRay, out laserHit))
        {
            // 라인렌더러의 길이를 잘라주는 코드 , 월드 좌표를 로컬 좌표로 바꿔주는 함수 ,
            // laserHit.point가 월드좌푤라서 로컬좌표로 변환해서 넣어줘야 함
            lineLaser.SetPosition(1, lineLaser.transform.InverseTransformPoint(laserHit.point));

            // 라인랜더러의 끝점에 조준점(crossHair)을 동기화 시켜준다.

                  Vector3 crosshairLocation = Camera.main.WorldToScreenPoint(laserHit.point);
            crosshair.transform.position = crosshairLocation;


        }
        else {
            crosshair.SetActive(false);
        }
        // 단발처리
        // if(Input.GetButtonDown("Fire1"))
        // {
        //     Shoot();
        // }

        // 연사모드
        if(Input.GetButtonDown("Fire1") && Time.time > nextTimeToFire)
        {
            //nextTimeToFire = Time.time + fireRate; // 1초단위 이상의 딜레이를 줄 수 있음
              nextTimeToFire = Time.time +  (1 / fireRate); // 0.초단위 이상의 딜레이를 줄 수 있음
            Shoot();

            // bReload On
            reloadMaxTime = nextTimeToFire - Time.time;
            reloadCurTime = 0f;
            bReload = true;

        }

        // 리로드 게이지 처리
        if(bReload)
        {
            reloadCurTime += Time.deltaTime;
            reloadImg.fillAmount = reloadCurTime / reloadMaxTime;
            if(reloadImg.fillAmount > 1f) {
                reloadImg.fillAmount = 0f;
                bReload = false;
            }
        }
    }

    void Shoot()
    {
        muzzleFlash.Play();

        RaycastHit hit;
        if(Physics.Raycast(lineLaser.transform.position, lineLaser.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            // 레이가 맞은 지점의 게임 오브젝트의 스크립트를 읽어와서 데미지 처리를 해준다.
            TargetBox tb = hit.transform.GetComponent<TargetBox>();
            if(tb != null) {
                tb.TakeDamage(power);
            }
            // 박스를 밀어버리는 처리
            if(hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }



            // 총알이 맞은 지점에 파티클 이펙트를 생성 후 사라지게 하는 처리
            // 오브젝트 풀링 - 분리
            GameObject impactObj =  Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactObj, 2f);
        }
    }
}


