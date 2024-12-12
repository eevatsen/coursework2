public class GameService : IGameService
{
    private readonly Database _db;

    public GameService(Database db)
    {
        _db = db;
    }

    public void PlayGame(string player1, string player2)
    {
        char[,] board = new char[3, 3];
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                board[i, j] = ' ';
            }
        }

        string currentPlayer = player1;
        char currentSymbol = 'X';

        while (true)
        {
            Console.Clear();
            PrintBoard(board);

            Console.WriteLine($"{currentPlayer}'s turn ({currentSymbol})");
            Console.Write("Enter row (1-3): ");
            int row = int.Parse(Console.ReadLine()) - 1;
            Console.Write("Enter column (1-3): ");
            int col = int.Parse(Console.ReadLine()) - 1;

            if (row < 0 || row >= 3 || col < 0 || col >= 3 || board[row, col] != ' ')
            {
                Console.WriteLine("Invalid move. Try again.");
                Console.ReadKey();
                continue;
            }

            board[row, col] = currentSymbol;

            if (CheckWin(board, currentSymbol))
            {
                Console.Clear();
                PrintBoard(board);
                Console.WriteLine();
                Console.WriteLine($"{currentPlayer} wins!");
                _db.AddGame(new Game(player1, player2, currentPlayer));
                var winnerUser = _db.GetUser(currentPlayer);
                if (winnerUser != null) winnerUser.Rating += 50;
                break;
            }

            if (IsDraw(board))
            {
                Console.Clear();
                PrintBoard(board);
                Console.WriteLine("It's a draw!");
                _db.AddGame(new Game(player1, player2, "Draw"));
                break;
            }

            // switch players
            currentPlayer = currentPlayer == player1 ? player2 : player1;
            currentSymbol = currentSymbol == 'X' ? 'O' : 'X';
        }
    }

    public List<Game> GetGamesByPlayer(string username) => _db.GetGamesByPlayer(username);

    public List<Game> GetAllGames() => _db.GetAllGames();

    private void PrintBoard(char[,] board)
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Console.Write(board[i, j]);
                if (j < 2) Console.Write("|");
            }
            Console.WriteLine();
            if (i < 2) Console.WriteLine("-+-+-");
        }
    }

    private bool CheckWin(char[,] board, char symbol)
    {
        // check rows and columns
        for (int i = 0; i < 3; i++)
        {
            if ((board[i, 0] == symbol && board[i, 1] == symbol && board[i, 2] == symbol) ||
                (board[0, i] == symbol && board[1, i] == symbol && board[2, i] == symbol))
            {
                return true;
            }
        }

        // check diagonals
        if ((board[0, 0] == symbol && board[1, 1] == symbol && board[2, 2] == symbol) ||
            (board[0, 2] == symbol && board[1, 1] == symbol && board[2, 0] == symbol))
        {
            return true;
        }

        return false;
    }

    private bool IsDraw(char[,] board)
    {
        foreach (var cell in board)
        {
            if (cell == ' ') return false;
        }
        return true;
    }
}
