/*
 * Created by SharpDevelop.
 * User: Technical department
 * Date: 28.10.2016
 * Time: 9:05
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace MSDataBase
{
	/// <summary>
	/// Интерфейс для работой с БД.
	/// </summary>
	public interface IDataBase
	{
		/// <summary>
		/// Сохранение параметров для соединения с БД
		/// </summary>
		/// <param name="DataSource">Источник данных</param>
		/// <param name="DBName">Имя базы</param>
		/// <param name="userName">Имя пользователя</param>
		/// <param name="password">Пароль пользователя</param>
		void SetDBConnectionParameters(string DataSource, string DBName, string userName, string password);
		
		/// <summary>
		/// Сохранение параметров для соединения с БД
		/// </summary>
		/// <param name="DataSource">Источник данных</param>
		/// <param name="userName">Имя пользователя</param>
		/// <param name="password">Пароль пользователя</param>
		void SetDBConnectionParameters(string DataSource, string userName, string password);
		
		/// <summary>
		/// Проверка соединения
		/// </summary>
		/// <returns>Доступна ли база</returns>
		bool Test();
		
		/// <summary>
		/// Считывание доступных баз данных для MS SQL Server
		/// </summary>
		/// <returns>Список доступных серверов</returns>
		List<string> Connect();
		
		/// <summary>
		/// Получение строки соединения
		/// </summary>
		void GetConnectionString();
		
		/// <summary>
		/// Получение параметров из справочников
		/// </summary>
		/// <param name="cbItem">Поле параметра на форме</param>
		/// <param name="tableName">Имя таблицы в БД</param>
		void GetParameters(DataGridViewComboBoxColumn cbItem, string tableName);
		
		/// <summary>
		/// Получение списка БД
		/// </summary>
		List<string> GetDBList();
		
		/// <summary>Получение данных из справочника </summary> 
		void GetDict(DataGridView dvgItem, DataSet ds, string tblItem);
		
		/// <summary> Получение данных из конфигурации </summary>
		void GetConfig(DataGridView dvgItem, string tblName);
		// Обновление данных в справочнике
		void UpdateDict(DataGridView dvgItem, DataSet ds, string tblItem);
		// Обновление данных в конфигурации
		void UpdateConfig(DataGridView dvgItem, string tblItem);
	}
}
