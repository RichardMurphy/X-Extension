using System;
using Microsoft.SmallBasic.Library;
using System.Net;

namespace Jibba
{
    /// <summary>
    /// provides some SQLite resources to create leader boards. Each board is a unique table in one large shared database.
    /// </summary>
    [SmallBasicType]
    public static class XLeadersBoard
    {
        private static string databasePath = "http://getjibba.com/Databases/LeadersBoard/";

        /// <summary>
        /// Creates a table (aka leaders board) of the name you specify if it doesn't already exist
        /// </summary>
        /// <param name="tableName">Unique Game name. Include the programmers name (or PIN) here as well to avoid any overwrites</param>
        /// <returns></returns>
        /// <example>XLeadersBoard.CreateTable("SolitaireByJibba")</example>
        public static void CreateTable(Primitive tableName)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    client.DownloadString(new Uri(databasePath + "createLeadersTable.php?tableName=" + tableName));
                }
            }
            catch (WebException ex)
            {

            }
        }

        /// <summary>
        /// Adds a record to the specified table (aka leaders board). The dateTime will be automatically added.
        /// Do this once per session at the start of the game. Then use XLeadersBoard.UpdateRecord() to update this record during the session.
        /// </summary>
        /// <param name="tableName">The game name used when the table was created. String</param>
        /// <param name="player">Players name. String</param>
        /// <param name="score">Game score. Integer</param>
        /// <param name="time">How long it took to get that score. Integer</param>
        /// <returns>DateTime added. Use this value as an ID for this session when updating the score with UpdateRecord().</returns>
        /// <example>XLeadersBoard.AddRecord("SolitaireByJibba", "Jack", 0, 0)</example>
        public static Primitive AddRecord(Primitive tableName, Primitive player, Primitive score, Primitive time)
        {
            string query = "tableName=" + tableName + "&player=" + player + "&score=" + score + "&time=" + time;
            try
            {
                using (WebClient client = new WebClient())
                {
                    return client.DownloadString(new Uri(databasePath + "addLeadersRecord.php?" + query));
                }
            }
            catch (WebException ex)
            {
                return "";
            }
        }

        /// <summary>
        /// Gets all the records sorted by score, time and player in DESC, ASC and ASC order repectively
        /// </summary>
        /// <param name="tableName">The game name used when the board was created</param>
        /// <returns>A sorted CSV table of all the records for the game</returns>
        /// <example>XLeadersBoard.GetAllRecords("SolitaireByJibba")</example>
        public static Primitive GetAllRecords(Primitive tableName)
        {
            string query = "tableName=" + tableName;
            try
            {
                using (WebClient client = new WebClient())
                {
                    return client.DownloadString(new Uri(databasePath + "selectAllLeaders.php?" + query));
                }
            }
            catch (WebException ex)
            {
                return "";
            }
        }

        /// <summary>
        /// Updates the record for this session
        /// </summary>
        /// <param name="tableName">The game name used when the board was created</param>
        /// <param name="player">Players name (can be "")</param>
        /// <param name="dateRef">The dateTime value that was returned by dateRef = XLeadersBoard.AddRecord(...)</param>
        /// <param name="score">Game score. Integer</param>
        /// <param name="time">How long it took to get that score. Integer</param>
        /// <example>XLeadersBoard.UpdateRecord("SolitaireByJibba", "Jack", dateRef, 100, 750)</example>
        public static void UpdateRecord(Primitive tableName, Primitive player, Primitive dateRef, Primitive score, Primitive time)
        {

            string query = "tableName=" + tableName + "&player=" + player + "&dateRef=" + dateRef + "&score=" + score + "&time=" + time;
            try
            {
                using (WebClient client = new WebClient())
                {
                    client.DownloadString(new Uri(databasePath + "updateLeadersRecord.php?" + query));
                }
            }
            catch (WebException ex)
            {

            }
        }
    }
}
