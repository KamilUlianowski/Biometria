using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Biometria_Projekt.KeystrokeClasses;

namespace Biometria_Projekt.Classes
{
    class Keystroke
    {

        public static Vector1 CreateVector1(List<ClickStatistics> clicks, int userId)
        {
            var keys = CreateListOfKeys();
            for (int i = 1; i < clicks.Count; i++)
            {
                if (clicks[i].Status == KeyStatus.KeyUp && clicks[i].Asci == clicks[i - 1].Asci &&
                    keys.Exists(x => x.Asci.Contains(clicks[i].Asci)))
                {
                    var item = keys.Single(x => x.Asci.Contains(clicks[i].Asci));
                    keys[keys.IndexOf(item)].TotalTime += (clicks[i].Time - clicks[i - 1].Time);
                    keys[keys.IndexOf(item)].CountOfClick++;
                }
                else if (i > 2 && clicks[i].Status == KeyStatus.KeyUp && clicks[i].Asci == clicks[i - 2].Asci &&
                   keys.Exists(x => x.Asci.Contains(clicks[i].Asci)))
                {
                    var item = keys.Single(x => x.Asci.Contains(clicks[i].Asci));
                    keys[keys.IndexOf(item)].TotalTime += (clicks[i].Time - clicks[i - 2].Time);
                    keys[keys.IndexOf(item)].CountOfClick++;
                }
            }
            return new Vector1(userId, keys);
        }

        public static Vector2 CreateVector2(List<ClickStatistics> clicks, int userId)
        {
            int countKeyDown = 0;
            int countBreakTime = 0;
            int countKeyUpSpaceDown = 0;
            int countSpaceUpKeyDown = 0;
            int averageKeyDownTime = 0;
            int averageBreakTime = 0;
            int averageKeyUpSpaceDownTime = 0;
            int averageSpaceUpKeyDownTime = 0;

            for (int i = 1; i < clicks.Count; i++)
            {
                if (clicks[i].Status == KeyStatus.KeyUp && clicks[i].Asci == clicks[i - 1].Asci
                    && clicks[i - 1].Status == KeyStatus.KeyDown)
                {
                    averageKeyDownTime += (clicks[i].Time - clicks[i - 1].Time);
                    countKeyDown++;
                }
                else if (i > 2 && clicks[i].Status == KeyStatus.KeyUp && clicks[i].Asci == clicks[i - 2].Asci
                    && clicks[i - 2].Status == KeyStatus.KeyDown)
                {
                    averageKeyDownTime += (clicks[i].Time - clicks[i - 2].Time);
                    countKeyDown++;
                }
                if (i > 2 && clicks[i].Status == KeyStatus.KeyDown && clicks[i - 2].Status == KeyStatus.KeyDown)
                {
                    averageBreakTime += (clicks[i].Time - clicks[i - 2].Time);
                    countBreakTime++;
                }
                else if (clicks[i].Status == KeyStatus.KeyDown && clicks[i - 1].Status == KeyStatus.KeyDown)
                {
                    averageBreakTime += (clicks[i].Time - clicks[i - 1].Time);
                    countBreakTime++;
                }
                if (i < clicks.Count - 1 && clicks[i].Status == KeyStatus.KeyUp && clicks[i + 1].Asci == 32
                    && clicks[i + 1].Status == KeyStatus.KeyDown)
                {
                    averageKeyUpSpaceDownTime += clicks[i + 1].Time - clicks[i].Time;
                    countKeyUpSpaceDown++;
                }
                if (i < clicks.Count - 1 && clicks[i].Asci == 32 && clicks[i].Status == KeyStatus.KeyUp
                    && clicks[i + 1].Status == KeyStatus.KeyDown)
                {
                    averageSpaceUpKeyDownTime = clicks[i + 1].Time - clicks[i].Time;
                    countSpaceUpKeyDown++;
                }
            }
            if (countKeyDown != 0)
            {
                averageKeyDownTime /= countKeyDown;
            }
            if (countBreakTime != 0)
            {
                averageBreakTime /= countBreakTime;
            }
            if (countKeyUpSpaceDown != 0)
            {
                averageKeyUpSpaceDownTime /= countKeyUpSpaceDown;
            }
            if (countSpaceUpKeyDown != 0)
            {
                averageSpaceUpKeyDownTime /= countSpaceUpKeyDown;
            }
            return new Vector2(userId,
                averageKeyDownTime, averageBreakTime, averageKeyUpSpaceDownTime, averageSpaceUpKeyDownTime);
        }

        public static List<ClickStatistics> ChangeStringToClicksStatistics(string fixedText)
        {
            var clicks = new List<ClickStatistics>();
            for (int i = 0; i < fixedText.Length; i++)
            {
                if (fixedText[i] == 'd')
                {
                    i += 2;
                    string asci = string.Empty;
                    string count = string.Empty;
                    string time = string.Empty;
                    while (fixedText[i] != '_')
                    {
                        asci += fixedText[i];
                        ++i;
                    }
                    ++i;
                    while (fixedText[i] != '_')
                    {
                        time += fixedText[i];
                        ++i;
                    }
                    ++i;
                    while (fixedText[i] != ' ')
                    {
                        count += fixedText[i];
                        ++i;
                    }
                    clicks.Add(new ClickStatistics(Int32.Parse(asci), Int32.Parse(time), KeyStatus.KeyDown, Int32.Parse(count)));
                }
                if (fixedText[i] == 'u')
                {
                    string time = string.Empty;
                    string asci = string.Empty;
                    i += 2;

                    while (fixedText[i] != '_')
                    {
                        asci += fixedText[i];
                        ++i;
                    }
                    ++i;
                    while (fixedText[i] != ' ')
                    {
                        time += fixedText[i];
                        ++i;
                    }

                    clicks.Add(new ClickStatistics(Int32.Parse(asci), Int32.Parse(time), KeyStatus.KeyUp));
                }
            }

            return clicks;
        }

        public static List<KeyStatistics> CreateListOfKeys()
        {
            var keys = new List<KeyStatistics>();
            for (int i = 65; i <= 90; i++)
            {
                keys.Add(new KeyStatistics()
                {
                    Asci = new List<int>() { i, i + 32 },
                    CountOfClick = 0

                });
            }
            return keys;
        }

        public static double CountDistancesVector1(Vector1 vector, Vector1 myVector)
        {
            double distance = 0;
            for (int i = 0; i < vector.Keys.Count; i++)
            {
                distance += Math.Abs(myVector.Keys[i].AverageTime - vector.Keys[i].AverageTime);
            }
            return distance;
        }

        public static double CountDistancesVector2(Vector2 vector, Vector2 myVector)
        {
            double distance = Math.Abs(myVector.AverageBreakTime - vector.AverageBreakTime) +
                Math.Abs(myVector.AverageKeyDownTime - vector.AverageKeyDownTime) +
                Math.Abs(myVector.AverageKeyUpSpaceDownTime - vector.AverageKeyUpSpaceDownTime) + 
                Math.Abs(myVector.AverageSpaceUpKeyDownTime - vector.AverageSpaceUpKeyDownTime);
            return distance;
        }

        #region Dll
        public enum MapType : uint
        {
            MAPVK_VK_TO_VSC = 0x0,
            MAPVK_VSC_TO_VK = 0x1,
            MAPVK_VK_TO_CHAR = 0x2,
            MAPVK_VSC_TO_VK_EX = 0x3,
        }

        [DllImport("user32.dll")]
        public static extern int ToUnicode(
            uint wVirtKey,
            uint wScanCode,
            byte[] lpKeyState,
            [Out, MarshalAs(UnmanagedType.LPWStr, SizeParamIndex = 4)]
            StringBuilder pwszBuff,
            int cchBuff,
            uint wFlags);

        [DllImport("user32.dll")]
        public static extern bool GetKeyboardState(byte[] lpKeyState);

        [DllImport("user32.dll")]
        public static extern uint MapVirtualKey(uint uCode, MapType uMapType);

        public static char GetCharFromKey(Key key)
        {
            char ch = ' ';

            int virtualKey = KeyInterop.VirtualKeyFromKey(key);
            byte[] keyboardState = new byte[256];
            GetKeyboardState(keyboardState);

            uint scanCode = MapVirtualKey((uint)virtualKey, MapType.MAPVK_VK_TO_VSC);
            StringBuilder stringBuilder = new StringBuilder(2);

            int result = ToUnicode((uint)virtualKey, scanCode, keyboardState, stringBuilder, stringBuilder.Capacity, 0);
            switch (result)
            {
                case -1:
                    break;
                case 0:
                    break;
                case 1:
                    {
                        ch = stringBuilder[0];
                        break;
                    }
                default:
                    {
                        ch = stringBuilder[0];
                        break;
                    }
            }
            return ch;
        }
        #endregion
    }
}
