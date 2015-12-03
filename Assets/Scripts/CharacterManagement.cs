using UnityEngine;
using System.Collections;
using System;

public class CharacterManagement : MonoBehaviour
{
    // MULTI
    [Tooltip("The player's number")]
    public int m_playerNumber;

    [Tooltip("Last player who hit this player")]
    /*[HideInInspector]*/ public int m_lastPlayerWhoHit;

    // Game buttons
    string button_horizontal;
    string button_vertical;
    string button_attack;
    string button_jump;
    string button_grappin;

    // MULTI

    // Movement related (mostly speed, but not only)
    [Tooltip("The car's acceleration")]
    public float m_acceleration;

    [Tooltip("The maximum speed of the car")]
    public float m_maxSpeed;

    [Tooltip("The time it takes to go back from current speed to 0")]
    public float m_speedBackToZero;

    [Tooltip("The particles associated to movement")]
    public GameObject m_movementParticle;

    [Tooltip("The sound associated to movement")]
    public AudioClip m_movementAudio;

    [HideInInspector] public bool m_canMove;
    [HideInInspector] public float m_speed;
    /*[HideInInspector]*/ public Vector3 m_lastDir;

    Vector3 m_movement;

    // Jump
    [Tooltip("The height of jump")]
    public float m_jumpPower;

    [Tooltip("The sound associated with the jump")]
    public AudioClip m_jumpAudio;

    [HideInInspector] public bool m_isJumping;

    // Kendo
    [Tooltip("The kendo gameobject")]
    public GameObject m_kendoStick;

    [Tooltip("The sound associated with the kendo")]
    public AudioClip m_kendoAudio;

    [Tooltip("The time between two kendo attacks")]
    public float m_kendoCooldownDuration;

    [Tooltip("The amount of time the player cannot move because of the bump")]
    public float m_kendoBumpDuration;

    [Tooltip("How much from the bump should be removed if you jump")]
    public float m_kendoBumpDecrease;

    [Tooltip("How much time does the kendo attack lasts ?")]
    public float m_kendoAttackDuration;

    [HideInInspector] public bool m_kendoBumped;
	bool m_kendoCooldown;

    [Tooltip("Percentage of enemy kendo bump reduction")]
    public float m_armor;
    float m_basicArmor;

    // Super attack
    /*
    public GameObject m_superAttack;
    public float m_superAttackDuration;
    public float m_superAttackEndingLag;
    */

    // Gun
    //public GameObject m_bullet;

    // Bonus
    bool m_bonusArmor;


    // Death
    [Tooltip("The sound associated with player's death")]
    public AudioClip m_deathAudio;

	[HideInInspector] public int m_playerScore;
	

	AudioSource m_thisAudioSource; // ref to this audiosource component
	float m_distToGround;
	GameObject m_gun;
	Rigidbody m_rb;


	// Controls init
    void Awake()
    {
        // Controller
        button_horizontal = "L_XAxis_" + m_playerNumber;
        button_vertical = "L_YAxis_" + m_playerNumber;
        button_attack = "RB_" + m_playerNumber;
        button_jump = "A_" + m_playerNumber;
        button_grappin = "LB_" + m_playerNumber;
        
        // Keyboard
        /*
        button_horizontal = "Horizontal";
        button_vertical = "Vertical";
        button_attack = "Fire1";
        button_jump = "A_" + m_playerNumber;
        button_grappin = "LB_" + m_playerNumber;
        */
    }

	// Char init
	void Start ()
    {
        m_basicArmor = m_armor;
		m_kendoCooldown = true;
		m_thisAudioSource = GetComponent<AudioSource>();
		m_canMove = false;
        m_rb = GetComponent<Rigidbody>();
		m_distToGround = 2f;
		m_lastDir = Vector3.forward;
		m_kendoBumpDecrease = 1 - (m_kendoBumpDecrease * 0.01f);
	}
	
    void Update()
    {  
		// Movement
		Vector3 charDir = new Vector3(Input.GetAxisRaw(button_horizontal), 0, Input.GetAxisRaw(button_vertical));
        if (charDir != Vector3.zero) m_lastDir = charDir;
        transform.rotation = Quaternion.LookRotation(m_lastDir);



		// If a movement button is pressed, then the character accelerates
		if (Input.GetAxisRaw(button_horizontal) != 0 || Input.GetAxisRaw(button_vertical) != 0) 
        {
            if (m_speed < m_maxSpeed) m_speed += m_acceleration;
        }



		// Start a coroutine, and if after "m_speedBackToZero" seconds, the player doesn't press any input anymore, then, the character stops
		else if (Input.GetAxisRaw(button_horizontal) == 0 || Input.GetAxisRaw(button_vertical) == 0)
		{
			StartCoroutine(GetOriginalSpeedBack());
		}



		// Kendo
		if (Input.GetButtonDown(button_attack) == true && m_kendoCooldown == true)
		{
			// Kendo
			if (m_kendoStick.activeInHierarchy == false) 
			{
				m_kendoCooldown = false;
				StartCoroutine(KendoStickAttack());
				StartCoroutine(KendoCooldown());
			}
		}



		// Jumping might be better in fixed update, isn't it ? 
		if (Input.GetButtonDown(button_jump) == true && IsGrounded())
		{
            m_isJumping = true;

            m_rb.velocity = new Vector3(m_rb.velocity.x, m_jumpPower, m_rb.velocity.z);

			if (m_kendoBumped == true) 
			{
				m_rb.velocity = m_rb.velocity * m_kendoBumpDecrease; // Reduces the intensity of the bump, if character is bumped.
				m_kendoBumped = false; // The technique can only occur once at the time.
			}
		}

        // Grappin
        if (Input.GetButtonDown(button_grappin) == true)
        {
            // Code grappin ici
        }
               



		// Only if the character is moving
		// Add movement particle effect
		if (m_movement != Vector3.zero && m_movementParticle.activeInHierarchy == false) m_movementParticle.SetActive(true);
		else if (m_movement == Vector3.zero) StartCoroutine(ClearMovementParticles());

		// Play moving sound
		if (IsGrounded ()) 
		{
			if (m_thisAudioSource.clip == m_jumpAudio) m_thisAudioSource.clip = m_movementAudio;
			m_thisAudioSource.clip = m_movementAudio;
		}

		else m_thisAudioSource.Pause();
        //if (!m_thisAudioSource.isPlaying && !m_thisAudioSource.clip == m_jumpAudio) m_thisAudioSource.Play();

        /*
        if (Input.GetKeyDown(KeyCode.Space) && !IsGrounded())
        {
            m_rb.velocity = new Vector3(m_rb.velocity.x, -m_jumpPower * 0.5f, m_rb.velocity.z);
            StartCoroutine(SuperAttack());
        }
        */
    }



	// Handles movement
	void FixedUpdate ()
    {
		int i = m_canMove ? 1 : 0; // "i" is used to get the value of m_canMove as an int and not a bool
        m_movement = new Vector3(Input.GetAxisRaw(button_horizontal), 0f, Input.GetAxisRaw(button_vertical)).normalized * i; 
		m_rb.MovePosition(m_rb.position + m_movement * m_speed * Time.deltaTime); // actual movement
    }



	// Checks if the player is on the floor
	bool IsGrounded()
	{
		return Physics.Raycast(transform.position, Vector3.down, m_distToGround);
	}



	// Get the original speed back to the player
	IEnumerator GetOriginalSpeedBack()
	{
		yield return new WaitForSeconds(m_speedBackToZero);
		
		if (Input.GetAxisRaw(button_horizontal) != 0 || Input.GetAxisRaw(button_vertical) != 0) yield break; // This has been made to handle the fact that the player might brutally change direction: though, it makes sure the speed doesn't go back to 0 immediately
		else m_speed = 0;
	}



	// The kendo attack
	IEnumerator KendoStickAttack()
	{
		m_kendoStick.SetActive(true);
		yield return new WaitForSeconds(m_kendoAttackDuration);
		m_kendoStick.SetActive(false);
	}



	// Kendo cooldown
	IEnumerator KendoCooldown()
	{
		yield return new WaitForSeconds(m_kendoAttackDuration + m_kendoCooldownDuration);
		m_kendoCooldown = true;
	}



	// Duration of the bump
	IEnumerator GettingBumped()
	{
		yield return new WaitForSeconds(m_kendoBumpDuration);
		if (m_kendoBumped == true) m_kendoBumped = false;
	}



	// Stop movement particles
	IEnumerator ClearMovementParticles()
	{
		yield return new WaitForSeconds(m_movementParticle.GetComponent<ParticleSystem>().duration * 0.25f);
		m_movementParticle.SetActive(false);
	}



    public void BonusLauncher(string bonus, float duration)
    {
        switch (bonus)
        {
            case "BonusArmor": // armure bonus
                StartCoroutine(ArmorBonus(duration));
            break;
        }
    }



    // Reset the armor to its normal amount
    IEnumerator ArmorBonus(float bonusDuration) 
    {
        yield return new WaitForSeconds(bonusDuration);
        m_armor = m_basicArmor;
    }



    void OnColliderEnter (Collider coll)
    {
        if (coll.gameObject.tag == "Kendo")
        {
            m_lastPlayerWhoHit = coll.gameObject.GetComponent<Kendo>().m_kendoNumber;
        }

        if (coll.gameObject.CompareTag("Player") == true)
        {
            m_lastPlayerWhoHit = coll.gameObject.GetComponent<CharacterManagement>().m_playerNumber;
        }
    }

    // Super attack
	/*
    IEnumerator SuperAttack()
    {
        // Activates the attack and prevents the player from moving;
        m_superAttack.SetActive(true);
        m_canMove = false;

        yield return new WaitForSeconds(m_superAttackDuration);
        m_superAttack.SetActive(false);

        // Moment where the player is vulnerable
        yield return new WaitForSeconds(m_superAttackEndingLag);
        m_canMove = true;
    }
    */
}

// Gun - DOESN'T WORK WELL ATM

/*
	GameObject clone = Instantiate(m_bullet, this.transform.position + (m_lastDir + m_lastDir), Quaternion.identity) as GameObject;
	clone.GetComponent<Rigidbody>().AddForce(m_lastDir * 2000);
*/