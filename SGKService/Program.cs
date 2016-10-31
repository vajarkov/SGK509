/*
 * Created by SharpDevelop.
 * User: Technical department
 * Date: 17.10.2016
 * Time: 12:13
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ServiceProcess;
using System.Diagnostics;
	
namespace SGKService
{
	static class Program
	{
		/// <summary>
		/// This method starts the service.
		/// </summary>
		static void Main()
		{
			try
			{
				ServiceBase.Run(new ServiceBase[] { new SGKService() });
			}
			catch (Exception ex)
			{
				EventLog eventLog = new EventLog();
                if (!EventLog.SourceExists("SGKService"))
                {
                    EventLog.CreateEventSource("SGKService", "SGKService");
                }
                eventLog.Source = "SGKService";
                eventLog.WriteEntry(String.Format("Exception: {0} \n\nStack: {1}", ex.Message + " : " + ex.ToString(), ex.StackTrace) , EventLogEntryType.Error );
			}
		}
	}
}
