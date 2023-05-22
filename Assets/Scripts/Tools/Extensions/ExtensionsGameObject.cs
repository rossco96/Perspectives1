using System.Collections.Generic;
using UnityEngine;

public static class ExtensionsGameObject
{
	public static T[] FindObjectsOfInterface<T>(this GameObject[] sceneRoots)
	{
		List<T> interfaces = new List<T>();

		for (int i = 0; i < sceneRoots.Length; ++i)
		{
			interfaces.AddRange(sceneRoots[i].GetComponentsInChildren<T>());
			//Transform[] children = sceneRoots[i].GetComponentsInChildren<Transform>();
			//for (int j = 0; j < children.Length; ++j)
			//{
			//	interfaces.AddRange(children[j].getcom)
			//}
		}

		return interfaces.ToArray();
	}
}
