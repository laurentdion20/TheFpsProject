using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunController : MonoBehaviour
{
	#region Variable List;
	public GunData gunData; 
    public Transform Cam;


    public GameObject Gun;
    private int shooted;
    bool isReloading;
    bool timeBetweenShootDone;
    public GameObject exit;
 
    //UI
    public Image CenterMark;    
    Animator CenterMarkAnim;
    bool isZooming;
    public Text bulletCount;

    ////Gun Stats
    int playerBulletStorage;
    int magBulletStorage;
    int bulletLoaded;
    float reloadtime;
    float timeBetweenShoot;
    int gunRange;
    float bulletDamage;

    //recoil
    float xMax;
    float yMax;
    float zMax;
    float recoilX;
    float recoilY;
    float recoilZ;
    bool asShoot;
    Quaternion startPosition;
	Quaternion testOne;

    //animation
    Animator ArAnimator;
    bool isAiming;
    bool reloadindDone;
    bool reAiming;
	#endregion

	void Start()
    {

        //Gun stats
        playerBulletStorage = gunData.bulletInventory;
        magBulletStorage = gunData.magBulletInventory;
        bulletLoaded = gunData.bulletLoaded;
        reloadtime = gunData.reloadtime;
        timeBetweenShoot = gunData.timeBetweenShoot;
        timeBetweenShootDone = true;
        gunRange = gunData.gunRange;
        bulletDamage = gunData.bulletDamage;

        //recoil;
        xMax = gunData.xMax;
        yMax = gunData.yMax;
        zMax = gunData.zMax;
        startPosition = Cam.transform.rotation;

        //UI
        CenterMarkAnim = CenterMark.GetComponent<Animator>();

        //Animation
        isAiming = false;
        ArAnimator = Gun.GetComponent<Animator>();
        reAiming = false;

    }
    void Update()
    {
    
        if (isReloading)
        {
            return;
        }
        Shoot();
       
        if (Input.GetButtonDown("Reload") && playerBulletStorage > 0)
        {
            StartCoroutine(Reload());
        }

        UiUpdate();

        if(Input.GetButtonDown("Fire2"))
        {
            CenterMarkAnim.SetBool("IsZooming", true);
            Fade fade = CenterMark.GetComponent<Fade>();
            fade.CallFadeIn();
            ArAnimator.SetBool("IsAiming", true);
            isAiming = true;
            
          
        }
        if (Input.GetButtonUp("Fire2"))
        {
            CenterMarkAnim.SetBool("IsZooming", true);
            Fade fade = CenterMark.GetComponent<Fade>();
            fade.CallFadeOut();
            ArAnimator.SetBool("IsAiming", false);
            isAiming = false;
            
        }
    }

     void Shoot()
    {
        if (Input.GetButton("Fire1") && bulletLoaded >= 1 && !isReloading) { 

            
            if (timeBetweenShootDone)
            {
				AddRecoil();
				//Debug.Log("Shoot is done ");
                //shoot
               RaycastHit ray;
                if (Physics.Raycast(exit.transform.position, exit.transform.forward, out ray, gunRange))
                {
                    Debug.DrawRay(exit.transform.position, exit.transform.TransformDirection(Vector3.forward) * ray.distance, Color.red);
                    Target_Hp target = ray.transform.GetComponent<Target_Hp>();
                    if(target != null)
                    {
                        target.TakeDamage(bulletDamage);
                    }
                }
                bulletLoaded--;

				//Debug.Log("Bullet loaded = " + bulletLoaded);
                timeBetweenShootDone = false;
                Invoke("TimeBetweenShootReset", timeBetweenShoot);
            }
        }
        else
        {
            Cam.transform.rotation = Quaternion.Lerp(Cam.transform.rotation, startPosition, 0);
            //Debug.Log("Recoil return to start position");
        }

    }
    void AddRecoil()

    {
        if (isAiming)
        {
            float recoilX = Random.Range(0, xMax);
            float recoilY = Random.Range(-yMax, yMax);
			//   Cam.transform.Rotate(Vector3.right, -recoilX);
			// Cam.transform.Rotate(Vector3.up,-recoilY);

	
		}
        else
        {
            float recoilX = Random.Range(0, xMax*3);
            float recoilY = Random.Range(0, yMax*3);
         //   Cam.transform.Rotate(Vector3.right, -recoilX);
          //  Cam.transform.Rotate(Vector3.up, -recoilY);
        }
    }

    void TimeBetweenShootReset()
    {
        timeBetweenShootDone = true;
    }

    IEnumerator Reload()
    { 
        isReloading = true;
        if (isAiming)
        {
            ArAnimator.SetBool("IsAiming", false);
            isAiming = false;
            reAiming = true;
            yield return new WaitForSeconds(1);
        }
        ArAnimator.SetBool("IsReloading", true);

     
        Debug.Log("Reloading...");

        yield return new WaitForSeconds(1);
        shooted = (magBulletStorage - bulletLoaded);

        if (playerBulletStorage >= shooted)
        {
            Debug.Log("loaded.");
            bulletLoaded += shooted;
            playerBulletStorage -= shooted;
        }
        else
        {
            Debug.Log("loaded.");
            bulletLoaded += playerBulletStorage;
        }
        shooted = 0;
        if (reAiming)
        {
            ArAnimator.SetBool("IsAiming", true);
            isAiming = true;
        }
        isReloading = false;
        ArAnimator.SetBool("IsReloading", false);

    }

    void UiUpdate()
    {
        bulletCount.text = bulletLoaded + " / " + playerBulletStorage;
    }
}

			