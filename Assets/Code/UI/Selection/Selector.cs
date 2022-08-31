﻿using System.Collections.Generic;
using UnityEngine;

namespace Code.UI.Selection
{
	public sealed class Selector : MonoBehaviour
	{
		private static Selector _instance;

		[SerializeField]
		private Camera _mainCamera = null;
		
		private ISelectable _selected;
		
		public static Selector Instance
		{
			get
			{
				return _instance;
			}
		}
		
		public ISelectable Selected
		{
			get
			{
				return _selected;
			}
			private set
			{
				if (_selected != null)
				{
					_selected.Unselect();
				}
				_selected = value;
				if (_selected != null)
				{
					_selected.Select();
				}
			}
		}

		private void Awake()
		{
			if(_instance != null)
			{
				Destroy(gameObject);
				return;
			}
			_instance = this;
		}

		private void Update()
		{
			if(Input.GetMouseButtonDown(0))
			{
				GameObject hitObjectOnTop = GetHitObjectOnTop();

				if (hitObjectOnTop != null)
				{
					// TODO: Try to interact
					//if (Selected != null)
					//{
						IInteractable interactable = hitObjectOnTop.GetComponent<IInteractable>();
						if (interactable != null)
						{
							if (interactable.Interact(Selected))
							{
								Selected = null;
								return;
							}
						}
					//}

					ISelectable newSelectable = hitObjectOnTop.GetComponent<ISelectable>();
					if (newSelectable != null)
					{
						if (newSelectable == Selected)
						{
							Selected = null;
						}
						else if(newSelectable.IsSelectable)
						{
							Selected = newSelectable;
						}
					}
					else
					{
						Selected = null;
					}
				} 
				else
				{
					Selected = null;
				}
			}
		}

		private void OnDestroy()
		{
			if (_instance == this)
			{
				_instance = null;
			}
		}

		public void UnselectAny()
		{
			Selected = null;
		}
		
		private GameObject GetHitObjectOnTop()
		{
			RaycastHit[] hits;
			List<int> layerList = new List<int>(); 
			hits = Physics.RaycastAll(_mainCamera.ScreenPointToRay(Input.mousePosition), 100f);
				
			for (int i = 0; i < hits.Length; i++) 
			{
				RaycastHit hit = hits[i];
				SpriteRenderer renderer = hit.transform.GetComponent<SpriteRenderer>();
				if (renderer != null)
				{
					layerList.Add(renderer.sortingOrder);
				}
				else
				{
					layerList.Add(-1);
				}
			}

			int selectIndex = -1;
			int maxOrder = -1;
			for (int i = 0; i < hits.Length; i++)
			{
				if (layerList[i] > maxOrder)
				{
					maxOrder = layerList[i];
					selectIndex = i;
				}
			}
			if (selectIndex > -1)
			{
				return hits[selectIndex].transform.gameObject;
			}
			return null;
		}
	}
}