using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsAppForCSHarp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void linqSampleMethods()
        {

            #region SUM

            var numbers = new List<int> { 8, 2, 6, 3 };
            int sum = numbers.Sum();

            var stringList = new List<string> { "88888888", "22", "666666", "333" };

            // these two lines do the same
            int lengthSum = stringList.Select(x => x.Length).Sum();  // lengthSum: 19
            lengthSum = stringList.Sum(x => x.Length);           // lengthSum: 19


            //LINQ query expression to get sum of numeric values in the collection.
            var list = new List<int> { 8, 2, 6, 3 };
            sum = (from x in list select x).Sum();  // sum: 19
            
            //LINQ query expression to get sum of numbers which match specified predicate.
            list = new List<int> { 8, 2, 6, 3 };
            sum = (from x in list where x > 4 select x).Sum();  // sum: 14
            
            //LINQ query expression to get sum of string lengths using selector.
            var listString = new List<string> { "88888888", "22", "666666", "333" };
            lengthSum = (from x in listString select x.Length).Sum();  // lengthSum: 19

            //Sum with Group By

            var players = new List<Player> {
                new Player { Name = "Alex", Team = "A", Score = 10 },
                new Player { Name = "Anna", Team = "A", Score = 20 },
                new Player { Name = "Luke", Team = "L", Score = 60 },
                new Player { Name = "Lucy", Team = "L", Score = 40 },
            };

            var teamTotalScores =
                from player in players
                group player by player.Team into playerGroup
                select new
                {
                    Team = playerGroup.Key,
                    TotalScore = playerGroup.Sum(x => x.Score),
                };
            // teamTotalScores is collection of anonymous objects:
            // { Team = "A", TotalScore = 30 }
            // { Team = "L", TotalScore = 100 }



            #endregion SUM

            #region Max/Min

            //Gets maximal number from list of integer numbers.
            numbers = new List<int> { 1, 8, 3, 2 };
            int maxNumber = numbers.Max();  // maxNumber: 8
            
            //Gets maximal number from list of decimal numbers.
            var numbersDec = new List<decimal> { 1.0m, 8.1m, 3.3m, 2.0m };
            decimal maxNumberD = numbersDec.Max();  // maxNumber: 8.1
            
            //Calling Max on empty collection throws exception.
            
            numbers = new List<int>();  // empty list
            maxNumber = numbers.Max();  // throws InvalidOperationException
            
            //Max for Nullable Numeric Types
            //Gets maximal number from list of nullable integers.
            var numbersN = new List<int?> { 1, 8, null, 3 };
            int? maxNumberN = numbersN.Max();  // maxNumber: 8

            //Returns null if the collection contains only null values.
            numbersN = new List<int?> { null };
            maxNumberN = numbers.Max();  // maxNumber: null

            //Returns null if the collection is empty.
            numbersN = new List<int?>();  // empty list
            maxNumberN = numbers.Max();  // maxNumber: null
            
            //Max with Selector
            //This example gets length of the longest string.
            stringList = new List<string> { "1", "88888888", "333", "22" };

            // these two lines do the same
            int maxLength = stringList.Select(x => x.Length).Max();  // maxLength: 8
            maxLength = stringList.Max(x => x.Length);           // maxLength: 8
            //Max for Types Implementing IComparable
            //LINQ method Max can be used also on collection of custom type(not numeric type like int or decimal).
            //The custom type have to implement IComparable interface.
            //Lets have custom type Money which implements IComparable.

            var amounts = new List<Money> { new Money(30), new Money(10) };
            Money maxAmount = amounts.Max();  // maxAmount: Money with value 30


            players = new List<Player> {
            new Player { Name = "Alex", Team = "A", Score = 10 },
            new Player { Name = "Anna", Team = "A", Score = 20 },
            new Player { Name = "Luke", Team = "L", Score = 60 },
            new Player { Name = "Lucy", Team = "L", Score = 40 },
            };

            var teamBestScores =
                from player in players
                group player by player.Team into playerGroup
                select new
                {
                    Team = playerGroup.Key,
                    BestScore = playerGroup.Max(x => x.Score),
                };

            // teamBestScores is collection of anonymous objects:
            // { Team = "A", BestScore = 20 }
            // { Team = "L", BestScore = 60 }



            #endregion Max/Min

            #region count

            //Counts number of items in the IEnumerable collection.
            IEnumerable<string> items = new List<string> { "A", "B", "C" };
            int count = items.Count();  // count: 3
            //Returns 0 if there is no item in the collection.
            items = new List<string> { };
            count = items.Count();  // count: 0
            //Count with Predicate
            //Counts number of items which match specified predicate.
            IEnumerable<int> itemsI = new List<int> { 8, 3, 2 };

            // these two lines do the same
            count = itemsI.Where(x => x < 5).Count();  // count: 2
            count = itemsI.Count(x => x < 5);          // count: 2
            //Count with Query Syntax
            //LINQ query expression to count number of all items in the collection.
            itemsI = new List<int> { 8, 3, 2 };
            count = (from x in itemsI select x).Count();  // count: 3
            //LINQ query expression to count number of items which match specified predicate.
            itemsI = new List<int> { 8, 3, 2 };
            count = (from x in itemsI where x < 5 select x).Count();  // count: 2
            //Count with Group By
            //This example shows how to count number of items per group.Lets count players in each team.
            players = new List<Player> {
            new Player { Name = "Alex", Team = "A" },
            new Player { Name = "Anna", Team = "A" },
            new Player { Name = "Luke", Team = "L" },
            new Player { Name = "Lucy", Team = "L" },
            new Player { Name = "Mike", Team = "M" },
            };

            var playersPerTeam =
                from player in players
                group player by player.Team into playerGroup
                select new
                {
                    Team = playerGroup.Key,
                    Count = playerGroup.Count(),
                };

            // playersPerTeam is collection of anonymous objects:
            // { Team = "A", Count = 2 }
            // { Team = "L", Count = 2 }
            // { Team = "M", Count = 1 }


            #endregion count

            #region Average
            //Computes average of int values. The result type is double.
            list = new List<int> { 1, 8, 3, 2 };
            var resultD = list.Average();  // result: 3.5
            
            //Computes average of decimal values. The result type is decimal.
            var listD = new List<decimal> { 1.0m, 8.1m, 3.3m, 2.0m };
            resultD = list.Average();  // result: 3.6
            
            //Computes average of nullable int values. The result type is nullable double.The null values in collection are ignored for calculation.
           var listN = new List<int?> { 1, 8, null, 3 };
           double ? resultDN = listN.Average();  // result: 4.0 = (1+8+3)/3
           
            //If Average method is called on empty collection, it throws the exception.
           list = new List<int>();      // empty list
           resultD = list.Average();  // throws InvalidOperationException
            
            //If Average method is called on empty collection of nullable type, it returns null.
            listN = new List<int?>();      // empty list of nullable ints
            double? result = list.Average();  // result: null
            
            //Average with Selector
            //You can calculate average value on collection of any type(IEnumerable<T>), but you have to tranform the type T into any numeric type using selector.It can be done by Select method or using selector as a parameter of the Average method.
            //This example counts the average length of the strings.
          
            stringList = new List<string> { "1", "88888888", "333", "22" };

            // these two lines do the same
            resultD = stringList.Select(x => x.Length).Average();  // result: 3.5
            resultD = stringList.Average(x => x.Length);           // result: 3.5
            //Average with Query Syntax
            //LINQ query expression to calculate average value of all items in the collection.
            list = new List<int> { 1, 8, 3, 2 };
            resultD = (from x in list select x).Average();  // result: 3.5
            
            //LINQ query expression to calculate average value of items which match specified predicate.
            list = new List<int> { 1, 8, 3, 2 };
            resultD = (from x in list where x < 5 select x).Average();  // result: 2.0

            //LINQ query expression to calculate average string length using selector.
            stringList = new List<string> { "1", "88888888", "333", "22" };
            resultD = (from x in stringList select x.Length).Average();  // result: 3.5
            
            //Average with Group By
            //This example shows how to calculate average value for each group. Lets have players.Each player belongs to a team and have a score.The result is average score per player in a team.
            
            players = new List<Player> {
                new Player { Name = "Alex", Team = "A", Score = 10 },
                new Player { Name = "Anna", Team = "A", Score = 20 },
                new Player { Name = "Luke", Team = "L", Score = 60 },
                new Player { Name = "Lucy", Team = "L", Score = 40 },
            };

            var teamAverageScores =
                from player in players
                group player by player.Team into playerGroup
                select new
                {
                    Team = playerGroup.Key,
                    AverageScore = playerGroup.Average(x => x.Score),
                };

            // teamAverageScores is collection of anonymous objects:
            // { Team = "A", AverageScore = 15.0 }
            // { Team = "L", AverageScore = 50.0 }

            #endregion Average

            #region Aggregate
            
            //This example is for demonstration purpose only.To compute sum of numbers use rather Enumerable.Sum.
            numbers = new List<int> { 6, 2, 8, 3 };
            sum = numbers.Aggregate(func: (result1, item) => result1 + item);
            // sum: (((6+2)+8)+3) = 19
            //In this example there is passed named method Add insted of lambda expression.
            numbers = new List<int> { 6, 2, 8, 3 };
            sum = numbers.Aggregate(func: Add);
            // sum: (((6+2)+8)+3) = 19


            numbers = new List<int> { 6, 2, 8, 3 };
            decimal average = numbers.Aggregate(
                seed: 0,
                func: (result1, item) => result1 + item,
                resultSelector: result1 => (decimal)result1 / numbers.Count);

            #endregion Aggregate

        }


        public void ListSampleSample()
        {
            var listA = new List<int>() { 8, 3, 2 };
            var listB = new List<int>(listA);
            var list = new List<int>(16);
            listB = new List<int>() { 5, 7,6 , 9, 1 };
            list.AddRange(listA);
            list.AddRange(listB);

            int index = list.BinarySearch(6);

            index = list.BinarySearch(item: 6, comparer: new MyComparer());

            index = list.BinarySearch(index: 1, count: 3, item: 6, comparer: new MyComparer());

            var item = list.Find(x => x > 2);

            index = list.FindIndex(x => x < 5);

            list.Sort();

            list.Sort((x, y) => x.CompareTo(y));

            list.Sort((x,y) => y.CompareTo(x));

            list.Sort(index: 2, count: 3, comparer: new MyComparer());
        }

        private static int Add(int x, int y) { return x + y; }
        private void button1_Click(object sender, EventArgs e)
        {
            linqSampleMethods();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            ListSampleSample();
        }



        #region FnP test to Sharoon

        public static string Maskify(string cc)
        {
            // var lastDigits = cardNumber.Substring(cardNumber.Length - 4, 4);
            //var requiredMask = new String('X', cardNumber.Length - lastDigits.Length);
            //var maskedString = string.Concat(requiredMask, lastDigits);
            //var maskedCardNumberWithSpaces = Regex.Replace(maskedString, ".{4}", "$0 ");
            //if cc is empty the null string will be returned


            //if (cc != null && cc != "")
            //var resultString = "";
            //if (string.IsNullOrEmpty(cc))
            // {
            //     var totalDigitCount = cc.Length;
            //     var lastDigits = cc.Substring(cc.Length - 4, 4);
            //     var requiredMask = new String('X', totalDigitCount - lastDigits.Length);

            //     var maskedString = string.Concat(requiredMask, lastDigits);
            //     resultString = string.Concat(requiredMask, lastDigits);
            // }
            var resultString = "";

            for (int i = 0; i < cc.Length; i++)
            {
                resultString += (i < cc.Length - 4) ? 'X' : cc[i];

            }

            //if cc is empty the null string will be returned
            return resultString;


            // return maskedCardNumberWithSpaces;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = Maskify(textBox1.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var a = new List<int> { 24,85,0};
            var b = ArrayPacking(a);
            textBox3.Text = b.ToString();
        }

        public static int ArrayPacking(List<int> integers)
        {
            var countIntegersConstraintsFails = (integers.Count != 3 || (integers.Where(x => x < 0 || x >= 256 ).Count() > 0 ));

            var tempList = new List<string> { "", "", "" };

            if (!countIntegersConstraintsFails)
            {
                int iCount = 0;
                foreach (int iToProcess in integers)
                {
                    string bin = Convert.ToString(iToProcess, 2).PadLeft(8, '0');
                    tempList[iCount] = bin;
                    iCount++;
                }
                var finalString = tempList[2] + tempList[1] + tempList[0];
                // Convert this final string to number now
                var finalNumber = Convert.ToInt32(finalString, 2);
                return finalNumber;
            }


            return 0;
        }

        public static char FirstNonRepeatedCharInString2(string str)
        {
            int i, j;
            bool isRepeted = false;
            char[] chars = str.ToCharArray();
            for (i = 0; i < chars.Length; i++)
            {
                isRepeted = false;
                for (j = 0; j < chars.Length; j++)
                {
                    if ((i != j) && (chars[i] == chars[j]))
                    {
                        isRepeted = true;
                        break;
                    }
                }
                if (isRepeted == false)
                {
                    return chars[i];
                }
            }
            return ' ';
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var c = FirstNonRepeatedCharInString2(textBox2.Text);
            textBox2.Text = c +  " is first Non Repeating character is ";
        }

        #endregion

        private void button5_Click(object sender, EventArgs e)
        {
            textBox4.Text = PowersofTwo(int.Parse(textBox4.Text));
        }


        public string SnakeCase(string str)
        {

            // code goes here  
            var stringLength = str.Length;
            var resultString = "";

            for (int iCount = 0; iCount < stringLength; iCount++)
            {

                if ((str[iCount] >= 'A' && str[iCount] <= 'Z') || (str[iCount] >= 'a' && str[iCount] <= 'z'))
                {
                    resultString += str[iCount].ToString().ToLower();
                }
                else
                {
                    resultString += '_';
                }
            }
            return resultString;
        }

        public string PowersofTwo(int num)
        {
            // code goes here             
            var numberIsPowerOfTwo = (num % 2) == 0;
            if (numberIsPowerOfTwo)
            {
                while ((num/2) != 1)
                {
                    if ((num % 2) == 0)
                    {
                        num = (num / 2);
                    }
                    else
                    {
                        numberIsPowerOfTwo = false;
                        break;
                    }
                }
            }

            return numberIsPowerOfTwo.ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox5.Text = SnakeCase(textBox5.Text);
        }

        public string FirstReverse(string str)
        {

            // code goes here  
            var length = str.Length;
            var returnString = "";
            if (length > 1)
            {
                for (var iCount = length - 1; iCount >= 0; iCount--)
                {
                    returnString += str[iCount];
                }
            }
            return returnString;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox6.Text = FirstReverse(textBox6.Text);
        }

        public string FindLongestWord(string str)
        {

            /*
             * 
            var arrayStrings = str.Split(new char[] { ' ' });
            var lengthOfCurrentString =0;
            var lengthOfPreviousString = 0;
            var longestLength = 0;
            var stringToReturn = "";

            arrayStrings.ToList().ForEach(x => {

              lengthOfCurrentString = x.Length;
              if(lengthOfCurrentString > lengthOfPreviousString && lengthOfCurrentString > longestLength)
              {
                  stringToReturn = x;
                  lengthOfPreviousString = longestLength = lengthOfCurrentString;
              }
            });

            return stringToReturn;
             */

            //One Liner For The Ages. 
            //Regex to strip our punctuation
            //Split on spaces,
            //Linq for the sorting by length
            //Linq to get the first item
            var tempString = Regex.Replace(str, @"(\p{P})", "");
            var temoOrder = tempString.Split(' ');            
            return tempString.Split(' ').OrderByDescending(i => i.Length).First();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox7.Text = FindLongestWord(textBox7.Text);
        }

        public string CodelandUsernameValidation(string str)
        {
            string isValidUser = "false";

            /*
            if (str.Length < 4 || str.Length > 25)
            {
                return "false";
            }

            if (!char.IsLetter(str[0]))
            {
                return "false";
            }

            for (int i = 0; i < str.Length; i++)
            {
                if (!char.IsLetterOrDigit(str, i) && str[i] != '_')
                    return "false";
            }

            if (str[str.Length - 1] == '_')
                return "false";

            str = "true";
            return str;
             */

            //Checking all criteria for the username validation
            if (str.Length > 4 && str.Length < 25
            && str.Substring(str.Length - 1, 1) != "_"
            && Regex.IsMatch(str, @"^[a-zA-Z0-9_]+$")
            && Regex.IsMatch(str.Substring(0, 1), @"^[a-zA-Z]+$"))
            {
                isValidUser = "true";
            }

            return isValidUser;

        }


        public string TreeConstructor(string[] strArr)
        {
            /*
            
            var childs = strArr.ToList().Select(i => i.Split(',')[0].Replace("(", string.Empty)).ToList();
            var parents = strArr.ToList().Select(i => i.Split(',')[1].Replace(")", string.Empty)).ToList();
            var distinCount = childs.Distinct().Count();
            var strCount = strArr.Count();
            var t1 = parents.GroupBy(p => p);
            var t2 = t1.Where(g => g.Count() > 2);

            return childs.Distinct().Count() == strArr.Count() &&
                  !parents.GroupBy(p => p).Where(g => g.Count() > 2).Any() ? "true" : "false";

            */
            var childCounts = new Dictionary<string, int>();
            foreach (string pair in strArr)
            {
                string[] split = pair.Split(',');
                string parent = split[1];

                if (!childCounts.ContainsKey(parent))
                {
                    childCounts[parent] = 0;
                }
                childCounts[parent] = childCounts[parent] + 1;
                if (childCounts[parent] > 2)
                {
                    return "false";
                }
            }
            return "true";
        }


        public static string BracketMatcher(string str)
        {
            var result = "0";
            var charArray = str.ToCharArray();
            var startBracket = 0;
            var endBracket = 0;
            foreach (char cr in charArray)
            {
                if (cr == '(') startBracket++;
                if (cr == ')') endBracket++;
            }

            if (endBracket == startBracket)
            {
                result = "1";
            }
            // code goes here  
            return result;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBox8.Text = CodelandUsernameValidation(textBox8.Text);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            /*
             * Input: new string[] {"(1,2)", "(2,4)", "(5,7)", "(7,2)", "(9,5)"}
                Output: true

            Input: new string[] {"(1,2)", "(3,2)", "(2,12)", "(5,2)"}
            Output: false
             */
            var s1 = new string[] { "(1,2)", "(2,4)", "(5,7)", "(7,2)", "(9,5)" }; //Passes

            var s2 = new string[] { "(1,2)", "(3,2)", "(2,12)", "(5,2)" }; //Fails

            var result = TreeConstructor(s2);
        }
    }

    public struct Money : IComparable<Money>
    {
        public Money(decimal value) : this() { Value = value; }
        public decimal Value { get; private set; }
        public int CompareTo(Money other) { return Value.CompareTo(other.Value); }
    }

    public class Player
    {
        public string Name { get; set; }
        public string Team { get; set; }
        public int Score { get; set; }
    }

    public class MyComparer : IComparer<int>
    {
        public int Compare(int x, int y) { return x.CompareTo(y); }
    }
}
