using System;
using System.Collections.Generic;
using System.Text;
using Framework.Interfaces;


namespace Framework.Rendering
{

	public class Layer : IComparable
	{
		public bool IsVisible { get; private set; }
		public string Name { get; private set; }
		public int ZIndex { get; private set; }
		public List<IRenderable> Drawables { get; private set; }

		public Layer(string name, int zIndex)
		{
			IsVisible = true;
			Name = name;
			ZIndex = zIndex;
			Drawables = new List<IRenderable>();
		}

		public void Add(IRenderable obj)
		{
			Drawables.Add(obj);
		}

		public void Remove(IRenderable obj)
		{
			Drawables.Remove(obj);
		}

		public void ClearLayer()
		{
			Drawables.Clear();
		}

		public void SetVisibility(bool state)
		{
			IsVisible = state;
		}


		public int CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}
			Layer layer = obj as Layer;
			return ZIndex.CompareTo(layer.ZIndex);
		}
	}
}
