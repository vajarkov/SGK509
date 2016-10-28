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
using System.Windows.Forms;
using System.Text;



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
				MessageBox.Show(ex.Message);
			}
			return listDB;
		}
		#endregion
		
		
		#region Заполнение ячеек таблиц конфигурации данными
		public void GetParameters(DataGridViewComboBoxColumn cbItem, string tableName)
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
						cbItem.DataSource = table;
						cbItem.DisplayMember = "name";
						cbItem.ValueMember = "id";
					}                                         
				}
				catch (Exception e){
					MessageBox.Show(e.Message);
				}
			}
		}
		#endregion
		
		#region Заполнение данными конфигурации комплекса
		public void GetConfig(DataGridView dvgItem, string tblName)
		{
			GetConnectionString();
			
			using(SqlConnection connection = new SqlConnection(builder.ConnectionString))
			{
				SqlCommand command = new SqlCommand("SELECT * FROM " + tblName, connection);
				connection.Open();
				SqlDataReader reader = command.ExecuteReader();
				if(reader.HasRows)
				{
					while(reader.Read())
					{
						dvgItem.Rows.Add(reader.GetInt32(0), reader.GetInt32(1),reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4));
					}
				}
				reader.Close();
			}
		}
		
		#endregion
		
		#region Заполнение справочников из БД
		public void GetDict(DataGridView dvgItem, DataSet ds, string tblItem)
		{
			GetConnectionString();
			
			using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
			{
				SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM " + tblItem, connection);
				adapter.Fill(ds);
				dvgItem.DataSource = ds.Tables[0];
			}
		}
		#endregion
		
		#region Обновление справочников в БД
		public void UpdateDict(DataGridView dvgItem, DataSet ds, string tblItem)
		{
			GetConnectionString();
			
			using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
			{
				SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM " + tblItem, connection);
				SqlCommandBuilder build = new SqlCommandBuilder(adapter);
				adapter.Update(ds.Tables[0]);
			}
		}
		#endregion
		
		#region Обновление данных конфигурации комплекса
		public void UpdateConfig(DataGridView dvgItem, string tblItem)
		{
			GetConnectionString();
			
			try
			{
				using(SqlConnection connection = new SqlConnection(builder.ConnectionString))
				{
					SqlCommand command = new SqlCommand("DELETE FROM " + tblItem, connection);
					connection.Open();
					command.ExecuteNonQuery();
					StringBuilder query = new StringBuilder();
					query.AppendFormat("INSERT INTO {0} VALUES ", tblItem);
					for(int i = 0; i < dvgItem.Rows.Count - 1; i++)
					{ 
						query.Append("(");
						for (int j = 0; j < dvgItem.Rows[i].Cells.Count; j++ )
	    				{
							query.AppendFormat("{0}, ", dvgItem.Rows[i].Cells[j].Value.ToString());
	    				}
						query.Remove(query.Length-2,2);
						query.Append("),");
					
					}
					
					query.Remove(query.Length-1, 1);
					
					
					command = new SqlCommand(query.ToString(), connection);
					command.ExecuteNonQuery();
					
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		#endregion
	}
}