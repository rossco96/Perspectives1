using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

	private Perspective m_currentPerspective;

	private IManager[] m_managers;
	
	//private IPerspective[] m_perspectives;
	private Dictionary<Perspective, IPerspective[]> m_perspectives;
	private IPerspective[] m_currentPerspectives;
	#endregion


	// [TODO] THIS NAMING CONVENTION IS EXTREMELY CONFUSING!!!


	#region Unity Methods
	private void Awake()
	{
		Instance = this;

		//IPerspectiveFirstPerson[] firstPersons = FindObjectsOfInterface<IPerspectiveFirstPerson>();
		//IPerspectiveBEV[] bevs = FindObjectsOfType<IPerspectiveBEV>();
		//IPerspectiveThirdPerson[] thirdPersons = FindObjectsOfType<IPerspectiveThirdPerson>();

		GameObject[] rootObjects = SceneManager.GetActiveScene().GetRootGameObjects();
		IPerspectiveFirstPerson[] firstPersons = rootObjects.FindObjectsOfInterface<IPerspectiveFirstPerson>();
		IPerspectiveBEV[] bevs = rootObjects.FindObjectsOfInterface<IPerspectiveBEV>();
		IPerspectiveThirdPerson[] thirdPersons = rootObjects.FindObjectsOfInterface<IPerspectiveThirdPerson>();

		m_perspectives.Add(Perspective.FirstPerson, firstPersons);
		m_perspectives.Add(Perspective.BEV, bevs);
		m_perspectives.Add(Perspective.ThirdPerson, thirdPersons);

		m_currentPerspective = Perspective.FirstPerson;
		m_currentPerspectives = m_perspectives[m_currentPerspective];

		InitManagers();
	}

	private void Update()
	{
		// [TODO]
		// Do not do it like this!
		// No need for using the Update()
		// Can have a bool m_isActive										(note: must ensure that this bool exists in EVERY IPerspective class)
		// And use that in Update, OnTriggerEnter, and others!
		// To ensure that only the current perspective functions can be used

		for (int i = 0; i < m_currentPerspectives.Length; ++i)
		{
			m_currentPerspectives[i].Update();
		}

		// DEBUG BELOW ONLY
		if (Input.GetKeyUp(KeyCode.I))
		{
			m_currentPerspective = Perspective.FirstPerson;
			m_currentPerspectives = m_perspectives[m_currentPerspective];
		}
		if (Input.GetKeyUp(KeyCode.O))
		{
			m_currentPerspective = Perspective.BEV;
			m_currentPerspectives = m_perspectives[m_currentPerspective];
		}
		if (Input.GetKeyUp(KeyCode.P))
		{
			m_currentPerspective = Perspective.ThirdPerson;
			m_currentPerspectives = m_perspectives[m_currentPerspective];
		}
		// DEBUG ABOVE ONLY
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
