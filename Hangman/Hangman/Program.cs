using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman
{
    class Program
    {
        //setting global variables and lists
        static bool guessingTheWord = true;
        static int numberOfGuesses = 7;
        static string lettersGuessed = string.Empty;
        static List<string> wordList = new List<string>() { "photograph", "lullaby", "animals", "hero", "nickelback", "someday" };
        static Random rng = new Random();
        static string wordToBeGuessed = wordList[rng.Next(0, wordList.Count)];
        static string playersGuess = string.Empty;
        static string playersName = string.Empty;
        static string nickelbackLyrics = string.Empty;

        static void Main(string[] args)
        {
            //calls Hangman game
            Hangman();

            //keeping the console open
            Console.ReadKey();
        }

        static void Hangman()
        {
            //clears the console
            Console.Clear();
            //greets the user and asks for their name
            Console.Write("Quick! What is your name?! ");

            //adds player's name to the <playersName> string
            playersName = Console.ReadLine();

            //clears the console
            Console.Clear();

            //Describes the game for the player and prompts them to begin the game
            Console.WriteLine(@"Okay {0},

You are going to play the most important game of Hangman of your life!

The fate of the world depends on you!

Your mission:

Travel back in time to 1994 to Hanna, Alberta to prevent the band Nickelback
from forming. The only way to save millions from developing a horrible taste in music is
to win a round of Hangman against Nickelback frontman, Chad Kroeger.", playersName);
            Console.ReadLine();
            Console.Clear();

            Console.WriteLine(@"Kroeger is notorious for betting and losing his cool when he 
is defeated in a game of Hangman. If you can beat him {0}, by guessing each letter in a word 
until you have either missed 7 times or have guessed the word, and ensure he promises to never 
play music ever again if you win, you might just save humanity.

Do you think you can do it? The fate of the world is in your hands...

Press enter to travel back in time. And {0}....Good luck.", playersName);

            //allows them to start playing the game
            Console.ReadLine();
            //clears the console
            Console.Clear();

            //keeps the game running
            while (guessingTheWord)
            {

                //keeps checks the users input to see if it anything in the word
                while (numberOfGuesses > 0 || MaskedWord().Contains(wordToBeGuessed))
                {
                    //prints blanks for the word to guess
                    Console.WriteLine("The word to guess is: {0}\n", MaskedWord());
                    //prints the letters guessed and number of guesses left
                    Console.WriteLine("{0}, you have guessed:\n\n{1}\n\nYou have {2} guesses left.\n", playersName, lettersGuessed, numberOfGuesses);
                    //stores the users guess
                    playersGuess = Console.ReadLine();

                    //checks length of input
                    if (playersGuess.Length == 1)
                    {
                        //checks if letter is in word
                        if (wordToBeGuessed.Contains(playersGuess.ToLower()))
                        {
                            //if so, calls correct guess
                            correctGuess();

                            //checks if all of the letters have been guessed
                            if (MaskedWord().Contains(wordToBeGuessed))
                            {
                                //if so, prints congrats message
                                Console.WriteLine("Congrats {0}! You guessed the word {1} letter by letter!!", playersName, wordToBeGuessed);
                                Console.WriteLine("You have saved us all from the radio torture of Nickleback! But be careful...Kroeger might want to play again...");
                                //asks the user to play again
                                playAgain();
                            }
                        }
                        //checks if it is a special character
                        else if (". ,';?!1234567890".Contains(playersGuess))
                        {
                            //prints error message
                            notALetter();
                        }

                        else
                        {
                            //calls wrong guess function
                            wrongGuess();
                            //run out of guesses
                            if (numberOfGuesses == 0)
                            {
                                //prints losing message
                                Console.WriteLine("YOU DIDN'T GUESS THE WORD!!\nNOOOOOOOOOOOOO!!!\n\n");
                                //calls nickelbackLives function
                                nickelbackLives();
                                //asks if they want to play again
                                playAgain();
                            }
                        }
                    }

                    else
                    {
                        //if they enter more than 1 letter
                        if (playersGuess.Length > 1)
                        {
                            //checks if it is the word to guess
                            if (playersGuess == wordToBeGuessed)
                            {
                                wholeWordGuessed();
                                playAgain();
                            }
                            //if not the word, calls wrong word function
                            wrongWord();
                        }
                    }
                }
                //closes game loop
                guessingTheWord = false;
            }
        }

        /// <summary>
        /// Masks the word that needs to be guessed
        /// </summary>
        /// <returns>blanks or letters depending on if it was guessed or not/returns>
        static string MaskedWord()
        {
            //empty string to store letters
            string returnedString = string.Empty;
            //checks each letter in the word
            for (int i = 0; i < wordToBeGuessed.Length; i++)
            {
                //if the player's guess is in the word
                if (lettersGuessed.Contains(wordToBeGuessed[i].ToString()))
                {
                    //adds it to the string
                    returnedString += wordToBeGuessed[i];
                }
                else
                {
                    //adds a blank space to the empty string
                    returnedString += "_ ";
                }
            }
            //returns filled string
            return returnedString;
        }

        /// <summary>
        /// Checks if the whole word was guessed
        /// </summary>
        static void wholeWordGuessed()
        {
            //checks the player's guess
            if (playersGuess == wordToBeGuessed)
            {
                //clears console
                Console.Clear();
                //prints congrats effort
                Console.WriteLine(@"You guessed {0} and were right! You've saved us all!
We must now send you to Stratford, Ontario to proctect the newborn musical savior...
Justin Bieber.", wordToBeGuessed);
            }
        }

        /// <summary>
        /// Returns a statment if the wrong word was guessed 
        /// </summary>
        static void wrongWord()
        {
            Console.WriteLine("That wasn't the word! Guess again before Kroeger wins!!");
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine(lettersGuessed);
            numberOfGuesses--;
            Console.Clear();
        }

        /// <summary>
        /// Prints message when the player makes a correct guess
        /// and adds to the counters and strings
        /// </summary>
        static void correctGuess()
        {
            //prints the congrats message
            Console.WriteLine("\nYou guessed one of the letters! You're one step closer to victory!");
            //times the message
            System.Threading.Thread.Sleep(1000);
            //adds to the letters guessed string
            lettersGuessed += playersGuess.ToLower();
            //clears console
            Console.Clear();
            //calls masked word function
            MaskedWord();
        }

        /// <summary>
        /// Prints a variety of messages if the player's guess was incorrect
        /// and adds to the counters and strings
        /// </summary>
        static void wrongGuess()
        {
            //calls a random song lyric
            switch (numberOfGuesses)
            {
                case 1:
                    nickelbackLyrics = "Look at this photographhhh. Everytime I'm here it makes me laugh..\n";
                    break;
                case 2:
                    nickelbackLyrics = "How did our eyes get so red? and what the hell is on Joey's head?\n";
                    break;
                case 3:
                    nickelbackLyrics = "How the hell did we wind up like this?\n";
                    break;
                case 4:
                    nickelbackLyrics = "Why weren't we able, to see the signs that we missed and try to turn the tables\n";
                    break;
                case 5:
                    nickelbackLyrics = "I wish you'd unclenched your fists and unpack your suitcase.\n";
                    break;
                case 6:
                    nickelbackLyrics = "Never made it as a wise man, never cut it as a poor man stealing\n";
                    break;
                case 7:
                    nickelbackLyrics = "I couldn't cut it as a poor man stealing. Tired of living like a blind man.\n";
                    break;
            }

            //prints message when guess is wrong
            Console.WriteLine("That's not in the word! Oh no! Kroger has started singing!\n\n♫ ♫ {0}", nickelbackLyrics);
            //times the message
            System.Threading.Thread.Sleep(1000);
            //clears console
            Console.Clear();
            lettersGuessed += playersGuess;
            Console.WriteLine(lettersGuessed);
            //decrements number of guesses
            numberOfGuesses--;
            Console.Clear();
        }

        /// <summary>
        /// Called when the player's guess isn't a letter
        /// </summary>
        static void notALetter()
        {
            //prints error message
            Console.WriteLine("Sorry that isn't a letter! Please enter a letter.");
            //times the message
            System.Threading.Thread.Sleep(1000);
            //decrements guess counter
            numberOfGuesses--;
            //clears console
            Console.Clear();
        }

        /// <summary>
        /// Asks the user to play again
        /// </summary>
        static void playAgain()
        {
            //printed message asks the user to play again
            Console.WriteLine("\nKroger wants to double down. Want to play again?\n\nIf so, press Enter.");
            //resets the guess counter
            numberOfGuesses = 7;
            //resets the string that stores the letters guessed
            lettersGuessed = string.Empty;
            //regenerates a random word
            wordToBeGuessed = wordList[rng.Next(0, wordList.Count)];
            //Calls masked word funtion
            MaskedWord();
            //clears console when player hits a key
            Console.ReadLine();
            Console.Clear();
        }

        /// <summary>
        /// Prints Nickelback banner
        /// </summary>
        static void nickelbackLives()
        {
            //prints to the console
            Console.WriteLine(@" _______  .__        __          .__ ___.                  __    
 \      \ |__| ____ |  | __ ____ |  |\_ |__ _____    ____ |  | __
 /   |   \|  |/ ___\|  |/ // __ \|  | | __ \\__  \ _/ ___\|  |/ /
/    |    \  \  \___|    <\  ___/|  |_| \_\ \/ __ \\  \___|    < 
\____|__  /__|\___  >__|_ \\___  >____/___  (____  /\___  >__|_ \
        \/        \/     \/    \/         \/     \/     \/     \/");


        }
    }
}
