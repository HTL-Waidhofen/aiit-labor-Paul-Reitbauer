using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_05_QuaderSteuern
{
    internal class RectangleShape
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Width { get; }
        public int Height { get; }
        private readonly char _horizontal;
        private readonly char _vertical;
        private readonly char _corner;
        private readonly char _fill;

        public RectangleShape(int x, int y, int width, int height,
            char horizontal = '-',
            char vertical = '|',
            char corner = '+',
            char fill = ' ')
        {
            X = x;
            Y = y;
            Width = Math.Max(1, width);
            Height = Math.Max(1, height);
            _horizontal = horizontal;
            _vertical = vertical;
            _corner = corner;
            _fill = fill;
        }

        public void Move(int dx, int dy, int maxWidth, int maxHeight, int reservedBottom = 1)
        {
            // Clamp new position so the rectangle stays visible inside console window.
            var maxX = Math.Max(0, maxWidth - Width);
            var maxY = Math.Max(0, maxHeight - Height - reservedBottom);
            X = Clamp(X + dx, 0, maxX);
            Y = Clamp(Y + dy, 0, maxY);
        }

        public bool Intersects(int x, int y, int width, int height)
        {
            // Axis-aligned rectangle intersection (inclusive bounds)
            int ax1 = x;
            int ay1 = y;
            int ax2 = x + width - 1;
            int ay2 = y + height - 1;

            int bx1 = X;
            int by1 = Y;
            int bx2 = X + Width - 1;
            int by2 = Y + Height - 1;

            return ax1 <= bx2 && bx1 <= ax2 && ay1 <= by2 && by1 <= ay2;
        }

        public void Draw()
        {
            // Draw top line
            if (Height == 1)
            {
                DrawHorizontalLine(Y);
                return;
            }

            DrawCorner(X, Y);
            DrawHorizontals(X + 1, Y, Width - 2);
            if (Width > 1) DrawCorner(X + Width - 1, Y);

            // Draw middle lines
            for (int row = Y + 1; row < Y + Height - 1; row++)
            {
                DrawVerticals(row);
            }

            // Draw bottom line
            if (Height > 1)
            {
                DrawCorner(X, Y + Height - 1);
                DrawHorizontals(X + 1, Y + Height - 1, Width - 2);
                if (Width > 1) DrawCorner(X + Width - 1, Y + Height - 1);
            }
        }

        private void DrawHorizontalLine(int row)
        {
            SafeSetCursor(X, row);
            if (Width == 1)
            {
                Console.Write(_vertical);
            }
            else
            {
                Console.Write(_corner);
                if (Width - 2 > 0) Console.Write(new string(_horizontal, Width - 2));
                Console.Write(_corner);
            }
        }

        private void DrawCorner(int x, int y)
        {
            SafeSetCursor(x, y);
            Console.Write(_corner);
        }

        private void DrawHorizontals(int x, int y, int count)
        {
            if (count <= 0) return;
            SafeSetCursor(x, y);
            Console.Write(new string(_horizontal, count));
        }

        private void DrawVerticals(int row)
        {
            if (Width == 1)
            {
                SafeSetCursor(X, row);
                Console.Write(_vertical);
                return;
            }

            SafeSetCursor(X, row);
            Console.Write(_vertical);
            if (Width - 2 > 0)
            {
                Console.Write(new string(_fill, Width - 2));
            }
            SafeSetCursor(X + Width - 1, row);
            Console.Write(_vertical);
        }

        private static void SafeSetCursor(int x, int y)
        {
            try
            {
                Console.SetCursorPosition(x, y);
            }
            catch (ArgumentOutOfRangeException)
            {
                // Ignore drawing outside visible area
            }
        }

        private static int Clamp(int value, int min, int max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }
    }

    internal class Program
    {
        private const int ReservedBottomLines = 1; // for instructions/status

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            var rectangles = new List<RectangleShape>();

            int count = ReadInt("Wie viele Rechtecke sollen gezeichnet werden? ", 1, 100);

            for (int i = 0; i < count; i++)
            {
                Console.Clear();
                WriteCenteredLine($"Rechteck {i + 1} von {count}");
                int width = ReadInt("Breite (Zeichen): ", 1, Console.WindowWidth);
                int height = ReadInt("Höhe (Zeilen): ", 1, Console.WindowHeight - ReservedBottomLines);

                int maxX = Math.Max(0, Console.WindowWidth - width);
                int maxY = Math.Max(0, Console.WindowHeight - height - ReservedBottomLines);

                int x = ReadInt($"X-Position (0..{maxX}): ", 0, maxX);
                int y = ReadInt($"Y-Position (0..{maxY}): ", 0, maxY);

                var rect = new RectangleShape(x, y, width, height);
                rectangles.Add(rect);
            }

            // Zeichne alle Rechtecke
            RedrawAll(rectangles);

            // Bewegungs-Steuerung für das zuletzt gezeichnete Rechteck
            if (rectangles.Count > 0)
            {
                var last = rectangles[rectangles.Count - 1];
                ShowInstructions(null);

                while (true)
                {
                    var keyInfo = Console.ReadKey(true);
                    bool moved = false;

                    if (keyInfo.Key == ConsoleKey.Escape)
                    {
                        break;
                    }

                    int maxX = Math.Max(0, Console.WindowWidth - last.Width);
                    int maxY = Math.Max(0, Console.WindowHeight - last.Height - ReservedBottomLines);

                    int proposedX = last.X;
                    int proposedY = last.Y;

                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.A:
                        case ConsoleKey.LeftArrow:
                            proposedX = Math.Max(0, Math.Min(last.X - 1, maxX));
                            break;
                        case ConsoleKey.D:
                        case ConsoleKey.RightArrow:
                            proposedX = Math.Max(0, Math.Min(last.X + 1, maxX));
                            break;
                        case ConsoleKey.W:
                        case ConsoleKey.UpArrow:
                            proposedY = Math.Max(0, Math.Min(last.Y - 1, maxY));
                            break;
                        case ConsoleKey.S:
                        case ConsoleKey.DownArrow:
                            proposedY = Math.Max(0, Math.Min(last.Y + 1, maxY));
                            break;
                    }

                    // Prüfen: bleibt Position gleich -> keine Bewegung
                    if (proposedX == last.X && proposedY == last.Y)
                    {
                        continue;
                    }

                    // Kollisionstest mit allen anderen Rechtecken
                    bool collision = rectangles
                        .Where(r => !object.ReferenceEquals(r, last))
                        .Any(r => r.Intersects(proposedX, proposedY, last.Width, last.Height));

                    if (collision)
                    {
                        // Bewegung verhindern und Status anzeigen
                        Console.Beep(800, 120);
                        ShowInstructions("Kollision: Bewegung verhindert");
                        continue;
                    }

                    // Keine Kollision -> Bewegung ausführen
                    last.Move(proposedX - last.X, proposedY - last.Y, Console.WindowWidth, Console.WindowHeight, ReservedBottomLines);
                    moved = true;

                    if (moved)
                    {
                        RedrawAll(rectangles);
                        ShowInstructions(null);
                    }
                }
            }

            Console.CursorVisible = true;
            Console.Clear();
            Console.WriteLine("Programm beendet. Taste drücken...");
            Console.ReadKey(true);
        }

        private static void RedrawAll(List<RectangleShape> rectangles)
        {
            Console.Clear();
            foreach (var r in rectangles)
            {
                r.Draw();
            }
        }

        private static void ShowInstructions(string status)
        {
            var instr = "Bewegen: A,S,D,W oder Pfeiltasten  |  Beenden: Esc";
            var line = string.IsNullOrEmpty(status) ? instr : instr + "  |  " + status;
            int row = Console.WindowHeight - 1;
            try
            {
                Console.SetCursorPosition(0, row);
                Console.Write(new string(' ', Console.WindowWidth)); // clear line
                Console.SetCursorPosition(0, row);
                Console.Write(line);
            }
            catch (ArgumentOutOfRangeException)
            {
                // ignore if console size changed unexpectedly
            }
        }

        private static int ReadInt(string prompt, int min, int max)
        {
            while (true)
            {
                Console.Write(prompt);
                var input = Console.ReadLine();
                if (int.TryParse(input, out int v))
                {
                    if (v < min || v > max)
                    {
                        Console.WriteLine($"Bitte Zahl zwischen {min} und {max} eingeben.");
                        continue;
                    }
                    return v;
                }
                Console.WriteLine("Ungültige Eingabe. Bitte Ganzzahl eingeben.");
            }
        }

        private static void WriteCenteredLine(string text)
        {
            int w = Console.WindowWidth;
            int x = Math.Max(0, (w - text.Length) / 2);
            try
            {
                Console.SetCursorPosition(x, 0);
                Console.WriteLine(text);
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine(text);
            }
        }
    }
}