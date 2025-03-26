using System.Collections.Generic;
using Google.Cloud.Firestore;

namespace ManagerApplication.Model
{
    [FirestoreData]
    public class FireCard
    {
        [FirestoreProperty]
        public int CardId { get; set; }

        [FirestoreProperty]
        public string CardName { get; set; }

        [FirestoreProperty]
        public double CardBalance { get; set; }

        [FirestoreProperty]
        public string CardLocation { get; set; } = "-";

        // Parameterless constructor required by Firestore
        public FireCard()
        {
        }

        // Constructor to convert Card to FireCard
        public FireCard(Card card)
        {
            CardId = card.card_id;
            CardName = card.card_name;
            CardBalance = card.card_balance;
            CardLocation = "Zarafshon";
        }

        // Static method to convert List<Card> to List<FireCard>
        public static List<FireCard> ConvertToFireCards(List<Card> cards)
        {
            List<FireCard> fireCards = new List<FireCard>();

            foreach (var card in cards)
            {
                fireCards.Add(new FireCard(card));
            }

            return fireCards;
        }
    }
}
