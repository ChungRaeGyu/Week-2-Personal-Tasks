using System;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;

namespace ConsoleApp1
{   
    class Program
    {
        static void Main(String[] args)
        {
            GameManger gameManager = new GameManger();
            gameManager.GameStart();
        }
    }

    internal class GameManger
    {
        Player player;
        Equipment[] eqs = new Equipment[7];
        Dongeon[] dg = new Dongeon[3];
        int eWeapon = -1;   //현재 장착한 장비관리
        int eDef = -1;      //현재 장착한 장비 관리
        public GameManger()
        {
            InitializeGame();
        }

        internal void GameStart()
        {
            Console.Clear();
            MainMenu();
        }

        private void MainMenu()
        {
            Console.Clear();
            Console.WriteLine("---------------------------------------------------------");
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.\r\n이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\r\n");
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");
            Console.WriteLine("4. 던전입장");
            Console.WriteLine("5. 휴식하기");

            int choice = ConsoleUtility.PromptMenuChoice(1, 5);
            switch (choice)
            {
                case 1:
                    StatusMenu();
                    break;
                case 2:
                    InventoryMenu();
                    break;
                case 3:
                    StoreMenu();
                    break;
                case 4:
                    DongeonMenu();
                    break;
                case 5:
                    RestMenu();
                    break;

            }
            MainMenu(); //재귀함수 while문이 싹 다 필요가 없어져버렸네 ㄷㄷ
        }

        private void RestMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("휴식하기");
            Console.Write("500");
            Console.ResetColor();
            Console.Write(" G 를 내면 체력을 회복할 수 있습니다. (보유 골드 : ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(player.GetGold());
            Console.ResetColor();
            Console.WriteLine(" G)\n");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("1. ");
            Console.ResetColor();
            Console.WriteLine("휴식하기");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("0. ");
            Console.ResetColor();
            Console.WriteLine("나가기\n");
            int choice = ConsoleUtility.PromptMenuChoice(0, 1);
            switch(choice) { 
                case 0:
                    MainMenu();
                    break;
                case 1:
                    HpRecovery();
                    RestMenu();
                    break;
            }
        }

        private void HpRecovery()
        {
            if (player.GetGold() > 500)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("휴식을 완료했습니다.");
                Console.ResetColor();
                player.SetGold(player.GetGold() - 500);
                player.SetHp(100);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Gold가 부족합니다.");
            }
        }

        private void DongeonMenu()
        {
            Console.Clear();
            Console.WriteLine("---------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("던전입장");
            Console.ResetColor();
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");

            for (int i = 0; i < dg.Length; i++)
            {
                dg[i].DongeonPrint();
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("0. ");
            Console.ResetColor();
            Console.WriteLine("나가기");

            int choice = ConsoleUtility.PromptMenuChoice(0, 3);
            switch (choice)
            {
                case 0:
                    MainMenu();
                    break;
                default:
                    InDongeon(choice);
                    DongeonMenu();
                    break;

            }
        }

        private void InDongeon(int choice)
        {
            int hp = player.GetHp();
            float gold = player.GetGold();
            Dongeon a = dg[choice - 1].DongeonPlay(player.GetDefence(), hp, player.GetAttack());
            player.SetHp(hp - a.reHp);
            player.SetGold(gold + a.reGold);
            if (dg[choice - 1].IsClear())
            {
                int aa = -1;
                dg[choice - 1].DongeonClearPrint(hp, player.GetHp(), gold, player.GetGold());
                player.PlayerUpdate();
                int choice2 = ConsoleUtility.PromptMenuChoice(0, 0);
                switch (choice)
                {
                    case 0:
                        DongeonMenu();
                        break;
                }
            }
            else
            {
                Console.WriteLine("---------------------------------------------------------");
                Console.WriteLine("실패~~~~~~");
                Console.WriteLine($"현재 체력 : {player.GetHp()}");
                Console.WriteLine("---------------------------------------------------------");
            }
        }

        private void StoreMenu()
        {
            Console.Clear();

            Console.WriteLine("---------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine("상점");
            Console.ResetColor();
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");

            Console.WriteLine("[보유 골드]");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{player.GetGold()}");
            Console.ResetColor();
            Console.WriteLine(" G\n\n");
            Console.WriteLine("[아이템 목록]");
            int eqCount1 = 0;
            foreach (Equipment eq in eqs)
            {
                eqCount1++;
                eq.PrintStore(eqCount1, false); //false값을 통해 목록의 숫자를 보이게 합니다.
                Console.WriteLine("\n");
            }
            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine("2. 아이템 판매");
            Console.WriteLine("0. 나가기\n");
            int choice = ConsoleUtility.PromptMenuChoice(0, 2);
            switch (choice)
            {
                case 0:
                    MainMenu();
                    break;
                case 1:
                    ItemBuy();
                    break;
                case 2:
                    ItemSale();
                    break;
            }
        }

        private void ItemSale()
        {
            Console.Clear();

            Console.WriteLine("---------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine("상점 - 아이템 판매");
            Console.ResetColor();
            Console.WriteLine("아이템을 판매 할 수 있습니다..\n");

            Console.WriteLine("[보유 골드]");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{player.GetGold()}");
            Console.ResetColor();
            Console.WriteLine(" G\n\n");
            Console.WriteLine("[아이템 목록]");
            int eqCount3 = 0;
            int checkNum = 0;
            int[] ints = new int[eqs.Length];
            foreach (Equipment eq in eqs)
            {
                if (eq.IsBuyCheck())
                {
                    eqCount3++;
                    eq.PrintSale(eqCount3, true);
                    Console.WriteLine();
                    ints[eqCount3 - 1] = checkNum;

                }
                checkNum++;
            }
            Console.WriteLine("0. 나가기\n");

            int choice = ConsoleUtility.PromptMenuChoice(0, eqCount3);
            switch (choice)
            {
                case 0:
                    StoreMenu();
                    break;
                default:
                    CanSale(ints,choice);
                    ItemSale();
                    break;
            }
        }

        private void CanSale(int[] ints, int choice)
        {
            eqs[ints[choice - 1]].IsBuyController();
            int sale = eqs[ints[choice - 1]].Getprice();
            player.SetGold(player.GetGold() + (sale * 85 / 100));
            if (eqs[ints[choice - 1]].GetWear())
            {
                eqs[ints[choice - 1]].IsWearController();
                player.Release(eqs[ints[choice - 1]].GetKind(), eqs[ints[choice - 1]].GetAbility());
                if (eqs[ints[choice - 1]].GetKind() == "무기")
                {
                    eWeapon = -1;
                }
                else
                {
                    eDef = -1;
                }
            }


        }

        private void ItemBuy()
        {
            Console.Clear();

            Console.WriteLine("---------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine("상점 - 아이템 구매");
            Console.ResetColor();
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");

            Console.WriteLine("[보유 골드]");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{player.GetGold()}");
            Console.ResetColor();
            Console.WriteLine(" G\n\n");
            Console.WriteLine("[아이템 목록]");
            int eqCount2 = 0;
            foreach (Equipment eq in eqs)
            {
                eqCount2++;
                eq.PrintStore(eqCount2, true); //false값을 통해 목록의 숫자를 보이게 합니다.
                Console.WriteLine("\n");
            }
            Console.WriteLine("0. 나가기\n");

            int choice = ConsoleUtility.PromptMenuChoice(0, 2);
            switch (choice)
            {
                case 0:
                    StoreMenu();
                    break;
                default:
                    CanBuy(choice);
                    ItemBuy();
                    break;
            }
        }

        private void CanBuy(int choice)
        {
            if (eqs[choice - 1].GetIsBuy())
            {
                Console.WriteLine("이미 구매한 아이템입니다.");
            }
            else if (eqs[choice - 1].Getprice() > player.GetGold())
            {
                Console.WriteLine("Gold가 부족합니다.");
            }
            else
            {
                eqs[choice - 1].IsBuyController();
                player.SetGold(player.GetGold() - eqs[choice - 1].Getprice());
            }
        }

        private void InventoryMenu()
        {
            Console.Clear();
            Console.WriteLine("---------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("인벤토리"); //글자 색 변경하기
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
            Console.WriteLine("[아이템 목록]\n");
            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("0. 나가기\n");

            int choice = ConsoleUtility.PromptMenuChoice(0, 1);
            switch (choice)
            {
                case 0:
                    MainMenu();
                    break;
                case 1:
                    EquipMenu();
                    break;
            }
        }

        private void EquipMenu()
        {
            Console.Clear();
            Console.WriteLine("---------------------------------------------------------");
            Console.WriteLine("");
            Console.WriteLine("인벤토리 - 장착관리"); //글자 색 변경하기
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
            Console.WriteLine("[아이템 목록]");
            int eqCount = 0;
            int checkNum = 0;
            int[] ints = new int[eqs.Length];
            foreach (Equipment eq in eqs)
            {
                if (eq.IsBuyCheck())
                {
                    eqCount++;
                    eq.PrintStorage(eqCount, true);
                    Console.WriteLine();
                    ints[eqCount - 1] = checkNum;
                    //여기서 장착하면 checkNum을 어디 저장을 하나 해놓고
                }
                checkNum++;
            }
            Console.WriteLine("0. 나가기\n");
            int choice = ConsoleUtility.PromptMenuChoice(0, eqCount);
            switch (choice)
            {
                case 0: 
                    InventoryMenu();
                    break;
                default:
                    EquipWear(ints,choice);
                    EquipMenu();
                    break;
            }
        }

        private void EquipWear(int[] ints, int choice)
        {
            if (!eqs[ints[choice - 1]].GetWear())//장착
            {
                //이전에 장착한 아이템이 있는지 확인
                if (eqs[ints[choice - 1]].GetKind() == "무기")
                {
                    if (eWeapon > -1)//장착한 무기가 있을 때
                    {
                        player.Release(eqs[eWeapon].GetKind(), eqs[eWeapon].GetAbility());
                        eqs[eWeapon].IsWearController();
                    }
                    eWeapon = ints[choice - 1];
                }
                else
                {
                    if (eDef > -1)
                    {
                        player.Release(eqs[eDef].GetKind(), eqs[eDef].GetAbility());
                        eqs[eDef].IsWearController();
                    }
                    eDef = ints[choice - 1];
                }
                //장착한 아이템 설정
                player.Wear(eqs[ints[choice - 1]].GetKind(), eqs[ints[choice - 1]].GetAbility());
            }
            else
            {
                //방어력이 빠진다.
                player.Release(eqs[ints[choice - 1]].GetKind(), eqs[ints[choice - 1]].GetAbility());
                if (eqs[ints[choice - 1]].GetKind() == "무기")
                {
                    eWeapon = -1;
                }
                else
                {
                    eDef = -1;
                }
            }
        }

        private void StatusMenu()
        {
            Console.Clear();

            player.print();
            int choice = ConsoleUtility.PromptMenuChoice(0, 0);
            switch (choice)
            {
                case 0:
                    MainMenu();
                    break;
            }
        }

        private void InitializeGame()
        {
            player = new Player(1, "전사", 10, 5, 100, 1500);

            //장비            
            eqs[0] = new Equipment("수련자 갑옷", 5, "방어구", false, "수련에 도움을 주는 갑옷입니다.", 1000, false);
            eqs[1] = new Equipment("무쇠갑옷", 9, "방어구", false, "무쇠로 만들어져 튼튼한 갑옷입니다.", 2000, true);
            eqs[2] = new Equipment("스파르타의 갑옷", 15, "방어구", false, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500, false);
            eqs[3] = new Equipment("낡은 검", 2, "무기", false, "쉽게 볼 수 있는 낡은 검 입니다.", 600, false);
            eqs[4] = new Equipment("청동 도끼", 5, "무기", false, "어디선가 사용됐던거 같은 도끼입니다.", 1500, false);
            eqs[5] = new Equipment("스파르타의 창", 7, "무기", false, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 3500, true);
            eqs[6] = new Equipment("황성빈의 방망이", 15, "무기", false, "롯데자이언츠의 황성빈의 방망이다.", 3500, false);
            //던전
            
            dg[0] = new Dongeon(1, "쉬운 던전", 5, 1000);
            dg[1] = new Dongeon(2, "일반 던전", 11, 1700);
            dg[2] = new Dongeon(3, "어려운 던전", 17, 2500);
        }
    }
}

