using System;

namespace SnakeLadderGame
{
    class Game
    {
        Player currentPlayer;
        Cell[] board;
        Player[] playerQueue;
        int totalPlayers;
        public Game(int BoardSize, int NumberOfPlayers)
        {
            totalPlayers = NumberOfPlayers;
            board = CreateBoard(BoardSize);
            playerQueue = AssignPlayers(totalPlayers);
        }
        private Cell[] CreateBoard(int boardSize)
        {
            Cell[] board = new Cell[boardSize];
            for (int i = 0; i < boardSize; i++)
            {
                Cell c = new Cell();
                c.CellNumber = i + 1;
                board[i] = c;
            }
            bool isSnakeCellLeft = true;
            while (isSnakeCellLeft) {
                Console.WriteLine("Want to define a snake (~~~~~~~~~@) y/n");
                if (Console.ReadLine().ToLower()=="y")
                {
                    Console.WriteLine("Snake cell number");
                    int snakeCellNumber = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Penalty cell number");
                    int penaltyCellNumber = Convert.ToInt32(Console.ReadLine());
                    SnakeCell s = new SnakeCell();
                    s.CellNumber = snakeCellNumber;
                    s.PenaltyCell = penaltyCellNumber;
                    board[snakeCellNumber-1] = s;
                }
                else
                {
                    isSnakeCellLeft = false;
                }
            }

            bool isLadderCellLeft = true;
            while (isLadderCellLeft)
            {
                Console.WriteLine("Want to define a ladder (|-|-|-|-|-|-|) y/n");
                if (Console.ReadLine().ToLower() == "y")
                {
                    Console.WriteLine("Ladder cell number");
                    int ladderCellNumber = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Advantage cell number");
                    int advantageCellNumber = Convert.ToInt32(Console.ReadLine());
                    LadderCell l = new LadderCell();
                    l.CellNumber = ladderCellNumber;
                    l.AdvantageCell = advantageCellNumber;
                    board[ladderCellNumber - 1] = l;
                }
                else
                {
                    isLadderCellLeft = false;
                }
            }
            return board;
        }
        private Player[] AssignPlayers(int numberOfPlayers)
        {
            Player[] players = new Player[numberOfPlayers];
            for (int i = 0; i < numberOfPlayers; i++)
            {
                players[i] = new Player();
                players[i].CurrentCellPosition = 0;
                players[i].PlayerNumber = i + 1;
                Console.WriteLine("Enter your name");
                players[i].PlayerName = Console.ReadLine();
            }
            return players;
        }
        private int RollDice()
        {
                Random rnd = new Random();
                return rnd.Next(1, 6);
        }
        private void NextChance()
        {
            if (currentPlayer.PlayerNumber< totalPlayers)
            {
                /*Player Number is one more than his/her position in Player Queue, hence (currentPlayer.PlayerNumber - 1)*/
                currentPlayer = playerQueue[(currentPlayer.PlayerNumber - 1) + 1];
            }
            else
            {
                currentPlayer = playerQueue[0];
            }
        }        
        private void CalculatePlayerPosition(int diceNumber)
        {
            Console.WriteLine(currentPlayer.PlayerName + ", your dice shows " +diceNumber);
            int moveLocation = currentPlayer.CurrentCellPosition;
            if ((moveLocation + diceNumber) <= board.Length)
            {
                moveLocation = moveLocation + diceNumber;
                Console.WriteLine(currentPlayer.PlayerName + ", moved to " + moveLocation);
            }
            else
            {
                Console.WriteLine(currentPlayer.PlayerName + ", stays at " + moveLocation);
            }
            
            while ( board[moveLocation-1].GetType() == typeof(SnakeCell) || board[moveLocation - 1].GetType() == typeof(LadderCell))
            {
                if (board[moveLocation - 1].GetType() == typeof(SnakeCell))
                {
                    moveLocation = (board[moveLocation - 1] as SnakeCell).PenaltyCell;
                    Console.WriteLine(currentPlayer.PlayerName + "!!! Snake bite :( , moving to " + moveLocation);
                }
                if (board[moveLocation - 1].GetType() == typeof(LadderCell))
                {
                    moveLocation = (board[moveLocation - 1] as LadderCell).AdvantageCell;
                    Console.WriteLine(currentPlayer.PlayerName + " found ladder :D , moving to " + moveLocation);
                }
            }
            currentPlayer.CurrentCellPosition = moveLocation;
        }
        public void Play()
        {
            currentPlayer = playerQueue[0];
            bool isFirstMove = true;
            while (currentPlayer.CurrentCellPosition != board.Length)
            {                
                if (!isFirstMove)
                {
                    NextChance();
                }
                isFirstMove = false;
                CalculatePlayerPosition(RollDice());
            }
            Console.WriteLine(currentPlayer.PlayerName + " wins");
            foreach (Player p in playerQueue)
            {
                Console.WriteLine(p.PlayerName + " is at "+ p.CurrentCellPosition);
            }
            Console.WriteLine("Game Over!!!");
        }
    }
}
