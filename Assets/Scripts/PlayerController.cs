using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

	// Create public variables for player speed, and for the Text UI game objects
	public float speed;

	//Referneces the score text
	public TextMeshProUGUI countText;

	//References the win screen text
	public GameObject winTextObject;

	private float movementX;
	private float movementY;

	private Rigidbody rb;
	private int count;

	//Only happens at start of game
	void Start()
	{
		rb = GetComponent<Rigidbody>();

		count = 0;

		SetCountText();
	}

	void FixedUpdate()
	{
		Vector3 movement = new Vector3(movementX, 0.0f, movementY);

		rb.AddForce(movement * speed);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Pickup"))
		{
			other.gameObject.SetActive(false);

			count = count + 1;

			SetCountText();
		}

		//Restart game if the goose falls
		if (other.gameObject.CompareTag("FallTag"))
		{
			Application.LoadLevel(Application.loadedLevel);
		}
	}

	void OnMove(InputValue value)
	{
		Vector2 v = value.Get<Vector2>();

		movementX = v.x;
		movementY = v.y;
	}

	void SetCountText()
	{
		countText.text = "Count: " + count.ToString();

		//Completes the game when the specified amount of items have been collected
		if (count >= 21)
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		}
	}
}
