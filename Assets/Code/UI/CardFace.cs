using System.Collections;
using Code.Cards;
using Code.UI.Selection;
using UnityEngine;

namespace Code.UI
{
	[RequireComponent(typeof(BoxCollider))]
	public sealed class CardFace : MonoBehaviour, ISelectable
	{
		[SerializeField]
		private SpriteRenderer _face;
		[SerializeField]
		private BoxCollider _boxCollider = null;
		[Header("Selection")]
		[SerializeField]
		private float _yOffsetWhenSelected = 2f;
		[SerializeField]
		private float _movementSelectionTime = .5f;
		[SerializeField]
		private AnimationCurve _movementCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
		
		private PlayerHand _playerHand;
		
		private Vector3 _originalPosition;
		private IEnumerator _moveCoroutine;

		public int OrderInLayer
		{
			get
			{
				return _face.sortingOrder;
			}
			set
			{
				_face.sortingOrder = value;
			}
		}

		public float Width
		{
			get
			{
				return _boxCollider.size.x;
			}
		}

		public float Height
		{
			get
			{
				return _boxCollider.size.y;
			}
		}

		public void Select()
		{
			if(_moveCoroutine != null)
			{
				StopCoroutine(_moveCoroutine);
			}
			else
			{
				_originalPosition = transform.localPosition;
			}
			
			_moveCoroutine = MoveTo(_originalPosition + Vector3.up * _yOffsetWhenSelected);
			StartCoroutine(_moveCoroutine);
		}

		public void Unselect()
		{
			if(_moveCoroutine != null)
			{
				StopCoroutine(_moveCoroutine);
			}
			
			_moveCoroutine = MoveTo(_originalPosition);
			StartCoroutine(_moveCoroutine);
		}
		
		public void SetFace(Card card, CardVisualSet visualSet)
		{
			_face.sprite = visualSet.GetCardSprite(card);
		}

		public void SetHolder(PlayerHand playerHand)
		{
			_playerHand = playerHand;
		}

		private IEnumerator MoveTo(Vector3 localPosition)
		{
			Vector3 startPosition = transform.localPosition;
			Vector3 totalDelta = localPosition - startPosition;
			float pathWalked = 0;
			float speed = 1f / _movementSelectionTime;
			
			while ( pathWalked < 1 )
			{
				pathWalked += Time.deltaTime * speed;
				transform.localPosition = Vector3.Lerp(startPosition, localPosition, _movementCurve.Evaluate(pathWalked));
				
				yield return new WaitForEndOfFrame();
			}
			
			transform.localPosition = localPosition;
			_moveCoroutine = null;
		}
	}
}