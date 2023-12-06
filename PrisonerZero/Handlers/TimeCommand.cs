using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonerZero.Handlers
{
    internal class TimeCommand
    {
        public static readonly IEnumerable<string> Commands = new[] { "now", "t", "time", "время", "сейчас" };

        public static async Task<string> GetHebrewJewishDateString()
        {
            System.Text.StringBuilder hebrewFormatedString = new System.Text.StringBuilder();

            // Create the hebrew culture to use hebrew (Jewish) calendar 
            CultureInfo jewishCulture = CultureInfo.CreateSpecificCulture("he-IL");
            jewishCulture.DateTimeFormat.Calendar = new HebrewCalendar();

            #region Format the date into a Jewish format 

            var date = DateTime.Now;

            // Day of the week in the format " " 
            hebrewFormatedString.Append(date.ToString("dddd", jewishCulture) + " ");

            // Day of the month in the format "'" 
            hebrewFormatedString.Append(date.ToString("dd", jewishCulture) + " ");

            // Month and year in the format " " 
            hebrewFormatedString.Append("" + date.ToString("y", jewishCulture));
            #endregion

            return hebrewFormatedString.ToString();
        }
    }
}
