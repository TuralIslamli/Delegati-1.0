using System;

namespace Delegati_1._0
{

    class Ping
    {
        private string  user;
        public int Rand { get; set; }

        public string User
        {
            get { return user; }
            set { user = value; }
        }
        public delegate void UserShoot(string info);
        public event UserShoot Note;
        public Ping(string user) 
        {
            User = user;
        }
        public Ping(string user, int rand)
        {
            User = user;
            Rand = rand;
        }

        public void ShootPing(int randZone)
        {
            Note?.Invoke($"Ping received Pong to zone {randZone}.");
        }
        public int DefendPing(string defender)
        {
            Console.WriteLine($"{defender} укажите зону, которую будете защищать. От 1 до 5.");
            int defend = Convert.ToInt32( Console.ReadLine());
            return defend;
        }

    }
    class Pong
    {
        private string user;
        public int Rand { get; set; }
        public string User
        {
            get { return user; }
            set { user = value; }
        }
        public delegate void UserShoot(string info);
        public event UserShoot Note;

        public Pong(string user) 
        {
            User = user;
        }
        public Pong(string user, int rand)
        {
            User = user;
            Rand = rand;
        }
        public void ShootPong(int randZone)
        {
            Note?.Invoke($"Pong received Ping to zone:{randZone}. ");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            bool start = false;
            Console.WriteLine("Когда будете готовы, наберите 1.");
            while (start == false)
            {
                string userChoise = Console.ReadLine();
                bool temp = userChoise == "1" ? (start = true) : (start = false);
            }
            
            while (start==true)
            {
                Console.Write("Укажите имя игрока, кто подаст первым:");
                string user1 = AskName();
                Random rand = new Random();
                int user1Count = 0;
                


                Console.Write("Укажие имя второго игрока:");
                string user2 = AskName();
                int user2Count = 0;
                bool game = true;
                bool set = true;
                while (game)
                {
                    while (set)
                    {
                        int user1Shoot = rand.Next(1, 5);
                        Ping ping = new Ping(user1, user1Shoot);
                        ping.Note += Print;
                        ping.ShootPing(user1Shoot);
                        Pong pong = new Pong(user2);
                        int defUser1 = 0;
                        int defUser2 = DefendZone(user2);
                        int user2Shoot = rand.Next(1, 5);

                        if (defUser2 != user1Shoot)
                        {
                            set = false;
                            user1Count++;
                            Console.WriteLine($"{user1} выиграл сет.");

                        }
                        else
                        {
                            pong = new Pong(user2, user2Shoot);
                            pong.Note += Print;
                            pong.ShootPong(user2Shoot);
                            defUser1 = DefendZone(user1);
                        }
                        if (defUser1 != user2Shoot && defUser2 == user1Shoot)
                        {
                            set = false;
                            user2Count++;
                            Console.WriteLine($"{user2} выиграл сет.");
                        }

                    }
                    Console.WriteLine($"scet {user1}:{user1Count}-{user2Count}:{user2}. Если хотите продолжить игру нажмите 1.");
                    string userChoise = Console.ReadLine();
                    game = userChoise == "1" ? (game = true) : (game = false);
                    set = true;
                    Console.WriteLine($"Игра окончилась со счетом {user1}:{user1Count}-{user2Count}:{user2}.");
                    Console.WriteLine("Если хотите начать сначала, наберите 1.");
                    string replay = Console.ReadLine();
                    start = replay == "1" ? (start = true) : (start = false);
                }
                Console.WriteLine("До свидания.");
                Console.ReadKey();


                
                
            }
        }

        static int DefendZone(string defender)
        {
            Console.WriteLine($"{defender} укажите зону, которую будете защищать. От 1 до 5.");
            int num = 0;
            for (; ; )
            {
                string read1 = Console.ReadLine();

                int.TryParse(read1, out num);
                if (num < 1 && num > 5)
                {
                    Console.WriteLine("Не правильно, укажите правильную информацию.");
                }
                else
                {
                    break;
                }
            }
            return num;

        }
        static void Print(string info)
        {
            Console.WriteLine(info);
        }
        static string AskName()
        {
            bool checkerName = true;
            string personName = "";
            do
            {
                personName = Console.ReadLine();

                for (int i = 0; i < personName.Length; i++)
                {
                    char element = personName[i];

                    if (!Char.IsLetter(element))
                    {
                        checkerName = false;
                        Console.WriteLine("Не корректная информация, укажите правильно: ");
                        break;
                    }
                    else
                    {
                        checkerName = true;
                    }
                }

                if (personName.Length < 1)
                {
                    Console.WriteLine("Не корректная информация, укажите правильно:");
                    checkerName = false;
                }
            }
            while (checkerName == false);

            return personName;
        }
    }
}
