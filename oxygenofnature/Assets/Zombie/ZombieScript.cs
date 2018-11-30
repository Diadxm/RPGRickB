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
    private int crawlState;
    private Animator animController;
    private CharacterController controller;
    private Rigidbody rb;

    private vp_FPPlayerDamageHandler PlayerDamage;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        heading = Random.Range(0, 360);
        transform.eulerAngles = new Vector3(0, heading, 0);
        controller = GetComponent<CharacterController>();
        animController = GetComponent<Animator>();
        //crawlState = Animator.StringToHash("Base.crawl");

        PlayerDamage = GameObject.Find("HeroHDWeapons").GetComponent<vp_FPPlayerDamageHandler>();
    }

    void Update()
    {
        SwingAtPlayer();

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

            AnimatorStateInfo currentBaseState = animController.GetCurrentAnimatorStateInfo(0);
            //if (currentBaseState.fullPathHash == crawlState)
            //{
            animController.SetFloat("Speed", speed);
            moveDirection = transform.forward * speed;
           // }
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

            animController.SetBool("SeesPlayer", true);
            animController.SetFloat("Speed", speed);



        }

        //Apply gravity
        moveDirection.y -= gravity;

        //Apply move
        controller.Move(moveDirection * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Look for player within the trigger, if player found set it as the target
        if(other.gameObject.tag == "Player")
        {
            //Set player transform as target.
            SpottedEnemy(other.gameObject.transform);
        }
    }

    private void SwingAtPlayer()
    {
        if (Vector3.Distance(this.gameObject.transform.position, target.position) <= 1)
        {
            Debug.Log(Vector3.Distance(this.gameObject.transform.position, target.position));
            animController.SetBool("isAttacking", true);
            DamagePlayer();
            StartTimer = true;
        }
        else
        {
            animController.SetBool("isAttacking", false);
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
