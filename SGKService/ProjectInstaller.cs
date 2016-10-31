/*
 * Created by SharpDevelop.
 * User: Technical department
 * Date: 17.10.2016
 * Time: 12:13
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace SGKService
{
	[RunInstaller(true)]
	public class ProjectInstaller : Installer
	{
		private ServiceProcessInstaller serviceProcessInstaller;
		private ServiceInstaller serviceInstaller;
		private System.Diagnostics.EventLogInstaller eventLogInstaller;
		public ProjectInstaller()
		{
			serviceProcessInstaller = new ServiceProcessInstaller();
			serviceInstaller = new ServiceInstaller();
			this.eventLogInstaller = new System.Diagnostics.EventLogInstaller();
			// serviceProcessInstaller
			serviceProcessInstaller.Account = ServiceAccount.LocalService;
			
			// serviceInstaller1
			this.serviceInstaller.Description = "Служба архивации и обмена данными для СГК509 и СГК510";
            this.serviceInstaller.DisplayName = "Служба СГК";
            this.serviceInstaller.ServiceName = "SGKService";
            this.serviceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            this.serviceInstaller.Installers.Clear();
            
            // EventLogInstaller
            this.eventLogInstaller.Source = "ModbusRTUService";
            this.eventLogInstaller.Log = "ModbusRTUService";
            
            // ProjectInstaller
			serviceInstaller.ServiceName = SGKService.MyServiceName;
			this.Installers.AddRange(new Installer[] { serviceProcessInstaller, serviceInstaller, eventLogInstaller });
		}
	}
}
