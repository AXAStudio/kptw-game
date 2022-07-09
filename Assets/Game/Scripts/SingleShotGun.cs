using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SingleShotGun : Gun
{

    [SerializeField] Camera cam;

    PhotonView PV;

    public bool isSingleShot;

    public PlayerController PC;


    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    void OnEnable()
    {
        PC.isReloading = false;
        gunAnim.SetBool("Reloading", false);
    }

    void Start()
    {
        PC.maxAmmo = ((GunInfo)itemInfo).maxAmmo;
    }

    void Update()
    {
        PC.singleShot = ((GunInfo)itemInfo).haveToClickPerShot;
        isSingleShot = ((GunInfo)itemInfo).haveToClickPerShot;
    }

    public override void Use()
    {
        PC.nextTimeToFire = Time.time + 1f / ((GunInfo)itemInfo).fireRate;
        Shoot();
    }

    public override void Aim()
    {
        AimGun();
    }

    public override void DeAim()
    {
        DeactivateAim();
    }

    public override void Reload()
    {
        StartCoroutine(ReloadGun());
    }

    IEnumerator ReloadGun()
    {
        PC.isReloading = true;

        Debug.Log("Reloading...");

        gunAnim.SetBool("Reloading", true);

        yield return new WaitForSeconds(PC.reloadTime - .75f);

        gunAnim.SetBool("Reloading", false);

        yield return new WaitForSeconds(.75f);

        PC.currentAmmo = ((GunInfo)itemInfo).maxAmmo;

        PC.isReloading = false;
    }



    void Shoot()
    {
        ps.Play();
        ac.Play();

        PC.currentAmmo--;

        PC.ammocounter.text = PC.currentAmmo.ToString() + "/" + PC.maxAmmo.ToString();

        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        ray.origin = cam.transform.position;
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            hit.collider.gameObject.GetComponent<IDamageable>()?.TakeDamage(((GunInfo)itemInfo).damage);
            if (hit.collider.gameObject.GetComponent<IDamageable>() != null)
            {
                ac2.Play();
            }
            PV.RPC("RPC_Shoot", RpcTarget.All, hit.point, hit.normal);
        }
    }

    void AimGun()
    {
        gunAnim.SetBool("Aim", true);
    }

    void DeactivateAim()
    {
        gunAnim.SetBool("Aim", false);
    }


    [PunRPC]
    void RPC_Shoot(Vector3 hitPosition, Vector3 hitNormal)
    {
        Collider[] colliders = Physics.OverlapSphere(hitPosition, 0.3f);
        if (colliders.Length != 0)
        {
            GameObject bulletImpactObj = Instantiate(bulletImpactPrefab, hitPosition + hitNormal * 0.001f, Quaternion.LookRotation(hitNormal, Vector3.up) * bulletImpactPrefab.transform.rotation);
            Destroy(bulletImpactObj, 10f);
            bulletImpactObj.transform.SetParent(colliders[0].transform);
        }
    }
}
