using System;
using System.Collections.Generic;
//using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Object = System.Object;

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
				RaycastHit[] hits;
				List<int> layerList = new List<int>(); 
				hits = Physics.RaycastAll(_mainCamera.ScreenPointToRay(Input.mousePosition));
				
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
					ISelectable newSelectable = hits[selectIndex].transform.GetComponent<ISelectable>();
					if (newSelectable != null)
					{
						if (newSelectable == Selected)
						{
							Selected = null;
						}
						else
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
	}
}