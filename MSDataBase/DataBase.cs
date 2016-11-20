/*
 * Created by SharpDevelop.
 * User: Technical department
 * Date: 28.10.2016
 * Time: 9:14
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using Microsoft.SqlServer.Management.Smo;
using System.Text;
using Microsoft.Windows.Controls;
using System.Collections.ObjectModel;
using Interfaces;
using DataTypes;


namespace MSDataBase
{
	/// <summary>
	/// Класс для работы с БД MS SQL Server
	/// </summary>
	public class DataBase : IDataBase
	{
		private string dbDataSource;
		private string dbName;
		private string dbUserName;
		private string dbPassword;
		private SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
		
		#region Сохранение параметров БД
		
		public void SetDBConnectionParameters(string DataSource, string userName, string password)
		{
			this.dbDataSource = DataSource;
			this.dbUserName = userName;
			this.dbPassword = password;
		}
		
		public void SetDBConnectionParameters(string DataSource, string DBName, string userName, string password)
		{
			this.dbDataSource = DataSource;
			this.dbName = DBName;
			this.dbUserName = userName;
			this.dbPassword = password;
		}
		#endregion
		
		#region Считывание доступных баз данных для MS SQL Server
		public List<string> Connect()
		{
			SqlDataSourceEnumerator instance = SqlDataSourceEnumerator.Instance;
			var tableDataSources = new System.Data.DataTable();
			if (tableDataSources.Rows.Count == 0)
			{
				tableDataSources = instance.GetDataSources();
				var listServers =  new List<string>();
				foreach(DataRow rowServer in tableDataSources.Rows)
				{
					if(String.IsNullOrEmpty(rowServer["InstanceName"].ToString()))
						listServers.Add(rowServer["ServerName"].ToString());
					else
						listServers.Add(rowServer["ServerName"] + "\\" + rowServer["InstanceName"]);
				}
				return listServers;
			}
			return null;
		}
		#endregion
		
		
		#region Проверка связи с MSSQL
		public bool Test()
		{
			GetConnectionString();
			
			using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
			{
				try
				{
					connection.Open();
					return true;
				}
				catch (SqlException)
				{
					return false;
				}
			}
		}
		#endregion
		
		
		#region Создание строки соединения
		public void GetConnectionString()
		{
			builder.DataSource = dbDataSource;
			builder.InitialCatalog = dbName;
			builder.UserID = dbUserName;
			builder.Password = dbPassword;
		}
		#endregion
		
		
		#region Получение списка БД
		public List<string> GetDBList()
		{
			var DBList = new Microsoft.SqlServer.Management.Smo.Server(dbDataSource);
			DBList.ConnectionContext.LoginSecure = false;
			DBList.ConnectionContext.Login = dbUserName;
			DBList.ConnectionContext.Password = dbPassword;
			var listDB = new List<string>();
			try
			{
				foreach(Microsoft.SqlServer.Management.Smo.Database db in DBList.Databases){
					listDB.Add(db.Name);
				}
			}
			catch (Exception ex)
			{
				System.Windows.MessageBox.Show(ex.Message);
			}
			return listDB;
		}
		#endregion
		
		#region Заполнение ячеек таблиц конфигурации данными
		public void GetParameters(DataGridComboBoxColumn cbItem, string tableName)
		{
			GetConnectionString();
			using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
			{
				try
				{
					connection.Open();
					
					using (SqlCommand command = new SqlCommand("SELECT id, name from " + tableName, connection))
					{
						SqlDataAdapter adapter = new SqlDataAdapter();
						adapter.SelectCommand = command;
						DataTable table = new DataTable();
						table.Locale = System.Globalization.CultureInfo.InvariantCulture;
						adapter.Fill(table);
						cbItem.ItemsSource= table.AsDataView();
						cbItem.DisplayMemberPath = "name";
						cbItem.SelectedValuePath = "id";
					}                                         
				}
				catch (Exception e){
					System.Windows.MessageBox.Show(e.Message);
				}
			}
		}
		#endregion
		
		#region Заполнение справочников из БД
		public void GetData(Microsoft.Windows.Controls.DataGrid dvgItem, DataSet ds, string tblItem)
		{
			try
			{
				GetConnectionString();
				using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
				{
					SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM " + tblItem, connection);
					if (ds.Tables.Count != 0)
						ds.Tables[0].Clear();
					adapter.Fill(ds);
					dvgItem.ItemsSource = ds.Tables[0].AsDataView();
				}
			}
			catch (Exception e)
			{
				System.Windows.MessageBox.Show(e.Message);
			}
		}
		#endregion
		
		#region Обновление справочников в БД
		public void UpdateData(Microsoft.Windows.Controls.DataGrid dvgItem, DataSet ds, string tblItem)
		{
			try
			{
				GetConnectionString();
				using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
				{
					SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM " + tblItem, connection);
					SqlCommandBuilder build = new SqlCommandBuilder(adapter);
					adapter.Update(ds.Tables[0]);
				}
			}
			catch (Exception e)
			{
				System.Windows.MessageBox.Show(e.Message);
			}
		}
		#endregion
		
		#region Запрос параметров для опроса по Modbus
		public Dictionary<int, DiscreteSignal> GetParams(string tblConfig)
		{
			Dictionary <int, DiscreteSignal> retValue = new Dictionary<int, DiscreteSignal>();
			GetConnectionString();
			using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
			{
				connection.Open();
				SqlCommand command = new SqlCommand("SELECT id_num, modbus_address from " + tblConfig, connection);
				
				using (var reader = command.ExecuteReader())
				{
					while(reader.Read())
					{
						retValue[(int)reader["id_num"]] = (DiscreteSignal) reader["modbus_address"];
					}
				}
			}
			return retValue;
		}
		#endregion
		
		#region Запрос параметров для опроса по Modbus
		public Dictionary<int, AnalogSignal> GetParams(string tblConfig, string tblDictinary)
		{
			Dictionary <int, AnalogSignal> retValue = new Dictionary<int, AnalogSignal>();
			GetConnectionString();
			using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
			{
				connection.Open();
				SqlCommand command = new SqlCommand("SELECT ca.id_num, ca.modbus_address, dt.bytes from " + tblConfig + " ca LEFT JOIN " + tblDictinary + " dt ON ca.id_type = dt.id", connection);
				
				using (var reader = command.ExecuteReader())
				{
					while(reader.Read())
					{
						retValue[(int)reader["id_num"]].Modbus_address = (int)reader["modbus_address"];
						retValue[(int)reader["id_num"]].Size = (int)reader["bytes"];
					}
				}
			}
			return retValue;
		}
		#endregion
		
		#region Запись в БД значения
		public void InsertSignal(string tblItem, string id_num, string timestamp, string value)
		{
			GetConnectionString();
			using(SqlConnection connection = new SqlConnection(builder.ConnectionString))
			{
				connection.Open();
				SqlCommand command = new SqlCommand("INSERT INTO " + tblItem + " VALUES ('" + timestamp + "', " + id_num  + ", " + value + ")", connection);
				command.ExecuteNonQuery();
			}
		}
		#endregion
	}
}