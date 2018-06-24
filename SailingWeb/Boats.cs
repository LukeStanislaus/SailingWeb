//using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailingWeb.Data
{
    public class Boats
    {

        //public static Dictionary<string, Boats> LoadFullExcel(string path)
        /*
         
        public static void LoadFullExcel(string path)
        {
            
            using (var stream = File.Open(@path + @"\WFSC_DATA (3).xlsx", FileMode.Open, FileAccess.Read))
            {

                // Auto-detect format, supports:
                //  - Binary Excel files (2.0-2003 format; *.xls)
                //  - OpenXml Excel files (2007 format; *.xlsx)
                
                using (StreamWriter file =
new StreamWriter(@path + @"\Full List.txt", true))
                {

                }
                

                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    //2. Use the AsDataSet extension method
                    var result = reader.AsDataSet();
                    DataTable table = new DataTable();
                    Console.ReadLine();
                    table = result.Tables[2];
                    File.Delete(@path + @"\Full List.txt");
                    //File.Create(@path + @"\Full List.txt");
                    using (StreamWriter file =
new StreamWriter(@path + @"\Full List.txt", true))
                    {

                        foreach (DataRow dr in table.Rows)
                        {
                            if (!dr["Column2"].ToString().Equals("") && !dr["Column2"].ToString().Equals("Class"))
                            {


                                file.WriteLine("{0}\t{1}\t{2}", dr["Column0"].ToString(),
                                dr["Column1"].ToString(), dr["Column2"].ToString());
                                //Console.WriteLine(dr["Column2"].ToString());
                                //Dictionary<string, Boats> nothing1 = new Dictionary<string, Boats>();

                            }
                            //Console.WriteLine(table.Rows[2][1]);


                            //DataRow hi =  new result.Tables[2].Rows[2];
                            // The result of each spreadsheet is in result.Tables
                        }
                        file.Close();
                    }
                }

            }
        }
        */
        //public static void ExcelWriteFile(string path, string name, string boat, int boatNumber)
        /*
        public static void ExcelWriteFile(string path)
        {
            File.Delete(@path + @"\test.xls");
            var connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + @path + @"\test.xls" + ";Extended Properties=Excel 8.0";
            using (var excelConnection = new OleDbConnection(connectionString))
            {
                // The excel file does not need to exist, opening the connection will create the
                // excel file for you
                if (excelConnection.State != ConnectionState.Open) { excelConnection.Open(); }

                // data is an object so it works with DBNull.Value
                object propertyOneValue = "Luke Stanislaus";
                object propertyTwoValue = "Laser";
                object BoatNumber = "162872";

                var sqlText = "CREATE TABLE Boatlistfull ([Name] VARCHAR(100), [Boat] VARCHAR(100), [BoatNumber] INT)";

                //var sqlText = "CREATE TABLE Boatlistfull ([Name] VARCHAR(100), [Boat] VARCHAR(100), [BoatNumber] VARCHAR(100)";

                // Executing this command will create the worksheet inside of the workbook
                // the table name will be the new worksheet name
                using (var command = new OleDbCommand(sqlText, excelConnection)) { command.ExecuteNonQuery(); }

                // Add (insert) data to the worksheet
                var commandText = $"Insert Into Boatlistfull ([Name], [Boat], [BoatNumber]) Values (@PropertyOne, @PropertyTwo, @BoatNumber)";

                using (var command = new OleDbCommand(commandText, excelConnection))
                {
                    // We need to allow for nulls just like we would with
                    // sql, if your data is null a DBNull.Value should be used
                    // instead of null 
                    command.Parameters.AddWithValue("@Name", propertyOneValue ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Boat", propertyTwoValue ?? DBNull.Value);
                    command.Parameters.AddWithValue("@BoatNumber", BoatNumber ?? DBNull.Value);

                    command.ExecuteNonQuery();
                }

            }
        }
        */

        public string boatName { get; set; }
        public int boatNumber { get; set; }
        
        [Key]
        [Required, StringLength(100)]
        public string name { get; set; }

        /*
        public Boats(string Name, string Boat, int BoatNumber)
        {



        name = Name;

            boatNumber = BoatNumber;
            boatName = Boat;

        }
        */
        public Boats(string Name)
        {
            name = Name;
        }
        public Boats()
        {
        }
        }

}
