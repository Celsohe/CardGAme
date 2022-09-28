using System;
using System.Collections.Generic;
using Code.Cards;
using Code.Game;
using Code.Turn;
using Code.UI.Selection;
using UnityEngine;

namespace Code.UI
{
    public sealed class CardPileFace : MonoBehaviour, ISelectable, IInteractable
    {
        public enum State
        {
            Ready,
            Winner,
            Loser
        }

        /// <summary>
        /// Lista com as propriedades de CardpileFace? (Lista com o mesmo nome da classe? )
        /// </summary>
        private static List<CardPileFace> _all = new List<CardPileFace>();

        [Header("Configuration")] [SerializeField]
        private Player.Index _playerIndex;

        [SerializeField] private CardFace _cardFacePrefab;
        [SerializeField] private CardVisualSet _cardVisualSet;

        [Header("Components")] [SerializeField]
        private Transform _cardsParent;

        [SerializeField] private GameObject _cover;
        [SerializeField] private ParticleSystem _selectParticleSystem;
        [SerializeField] private ParticleSystem _victoryParticleSystem;
        [SerializeField] private ParticleSystem _defeatParticleSystem;

        [Header("Presentation")] [SerializeField]
        private float _cardDistance = .2f;

        private CardPile _cardPile = new CardPile();
        private List<CardFace> _cardFaces = new List<CardFace>();
        private State _state = State.Ready;

        public bool IsSelectable
        {
            get
            {
                switch (GameController.Instance.Stage)
                {
                    case GameController.GameStage.DistributeCards:
                        return false;
                    case GameController.GameStage.PlayersCreatePiles:
                        return false;
                    case GameController.GameStage.CombatRounds:
                        if (_state == State.Ready)
                        {
                            return true;
                        }

                        return false;
                    case GameController.GameStage.GameOver:
                        return false;
                    default:
                        return false;
                }
            }
        }

        public State PileState
        {
            get { return _state; }
        }

        public static List<CardPileFace> GetCardPilesFromPlayer(Player.Index playerIndex)
        {
            List<CardPileFace> playerCardPiles = new List<CardPileFace>();
            for (int i = 0; i < _all.Count; i++)
            {
                if (_all[i]._playerIndex == playerIndex)
                {
                    playerCardPiles.Add(_all[i]);
                }
            }

            return playerCardPiles;
        }

        public int Count
        {
            get { return _cardPile.Count; }
        }

        private void OnEnable()
        {
            TurnController.OnTurnChanged += OnTurnChanged;
            _all.Add(this);
        }

        private void Start()
        {
            OnTurnChanged(TurnController.Instance.CurrentPlayerIndex);
        }

        private void OnDisable()
        {
            TurnController.OnTurnChanged -= OnTurnChanged;
            _all.Remove(this);
        }

        public void Select()
        {
            _selectParticleSystem.Play(true);
        }

        public void Unselect()
        {
            _selectParticleSystem.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }

        public bool Interact(ISelectable selectedObject)
        {
            switch (GameController.Instance.Stage)
            {
                case GameController.GameStage.PlayersCreatePiles:
                    if (TurnController.Instance.CurrentPlayerIndex != _playerIndex)
                    {
                        return false;
                    }

                    if (selectedObject == null)
                    {
                        Card topCard = RemoveCardFromPile();
                        if (topCard != null)
                        {
                            PlayerHand playerHand = PlayerHand.GetPlayerHand(_playerIndex);
                            if (playerHand != null)
                            {
                                playerHand.AddCard(topCard);
                                return true;
                            }
                        }
                    }
                    else if (selectedObject is SelectableCard)
                    {
                        SelectableCard selectedCard = (SelectableCard) selectedObject;
                        AddCardToPile(selectedCard.GetComponent<CardFace>().Card);
                        selectedCard.RemoveSelfFromHand();
                        return true;
                    }

                    break;
                case GameController.GameStage.CombatRounds:
                    if (_state != State.Ready)
                    {
                        break;
                    }

                    if (selectedObject == null)
                    {
                        break;
                    }

                    if (selectedObject is CardPileFace)
                    {
                        CardPileFace otherPile = (CardPileFace) selectedObject;
                        if (_playerIndex != otherPile._playerIndex)
                        {
                            CardPile winnerPile = Combat.Fight(_cardPile, otherPile._cardPile);
                            if (winnerPile == _cardPile)
                            {
                                SetState(State.Winner);
                                otherPile.SetState(State.Loser);
                            }
                            else
                            {
                                SetState(State.Loser);
                                otherPile.SetState(State.Winner);
                            }

                            Selector.Instance.UnselectAny();
                            TurnController.Instance.ChangeTurn();
                        }
                    }

                    break;
            }

            return false;
        }

        public void AddCardToPile(Card card)
        {
            _cardPile.Add(card);

            CardFace cardFace = Instantiate(_cardFacePrefab, _cardsParent);
            cardFace.SetFace(card, _cardVisualSet);
            cardFace.OrderInLayer = _cardFaces.Count;
            _cardFaces.Add(cardFace);

            Refresh();
        }

        public Card RemoveCardFromPile()
        {
            Card topCard = _cardPile.RemoveTopCard();
            if (topCard != null)
            {
                CardFace cardFace = _cardFaces[_cardFaces.Count - 1];
                _cardFaces.RemoveAt(_cardFaces.Count - 1);
                Destroy(cardFace.gameObject);
                Refresh();
            }

            return topCard;
        }

        private void Refresh()
        {
            for (int i = 0; i < _cardFaces.Count; i++)
            {
                Vector3 cardPosition = _cardFaces[i].transform.localPosition;
                cardPosition.y = -_cardDistance * i;
                _cardFaces[i].transform.localPosition = cardPosition;
                _cardFaces[i].OrderInLayer = i;
            }
        }

        private void OnTurnChanged(Player.Index turn)
        {
            if (turn == _playerIndex)
            {
                _cardsParent.gameObject.SetActive(true);
                _cover.SetActive(false);
            }
            else
            {
                _cardsParent.gameObject.SetActive(false);
                _cover.SetActive(true);
            }
        }

        private void SetState(State state)
        {
            _state = state;
            switch (state)
            {
                case State.Ready:
                    break;
                case State.Winner:
                    _victoryParticleSystem.Play();
                    break;
                case State.Loser:
                    _defeatParticleSystem.Play();
                    break;
            }
        }
    }
}