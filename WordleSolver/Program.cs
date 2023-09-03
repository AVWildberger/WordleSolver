/* Author: André V. Wildberger *
 * Date: September 2023        */
namespace WordleSolver
{
    internal class Program
    {
        static string file = "words.txt";

        static void Main()
        {
            char[] rightLettersInPlace = GetLettersInPlace();
            Console.WriteLine();
            char[] rightLetters = GetLetters(5);
            Console.WriteLine();

            char[] wrongLetters = {' '};

            string[] words = SearchWords(rightLettersInPlace, rightLetters, wrongLetters);

            if (words.Length > 1)
            {
                wrongLetters = GetLetters();

                words = SearchWords(rightLettersInPlace, rightLetters, wrongLetters);
            }

            if (words.Length > 1)
            {
                Console.WriteLine("Possible words are:\n");
                
                for (int i = 0; i < words.Length; i++)
                {
                    Console.WriteLine(words[i].ToUpper());
                }
            }
            else if (words.Length == 1)
            {
                Console.WriteLine($"Your word is: {words[0].ToUpper()}");
            }
            else
            {
                Console.WriteLine("Oh no! No words found with your filters!");
            }

            Console.WriteLine("\nDrücken Sie eine beliebige Taste zum beenden...");
            Console.ReadKey();
        }

        /// <summary>
        /// Searches words with the given filters.
        /// </summary>
        /// <param name="rightLettersInPlace">letters which are in the right place</param>
        /// <param name="rightLetters">letters which are right, but in the wrong place</param>
        /// <param name="wrongLetters">letters which are wrong</param>
        /// <returns>words with the given filter</returns>
        static string[] SearchWords(char[] rightLettersInPlace, char[] rightLetters, char[] wrongLetters)
        {
            string[] wordList = File.ReadAllLines(file);
            List<string> words = new List<string>();

            for (int i = 0; i < wordList.Length; i++)
            {
                bool isValid = true;

                for (int k = 0; k < rightLettersInPlace.Length; k++)
                {
                    if (rightLettersInPlace[k] != ' ' && rightLettersInPlace[k] != wordList[i].ToLower()[k])
                    {
                        isValid = false;
                    }
                }

                if (isValid && rightLetters.All(word => wordList[i].ToLower().Contains(word)) && !wrongLetters.Any(word => wordList[i].ToLower().Contains(word)))
                {
                    words.Add(wordList[i]);
                }
            }

            return words.ToArray();
        }

        /// <summary>
        /// Gets User Input for the letter which are already in place
        /// </summary>
        /// <returns>all letters which are in place</returns>
        static char[] GetLettersInPlace()
        {
            char[] rightLettersInPlace = new char[5];

            Console.WriteLine("Input letters right in place: (empty if there is no right letter in that place)");

            Console.Write("1st place: ");
            rightLettersInPlace[0] = ReadAndCheckUserInput();

            Console.Write("2nd place: ");
            rightLettersInPlace[1] = ReadAndCheckUserInput();

            Console.Write("3rd place: ");
            rightLettersInPlace[2] = ReadAndCheckUserInput();

            Console.Write("4th place: ");
            rightLettersInPlace[3] = ReadAndCheckUserInput();

            Console.Write("5th place: ");
            rightLettersInPlace[4] = ReadAndCheckUserInput();

            return rightLettersInPlace;
        }

        /// <summary>
        /// Gets User Input for letters
        /// </summary>
        /// <param name="maxLength">maximum length of the chars </param>
        /// <returns>returns array of letters</returns>
        static char[] GetLetters(int maxLength = int.MaxValue)
        {
            string chars;
            bool isValidInput = true;

            do
            {
                Console.WriteLine("Input letters right but not already in place: (directly next to each other) (empty if there are none)");

                chars = Console.ReadLine()!;

                if (chars.Length > maxLength)
                {
                    isValidInput = false;
                }

                for (int i = 0; i < chars.Length; i++)
                {
                    if ((chars[i] < 'a' && chars[i] > 'Z') || (chars[i] < 'A') || (chars[i] > 'z'))
                    {
                        isValidInput = false;
                    }
                }

                if (!isValidInput)
                {
                    Console.WriteLine("Wrong Input! Try again...");
                }

            } while (!isValidInput);

            char[] letters = chars.ToCharArray();

            return letters;
        }

        /// <summary>
        /// Reads chars from the user and checks if is valid
        /// </summary>
        /// <returns>char from user</returns>
        static char ReadAndCheckUserInput()
        {
            bool isValidInput = false;
            char ch = ' ';

            do
            {
                ConsoleKeyInfo cki = Console.ReadKey(true);

                if (cki.Key != ConsoleKey.Enter)
                {
                    ch = cki.KeyChar;

                    if ((ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z'))
                    {
                        isValidInput = true;
                    }
                }
                else
                {
                    isValidInput = true;
                }
            } while (!isValidInput);

            Console.WriteLine(char.ToLower(ch));

            return char.ToLower(ch);
        }
    }
}