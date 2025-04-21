using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DungeonSystem : Choice
{
    //public int dungeonDay = 3; 일수
    //public int dungeonHour = 0; 시간
    public void dungeonTime()
    {
        if (dungeonHour >= 24)
        {
            dungeonDay--;
            dungeonHour -= 24;
        }
    }
    public bool quest(ref bool questError, ref bool dungeonEnd)
    {
        dungeonTime();
        if (dungeonDay == 0) // 던전 퇴출
        {
            dungeonDay = 3;
            dungeonHour = 0;
            floor = 1;
            playerX = -1; playerY = -1;
            portalX = -2; portalY = -2;
            dungeonEnd = true;
            return false;
        }
        if (Hp <= 0) // 사망
        {
            dungeonEnd = true;
            return false;
        }
        if (portalY == playerY && portalX == playerX) // 포탈 도달시
        {
            playerX = -1; playerY = -1;
            portalX = -2; portalY = -2;
            floor++;
            dungeonDay *= 2;

            Money += 200 * floor * floor;
        }
        Mainmap();  // 맵 초기화

        PrintMap(); // 맵 출력
        Console.WriteLine($"\n\n{floor}층 던전 안 입니다.");
        Console.WriteLine($"던전 남은 일수   {dungeonDay}");
        Console.WriteLine($"던전 지난 시간   {dungeonHour}");
        Console.WriteLine("\n행동을 정해 주세요.");
        Console.WriteLine("1. 상태 보기");
        Console.WriteLine("2. 인벤토리");
        Console.WriteLine("3. 이동하기");
        Console.WriteLine("4. 휴식하기");

        if (questError)
        {
            Console.WriteLine("잘못된 입력입니다.\n");
            questError = false;
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

            case "3": // 이동하기
                if (dice6() >= 4)
                {
                    dungeonHour += 2;
                    battle();
                }
                else
                {
                    dungeonHour += 1;
                    bool moveError = false;
                    bool moveError2 = false;
                    bool moveRepeat = move(ref moveError, ref moveError2);
                    while (!moveRepeat)
                    {
                        moveRepeat = move(ref moveError, ref moveError2);
                    }
                    Console.Clear();
                }
                break;

            case "4": // 휴식하기
                if (dice20() >= 20)
                {
                    Console.Clear();
                    Console.WriteLine("편하게 쉬었습니다.");
                    dungeonHour += 12;
                    Hp += HpMax;
                    Mp += MpMax;
                    UpdateStats();
                }
                else if (dice20() >= 10)
                {
                    Console.Clear();
                    Console.WriteLine("잠들었습니다.");
                    dungeonDay -= 1;
                    Hp += HpMax;
                    Mp += MpMax;
                    UpdateStats();
                }
                else
                {
                    dungeonHour += 2;
                    battle();
                }
                break;

            default:
                Console.Clear();
                questError = true;
                break;
        }
        return true; // 정상적으로 행동이 종료된 경우
    }
    //====================전투 시스탬====================
    //전투
    public void battle()
    {
        Console.Clear();
        Console.WriteLine("적을 만났습니다! 전투를 시작합니다.\n\n\n");
        int enemyHP = random.Next(1 + (floor * floor), (floor * 5) + (floor * floor)); // 적 체력
        int enemyAttack = random.Next(1 + floor, 3 + floor * 2); // 적 공격력
        bool defense = false; //방어여부
        bool battleError = false;

        while (Hp > 0 && enemyHP > 0)
        {
            Console.WriteLine($"\n\n적의 체력:   {enemyHP}");
            Console.WriteLine($"기본 공격력: {enemyAttack}");
            Console.WriteLine($"\n\n현재 체력:   {Hp} / {HpMax}");
            Console.WriteLine($"현재 체력:   {Mp} / {MpMax}");
            Console.WriteLine("\n당신의 차례입니다. 행동을 선택하세요:");
            Console.WriteLine("1. 공격하기");
            Console.WriteLine("2. 방어하기");
            Console.WriteLine("3. 마법 사용하기 (MP 2 소모)");
            if (battleError)
            {
                Console.WriteLine("잘못된 입력. 아무 행동도 하지 못했습니다.");
            }
            string action = Console.ReadLine();
            defense = false;

            switch (action)
            {
                case "1": //공격
                    Console.Clear();
                    int damage = 0;
                    if (dice20() >= 20)
                    {
                        damage = Atk * 3;
                    }
                    else if (dice20() >= 10)
                    {
                        damage = Atk * 2;
                    }
                    else
                    {
                        damage = Atk;
                    }
                    Console.WriteLine($"공격! {damage}의 피해");
                    enemyHP -= damage;
                    break;


                case "2": //방어
                    Console.Clear();
                    Console.WriteLine("방어 태세! 다음 턴 적의 피해가 체력 비례로 감소합니다.");
                    defense = true;
                    break;


                case "3": //마법
                    Console.Clear();
                    int magicDamage = 0;
                    if (dice20() >= 20)
                    {
                        magicDamage = Int * 4;
                    }
                    else if (dice20() >= 10)
                    {
                        magicDamage = Int * 3;
                    }
                    else
                    {
                        magicDamage = Int * 2;
                    }
                    if (Mp >= 2)
                    {
                        Console.WriteLine($"마법 공격! {magicDamage}의 피해");
                        enemyHP -= magicDamage;
                        Mp -= 2;
                    }
                    else
                    {
                        Console.WriteLine("마나가 부족합니다.");
                    }
                    break;

                default:
                    Console.Clear();
                    break;
            }

            if (enemyHP <= 0)
            {
                Console.WriteLine("적을 쓰러뜨렸습니다!");
                experience += dice20() * floor;
                Money += dice20() * (floor * floor);

                UpdateStats();
                break;
            }


            Console.WriteLine("\n적의 차례입니다.");   // 적의 턴
            int enemyDice = random.Next(floor, (floor * 2)); //적 주사위
            int enemyDamage = enemyAttack + enemyDice; //적 대미지

            int evasion = Math.Min(Dex * 2, 101); //회피율
            int evasionRoll = random.Next(1, 101);

            if (evasionRoll <= evasion)//회피 했다면
            {
                Console.WriteLine($"회피 성공! (회피 확률: {evasion}%)");
            }
            else//실패 했다면
            {
                if (defense)//방어를 했다면
                {
                    enemyDamage = (int)(enemyDamage * 0.5 * (1.0 - Def * 0.05)); // 기본 방어율 50% + 방어력 비례
                    if (enemyDamage < 0) enemyDamage = 0; // 피해 음수 방지
                    Console.WriteLine($"방어 중! 체력 기반 피해 감소 적용 → 실제 피해: {enemyDamage}");
                }
                //안했다면
                Console.WriteLine($"적의 공격! {enemyDamage}의 피해를 입었습니다.");
                Hp -= enemyDamage;
            }

            if (Hp <= 0)// 사망
            {
                Console.WriteLine("당신은 쓰러졌습니다...");
                break;
            }
        }
    }
}
