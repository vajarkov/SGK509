/*
 * Created by SharpDevelop.
 * User: Technical department
 * Date: 20.10.2016
 * Time: 12:19
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;   
using System.Configuration;

namespace SGK509
{
	
	public class ModbusSettings : ConfigurationSection
	{
		//System.Configuration.Configuration _Config;

		[ConfigurationProperty("", IsDefaultCollection = true, IsRequired = false)]
		public ModbusParamsSettings ModbusParams
		{
		 	get { return (ModbusParamsSettings) base[""]; }
		 	set { base[""] = value; }
		}
	}
	
	public class ModbusParamsSettings : ConfigurationElementCollection
	{
		const string ELEMENT_NAME = "Modbus";
		
		public override ConfigurationElementCollectionType CollectionType
        {
	        get { return ConfigurationElementCollectionType.BasicMap; }
        }

        protected override string ElementName
        {
	        get {   return ELEMENT_NAME;  }
        }

        protected override ConfigurationElement CreateNewElement()
        {
 	        return new ModbusType();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
 	        return ((ModbusType)element).Type;
        }
	}
	
	public class ModbusType : ConfigurationElement
    {
        [ConfigurationProperty("type", IsRequired=true)]
        public string Type 
        {
            get {   return (string)base["type"];    }
            set {   base["type"] = value;   }
        }

        [ConfigurationProperty("COM", IsRequired = false)]
        public string  COM
        {
            get { return (string)base["COM"]; }
            set { base["COM"] = value; }
        }

        [ConfigurationProperty("BaudRate", IsRequired=false)]
        public string BaudRate
        {
            get { return (string)base["BaudRate"]; }
            set { base["BaudRate"] = value; }
        }
        
        
        [ConfigurationProperty("Parity", IsRequired=false)]
        public string Parity
        {
            get { return (string)base["Parity"]; }
            set { base["Parity"] = value; }
        }
        
        [ConfigurationProperty("StopBits", IsRequired=false)]
        public string StopBits
        {
            get { return (string)base["StopBits"]; }
            set { base["StopBits"] = value; }
        }
        
        [ConfigurationProperty("StopBits", IsRequired=false)]
        public string DataBits
        {
            get { return (string)base["DataBits"]; }
            set { base["DataBits"] = value; }
        }
        
        [ConfigurationProperty("IP", IsRequired=false)]
        public string IP
        {
            get { return (string)base["IP"]; }
            set { base["IP"] = value; }
        }
        
        [ConfigurationProperty("TCPPort", IsRequired=false)]
        public string TCPPort
        {
            get { return (string)base["TCPPort"]; }
            set { base["TCPPort"] = value; }
        }
        
        [ConfigurationProperty("SlaveId", IsRequired=false)]
        public string SlaveId
        {
            get { return (string)base["SlaveId"]; }
            set { base["Slave"] = value; }
        }
    }
}

