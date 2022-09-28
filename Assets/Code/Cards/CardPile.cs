using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Cards
{
    /// <summary>
    /// Class Carpile that contain methods for a pile of cards. It contains properties like number of cards,
    /// values, and types of cards. ~Sealed~ prevent other classes to inherit its
    /// properties.
    /// </summary>
    public sealed class CardPile
    {
        /// <summary>
        ///Creates a list of cards called _card. 
        /// </summary>
        private List<Card> _cards = new List<Card>();

        /// <summary>
        /// Return a number of cards. 
        /// </summary>
        public int Count
        {
            get { return _cards.Count; }
        }

        /// <summary>
        /// Return value of sum of value of the cards.
        /// </summary>
        public int Value
        {
            get
            {
                int value = 0;
                foreach (Card card in _cards)
                {
                    value += (int) card.Value;
                }

                return value;
            }
        }

        /// <summary>
        /// Return if the Card mount have queens in your deck.
        /// </summary>
        public bool HasQueen
        {
            get
            {
                foreach (Card card in _cards)
                {
                    if (card.Value == CardValue.Queen)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        /// <summary>
        /// Return if the Card mount have kings in your deck.
        /// </summary>
        public bool HasKing
        {
            get
            {
                foreach (Card card in _cards)
                {
                    if (card.Value == CardValue.King)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        /// <summary>
        /// Return if the Card mount have Jacks in your deck.
        /// </summary>
        public bool HasJack
        {
            get
            {
                foreach (Card card in _cards)
                {
                    if (card.Value == CardValue.Jack)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        /// <summary>
        /// Methods that creates a list of Cards.
        /// </summary>
        public CardPile()
        {
            _cards = new List<Card>();
            //Debug.Log("CardPile created");
        }

        /// <summary>
        /// Duvida A1
        /// </summary>
        /// <param name="original"></param>
        public CardPile(CardPile original)
        {
            _cards = new List<Card>(original._cards);
        }

        public void Add(Card card)
        {
            _cards.Add(card);
            //Debug.Log($"Card {Enum.GetName(typeof(CardValue), card.Value)} {Enum.GetName(typeof(CardSuit), card.Suit)} added to pile");
        }

        public void Remove(Card card)
        {
            _cards.Remove(card);
        }

        public Card RemoveTopCard()
        {
            if (_cards.Count > 0)
            {
                Card card = _cards[_cards.Count - 1];
                _cards.RemoveAt(_cards.Count - 1);
                return card;
            }

            return null;
        }
    }
}