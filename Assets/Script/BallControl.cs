using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
// using UnityEngine.AR;
[RequireComponent(typeof(Rigidbody))]
public class BallControl : MonoBehaviour
{
	public float m_ThrowForce = 100f;
	public float m_ThrowDirectionX = 0.17f;
	public float m_ThrowDirectionY = 0.67f;
	public Vector3 m_BallCameraOffset = new Vector3(0f, -1.4f, 2f);

	private Vector3 startPosition;
	private Vector3 direction;
	private float startTime;
	private float endTime;
	private float duration;
	private bool directionChosen = false;
	private bool throwStarted = false;

	[SerializeField]
	GameObject ARCam;

	[SerializeField]
	ARSessionOrigin m_SessionOrigin;
	Rigidbody rb;

	void Start()
	{
		rb = GameObject.GetComponent<RigidBody>();
		m_SessionOrigin = GameObject.Find("AR Session Origin").GetComponent<ARSessionOrigin>();
		ARCam = m_SessionOrigin.transform.Find("AR Camera").GameObject;
		transform.parent = ARCam.transform;
		ResetBall();
	}

	// Update is called once per frame
	void Update()
	{

	}
}
