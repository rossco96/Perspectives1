using UnityEngine;

public class AreaManager : MonoBehaviour, IManager
{
	[SerializeField] private HeightBound[] m_allAreas;					// [TODO] Delete! SF for testing only!
	private HeightBound m_currentArea;

	public void Init()
	{
		m_allAreas = FindObjectsOfType<HeightBound>();					// [TODO][Q] What am I doing with this anyway, if I'm just passing the current area?
		for (int i = 0; i < m_allAreas.Length; ++i)
		{
			Debug.Log($"[AreaManager::Init] {m_allAreas[i].name}");
		}
	}

	public void SetCurrentGameArea(HeightBound gameArea)
	{
		m_currentArea = gameArea;
	}

	public float GetMaxHeight()
	{
		return m_currentArea.GetMaxHeight();
	}

	// [TODO]
	// If doing the below like this, could work out the max height on HeightBounds like this too?
	// But like, do it from here, so can iterate through all the HeightBounds rather than doing individually
	// Better still! Should create separate window with all environment functions!

	#region Inspector (ContextMenu)
	[ContextMenu(itemName: "Assign All Areas")]
	private void AssignAllAreas()
	{
		m_allAreas = FindObjectsOfType<HeightBound>();                  // [TODO][Q] What am I doing with this anyway, if I'm just passing the current area?
		for (int i = 0; i < m_allAreas.Length; ++i)
		{
			Debug.Log($"[AreaManager::Init] {m_allAreas[i].name}");
		}
	}
	#endregion
}
