
using System;
using MonoDevelop.Projects;
using MonoDevelop.Projects.Serialization;

namespace MonoDevelop.Deployment.Linux
{
	public class LinuxDeployData
	{
		[ItemProperty (DefaultValue=true)]
		bool generateScript = true;
		
		[ItemProperty]
		string scriptName;
		
		[ItemProperty (DefaultValue=false)]
		bool generateDesktopEntry;
		
		[ItemProperty (DefaultValue=true)]
		bool generatePcFile = true;
		
		CombineEntry entry;
		bool connected;
		
		internal LinuxDeployData (CombineEntry entry)
		{
			this.entry = entry;
		}
		
		internal LinuxDeployData ()
		{
		}
		
		public static LinuxDeployData GetLinuxDeployData (CombineEntry entry)
		{
			LinuxDeployData data = (LinuxDeployData) entry.ExtendedProperties ["Deployment.LinuxDeployData"];
			if (data != null && data.entry == null) {
				data.Bind (entry);
				data.connected = true;
				return data;
			}
			
			data = (LinuxDeployData) entry.ExtendedProperties ["Temp.Deployment.LinuxDeployData"];
			if (data != null)
				return data;
			
			data = CreateDefault (entry);
			entry.ExtendedProperties ["Temp.Deployment.LinuxDeployData"] = data;
			data.Bind (entry);
			return data;
		}
		
		internal static LinuxDeployData CreateDefault (CombineEntry entry)
		{
			LinuxDeployData data = new LinuxDeployData (entry);
			data.ScriptName = entry.Name.ToLower ();
			return data;
		}
		
		void Bind (CombineEntry entry)
		{
			this.entry = entry;
		}
		
		void UpdateEntry ()
		{
			if (connected)
				return;
			entry.ExtendedProperties ["Deployment.LinuxDeployData"] = this;
			entry.ExtendedProperties.Remove ("Temp.Deployment.LinuxDeployData");
			connected = true;
		}
		
		public bool GenerateScript {
			get { return generateScript; }
			set {
				if (generateScript != value) {
					generateScript = value; 
					UpdateEntry ();
				}
			}
		}
		
		public string ScriptName {
			get { return scriptName; }
			set {
				if (scriptName != value) {
					scriptName = value; 
					UpdateEntry ();
				}
			}
		}
		
		public bool GenerateDesktopEntry {
			get { return generateDesktopEntry; }
			set {
				if (generateDesktopEntry != value) {
					generateDesktopEntry = value; 
					UpdateEntry ();
				}
			}
		}
		
		public bool GeneratePcFile {
			get { return generatePcFile; }
			set {
				if (generatePcFile != value) {
					generatePcFile = value; 
					UpdateEntry ();
				}
			}
		}
	}
}
