using System;
using System.Collections.Generic;

namespace Gambling
{
    class Program
    {
        static Random random = new Random();

        static List<int> DealHand()
        {
            List<int> hand = new List<int>();
            hand.Add(DealCard());
            hand.Add(DealCard());
            return hand;
        }

        static int DealCard()
        {
            return random.Next(1, 14);
        }

        static int CalculateHandValue(List<int> hand)
        {
            int value = 0;
            int aces = 0;

            foreach (int card in hand)
            {
                if (card == 1)
                {
                    aces++;
                }
                else if (card > 10)
                {
                    value += 10;
                }
                else
                {
                    value += card;
                }
            }

            while (aces > 0 && value + 10 <= 21)
            {
                value += 10;
                aces--;
            }

            return value;
        }

        static void Main()
        {
            int green = 1000;
            Console.WriteLine("Mit szeretnél játszani?");
            Console.WriteLine("BlackJack (1)");
            int ans = Convert.ToInt32(Console.ReadLine());
            switch (ans)
            {
                case 1:
                    BlackJack(green);
                    Console.WriteLine();
                    break;

            }
        }
        static int BlackJack(int funds)
        {
            Console.WriteLine("Your bet (max = " + funds + "):");
            int bet = Convert.ToInt32(Console.ReadLine());


            List<int> playerHand = DealHand();
            List<int> dealerHand = DealHand();

            Console.WriteLine("Your hand: " + string.Join(", ", playerHand));
            Console.WriteLine("Dealer's hand: " + dealerHand[0] + ", hidden");

            while (true)
            {
                int playerValue = CalculateHandValue(playerHand);
                if (playerValue > 21)
                {
                    Console.WriteLine("You busted!");
                    return 0 - bet;
                    
                }
                else if (playerValue == 21)
                {
                    Console.WriteLine("Blackjack! You win!");
                    return bet;
                }

                Console.WriteLine("Hit or stand? (h/s): ");
                string input = Convert.ToString(Console.ReadLine());

                if (input == "h")
                {
                    playerHand.Add(DealCard());
                    Console.WriteLine("Your hand: " + string.Join(", ", playerHand));
                }
                else
                {
                    if (CalculateHandValue(playerHand) <= 21)
                    {
                        Console.WriteLine("Dealer's hand: " + string.Join(", ", dealerHand));
                        int dealerValue = CalculateHandValue(dealerHand);

                        while (dealerValue < 17)
                        {
                            dealerHand.Add(DealCard());
                            dealerValue = CalculateHandValue(dealerHand);
                            Console.WriteLine("Dealer hits: " + string.Join(", ", dealerHand));
                        }

                        if (dealerValue > 21)
                        {
                            Console.WriteLine("Dealer busts! You win!");
                            return bet;
                        }
                        else if (dealerValue > CalculateHandValue(playerHand))
                        {
                            Console.WriteLine("Dealer wins.");
                            return 0 - bet;
                        }
                        else if (dealerValue < CalculateHandValue(playerHand))
                        {
                            Console.WriteLine("You win!");
                            return bet;
                        }
                        else
                        {
                            Console.WriteLine("It's a tie.");
                            return 0;
                        }
                    }
                }

            
            }
        }
    }
}