void MyCreateRectangle(int width, int height, int color, char symbol)
{
    color--;
    List<ConsoleColor> colors = new() {
        ConsoleColor.Red, ConsoleColor.Green,
        ConsoleColor.Blue, ConsoleColor.Magenta,
        ConsoleColor.Yellow, ConsoleColor.Cyan
    };

    int sred_height = height / 2 + height % 2;
    int sred_width = width / 2 + width % 2;

    for (int k = 0; k < height; k++)
    {
        int count_right_change = 0;
        int count_left_change = 0;
        for (int i = 0; i < width; i++)
        {
            if (k < sred_height)
            {
                if (i < k || i +k >width - 1)
                {
                    if (i < width / 2 + width % 2)
                    {
                        Console.ForegroundColor = colors[(i + color) % 6];
                        Console.Write(symbol);
                        Console.ResetColor();
                        count_left_change = i < width/2 ? ++count_left_change : count_left_change;
                    }
                    else
                    {
                        Console.ForegroundColor = colors[Math.Abs((color + count_left_change - 1 - count_right_change) % 6)];
                        Console.Write(symbol);
                        Console.ResetColor();
                        count_right_change++;
                    }

                }
                else
                {
                    Console.ForegroundColor = colors[(color + k) % 6];
                    Console.Write(symbol);
                    Console.ResetColor();
                }
            }
            else
            {
                if (k >= height - (sred_width))
                {
                    if (i< (height - 1) - k || i + (height -1 - k) >width - 1)
                    {
                        if (i < sred_width)
                        {
                            Console.ForegroundColor = colors[(i + color) % 6];
                            Console.Write(symbol);
                            Console.ResetColor();
                            count_left_change = i < width/2 ? ++count_left_change : count_left_change;
                        }
                        else
                        {
                            Console.ForegroundColor = colors[Math.Abs((color + count_left_change - 1 - count_right_change) % 6)];
                            Console.Write(symbol);
                            Console.ResetColor();
                            count_right_change++;
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = colors[(color + height -1 - k) % 6];
                        Console.Write(symbol);
                        Console.ResetColor();
                    }
                }
                else
                {
                    if (i < width / 2 + width % 2)
                    {
                        Console.ForegroundColor = colors[(i + color) % 6];
                        Console.Write(symbol);
                        Console.ResetColor();
                        count_left_change = i < width/2 ? ++count_left_change : count_left_change;
                    }
                    else
                    {
                        Console.ForegroundColor = colors[Math.Abs((color + count_left_change - 1 - count_right_change) % 6)];
                        Console.Write(symbol);
                        Console.ResetColor();
                        count_right_change++;
                    }
                }

            }
        }
        Console.WriteLine();
    }

}

void DrawSnake(int width, int height, bool clock)
{
    //char symbol = '█';
    char symbol = '*';


    Console.ForegroundColor = ConsoleColor.Green;

    int sdvig = 1;
    int x_start = 0;
    int x_end = width;
    int max_count_line = height / 2 + height % 2;
    int sred_width = width / 2 + width % 2;
    if (clock)
    {
        for (int i = 0; i < max_count_line; i++)
        {
            x_end = x_end > width / 2 + width % 2 - 1 ? --x_end : x_end;
            x_start = i > 2 && x_start <= x_end ? x_start++ : x_start;
            Console.WriteLine(x_start + " "+ x_end);
            for (int j = 0; j <width; j++)
            {
                if (i % 2 == 0)
                {
                    if (j < x_start)
                    {
                        if (j % 2 == 0)
                        {
                            Console.Write(symbol);
                        }
                        else
                        {
                            Console.Write(' ');
                        }
                    }
                    if (j >= x_start &&  j <= x_end)
                    {
                        Console.Write(symbol);
                    }
                    else
                    {
                        if (x_end % 2 == 1)
                        {
                            if (j % 2 == 0)

                                Console.Write(' ');

                            else
                                Console.Write(symbol);
                        }
                        else
                        {
                            if (j % 2 == 0)

                                Console.Write(symbol);

                            else
                                Console.Write(' ');
                        }
                    }
                }
                else
                {
                    if (j < x_start)
                    {
                        if (j % 2 == 0)
                        {
                            Console.Write(symbol);
                        }
                        else
                        {
                            Console.Write(' ');
                        }
                    }
                    if (j >= x_start &&  j <= x_end)
                    {
                        Console.Write(' ');
                    }
                    else
                    {
                        if (x_end % 2 == 0)
                        {
                            if (j % 2 == 1)

                                Console.Write(symbol);

                            else
                                Console.Write(' ');
                        }
                        else
                        {
                            if (j % 2 == 1)

                                Console.Write(' ');

                            else
                                Console.Write(symbol);
                        }
                    }
                }
            }
            Console.WriteLine();
        }
    }
}

Console.WriteLine("Что вы хотите нарисовать? 1 - прямоугольник; 2 - змейка");
int.TryParse(Console.ReadLine(), out int Change_play);
bool Check = true;
switch (Change_play)
{
    case 1:
        while (Check)
        {
            Console.WriteLine("Ширина фигуры?");
            int.TryParse(Console.ReadLine(), out int width);
            Console.WriteLine("Высота фигуры?");
            int.TryParse(Console.ReadLine(), out int heigth);
            Console.WriteLine("Цвет (1 - Красный, 2 - зелёный, 3 - Синий\n" +
                "      4 - Пурпурный, 5 - жёлтый, 6 - Голубой)?");
            int.TryParse(Console.ReadLine(), out int color);
            Console.WriteLine("Какой символ использовать?");
            char symbol = char.Parse(Console.ReadLine());

            MyCreateRectangle(width, heigth, color, symbol);

            Console.WriteLine("Хотите ещё порисовать? [Yes/No]");
            string text = Console.ReadLine();
            Check = (text.ToUpper() == "YES") ? true : false;
        }
        break;
    case 2:
        while (Check)
        {
            Console.WriteLine("Ширина Улитки?");
            int.TryParse(Console.ReadLine(), out int width);
            Console.WriteLine("Высота Улитки?");
            int.TryParse(Console.ReadLine(), out int heigth);
            Console.WriteLine("По часовой? [Yes/No]");
            string vector = Console.ReadLine();
            bool clock = (vector.ToUpper() == "YES") ? true : false;

            DrawSnake(width, heigth, clock);

            Console.WriteLine("Хотите ещё порисовать? [Yes/No]");
            string text = Console.ReadLine();
            Check = (text.ToUpper() == "YES") ? true : false;
        }
        break;

    default:

        break;

}


