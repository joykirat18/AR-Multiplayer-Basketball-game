using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class Placehoop : MonoBehaviour
{
	[SerializeField]
	[Tooltip("Instantiates this prefab on a plane at the touch location.")]
	GameObject m_HoopPrefab;

	[SerializeField]
	[Tooltip("Instantiates this ball prefab on a plane at the touch location.")]
	GameObject m_BallPrefab;


	public GameObject placedHoop
	{
		get { return m_HoopPrefab; }
		set { m_HoopPrefab = value; }
	}

	public GameObject placedBall
	{
		get { return m_BallPrefab; }
		set { m_BallPrefab = value; }
	}


	public GameObject spawnedHoop { get; private set; }
	public GameObject spawnedBall { get; private set; }


	public static event Action onPlacedObject;

	private bool isPlaced = false;

	ARRaycastManager m_RaycastManager;

	static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

	void Awake()
	{
		m_RaycastManager = GetComponent<ARRaycastManager>();
	}

	void Update()
	{
		if (isPlaced)
		{
			return;
		}
		if (Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);

			if (touch.phase == TouchPhase.Began)
			{
				if (m_RaycastManager.Raycast(touch.position, s_Hits, TrackableType.PlaneWithinPolygon))
				{
					Pose hitPose = s_Hits[0].pose;

					spawnedHoop = Instantiate(m_HoopPrefab, hitPose.position, Quaternion.AngleAxis(180, Vector3.up));
					spawnedHoop.transform.parent = transform.parent;

					isPlaced = true;
					spawnedBall = Instantiate(m_BallPrefab);
					spawnedBall.transform.parent = m_RaycastManager.transform.Find("AR Camera").gameObject.transform;
					if (onPlacedObject != null)
					{
						onPlacedObject();
					}
				}
			}
		}
	}
}
