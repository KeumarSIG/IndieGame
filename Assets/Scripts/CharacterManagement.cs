using UnityEngine;
using System.Collections;

public class CharacterManagement : MonoBehaviour
{
    // MULTI
    public int m_playerNumber;
    string button_horizontal;
    string button_vertical;
    string button_attack;
    string button_jump;
    // MULTI

    // Movement related (mostly speed, but not only)
    public float m_acceleration;
	public float m_maxSpeed;
	public float m_speedBackToZero;

    public GameObject m_movementParticle;
    public AudioClip m_movementAudio;

    [HideInInspector] public bool m_canMove;
    [HideInInspector] public float m_speed;
    [HideInInspector] public Vector3 m_lastDir;

    Vector3 m_movement;

    // Jump
    public float m_jumpPower;
	public AudioClip m_jumpAudio;
    [HideInInspector] public bool m_isJumping;

    // Kendo
	public GameObject m_kendoStick;
	public AudioClip m_kendoAudio;
	public float m_kendoCooldownDuration;
	public float m_kendoBumpDuration;
	public float m_kendoBumpDecrease;
	public float m_kendoAttackDuration;
    [HideInInspector] public bool m_kendoBumped;
	bool m_kendoCooldown; 

	// Super attack
    public GameObject m_superAttack;
    public float m_superAttackDuration;
    public float m_superAttackEndingLag;

    // Gun
    public GameObject m_bullet;

    // Movement
	

    // Death
	public AudioClip m_deathAudio;

	[HideInInspector] public int m_playerScore;
	

	AudioSource m_thisAudioSource; // ref to this audiosource component
	float m_distToGround;
	GameObject m_gun;
	Rigidbody m_rb;


	// Controls init
    void Awake()
    {
		if (m_playerNumber == 1)
        {
            button_horizontal = "Horizontal";
            button_jump = "Jump";
            button_attack = "Attack";
            button_vertical = "Vertical";
        }

        else if (m_playerNumber == 2)
        {
            button_horizontal = "Horizontal_360";
            button_jump = "Jump_360";
            button_attack = "Attack_360";
            button_vertical = "Vertical_360";
        }
    }

	// Char init
	void Start ()
    {
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

		// Temporary lines... Will be replaced to handle a controller ; Changes direction of the player according to where he moves... Will be two separated things later on
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
		if (Input.GetAxisRaw(button_attack) == 1 && m_kendoCooldown == true)
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
		if (Input.GetAxisRaw(button_jump) == 1 && IsGrounded())
		{
            m_isJumping = true;

            m_rb.velocity = new Vector3(m_rb.velocity.x, m_jumpPower, m_rb.velocity.z);

			if (m_kendoBumped == true) 
			{
				m_rb.velocity = m_rb.velocity * m_kendoBumpDecrease; // Reduces the intensity of the bump, if character is bumped.
				m_kendoBumped = false; // The technique can only occur once at the time.
			}
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