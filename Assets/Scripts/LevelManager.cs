using UnityEngine;

public class LevelManager : MonoBehaviour
{
	public static LevelManager Instance;

	private PhysicsBase m_physics;



	private void Awake()
	{
		Instance = this;
	}



	public void SetNextPhysics(PhysicsBase physics)
	{
		m_physics = physics;
	}
}
