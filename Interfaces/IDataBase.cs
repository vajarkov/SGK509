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
using DataTypes;

namespace Interfaces
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
		void GetParameters(Microsoft.Windows.Controls.DataGridComboBoxColumn cbItem, string tableName);
		
		
		/// <summary>
		/// Получение списка БД
		/// </summary>
		List<string> GetDBList();
		
		/// <summary>Получение данных из справочников и конфигурации в БД</summary> 
		/// <param name="dvgItem">Объект для отображения данных</param>
		/// <param name="ds">Промежуточный набор для хранения данных</param>
		/// <param name="tblItem">Имя таблица в БД</param>
		void GetData(Microsoft.Windows.Controls.DataGrid dvgItem, DataSet ds, string tblItem);
		
		/// <summary>Обновление данных справочников и конфигурации в БД </summary>
		/// <param name="dvgItem">Объект для отображения данных</param>
		/// <param name="ds">Промежуточный набор для хранения данных</param>
		/// <param name="tblItem">Имя таблица в БД</param>
		void UpdateData(Microsoft.Windows.Controls.DataGrid dvgItem, DataSet ds, string tblItem);
		
		/// <summary>
		/// Запрос параметров для опроса по дискретных сигналов Modbus
		/// </summary>
		/// <param name="tblConfig">Таблица с параметрами</param>
		/// <returns></returns>
		Dictionary<int, DiscreteSignal> GetParams(string tblConfig);
		
		
		/// <summary>
		/// Запрос параметров для опроса по аналоговых сигналов Modbus
		/// </summary>
		/// <param name="tblConfig">Таблица с параметрами</param>
		/// <returns></returns>
		Dictionary<int, DiscreteSignal> GetParams(string tblConfig);
		
		/// <summary>
		/// Запись параметров в журнал
		/// </summary>
		/// <param name="tblItem">Имя журнала</param>
		/// <param name="id_num">Идентификатор параметра</param>
		/// <param name="timestamp">Время записи</param>
		/// <param name="value">Значение</param>
		void InsertSignal(string tblItem, string id_num, string timestamp, string value);
	}
}
