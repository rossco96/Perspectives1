using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	[SerializeField] private GameObject m_player;

	private void FixedUpdate()
	{
		//transform.position = m_player.transform.position;
		//transform.rotation = m_player.transform.rotation;
		
		transform.SetPositionAndRotation(m_player.transform.position, m_player.transform.rotation);

		/*
		transform.position = new Vector3
		(
			m_player.transform.position.x,
			m_player.transform.position.y,
			m_player.transform.position.z
		);
		transform.rotation = new Quaternion
		(
			m_player.transform.rotation.x,
			m_player.transform.rotation.y,
			m_player.transform.rotation.z,
			m_player.transform.rotation.w
		);
		//*/
	}
}
