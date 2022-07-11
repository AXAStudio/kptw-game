using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviourPunCallbacks, IDamageable
{
    [SerializeField] Image healthBarImage;
    [SerializeField] GameObject ui;
    public TMP_Text ammocounter;

    [SerializeField] GameObject cameraHolder;

    [SerializeField] float mouseSensitivity, sprintSpeed, walkSpeed, jumpForce, smoothTime;

    [SerializeField] Item[] items;

    

    [HideInInspector] public bool singleShot;
    [HideInInspector] public int maxAmmo;
    [HideInInspector] public int currentAmmo;
    [HideInInspector] public float reloadTime = 1f;
    [HideInInspector] public bool isReloading = false;

    int itemIndex;
    int previousItemIndex = -1;

    float verticleLookRotation;
    bool grounded;
    Vector3 smoothMoveVelocity;
    Vector3 moveAmount;

    Rigidbody rb;

    PhotonView PV;

    const float maxHealth = 100;
    float currentHealth = maxHealth;

    PlayerManager playerManager;

    public float nextTimeToFire = 0f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        PV = GetComponent<PhotonView>();

        playerManager = PhotonView.Find((int)PV.InstantiationData[0]).GetComponent<PlayerManager>();
    }

    void Start()
    {
        if (PV.IsMine)
        {
            EquipItem(0);
            currentAmmo = maxAmmo;
        }
        else
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
            Destroy(rb);
            Destroy(ui);
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
    }

    void Update()
    {
        if (!PV.IsMine)
            return;
        Look();
        Move();
        Jump();

        


        if (isReloading)
        {
            return;
        }

        for (int i = 0; i < items.Length; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                EquipItem(i);
                break;
            }
        }

        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0f)
        {
            if (itemIndex >= items.Length - 1)
            {
                EquipItem(0);
            }
            else
            {
                EquipItem(itemIndex + 1);
            }
            
        }
        else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0f)
        {
            if (itemIndex <= 0)
            {
                EquipItem(items.Length - 1);
            }
            else
            {
                EquipItem(itemIndex - 1);
            }
            
        }



        if (Input.GetMouseButton(1))
        {
            items[itemIndex].Aim();
        }
        if (!Input.GetMouseButton(1))
        {
            items[itemIndex].DeAim();
        }

        if (transform.position.y < -10f)
        {
            Die();
        }
    }

    void Look()
    {
        transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * mouseSensitivity);

        verticleLookRotation += Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
        verticleLookRotation = Mathf.Clamp(verticleLookRotation, -90f, 90f);

        cameraHolder.transform.localEulerAngles = Vector3.left * verticleLookRotation;
    }

    void Move()
    {
        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

        moveAmount = Vector3.SmoothDamp(moveAmount, moveDir * (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed), ref smoothMoveVelocity, smoothTime);
    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.AddForce(transform.up * jumpForce);
        }
    }

    void EquipItem(int _index)
    {
        if (_index == previousItemIndex)
            return;

        itemIndex = _index;

        items[itemIndex].itemGameObject.SetActive(true);

        if (previousItemIndex != -1)
        {
            items[previousItemIndex].itemGameObject.SetActive(false);
        }

        previousItemIndex = itemIndex;

        if (PV.IsMine)
        {
            Hashtable hash = new Hashtable();
            hash.Add("itemIndex", itemIndex);
            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        }
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if(!PV.IsMine && targetPlayer == PV.Owner)
        {
            EquipItem((int)changedProps["itemIndex"]);
        }
    }

    public void SetGroundedState(bool _grounded)
    {
        grounded = _grounded;
    }

    void FixedUpdate()
    {
        if (!PV.IsMine)
            return;
        rb.MovePosition(rb.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
    }

    public void TakeDamage(float damage)
    {
        PV.RPC("RPC_TakeDamage", RpcTarget.All, damage);
    }

    [PunRPC]
    void RPC_TakeDamage(float damage)
    {
        if (!PV.IsMine)
            return;
        currentHealth -= damage;

        

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        playerManager.Die();
    }

    
}
