using System;
using System.Drawing;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text.Json;
using System.Xml.Linq;
using static Choice;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class GameSystem : DungeonSystem
{
    public static void Main(string[] args)
    {
        // 게임 시스템 객체 생성
        GameSystem gameSystem = new GameSystem();

        // 게임 시작
        gameSystem.Game();
    }
    //====================시작화면====================
    public void Game()
    {
        bool start = false;
        bool play = false;
        bool startError = false;

        while (!start)
        {
            Console.WriteLine("Dungeon and Stone \n\n1. 시작하기\n2. 불러오기\n3. 설명듣기"); // 시작화면
            if (startError)
            {
                Console.WriteLine("잘못된 입력입니다.\n");
                startError = false;
            }
            string text = Console.ReadLine();

            switch (text)
            {
                case "1": // 게임 시작
                    SaveSystem.ResetSave();
                    Console.Clear();
                    bool nameError = false;
                    bool jobError = false;

                    bool nameRepeat = nameChoice(nameError);
                    while (!nameRepeat)
                    {
                        if (!nameError)
                        {
                            nameError = true;
                        }
                        nameRepeat = nameChoice(nameError);
                    }
                    Console.Clear();

                    bool jobRepeat = jobChoice(jobError);
                    while (!jobRepeat)
                    {
                        if (!jobError)
                        {
                            jobError = true;
                        }
                        jobRepeat = jobChoice(jobError);
                    }
                    Console.Clear();
                    start = true;
                    play = true;
                    break;

                case "2": // 불러오기
                    Character loaded = SaveSystem.LoadGame();
                    if (loaded != null)
                    {
                        CopyCharacterData(loaded, this); // 현재 캐릭터에 데이터 복사
                        Console.WriteLine("세이브 데이터를 불러왔습니다.\n");
                        UpdateStats(); // 불러온 스탯 적용
                        play = true;   // 게임 루프 진입
                        Console.Clear();
                    }
                    else
                    {
                        Console.WriteLine("저장된 데이터가 없습니다.\n");
                    }
                    break;

                case "3":
                    Console.Clear();
                    Console.WriteLine("당신은 미궁 도시에 도달했습니다. 살아남기를 기도합니다.");
                    Console.WriteLine("\n주사위");
                    Console.WriteLine("행동에 영향을 미칩니다.");
                    Console.WriteLine("1~9 실패 / 10~19 성공 / 20이상 대성공");
                    Console.WriteLine("\n캐릭터 스탯");
                    Console.WriteLine("Str: 물리 공격에 영향을 미칩니다.");
                    Console.WriteLine("Dex: 회피에 영향을 미칩니다.");
                    Console.WriteLine("Int: 마법 공격에 영향을 미칩니다.");
                    Console.WriteLine("Con: 최대 체력에 영향을 미칩니다.");
                    Console.WriteLine("Wis: 최대 마나에 영향을 미칩니다.");
                    Console.WriteLine("Luk: 주사위 값에 영향을 미칩니다.");
                    Console.WriteLine("\n마을");
                    Console.WriteLine("세금이 존재합니다. 세금은 7일간 면제됩니다.");
                    Console.WriteLine("세금은 점점 늘어나며, 세금이 부족하면 처형당합니다.");
                    Console.WriteLine("\n던전");
                    Console.WriteLine("던전에서는 시간이 따로 존재합니다.");
                    Console.WriteLine("처음 3일간 탐험을 합니다.");
                    Console.WriteLine("다음 층으로 넘어가면 남은 일수가 2배로 늘어납니다.");
                    Console.WriteLine("적과 조우할 수 있습니다.");
                    break;

                default:
                    startError = true;
                    Console.Clear();
                    break;
            }
        }

        //==================== 게임화면 ====================
        bool playError = false;
        bool duty = false;
        int Day = 1;
        while (play)
        {
            Console.WriteLine($"{Day}일");
            if (Day <= 7)
            {
                Console.WriteLine("\n7일간 세금면제입니다.\n");
            }
            else if (duty)
            {
                Console.WriteLine("\n세금을 징수합니다.");
                Console.WriteLine($"세금{Day * 143}.\n");
                if(Money < Day * 143)
                {
                    Console.WriteLine("\n세금이 부족하여 처형됩니다..");
                    Console.WriteLine($"{Day}일 동안 생존하였습니다.");
                    SaveSystem.ResetSave();
                    play = false;
                }
                else
                {
                    Money -= Day * 143;
                    duty = false;
                }
            }
            SaveSystem.SaveGame(this);
            Console.WriteLine("\n도시입니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");
            Console.WriteLine("4. 던전입장");
            Console.WriteLine("5. 휴식하기");
            if (playError)
            {
                Console.WriteLine("잘못된 입력입니다.\n");
                playError = false;
            }

            string text = Console.ReadLine();

            switch (text)
            {
                case "1": // 상태 보기
                    bool windowsError = false;
                    bool windowsRepeat = systemWindows(windowsError);
                    while (!windowsRepeat)
                    {
                        if (!windowsError)
                        {
                            windowsError = true;
                        }
                        windowsRepeat = systemWindows(windowsError);
                    }
                    break;

                case "2": // 인벤토리
                    bool invenError = false;
                    bool invenEquipError = false;
                    bool invenNext = false;

                    bool inventoryRepeat = inventory(invenError, out invenNext);
                    while (!inventoryRepeat)
                    {
                        if (!invenError)
                        {
                            invenError = true;
                        }
                        inventoryRepeat = inventory(invenError, out invenNext);
                    }

                    if (invenNext)
                    {
                        bool inventoryEquipRepeat = inventoryEquip(invenEquipError);
                        while (!inventoryEquipRepeat)
                        {
                            if (!invenEquipError)
                            {
                                invenEquipError = true;
                            }
                            inventoryEquipRepeat = inventoryEquip(invenEquipError);
                        }
                    }
                    Console.Clear();
                    break;

                case "3": // 상점
                    bool StoreError = false;
                    bool MoneyLack = false;
                    bool StoreRepeat = Store(ref StoreError, ref MoneyLack);
                    while (!StoreRepeat)
                    {
                        StoreRepeat = Store(ref StoreError, ref MoneyLack);
                    }
                    Console.Clear();
                    break;

                case "4": // 던전입장
                    Console.Clear();
                    duty = true;
                    
                    bool dungeonEnd = false;
                    bool questError = false;

                    while (!dungeonEnd)
                    {
                        quest(ref questError, ref dungeonEnd);
                    }
                    Console.Clear();
                    Day += 1;
                    break;

                case "5": // 휴식하기
                    bool hotelError = false;
                    bool MoneyLackHotel = false;
                    bool hotelRepeat = hotel(ref hotelError, ref MoneyLackHotel);

                    while (!hotelRepeat)
                    {
                        hotelRepeat = hotel(ref hotelError, ref MoneyLackHotel);
                    }
                    break;

                default:
                    Console.Clear();
                    playError = true;
                    break;
            }

            if (Hp <= 0) // 사망
            {
                Console.WriteLine("\n사망하였습니다.");
                Console.WriteLine($"{Day}일 동안 생존하였습니다.");
                SaveSystem.ResetSave();
                play = false;
            }
        }
    }
}


