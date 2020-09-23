using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody playerRb;
    private WeaponSwitching weaponSwitcher;

    public Transform playerBody;

    private int selectedWeapon;

    private float speed;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public float jumpHeight = 3.0f;

    bool isGrounded;

    Vector3 gravity;

    private void Start()
    {
        weaponSwitcher = GetComponentInChildren<WeaponSwitching>();
        gravity = Physics.gravity;
    }

    // Update is called once per frame
    void Update()
    {
        selectedWeapon = weaponSwitcher.selectedWeapon;

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (selectedWeapon == 1)
        {
            speed = 2;
            Physics.gravity = new Vector3 (gravity.x * 1.5f, gravity.y * 1.5f, gravity.z * 1.5f);
        }
        else if (selectedWeapon == 0)
        {
            speed = 2;
            Physics.gravity = gravity;
        }

        // Vector3 move = transform.right * x + transform.forward * z;

        // controller.Move(move * speed * Time.deltaTime);

        // velocity.y += gravity * Time.deltaTime;

        // controller.Move(velocity * Time.deltaTime);
    }

    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //if (x == 0 && z == 0 && isGrounded)
        //{
        //  playerRb.Sleep();
        //}

        playerRb.AddForce(playerBody.transform.forward * speed * z, ForceMode.VelocityChange);
        playerRb.AddForce(playerBody.transform.right * speed * x, ForceMode.VelocityChange);


        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerRb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        }
    }
}
