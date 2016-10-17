using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace SGKService
{
	
    public class SlaveSettings : ConfigurationSection
    {
        [ConfigurationProperty("", IsDefaultCollection=true)]
        public SlaveParamsSettings SlaveFiles
        {
            get { return ((SlaveParamsSettings)(base[""])); }
        }
    }

    
    public class SlaveParamsSettings : ConfigurationElementCollection 
    {
        const string ELEMENT_NAME = "SlaveFiles";

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
 	        return new Slaves();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
 	        return ((Slaves)element).Id;
        }

    }

    public class Slaves : ConfigurationElement
    {
        const string ID = "id";
        
        [ConfigurationProperty(ID, IsRequired=true)]
        public byte Id 
        {
            get { return Convert.ToByte(base[ID]); }
        }

        [ConfigurationProperty("", IsDefaultCollection = true)]
        public SlaveElementCollection Slave {
            get { return (SlaveElementCollection)base[""]; }
        }

    }

    public class SlaveElementCollection : ConfigurationElementCollection {
        const string ELEMENT_NAME = "slave";

        public override ConfigurationElementCollectionType CollectionType
        {
	        get {   return ConfigurationElementCollectionType.BasicMap;    }
        }

        protected override string ElementName
        {
	        get {   return ELEMENT_NAME;    }
        }

        protected override ConfigurationElement CreateNewElement()
        {
                return new SlaveElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
 	        return ((SlaveElement)element).FilePath;
        }
    }

    public class SlaveElement : ConfigurationElement{
        
        [ConfigurationProperty("type", IsRequired=true)]
        public string Type 
        {
            get {   return (string)base["type"];    }
            set {   base["type"] = value;   }
        }

        [ConfigurationProperty("filepath", IsRequired = true)]
        public string  FilePath
        {
            get { return (string)base["filepath"]; }
            set { base["filepath"] = value; }
        }

        [ConfigurationProperty("source", IsRequired=true)]
        public string Source
        {
            get { return (string)base["source"]; }
            set { base["source"] = value; }
        }
    }
}
