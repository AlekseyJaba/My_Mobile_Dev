using System.ComponentModel.Design;
using System.Drawing;
using System.Net.Mail;

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
    char symbol = '█';
    Console.ForegroundColor = ConsoleColor.Green;
    int sred_height = height / 2 + height % 2;
    int sred_width = width / 2 + width % 2;
    char[,] mass = new char[height, width];
    int min_ = width <= height ? sred_width : sred_height;
    int now_width = width;
    int now_height = height;
    int count = 0;
    while (count <= min_)
    {
        if (count % 2 == 0)
        {
            for (int y = count; y < now_height; y++)
            {
                for (int x = count; x < now_width; x++)
                {
                    if (y == count || y == now_height - 1)
                        mass[y, x] = symbol;
                    else
                    {
                        mass[y, count] = symbol;
                        mass[y, now_width - 1] = symbol;
                    }
                }
                if (clock)
                    mass[count + 1, count] = ' ';
                else
                    mass[count + 1, now_width - 1] = ' ';
            }
        }
        else
        {
            for (int y = count; y < now_height; y++)
            {
                for (int x = count; x < now_width; x++)
                {
                    if (y == count || y == now_height - 1)
                        mass[y, x] = ' ';
                    else
                    {
                        mass[y, count] = ' ';
                        mass[y, now_width - 1] = ' ';
                    }
                }
                if (clock)
                    mass[count + 1, count] = symbol;
                else
                    mass[count + 1, now_width - 1] = symbol;
            }
        }
        now_width--;
        now_height--;
        count++;
    }
    for (int y = 0; y < height; y++)
    {
        for (int j = 0; j < width; j++)
        {
            Console.Write(mass[y, j]);
        }
        Console.WriteLine();
    }
    Console.ForegroundColor = ConsoleColor.White;
}

void CreateMap(int width, int height, int count_mine)
{
    Random rnd = new Random();
    int[,] map = new int[height, width];
    List<ConsoleColor> colors = new() {
        ConsoleColor.DarkGray, ConsoleColor.Blue,
        ConsoleColor.Green, ConsoleColor.Magenta,
        ConsoleColor.Yellow, ConsoleColor.Cyan,
        ConsoleColor.DarkGreen, ConsoleColor.DarkMagenta,
        ConsoleColor.DarkYellow, ConsoleColor.DarkRed
    };
    List<char> symbol = new()
    {
        ' ', '1','2','3','4','5','6','7', '8', '*'
    };
    int around_mine;
    while (count_mine > 0)
    {
        (int x, int y) mine;
        mine.x = rnd.Next(0, width);
        mine.y = rnd.Next(0, height);
        while (map[mine.y, mine.x] == -1)
        {
            mine.x = rnd.Next(0, width);
            mine.y = rnd.Next(0, height);
        }
        map[mine.y, mine.x] = -1;
        if (mine.y + 1 < height && mine.y - 1 >=0 && mine.x + 1 < width && mine.x - 1 >=0) // не крайние 
        {
            for (int i = mine.y - 1; i <= mine.y + 1; i++)
            {
                for (int j = mine.x - 1; j <= mine.x + 1; j++)
                {
                    if (map[i, j] != -1)
                    {
                        map[i, j] += 1;
                    }
                }
            }
        }
        else // края 
        {
            if (mine.y == 0) // первая строка
            {
                if (mine.x != 0 &&  mine.x != width - 1) // не крайние
                {
                    for (int y = mine.y; y <= mine.y + 1; y++)
                    {
                        for (int x = mine.x - 1; x <= mine.x + 1; x++)
                        {
                            if (map[y, x] != -1)
                            {
                                map[y, x] += 1;
                            }
                        }
                    }
                }
                else // крайние в первой строке
                {
                    if (mine.x == 0) // левый верхний угол
                    {
                        for (int y = mine.y; y <= mine.y + 1; y++)
                        {
                            for (int x = mine.x; x <= mine.x + 1; x++)
                            {
                                if (map[y, x] != -1)
                                {
                                    map[y, x] += 1;
                                }
                            }
                        }
                    }
                    else //Верхний правый угол
                    {
                        for (int y = mine.y; y <= mine.y + 1; y++)
                        {
                            for (int x = mine.x - 1; x <= mine.x; x++)
                            {
                                if (map[y, x] != -1)
                                {
                                    map[y, x] += 1;
                                }
                            }
                        }
                    }
                }
            }
            if (mine.y == height - 1) // нижняя строка
            {
                if (mine.x != 0 &&  mine.x != width - 1) // не крайние
                {
                    for (int y = mine.y - 1; y <= mine.y; y++)
                    {
                        for (int x = mine.x - 1; x <= mine.x + 1; x++)
                        {
                            if (map[y, x] != -1)
                            {
                                map[y, x] += 1;
                            }
                        }
                    }
                }
                else // крайние в последней строке
                {
                    if (mine.x == 0) // левый нижний угол
                    {
                        for (int y = mine.y - 1; y <= mine.y; y++)
                        {
                            for (int x = mine.x; x <= mine.x + 1; x++)
                            {
                                if (map[y, x] != -1)
                                {
                                    map[y, x] += 1;
                                }
                            }
                        }
                    }
                    else //Нижний правый угол
                    {
                        for (int y = mine.y - 1; y <= mine.y; y++)
                        {
                            for (int x = mine.x - 1; x <= mine.x; x++)
                            {
                                if (map[y, x] != -1)
                                {
                                    map[y, x] += 1;
                                }
                            }
                        }
                    }
                }
            }
            if (mine.y !=0 && mine.y != height-1) //левая и правая стена не включая углы
            {
                if (mine.x == 0) // левая
                {
                    for (int y = mine.y - 1; y <= mine.y+1; y++)
                    {
                        for (int x = mine.x; x <= mine.x + 1; x++)
                        {
                            if (map[y, x] != -1)
                            {
                                map[y, x] += 1;
                            }
                        }
                    }
                }
                else//правая
                {
                    for (int y = mine.y - 1; y <= mine.y+1; y++)
                    {
                        for (int x = mine.x - 1; x <= mine.x; x++)
                        {
                            if (map[y, x] != -1)
                            {
                                map[y, x] += 1;
                            }
                        }
                    }
                }
            }
        }

        count_mine--;
    }
    for (int i = 0; i<height; i++)
    {
        for (int j = 0; j < width; j++)
        {
            switch (map[i, j])
            {
                case 0:
                    Console.BackgroundColor = colors[0];
                    Console.Write(symbol[0]);
                    Console.ResetColor();
                    break;
                case 1:
                    Console.ForegroundColor = colors[1];
                    Console.Write(symbol[1]);
                    Console.ResetColor();
                    break;
                case 2:
                    Console.ForegroundColor = colors[2];
                    Console.Write(symbol[2]);
                    Console.ResetColor();
                    break;
                case 3:
                    Console.ForegroundColor = colors[3];
                    Console.Write(symbol[3]);
                    Console.ResetColor();
                    break;
                case 4:
                    Console.ForegroundColor = colors[4];
                    Console.Write(symbol[4]);
                    Console.ResetColor();
                    break;
                case 5:
                    Console.ForegroundColor = colors[5];
                    Console.Write(symbol[5]);
                    Console.ResetColor();
                    Console.Beep();
                    break;
                case 6:
                    Console.ForegroundColor = colors[6];
                    Console.Write(symbol[6]);
                    Console.ResetColor();
                    Console.Beep();
                    break;
                case 7:
                    Console.ForegroundColor = colors[7];
                    Console.Write(symbol[7]);
                    Console.ResetColor();
                    Console.Beep();
                    break;
                case 8:
                    Console.ForegroundColor = colors[8];
                    Console.Write(symbol[8]);
                    Console.ResetColor();
                    Console.Beep();
                    break;
                default:
                    Console.BackgroundColor = colors[9];
                    Console.Write(symbol[9]);
                    Console.ResetColor();
                    break;

            }
        }
        Console.ResetColor();
        Console.WriteLine();
    }
}
void GeneratePassword(int n_pass, int n_symbol, bool cifry, bool small_latter, bool up_latter, bool spec_cymbol, bool special)
{
    string validChars = "";

    if (cifry)
    {
        validChars = validChars.Insert(validChars.Length, "1234567890");
    }

    if (small_latter)
    {
        validChars = validChars.Insert(validChars.Length, "abcdefghijklmnopqrstuvwxyz");
    }

    if (up_latter)
    {
        validChars = validChars.Insert(validChars.Length, "ABCDEFGHIJKLMNOPQRSTUVWXYZ");
    }

    if (spec_cymbol)
    {
        validChars = validChars.Insert(validChars.Length, "!@#$%^&*()_+-=[]{}|;:,.<>?");
    }

    if (special)
    {
        Console.WriteLine("Введите доп. символы");
        string symbolDop = Console.ReadLine();
        validChars = validChars.Insert(validChars.Length, symbolDop);
    }
    Console.WriteLine("Сгенерированные пароли: ");
    for (int i = 0; i < n_pass; i++)
    {

        char[] password_char = new char[n_symbol];
        for (int j = 0; j < n_symbol; j++)
        {
            Random random = new Random();
            int index = random.Next(validChars.Length);
            password_char[j] = validChars[index];

        }
        string str = string.Join("", password_char);
        Console.WriteLine(str);
    }
}


Console.WriteLine("Что вы хотите нарисовать? 1 - прямоугольник; 2 - змейка; 5 - сапер");
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
    case 3:
        while (Check)
        {
            Console.WriteLine("Кол-во паролей?");
            int.TryParse(Console.ReadLine(), out int n_pass);
            Console.WriteLine("Кол-во символов?");
            int.TryParse(Console.ReadLine(), out int n_symbol);
            Console.WriteLine("Исп. цифры? [Yes/No]");
            string cifr = Console.ReadLine();
            bool cifry = (cifr.ToUpper() == "YES") ? true : false;
            Console.WriteLine("Исп. мал буквы? [Yes/No]");
            string buk = Console.ReadLine();
            bool bukvy = (buk.ToUpper() == "YES") ? true : false;
            Console.WriteLine("Исп. Бол буквы? [Yes/No]");
            string Buk = Console.ReadLine();
            bool Bukvy = (Buk.ToUpper() == "YES") ? true : false;
            Console.WriteLine("Исп. спец символы? [Yes/No]");
            string spec_sym = Console.ReadLine();
            bool spec_cymbol = (spec_sym.ToUpper() == "YES") ? true : false;
            Console.WriteLine("Использовать дополнительные символы? [Yes/No]");
            bool special = Console.ReadLine().ToUpper() == "Yes";

            GeneratePassword(n_pass, n_symbol, cifry, bukvy, Bukvy, spec_cymbol, special);

            Console.WriteLine("Хотите ещё порисовать? [Yes/No]");
            string text = Console.ReadLine();
            Check = (text.ToUpper() == "YES") ? true : false;
        }
        break;
    case 5:
        while (Check)
        {
            Console.WriteLine("Ширина Поля?");
            int.TryParse(Console.ReadLine(), out int width);
            width = width == 0 || width<0 ? 9 : width;
            Console.WriteLine("Высота Поля?");
            int.TryParse(Console.ReadLine(), out int height);
            height = height == 0 || height<0 ? 9 : height;
            Console.WriteLine("Введите кол-во мин: ");
            int.TryParse(Console.ReadLine(), out int count_mine);
            count_mine = count_mine == 0 ? 10 : count_mine;
            int str_cifr_nine = width/2 + width%2;
            int column_cifr_nine = height/2+ height%2;
            while (count_mine > width * height - str_cifr_nine* column_cifr_nine || count_mine <= 0)
            {
                Console.WriteLine("Введите кол-во мин: ");
                int.TryParse(Console.ReadLine(), out count_mine);
            }

            CreateMap(width, height, count_mine);

            Console.WriteLine("Хотите ещё порисовать? [Yes/No]");
            string text = Console.ReadLine();
            Check = (text.ToUpper() == "YES") ? true : false;
        }
        break;

    default:

        break;

}
