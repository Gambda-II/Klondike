





using System.Reflection.Metadata;

namespace Klondike;

internal class Card
{
    public Suit suit;
    public Value value;
    public bool faceUp;

    private protected string[] symbols = { "♥", "♣", "♦", "♠" };

    public const int renderHeight = 7;
    public const int renderWidth = 9;

    public Card(Suit suit, Value value, bool faceUp)
    {
        this.suit = suit;
        this.value = value;
        this.faceUp = faceUp;
    }

    public string[] Render()
    {
        if (!faceUp && (int)value > 0)
        {
            return RenderBackSide();
        }
        else if (faceUp && (int)value > 0)
        {
            return RenderFrontSide();
        }
        else
        {
            return RenderPlaceholder();
        }
    }

    private static string[] RenderBackSide()
    {
        Console.BackgroundColor = ConsoleColor.DarkGreen;

        return new string[]
        {
                $"┌───────┐",
                $"|░░░░░░░|",
                $"│▒▒▒▒▒▒▒│",
                $"│▓▓▓▓▓▓▓│",
                $"│▓▓▓▓▓▓▓│",
                $"│▒▒▒▒▒▒▒│",
                $"|░░░░░░░|",
                $"└───────┘",
        };
    }

    private string[] RenderFrontSide()
    {
        int suitIndex = (int)suit;
        int cardValue = (int)value;
        string symbol = symbols[suitIndex];
        string displayedValue;

        if (CardHasAPicture(cardValue))
        {
            string valueConverted = value.ToString();
            char firstLetterOfPicture = valueConverted[0];

            displayedValue = $"{symbol}{firstLetterOfPicture}";
        }
        else
        {
            displayedValue = $"{symbol}{cardValue}";
        }


        string topLeftCorner = AddWhiteSpaceInString(displayedValue, 0, 1);
        string bottomRightCorner = AddWhiteSpaceInString (displayedValue, 1, 0);

        SetBackgroundColor(suit);

        Console.ForegroundColor = ConsoleColor.Black;
        return new string[]
        {
                $"┌───────┐",
                $"|{topLeftCorner}    |",
                $"│▀▄▀▄▀▄▀│",
                $"│       │",
                $"│       │",
                $"│▄▀▄▀▄▀▄│",
                $"|    {bottomRightCorner}|",
                $"└───────┘",
        };
    }

    private string AddWhiteSpaceInString(string original, int whitespacesOnTheLeft, int whitespacesOnTheRight)
    {
        int originalLength = original.Length;

        if (originalLength < 3)
        {
            return original;
        }

        string newText = original;

        if (whitespacesOnTheLeft > 0)
            newText = original.PadLeft(originalLength + whitespacesOnTheLeft);
        if (whitespacesOnTheRight > 0)
            newText = original.PadRight(originalLength + whitespacesOnTheLeft + whitespacesOnTheRight);
        
        return newText;
    }

    private bool CardHasAPicture(int cardValue)
    {
        return cardValue > 10 || cardValue == 1;
    }

    private void SetBackgroundColor(Suit suit)
    {
        if (SuitIsRed(suit))
        {
            SetConsoleBackgroundColorTo(ConsoleColor.Red);
            return;
        }

        if (SuitIsBlue(suit))
        {
            SetConsoleBackgroundColorTo(ConsoleColor.Blue);
            return;
        }

        SetConsoleBackgroundColorTo(ConsoleColor.Black);

    }

    private void SetConsoleBackgroundColorTo(ConsoleColor color)
    {
        Console.BackgroundColor = color;
    }

    private bool SuitIsBlue(Suit suit)
    {
        return suit.Equals(Suit.Spades) || suit.Equals(Suit.Clubs);
    }

    private bool SuitIsRed(Suit suit)
    {
        return suit.Equals(Suit.Hearts) || suit.Equals(Suit.Diamonds);
    }

    private static string[] RenderPlaceholder()
    {
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.White;
        return new string[]
        {
                $"┌───────┐",
                $"|       |",
                $"│       │",
                $"│       │",
                $"│       │",
                $"│       │",
                $"|       |",
                $"└───────┘",
        };
    }
}