using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    class Program
    {
        enum HandType { Rock, Paper, Scissors };
        enum MainMenuSelection { StartGame, ShowRules, EndGame };
        enum SubMenuSelection { NextRound, ShowStatus, GiveUp }; //
        
        static void Main(string[] args)
        {
            
        int UserInput;
            do
            {
                Console.Clear();
                Console.WriteLine("#Rock Paper Scissors");
                Console.WriteLine("1) Start game");
                Console.WriteLine("3) Show rules");
                Console.WriteLine("4) End game");
                UserInput = int.Parse(Console.ReadLine());


                if (UserInput == (int)MainMenuSelection.StartGame + 1)
                {
                    Console.Clear();
                    int gameMode;
                    Console.WriteLine("1) Player vs Player");
                    Console.WriteLine("2) Player vs AI");
                    Console.WriteLine("3) Tournament, requires 4 people");
                    List<Player> players;
                    List<Player> players2;
                    gameMode = int.Parse(Console.ReadLine());
                    if (gameMode == 1)
                    {
                        players = definePlayers(2);
                        defineHand(players);
                        compareHands(players);
                    }
                    else if (gameMode == 3)
                    {
                       // int userInputNum;
                        Console.Clear();
                       // Console.WriteLine("How many players will participate in the tournament? Max 4 min 3");
                       // userInputNum = int.Parse(Console.ReadLine());
                        players = definePlayers(2);
                        players2 = definePlayers(2);
                        PlayTournament(players, players2);
                        
                    }
                    else
                    {
                        players = new List<Player>();
                        Console.Clear();
                        string name;
                        Console.WriteLine("Enter your name: ");
                        name = Console.ReadLine();
                        players.Add(new Player { Name = name });
                        players.Add(new Player { Name = "AI" });

                        Console.Clear();
                        defineHand(players);
                        compareHands(players);
                    }             
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("1) Next round");
                        Console.WriteLine("2) Status");
                        UserInput = int.Parse(Console.ReadLine());
                        if (UserInput == (int)SubMenuSelection.NextRound + 1)
                        {
                            defineHand(players);
                            compareHands(players);
                        }
                        if(UserInput == (int)SubMenuSelection.ShowStatus + 1)
                        {
                            ShowStatus(players);
                        }            

                    } while (!hasWonYet(players));
                    Console.Clear();
                    if(players[0].RoundsWon == 3)
                    {
                        Console.WriteLine($"{players[0].Name} won the game!");
                        Console.WriteLine("Press enter to continue");
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine($"{players[1].Name} won the game!");
                        Console.WriteLine("Press enter to continue");
                        Console.ReadLine();
                    }
                }

                if(UserInput == (int)MainMenuSelection.ShowRules + 1)
                {
                    ShowRules();
                }
            } while (UserInput != (int)MainMenuSelection.EndGame + 1);
        }


        public static void PlayRound(List<Player> players)
        {
            int UserInput;
            int player1rw = players[0].RoundsWon;
            int player2rw = players[1].RoundsWon;

            do
            {
                Console.Clear();
                Console.WriteLine("1) Next round");
                Console.WriteLine("2) Status");
                UserInput = int.Parse(Console.ReadLine());
                if (UserInput == (int)SubMenuSelection.NextRound + 1)
                {
                    defineHand(players);
                    compareHands(players);
                }
                if (UserInput == (int)SubMenuSelection.ShowStatus + 1)
                {
                    ShowStatus(players);
                }

            } while (players[0].RoundsWon - player1rw < 3 && players[1].RoundsWon - player2rw < 3);
            Console.Clear();
            if (players[0].RoundsWon - player1rw >= 3)
            {
                players[0].GamesWon++;
                Console.WriteLine($"{players[0].Name} won the game!");
                Console.WriteLine("Press enter to continue");
                Console.ReadLine();
            }
            else if (players[1].RoundsWon - player2rw >= 3)
            {
                players[1].GamesWon++;
                Console.WriteLine($"{players[1].Name} won the game!");
                Console.WriteLine("Press enter to continue");
                Console.ReadLine();
            }


        }


        public static void ShowRules() //Command (Clears the console, aka changes something)
        {
            Console.Clear();
            Console.WriteLine("#Rules:\n"+"1) Rock beats scissors\n2) Scissors beats paper\n3) Paper beats rock\n"+
                "Win three rounds.\n");
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
        }



        public static void ShowStatus(List<Player> players) //Command (Clears the console, aka changes something)
        {
            Console.Clear();
            Console.WriteLine($"#Current score:\n"
                 + $"({players[0].RoundsWon}){players[0].Name}\n"
                 + $"({players[1].RoundsWon}){players[1].Name}");
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
        }

        

        public static List<Player> definePlayers(int numOfPlayers) //Command, defines the player names dependant on player input
        {
            List<Player> players = new List<Player>();

            for (int i = 0; i < numOfPlayers; i++)
            {
                Console.Clear();
                string name;
                Console.WriteLine($"Player {i+1}, enter your name");
                name = Console.ReadLine();
                players.Add(new Player { Name = name });
            }
            return players;
        }
        //ensure: name_defined
            //name_for(a_player).is_equal(a_name)
        //ensure list_players exists
            //list_players != Void



        //require: name_defined
            //name_for(a_player).is_equal(a_name)
        //require list_players exists
            //list_players != void
        public static List<Player> defineHand(List<Player> players)//Command, defines the hand of every player based on input
        {
            foreach (Player p in players) //0 --> 1
            {
                if (p.Name != "AI")
                {
                    Console.Clear();
                    Console.WriteLine($"{p.Name} select your hand");
                    Console.WriteLine("1) Rock\n2) Paper\n3) Scissor");
                    
                    p.Hand = int.Parse(Console.ReadLine()) - 1;
                }
                else
                {
                    Random rnd = new Random();
                    p.Hand = rnd.Next(0, 2);
                   // p.Hand = random number between 0-2
                }
             
            }
            return players;
        }
        //ensure: hand_defined
            //hand_for(a_player).is_equal(a_hand)


        //require: hand_defined
            //hand_for(a_player).is_equal(a_hand)
        public static string CompareHandsHelper(List<Player> players)//Query
        {           
            if (players[0].Hand == players[1].Hand)
            {
                return "Draw";
            }

            if (players[0].Hand == (int)HandType.Rock  && players[1].Hand == (int)HandType.Scissors ) // Rock vs Scissors = Rock wins	
            {
                return players[0].Name;
            }

            if (players[0].Hand == (int)HandType.Rock && players[1].Hand == (int)HandType.Paper) // Rock vs Paper = Paper wins
            {
                return players[1].Name;
            }

            if (players[0].Hand == (int)HandType.Scissors  && players[1].Hand == (int)HandType.Rock ) // Scissors vs Rock = Rock wins	
            {
                return players[1].Name;
            }

            if (players[0].Hand == (int)HandType.Scissors  && players[1].Hand == (int)HandType.Paper ) //Scissors vs Paper = Scissors wins	
            {
                return players[0].Name;
            }

            if (players[0].Hand == (int)HandType.Paper  && players[1].Hand == (int)HandType.Scissors ) //Paper vs Scissors = Scissors wins	
            {
                return players[1].Name;
            }

            else // Paper vs Rock = Paper wins
            {
                return players[0].Name;
            }
        }


        //require list_players exists
            //list_players != Void
        public static bool hasWonYet(List<Player> players) //Query 
        {
            foreach (var player in players)
            {
                if (player.RoundsWon == 3)
                {
                    return true;
                }
            }
            return false;
        }



        //require: name_defined
            //name_for(a_player).is_equal(a_name)

        //require: hand_defined
            //hand_for(a_player).is_equal(a_hand)

        //require: list_players exists
            //list_players != void
        public static void compareHands(List<Player> players) //command
        {
            string winner = CompareHandsHelper(players);
            Console.Clear();
            if (winner == players[0].Name)
            {
                players[0].RoundsWon += 1;
                Console.WriteLine($"{players[0].Name} selected {(HandType)players[0].Hand}\n"
                                 +$"{players[1].Name} selected {(HandType)players[1].Hand}\n");
                Console.WriteLine($"{(HandType)players[0].Hand} beats {(HandType)players[1].Hand}!\n"
                                 +$"{players[0].Name} wins this round!\n");
                Console.WriteLine("Press enter to continue");
                Console.ReadLine();
            }
            else if (winner == players[1].Name)
            {
                players[1].RoundsWon += 1;
                Console.WriteLine($"{players[0].Name} selected {(HandType)players[0].Hand}\n"
                                 + $"{players[1].Name} selected {(HandType)players[1].Hand}\n");
                Console.WriteLine($"{(HandType)players[1].Hand} beats {(HandType)players[0].Hand}!\n"
                                 + $"{players[1].Name} wins this round!\n");
                Console.WriteLine("Press enter to continue");
                Console.ReadLine();
            }
            else //if(winner == "Draw")
            {
                Console.WriteLine("Draw!");
                Console.WriteLine("Press enter to continue");
                Console.ReadLine();
            }
        }

        public static void PlayTournament(List<Player> players, List<Player> players2)
        {
            List<Player> allPlayers = new List<Player>();
            Console.WriteLine("{0} and {1} are going to play against eachother first",players[0].Name,players[1].Name);
            Console.ReadLine();
            PlayRound(players);
            Console.WriteLine("Now {0} and {1} are going to play against eachother",players2[0].Name,players2[1].Name);
            Console.ReadLine();
            PlayRound(players2);

            players2.Add(players[0]);
            players.RemoveAt(0);

            players.Add(players2[0]);
            players2.RemoveAt(0);

            Console.WriteLine("Now {0} and {1} are going to play against eachother", players[0].Name, players[1].Name);
            Console.ReadLine();

            PlayRound(players);
            Console.WriteLine("Now {0} and {1} are going to play against eachother", players2[0].Name, players2[1].Name);
            Console.ReadLine();
            PlayRound(players2);

            players2.Add(players[0]);
            players.RemoveAt(0);

            players.Add(players2[1]);
            players2.RemoveAt(1);

            Console.WriteLine("Now {0} and {1} are going to play against eachother", players[0].Name, players[1].Name);
            Console.ReadLine();
            PlayRound(players);
            Console.WriteLine("Now {0} and {1} are going to play against eachother", players2[0].Name, players2[1].Name);
            Console.ReadLine();
            PlayRound(players2);

            allPlayers.Add(players[0]);
            allPlayers.Add(players[1]);
            allPlayers.Add(players2[0]);
            allPlayers.Add(players2[1]);

            allPlayers.OrderBy(x => x.GamesWon);
            allPlayers = new List<Player>(allPlayers.OrderBy(player => player.GamesWon));
            Console.WriteLine("The scores are:");
            for (int i = 0; i < allPlayers.Count; i++)
            {
                Console.WriteLine(allPlayers[i].Name + " Rounds won: " + allPlayers[i].RoundsWon + " Games won: " + allPlayers[i].GamesWon );
            }

            //Console.WriteLine("The scores are:");
            //foreach (var item in allPlayers)
            //{
            //    Console.WriteLine(item.Name + " " + item.RoundsWon);
            //}
            Console.ReadLine();
        }
    }
}
