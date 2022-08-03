using System;
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
				RaycastHit hit;
				if(Physics.Raycast(_mainCamera.ScreenPointToRay(Input.mousePosition), out hit))
				{
					ISelectable selectable = hit.transform.GetComponent<ISelectable>();
					if(selectable != null)
					{
						Selected = selectable;
					}
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