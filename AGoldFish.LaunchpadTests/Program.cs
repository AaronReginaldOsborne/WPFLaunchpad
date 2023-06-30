using AGoldFish.Launchpad;
using System;

namespace AGoldFish.LaunchpadTests
{
    class Program
    {
        static void Main(string[] args)
        {
            LaunchpadDevice device;

            Console.WriteLine("Launchpad Tests");
            Console.WriteLine("A Gold Fish 2019");
            Console.WriteLine("");

            try
            {
                device = new LaunchpadDevice();
                device.DoubleBuffered = true;

                Console.WriteLine("Launchpad found");
            }
            catch
            {
                Console.WriteLine("No launchpad found");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("");
            Console.WriteLine("0: Grid toggle");
            Console.WriteLine("1: Hello World");
            Console.WriteLine("2: Scrolling message");
            Console.WriteLine("3: Bulldog");
            Console.WriteLine("4: Rain sequencer");
            Console.WriteLine("5: Reversi");
            Console.WriteLine("6: Snake");
            Console.WriteLine("7: Btn Press");

            int i;
            while (!Int32.TryParse(Console.ReadLine(), out i))
            {
                Console.WriteLine("Try again...");
            }

            switch (i)
            {
                case 0:
                    ToggleGrid toggleGrid = new ToggleGrid(device);
                    toggleGrid.Run();
                    break;
                case 1:
                    string hwmsg = "Hello World";

                    HelloWorld helloWorld = new HelloWorld(device);
                    helloWorld.Text = hwmsg.ToUpper();
                    helloWorld.ScrollText();
                    break;
                case 2:
                    Console.Write("Type a message:");
                    string message = Console.ReadLine();

                    ScrollingLetters scrollingLetters = new ScrollingLetters(device);
                    scrollingLetters.Text = message.ToUpper();
                    scrollingLetters.ScrollText();
                    break;
                case 3:
                    Bulldog bulldog = new Bulldog(device);
                    bulldog.Play();
                    break;
                case 4:
                    RainSequencer rain = new RainSequencer(device);
                    rain.Run();
                    break;
                case 5:
                    Reversi reversi = new Reversi(device);
                    reversi.Run();
                    break;
                case 6:
                    Snake snake = new Snake(device);
                    snake.Run();
                    break;
                case 7:
                    Btn_Press btn_Press = new Btn_Press(device);
                    btn_Press.Run();
                    break;
                default:
                    Console.WriteLine("No such application");
                    break;
            }
        }
    }
}
