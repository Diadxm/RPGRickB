using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScript : MonoBehaviour
{
	public float speed = 0;
    public float maxSpeed = 5;
    public float turningSpeed = 1;
    public float maxHeadingChange = 1;
    public float gravity = 9.81f;

    private float Timer = 1;
    private bool StartTimer = false;

    private bool isDamaged = false;

    private float heading;
    private Vector3 targetRotation;
    public Transform target { get; set; }
    private Animator animController;
    private CharacterController controller;

    private vp_FPPlayerDamageHandler PlayerDamage;

    private GameObject Player;

    public AudioSource Footstep;

    void Start()
    {
        heading = Random.Range(0, 360);
        transform.eulerAngles = new Vector3(0, heading, 0);
        controller = GetComponent<CharacterController>();
        animController = GetComponent<Animator>();

        PlayerDamage = GameObject.Find("HeroHDWeapons").GetComponent<vp_FPPlayerDamageHandler>();
        Player = GameObject.Find("HeroHDWeapons");
    }

    void Update()
    {
        if (StartTimer)
        {
            Timer -= Time.deltaTime;
            isDamaged = true;
        }

        if (Timer <= 0.0f)
        {
            StartTimer = false;
            Timer = 1;
            isDamaged = false;
        }

        Vector3 moveDirection = Vector3.zero;
        if (speed < maxSpeed)
        {
            speed += Time.deltaTime;
        }

        //wander
        if (target == null)
        {
            transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, targetRotation, Time.deltaTime * turningSpeed);
            NewHeading();

            animController.SetFloat("Speed", speed);
            moveDirection = transform.forward * speed;
        }
        //chase enemy
        else
        {
            // Calculate the direction from the current position to the target
            Vector3 targetDirection = target.position - transform.position;
            // Calculate the rotation required to point at the target
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            // Rotate from the current rotation towards the target rotation, but not
            // faster than mRotationSpeed degrees per second
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turningSpeed * Time.fixedTime);
            // Move forward
            moveDirection = transform.forward * speed;
            animController.SetFloat("Speed", speed);
        }



        //Apply gravity
        moveDirection.y -= gravity;

        //Apply move
        controller.Move(moveDirection * Time.deltaTime);

        TargetPlayer();
        SwingAtPlayer();
    }

    private void SwingAtPlayer()
    {
        if (target != null)
        {
            if (Vector3.Distance(this.gameObject.transform.position, target.position) <= 1)
            {
                DamagePlayer();
                StartTimer = true;
            }
        }
    }

    private void TargetPlayer()
    {
        if (Vector3.Distance(this.gameObject.transform.position, Player.transform.position) < 10)
        {
            target = Player.transform;
            Footstep.Play();
        }
        else if (Vector3.Distance(this.gameObject.transform.position, Player.transform.position) < 20 && target == Player.transform)
        {
           
        }
        else
        {
            target = null;
            Footstep.Pause();
        }
    }

    private void DamagePlayer()
    {
        if (!isDamaged)
        {
            PlayerDamage.Damage(1);
        }
    }

    void NewHeading()
    {
        float floor = Mathf.Clamp(heading - maxHeadingChange, 0, 360);
        float ceil = Mathf.Clamp(heading + maxHeadingChange, 0, 360);
        heading = Random.Range(floor, ceil);
        targetRotation = new Vector3(0, heading, 0);
    }

    public void SpottedEnemy(Transform enemy)
    {
        //target aquired...
        target = enemy.transform;
    }
}
