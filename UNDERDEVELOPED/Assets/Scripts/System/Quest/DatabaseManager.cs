using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
using System;
using System.Runtime.InteropServices.WindowsRuntime;

public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager _instance;
    private string _dbPath;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
            return;
        }

        _instance = this;
    }

    void Start()
    {
        string fileName = "Underdeveloped.db";
        string folderName = "Database";
        string streamingAssetsPath = Path.Combine(Application.streamingAssetsPath, Path.Combine(folderName, fileName));
        string persistentDataPath = Path.Combine(Application.persistentDataPath, fileName);         
        
        if (!File.Exists(persistentDataPath))
        {
            File.Copy(streamingAssetsPath, persistentDataPath);
        }

        _dbPath = "URI=file:" + persistentDataPath;
        Debug.Log("We in");

        // using (var connection = new SqliteConnection(dbPath))
        // {
        //     connection.Open();
        //     using (var command = connection.CreateCommand())
        //     {
        //         command.CommandText = "SELECT * FROM Challenges";
        //         using (IDataReader reader = command.ExecuteReader())
        //         {
        //             while (reader.Read())
        //             {
        //                 string columnName = reader["Name"].ToString(); 
        //                 Debug.Log("Column Value: " + columnName);
        //             }
        //         }
        //     }
        //     connection.Close();
        // }
    }

    //Would return [name, dir, testDir]
    public string[] LoadDataViaArea(string area, string level, string status)
    {
        string[] selectedData = new string[3];

        using (SqliteConnection connection = new SqliteConnection(_dbPath))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Challenges WHERE Level = @level AND Area = @area AND Status = @status LIMIT 1;";
                command.Parameters.AddWithValue("@level", level);
                command.Parameters.AddWithValue("@area", area);
                command.Parameters.AddWithValue("@status", status);

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string name = reader["Name"].ToString();
                        string directory = reader["Directory"].ToString();
                        string testDir = reader["TestDir"].ToString();

                        selectedData[0] = name;
                        selectedData[1] = directory;
                        selectedData[2] = testDir;

                        Debug.Log("Column Value: " + name);
                    }
                }
            }
            connection.Close();
        }

        return selectedData;
    }
    
    public string[][] LoadAllChallengesViaAreaAndStatus(string area, string status)
    {
        string[][] data = null;

        using (var connection = new SqliteConnection(_dbPath))
        {
            connection.Open();

            string query = $"SELECT * FROM Challenges WHERE Area = @area AND Status = @status";

            using (var command = new SqliteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@area", "South"); //change back to area
                command.Parameters.AddWithValue("@status", "NotDone"); //change back to status
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    var dataTable = new DataTable();
                    dataTable.Load(reader);

                    data = new string[dataTable.Rows.Count][];
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        data[i] = new string[dataTable.Columns.Count];
                        for (int j = 0; j < dataTable.Columns.Count; j++)
                        {
                            data[i][j] = dataTable.Rows[i][j].ToString();
                        }
                        Debug.Log("Row: "+i);
                    }
                }
            }
        }

        return data;
    }

    public string[] GetChallengeViaName(string challengeName)
    {
        string[] selectedData = new string[7];

        using (SqliteConnection connection = new SqliteConnection(_dbPath))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Challenges WHERE Name = @challengeName;";
                command.Parameters.AddWithValue("@challengeName", challengeName);

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string name = reader["Name"].ToString();
                        string description = reader["Description"].ToString();
                        string status = reader["Status"].ToString();                
                        string directory = reader["Directory"].ToString();
                        string area = reader["Area"].ToString();
                        string level= reader["Level"].ToString();
                        string testDir = reader["TestDir"].ToString();

                        selectedData[0] = name;
                        selectedData[1] = description;
                        selectedData[2] = status;
                        selectedData[3] = directory;
                        selectedData[4] = area;
                        selectedData[5] = level;
                        selectedData[6] = testDir;
                    }
                }
            }
            connection.Close();
        }

        return selectedData;
    }

    public string[] GetChallengeViaAreaAndLevel(string challengeArea, string challengeLevel)
    {
        string[] selectedData = new string[7];
        int numOfChallenge = 0,
        index = 0,
        targetIndex = 0;

        using (SqliteConnection connection = new SqliteConnection(_dbPath))
        {   
            connection.Open();
            
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT COUNT(*) FROM Challenges WHERE Area = @area AND Level = @level;";
                command.Parameters.AddWithValue("@area", challengeArea);
                command.Parameters.AddWithValue("@level", challengeLevel);

                numOfChallenge = Convert.ToInt32(command.ExecuteScalar());
            }

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Challenges WHERE Area = @area AND Level = @level;";
                command.Parameters.AddWithValue("@area", challengeArea);
                command.Parameters.AddWithValue("@level", challengeLevel);
                
                if (numOfChallenge == 0)
                {
                    return null;
                }
                else if (numOfChallenge > 0)
                {
                    System.Random random = new System.Random();
                    targetIndex = random.Next(0, numOfChallenge);
                }

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (index++ != targetIndex)
                        {
                            continue;
                        }

                        string name = reader["Name"].ToString();
                        string description = reader["Description"].ToString();
                        string status = reader["Status"].ToString();                
                        string directory = reader["Directory"].ToString();
                        string area = reader["Area"].ToString();
                        string level= reader["Level"].ToString();
                        string testDir = reader["TestDir"].ToString();

                        selectedData[0] = name;
                        selectedData[1] = description;
                        selectedData[2] = status;
                        selectedData[3] = directory;
                        selectedData[4] = area;
                        selectedData[5] = level;
                        selectedData[6] = testDir;
                        break;
                    }
                }
            }
            connection.Close();
        }

        return selectedData;
    }

    //To be tested, might return only one
    //Will fecth all the challenges that have "Done" status value
    public string[] LoadAllCompletedChallenges(string challengeStatus)
    {
        string[] selectedData = new string[7];

        using (SqliteConnection connection = new SqliteConnection(_dbPath))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Challenges WHERE Status = @status;";
                command.Parameters.AddWithValue("@status", challengeStatus);

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string name = reader["Name"].ToString();
                        string description = reader["Description"].ToString();
                        string status = reader["Status"].ToString();                
                        string directory = reader["Directory"].ToString();
                        string area = reader["Area"].ToString();
                        string level= reader["Level"].ToString();
                        string testDir = reader["TestDir"].ToString();

                        selectedData[0] = name;
                        selectedData[1] = description;
                        selectedData[2] = status;
                        selectedData[3] = directory;
                        selectedData[4] = area;
                        selectedData[5] = level;
                        selectedData[6] = testDir;
                    }
                }
            }
            connection.Close();
        }

        return selectedData;
    }

    //fetch all the challenges from the table Challenges
    public string[][] LoadAllChallenges()
    {
        string[][] data = null;

        using (var connection = new SqliteConnection(_dbPath))
        {
            connection.Open();

            string query = $"SELECT * FROM Challenges";
            using (var command = new SqliteCommand(query, connection))
            {
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    var dataTable = new DataTable();
                    dataTable.Load(reader);

                    data = new string[dataTable.Rows.Count][];
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        data[i] = new string[dataTable.Columns.Count];
                        for (int j = 0; j < dataTable.Columns.Count; j++)
                        {
                            data[i][j] = dataTable.Rows[i][j].ToString();
                        }
                    }
                }
            }
        }

        return data;
    }

    public void UpdateChallengeStatus(string name, string status)
    {
        using (SqliteConnection connection = new SqliteConnection(_dbPath))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE Challenges SET Status = @status WHERE Name = @name";
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@status", status);
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }
}
