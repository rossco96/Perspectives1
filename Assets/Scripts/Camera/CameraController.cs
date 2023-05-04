using UnityEngine;

public class CameraController : MonoBehaviour
{
	//private const float k_animateTime = 1.0f;
	[SerializeField] private float k_animateTime = 1.0f;

	[Header("Cameras")]
	[SerializeField] private GameObject m_cameraFirstPerson;
	[SerializeField] private GameObject m_cameraBEV;
	[SerializeField] private GameObject m_cameraThirdPerson;
	[SerializeField] private GameObject m_cameraAnimated;

	// [TODO]
	// Rather than having a camera mode int and the camera gameObjects,
	// have a dictionary (or create a serializable version of one)

	private int m_currentCameraMode;
	private bool m_isMovingToNextCamera;

	// Lerping values:
	private Vector3 m_startPosition;
	private Vector3 m_targetPosition;
	private Quaternion m_startRotation;
	private Quaternion m_targetRotation;
	private float m_currentAnimatedTime;



	private void Awake()
	{
		m_currentCameraMode = 1;
		m_isMovingToNextCamera = false;
		m_currentAnimatedTime = 0.0f;
		OnMoveCameraComplete();
	}

	private void Update()
	{
		if (m_isMovingToNextCamera)
		{
			AnimateCamera();
			return;
		}

		if (GetCameraChangeInput(out int nextCameraMode))
		{
			MoveToNextCamera(nextCameraMode);
		}
	}



	private bool GetCameraChangeInput(out int nextCameramMode)
	{
		nextCameramMode = 0;
		if (Input.GetKeyUp(KeyCode.Alpha1) && m_currentCameraMode != 1)
		{
			nextCameramMode = 1;
			return true;
		}
		else if (Input.GetKeyUp(KeyCode.Alpha2) && m_currentCameraMode != 2)
		{
			nextCameramMode = 2;
			return true;
		}
		else if (Input.GetKeyUp(KeyCode.Alpha3) && m_currentCameraMode != 3)
		{
			nextCameramMode = 3;
			return true;
		}
		return false;
	}
	
	private void MoveToNextCamera(int nextCameraMode)
	{
		switch (m_currentCameraMode)
		{
			case 1:
				m_startPosition = m_cameraFirstPerson.transform.localPosition;
				m_startRotation = m_cameraFirstPerson.transform.localRotation;
				break;
			case 2:
				m_startPosition = m_cameraBEV.transform.localPosition;
				m_startRotation = m_cameraBEV.transform.localRotation;
				break;
			case 3:
				m_startPosition = m_cameraThirdPerson.transform.localPosition;
				m_startRotation = m_cameraThirdPerson.transform.localRotation;
				break;
			default:
				break;
		}

		switch (nextCameraMode)
		{
			case 1:
				m_targetPosition = m_cameraFirstPerson.transform.localPosition;
				m_targetRotation = m_cameraFirstPerson.transform.localRotation;
				break;
			case 2:
				m_targetPosition = m_cameraBEV.transform.localPosition;
				m_targetRotation = m_cameraBEV.transform.localRotation;
				break;
			case 3:
				m_targetPosition = m_cameraThirdPerson.transform.localPosition;
				m_targetRotation = m_cameraThirdPerson.transform.localRotation;
				break;
			default:
				break;
		}

		m_cameraFirstPerson.SetActive(false);
		m_cameraBEV.SetActive(false);
		m_cameraThirdPerson.SetActive(false);

		m_cameraAnimated.transform.localPosition = m_startPosition;
		m_cameraAnimated.transform.localRotation = m_startRotation;
		m_cameraAnimated.SetActive(true);

		m_currentCameraMode = nextCameraMode;
		m_isMovingToNextCamera = true;
	}

	private void AnimateCamera()
	{
		m_cameraAnimated.transform.localPosition = Vector3.Lerp(m_startPosition, m_targetPosition, (m_currentAnimatedTime / k_animateTime));
		m_cameraAnimated.transform.localRotation = Quaternion.Lerp(m_startRotation, m_targetRotation, (m_currentAnimatedTime / k_animateTime));
		m_currentAnimatedTime += Time.deltaTime;
		if (m_currentAnimatedTime >= k_animateTime)
		{
			OnMoveCameraComplete();
		}
	}

	private void OnMoveCameraComplete()
	{
		m_cameraAnimated.SetActive(false);
		switch (m_currentCameraMode)
		{
			case 1:
				//LevelManager.Instance.SetNextPhysics(new PhysicsFirstPerson());		// [TODO] Temp(?) moved to LevelManager
				m_cameraFirstPerson.SetActive(true);
				m_cameraBEV.SetActive(false);
				m_cameraThirdPerson.SetActive(false);
				break;

			case 2:
				//LevelManager.Instance.SetNextPhysics(new PhysicsBEV());
				m_cameraFirstPerson.SetActive(false);
				m_cameraBEV.SetActive(true);
				m_cameraThirdPerson.SetActive(false);
				break;

			case 3:
				//LevelManager.Instance.SetNextPhysics(new PhysicsThirdPerson());
				m_cameraFirstPerson.SetActive(false);
				m_cameraBEV.SetActive(false);
				m_cameraThirdPerson.SetActive(true);
				break;

			default:
				break;
		}
		m_isMovingToNextCamera = false;
		m_currentAnimatedTime = 0.0f;
		LevelManager.Instance.SetCameraMode(m_currentCameraMode);						// [TODO] (see TODO above)
	}
}
