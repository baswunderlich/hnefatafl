using Hnefatafl.Hnefatafl;
using Hnefatafl.Hnefatafl.Rules;

var hnefatafl = new HnefataflGame(new RulesFetlar());
//Test for victory but flight
//Console.WriteLine("FirstMove");
//MakeMove(new Move(new Position(1, 2), new Position(0, 2)));
//Console.WriteLine("SecondMove");
//MakeMove(new Move(new Position(4, 0), new Position(0, 0)));
//
printBoard();
while (true)
{
    Console.WriteLine("From:");
    string fromEingabe;
    Position? fromPos = null;
    while (fromPos == null)
    {
        fromEingabe = getPosInput();
        fromPos = Position.CreatePosition(fromEingabe);
    }
    Console.WriteLine("To:");
    string toEingabe;
    Position? toPos = null;
    while (toPos == null)
    {
        toEingabe = getPosInput();
        toPos = Position.CreatePosition(toEingabe);
    }
    var events = MakeMove(new Move(fromPos, toPos));
    if (events.GetEvent().GetStatuscode() == 1)
    {
        break;
    }
}

void printBoard()
{
    var board = hnefatafl.GetBoard();
    Console.WriteLine("________________________________________");
    for(int y = 0; y < hnefatafl.GetBoardHeight(); y++)
    {
        for(int x = 0; x < hnefatafl.GetBoardWidth(); x++)
        {
            if (board[x, y] == Piece.Empty)
                Console.Write("   ");
            else if (board[x, y] == Piece.Attacker)
                Console.Write(" A ");
            else if (board[x, y] == Piece.Defender)
                Console.Write(" D ");
            else if (board[x, y] == Piece.King)
                Console.Write(" K ");
            else if (board[x, y] == Piece.Escape)
                Console.Write(" O ");
        }
        Console.WriteLine("\n");
    }
    Console.WriteLine("________________________________________");
}

string getPosInput()
{
    while (true)
    {
        var eingabe = Console.ReadLine();
        if (eingabe == null) continue;
        return eingabe;
    }
}

GameEventsObject MakeMove(Move move)
{
    var events = hnefatafl.PlayMove(move);
    if (events.GetEvent().GetStatuscode() == 1)
    {
        printBoard();
        Console.WriteLine(events.GetEvent().GetMessage());
    }
    else if (events.GetEvent().GetStatuscode() == 2)
    {
        Console.WriteLine(events.GetEvent().GetMessage());
    }
    printBoard();
    return events;
}