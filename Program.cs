using System;
using System.Collections.Generic;

namespace AnagramGameApp
{
    // Interface for the anagram game
    public interface IAnagramGame
    {
        void StartGame();
    }

    // Class responsible for handling the anagram game logic
    public class AnagramGame : IAnagramGame
    {
        private readonly List<string> words;
        private static readonly Random random = new();

        private int score;

        public AnagramGame(List<string> words)
        {
            this.words = words;
            score = 0;
        }

        public void StartGame()
        {
            Console.WriteLine("Welcome to the White Stripes Anagram Game!");
            Console.WriteLine($"Score: {score}");

            bool playAgain = true;

            while (playAgain && words.Count > 0)
            {
                string selectedWord = GetRandomWord();
                string anagram = GenerateAnagram(selectedWord);

                Console.WriteLine("Guess the word:");
                Console.WriteLine($"Anagram: {anagram}");
                string guess = ReadUserInputToLower();
                int attempts = 1;
                bool hintGiven = false;

                while (guess != selectedWord && attempts < 3)
                {
                    Console.WriteLine("Oops! That's incorrect.");

                    if (!hintGiven)
                    {
                        Console.WriteLine("Do you want a hint? (yes/no)");
                        string hintInput = ReadUserInputToLower();

                        if (hintInput == "yes")
                        {
                            Console.WriteLine($"Hint: The first letter is {selectedWord[0]}");
                            hintGiven = true;
                        }
                    }

                    Console.WriteLine("Guess the word again:");
                    guess = ReadUserInputToLower();
                    attempts++;
                }

                if (guess == selectedWord)
                {
                    Console.WriteLine("Congratulations! You guessed it right!");
                    score++;
                }
                else
                {
                    Console.WriteLine("Sorry, you didn't guess correctly within the given attempts.");
                    Console.WriteLine($"The word was: {selectedWord}");
                }

                Console.WriteLine($"Score: {score}");

                if (words.Count > 0)
                {
                    Console.WriteLine("Play another word? (yes/no)");
                    string playAgainInput = ReadUserInputToLower();
                    // Determine if the user wants to play another word based on their input
                    playAgain = playAgainInput == "yes";
                }
                else
                {
                    Console.WriteLine("No more words to play.");
                    playAgain = false;
                }
            }

            Console.WriteLine($"Your final score: {score}");
            Console.WriteLine("Thank you for playing!");
        }

        private string GetRandomWord()
        {
            int index = random.Next(0, words.Count);
            string word = words[index];
            words.RemoveAt(index);
            return word;
        }

        private static string GenerateAnagram(string word)
        {
            char[] chars = word.ToCharArray();

            for (int i = 0; i < chars.Length; i++)
            {
                int j = random.Next(i, chars.Length);
                (chars[j], chars[i]) = (chars[i], chars[j]);
            }

            return new string(chars);
        }

        private static string ReadUserInputToLower()
        {
            return (Console.ReadLine() ?? string.Empty).ToLower();
        }
    }

    // Entry point of the program
    public class Program
    {
        public static void Main()
        {
            List<string> whiteStripesWords = new() { "acorn", "aluminum", "apple", "biscuit", "blossom", "button", "denial", "dirty", "elephant", "faith", "finger", "forever", "friend", "ghost", "gentleman", "ground", "hello", "hotel", "infirmary", "little", "lonely", "martyr", "medicine", "nation", "offend", "operator", "orchid", "pocket", "pretty", "protector", "sister", "special", "stripe", "thump", "truth", "twist", "union", "white" };
            List<string> nickCaveWords = new() { "breathless", "beautiful", "people", "nowhere", "black", "world", "bright", "horse", "children", "jubilee", "boson", "weeping", "prayer", "idiot", "cannibal", "fable" };

            Console.WriteLine("Choose word array: 1 for White Stripes, 2 for Nick Cave");

            string? arrayChoice = Console.ReadLine();

            IAnagramGame anagramGame;

            if (arrayChoice == "1")
            {
                anagramGame = new AnagramGame(whiteStripesWords);
            }
            else if (arrayChoice == "2")
            {
                anagramGame = new AnagramGame(nickCaveWords);
            }
            else
            {
                Console.WriteLine("Invalid choice. Exiting game.");
                return;
            }
            anagramGame.StartGame();
        }
    }
}