using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class HeightBound : MonoBehaviour
{
	#region Vars
	private const float k_maxLevelHeight = 1000.0f;	// [TODO][Q] Have k_maxLevelHeight here?
	private const float k_halfPlayerHeight = 0.5f;		// [TODO][Q] Have k_playerHeight here?

	//private float m_minHeight;					// [TODO][Q] Do we need this? Will each game area only consist of one (max) height?
	[SerializeField] private float m_maxHeight;		// [TODO][DELETE] SF only for testing
	#endregion



	#region GETs
	public float GetMaxHeight() => m_maxHeight;
	#endregion



	#region Unity Methods
	private void Awake()
	{
		Vector3 centre = new Vector3
		(
			transform.position.x,
			k_maxLevelHeight,
			transform.position.z
		);

		//BoxCollider collider = GetComponent<BoxCollider>();
		//Vector3 halfs = 0.5f * collider.size;   // [TODO][Q] Do we need 0.5f * collider.size? or just collider.size?
		Vector3 halfs = 0.5f * transform.localScale;

		LayerMask layerMask = LayerMask.GetMask("TEST_LAYER");

		//string DEBUG_NAME = gameObject.name;

		// [TODO][Q] Also use layerMask belwow?
		Physics.BoxCast(centre, halfs, Vector3.down, out RaycastHit hit, Quaternion.identity, Mathf.Infinity, layerMask);
		m_maxHeight = hit.point.y + k_halfPlayerHeight;
	}



	//	o Using layers, not tags!
	//		--> GameObject marked with layer as GameArea
	//		--> GameAreas only interact with Player
	//		--> Don't need to check tag (etc.) with OnTriggerEnter!

	// [TODO][ISSUE] We can enter one collider whilst still within the other...
	// Make sure to leave gap!
	// And automate how they're generated please

	// [TODO][WARNING] Is there a race condition here? Between LevelManager::Awake and OnTriggerEnter?
	private void OnTriggerEnter(Collider col)
	{
		Debug.Log("[GameArea::OnTriggerEnter]");
		LevelManager.Instance.SetCurrentGameArea(this);

		// DO NOT DO THIS HERE! TESTING ONLY!
		if (LevelManager.Instance.GetCameraMode() == 2)
		{
			TEST_PlayerInpt_Extension.Instance.MovePlayerToMaxHeight();
		}
	}
	#endregion
}
