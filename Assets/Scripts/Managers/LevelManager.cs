using UnityEngine;

public class LevelManager : MonoBehaviour
{
	#region Vars
	public static LevelManager Instance;

	// [TODO][Q]
	//	o Is doing the LevelManager like this too convoluted?						<-- (check spelling of convoluted pls)
	//	o Theoretically any method in here can call any type of sub-manager
	//	o Should I just Instance the other sub-managers and call where needed?
	//	o Try drawing out dependency graph and see...

	[SerializeField] private PhysicsManager m_physicsManager;
	[SerializeField] private AreaManager m_areaManager;

	private IManager[] m_managers;
	#endregion



	#region Unity Methods
	private void Awake()
	{
		Instance = this;
		InitManagers();
	}
	#endregion



	#region PUBLIC METHODS
	public void SetCameraMode(int mode)
	{
		switch (mode)
		{
			case 1:
				SetNextPhysics(new PhysicsFirstPerson());
				break;

			case 2:
				SetNextPhysics(new PhysicsBEV());
				TEST_PlayerInpt_Extension.Instance.MovePlayerToMaxHeight();			// [TODO][DELETE] This is debug script only!!!
				break;

			case 3:
				SetNextPhysics(new PhysicsThirdPerson());
				break;

			default:
				break;
		}
		m_cameraMode = mode;														// temp here???
	}

	// [TODO] Temp (???) here!
	private int m_cameraMode = 0;
	public int GetCameraMode() => m_cameraMode;
	#endregion



	#region PRIVATE METHODS
	private void InitManagers()
	{
		m_managers = GetComponentsInChildren<IManager>();
		for (int i = 0; i < m_managers.Length; ++i)
		{
			Debug.Log($"[LevelManager::Awake][{i}] {m_managers[i].GetType()}.");
			m_managers[i].Init();
		}
	}
	#endregion



	#region Area Methods
	public void SetCurrentGameArea(HeightBound gameArea)
	{
		m_areaManager.SetCurrentGameArea(gameArea);
	}

	public float GetGameAreaMaxHeight()
	{
		return m_areaManager.GetMaxHeight();
	}
	#endregion



	#region Physics Methods
	public void SetNextPhysics(PhysicsBase physics)
	{
		m_physicsManager.SetCameraPhysics(physics);
	}

	public void PhysicsOnAction()
	{
		m_physicsManager.OnAction();
	}
	#endregion
}
