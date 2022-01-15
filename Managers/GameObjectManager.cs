using System;
using System.Collections.Generic;
using System.Text;
using Framework.Objects;
using Framework.Rendering;

namespace Framework.Managers
{
	public class GameObjectManager
	{
		private Dictionary<ulong, GameObject> objectDictionary;
		private ulong objectIdCount = 0;
		private Renderer renderer;
		public GameObjectManager(Renderer rend)
		{
			objectDictionary = new Dictionary<ulong, GameObject>();
			renderer = rend;
		}

		public ulong Add(GameObject go)
		{
			++objectIdCount;
			go.Id = objectIdCount;
			objectDictionary.Add(objectIdCount, go);
			renderer.AddObjectToLayer(go.Layer, go);
			return objectIdCount;
		}

		public bool Remove(ulong id)
		{
			if (objectDictionary.TryGetValue(id, out GameObject go))
			{
				renderer.RemoveObjectFromLayer(go.Layer, go);
			}
			return objectDictionary.Remove(id);
		}

		public bool Remove(GameObject go)
		{
			renderer.RemoveObjectFromLayer(go.Layer, go);
			return objectDictionary.Remove(go.Id);
		}

		public GameObject Get(ulong id)
		{
			objectDictionary.TryGetValue(id, out GameObject go);
			return go;
		}
	}
}
