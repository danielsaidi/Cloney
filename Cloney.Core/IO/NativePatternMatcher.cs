using System;
using Cloney.Core.Extensions;

namespace Cloney.Core.IO
{
    public static class NativePatternMatcher
    {
        public static bool StrictMatchPattern(string path, string pattern)
        {
            if (pattern.IsNullOrEmpty() || path.IsNullOrEmpty())
                return false;

            pattern = pattern.ToLowerInvariant();
            path = path.ToLowerInvariant();

            if (pattern.Equals("*") || pattern.Equals("*.*"))
                return true;

            if ((pattern[0] == '*') && (pattern.IndexOf('*', 1) == -1))
            {
                var length = pattern.Length - 1;
                if ((path.Length >= length) && (String.Compare(pattern, 1, path, path.Length - length, length, StringComparison.OrdinalIgnoreCase) == 0))
                    return true;
            }

            var num = 0;
            var num7 = 1;
            var num8 = pattern.Length * 2;
            int num9;

            var ch = '\0';
            var sourceArray = new int[16];
            var numArray2 = new int[16];
            var flag = false;

            sourceArray[0] = 0;

            while (!flag)
            {
                if (num < path.Length)
                {
                    ch = path[num];
                    num++;
                }
                else
                {
                    flag = true;
                    if (sourceArray[num7 - 1] == num8)
                    {
                        break;
                    }
                }

                var index = 0;
                var num5 = 0;
                var num6 = 0;

                while (index < num7)
                {
                    var num2 = (sourceArray[index++] + 1) / 2;
                    var num3 = 0;

                    Label_00F2:
                    if (num2 != pattern.Length)
                    {
                        num2 += num3;
                        num9 = num2 * 2;
                        if (num2 == pattern.Length)
                        {
                            numArray2[num5++] = num8;
                        }
                        else
                        {
                            var ch2 = pattern[num2];
                            num3 = 1;
                            if (num5 >= 14)
                            {
                                var num11 = numArray2.Length * 2;
                                var destinationArray = new int[num11];
                                Array.Copy(numArray2, destinationArray, numArray2.Length);
                                numArray2 = destinationArray;
                                destinationArray = new int[num11];
                                Array.Copy(sourceArray, destinationArray, sourceArray.Length);
                                sourceArray = destinationArray;
                            }
                            if (ch2 == '*')
                            {
                                numArray2[num5++] = num9;
                                numArray2[num5++] = num9 + 1;
                                goto Label_00F2;
                            }
                            if (ch2 == '>')
                            {
                                var flag2 = false;
                                if (!flag && (ch == '.'))
                                {
                                    var num13 = path.Length;
                                    for (var i = num; i < num13; i++)
                                    {
                                        var ch3 = path[i];
                                        num3 = 1;
                                        if (ch3 != '.')
                                            continue;

                                        flag2 = true;
                                        break;
                                    }
                                }
                                if ((flag || (ch != '.')) || flag2)
                                {
                                    numArray2[num5++] = num9;
                                    numArray2[num5++] = num9 + 1;
                                }
                                else
                                {
                                    numArray2[num5++] = num9 + 1;
                                }
                                goto Label_00F2;
                            }

                            num9 += num3 * 2;
                            
                            switch (ch2)
                            {
                                case '<':
                                    if (flag || (ch == '.'))
                                        goto Label_00F2;
                                    numArray2[num5++] = num9;
                                    goto Label_028D;

                                case '"':
                                    if (flag)
                                        goto Label_00F2;
                                    if (ch == '.')
                                    {
                                        numArray2[num5++] = num9;
                                        goto Label_028D;
                                    }
                                    break;
                            }

                            if (!flag)
                            {
                                if (ch2 == '?')
                                {
                                    numArray2[num5++] = num9;
                                }
                                else if (ch2 == ch)
                                {
                                    numArray2[num5++] = num9;
                                }
                            }
                        }
                    }

                    Label_028D:
                    if ((index >= num7) || (num6 >= num5))
                        continue;

                    while (num6 < num5)
                    {
                        var num14 = sourceArray.Length;
                        while ((index < num14) && (sourceArray[index] < numArray2[num6]))
                            index++;
                        num6++;
                    }
                }

                if (num5 == 0)
                {
                    return false;
                }

                var numArray4 = sourceArray;
                sourceArray = numArray2;
                numArray2 = numArray4;
                num7 = num5;
            }

            num9 = sourceArray[num7 - 1];
            
            return (num9 == num8);
        }
    }
}