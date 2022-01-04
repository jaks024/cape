using System;
using System.Collections.Generic;
using System.Text;

namespace wireman
{
	public static class GlobalObjectManager
	{
		private static Dictionary<ulong, GameObject> objectDictionary = new Dictionary<ulong, GameObject>();
		private static ulong objectIdCount = 0;


		public static ulong Add(GameObject go)
		{
			++objectIdCount;
			objectDictionary.Add(objectIdCount, go);
			Renderer.AddObjectToLayer(go.Layer, go);
			return objectIdCount;
		}

		public static bool Remove(ulong id)
		{
			if (objectDictionary.TryGetValue(id, out GameObject go))
			{
				Renderer.RemoveObjectFromLayer(go.Layer, go);
			}
			return objectDictionary.Remove(id);
		}

		public static GameObject Get(ulong id)
		{
			objectDictionary.TryGetValue(id, out GameObject go);
			return go;
		}
	}
}
