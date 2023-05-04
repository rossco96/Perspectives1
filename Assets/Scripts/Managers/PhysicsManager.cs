using UnityEngine;

public class PhysicsManager : MonoBehaviour, IManager
{
	private PhysicsBase m_currentPhysics;

	public void Init()
	{
		Debug.Log("[PhysicsManager::Init]");
	}

	public void SetCameraPhysics(PhysicsBase physics)
	{
		m_currentPhysics = physics;
	}

	public void OnAction()
	{
		m_currentPhysics.OnAction();
	}
}
