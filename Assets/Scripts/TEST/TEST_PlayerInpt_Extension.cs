using UnityEngine;

public class TEST_PlayerInpt_Extension : MonoBehaviour
{
	public static TEST_PlayerInpt_Extension Instance;

	[SerializeField] private GameObject m_playerGO;

	private void Awake()
	{
		Instance = this;
	}

	private void Update()
	{
		if (Input.GetKeyUp(KeyCode.Space))
		{
			LevelManager.Instance.PhysicsOnAction();
		}
	}

	public void MovePlayerToMaxHeight()
	{
		m_playerGO.transform.position = new Vector3
		(
			m_playerGO.transform.position.x,
			LevelManager.Instance.GetGameAreaMaxHeight(),
			m_playerGO.transform.position.z
		);

		Rigidbody rb = m_playerGO.GetComponent<Rigidbody>();
		rb.useGravity = false;
	}
}
