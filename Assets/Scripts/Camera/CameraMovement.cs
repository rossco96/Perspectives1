using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	[SerializeField] private GameObject m_player;
	[SerializeField] private GameObject m_bevShadow;

	private Vector3 m_offset;
	//private Vector3 m_initialShadowPosition;
	//private Vector3 m_currentShadowPosition;
	//private Quaternion m_initialShadowRotation;

	private void Awake()
	{
		//m_initialShadowPosition = m_bevShadow.transform.localPosition;
		//m_initialShadowRotation = m_bevShadow.transform.localRotation;
		//m_initialShadowPosition = m_bevShadow.transform.position;
		//m_initialShadowRotation = m_bevShadow.transform.rotation;

		m_offset = new Vector3(0, 0.5f, 0.5f);
	}

	private void FixedUpdate()
	{
		transform.SetPositionAndRotation(m_player.transform.position, m_player.transform.rotation);

		//m_bevShadow.transform.rotation = m_initialShadowRotation;
		//m_bevShadow.transform.SetPositionAndRotation(m_player.transform.position + m_offset, m_initialShadowRotation);
		m_bevShadow.transform.position = m_player.transform.position + m_offset;

		//if (LevelManager.Instance.GetCameraMode() == 2)
		{
			//m_currentShadowPosition = Vector3.Cross(m_initialShadowPosition, m_player.transform.rotation.eulerAngles.normalized);
			//m_currentShadowPosition = Vector3.Cross(m_player.transform.rotation.eulerAngles.normalized, m_initialShadowPosition);
			//m_bevShadow.transform.SetPositionAndRotation(m_initialShadowPosition, m_initialShadowRotation);
		}
	}
}
