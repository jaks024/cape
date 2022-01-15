using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Objects.CellularAutomata
{
	public abstract class Cell
	{
		public GameObject VisualObject { get; private set; }
		public Cell[] Neighbours { get; private set; }
		protected CellState currentState;
		protected LinkedList<CellRule> rules;
		protected bool enabled;
		
		public Cell() { }

		public Cell(GameObject visualObject, CellState defaultState)
		{
			this.VisualObject = visualObject;
			currentState = defaultState;
			Neighbours = new Cell[8];
			rules = new LinkedList<CellRule>();
			enabled = true;
		}

		protected CellState[] GetNeighbourState()
		{
			CellState[] state = new CellState[8];
			for (int i = 0; i < Neighbours.Length; i++)
			{
				state[i] = Neighbours[i].currentState;
			}
			return state;
		}

		public void AddRule(CellRule rule)
		{
			rules.AddLast(rule);
		}

		public void RemoveRule(CellRule rule)
		{
			rules.Remove(rule);
		}

		public void ClearRules()
		{
			rules.Clear();
		}

		public void AssignNeighbours(Cell[] newNeighbours)
		{
			Neighbours = newNeighbours;
		}

		public void Update()
		{
			EvaluateRules();
			UpdateVisual();
		}

		public void EvaluateRules()
		{
			LinkedListNode<CellRule> node = rules.First;
			while(node != null)
			{
				if (node.Value.ReadyToEvaluate())
				{
					node.Value.EvaluateRule();
				}
				node = node.Next;
			}
		}
		public void UpdateState(int enumIndex)
		{
			currentState.UpdateState(enumIndex);
		}

		public abstract void UpdateVisual();

	}
}
