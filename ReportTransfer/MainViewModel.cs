﻿using System;
using System.ComponentModel;
using System.Windows;

namespace ReportTransfer
{
	public class MainViewModel : INotifyPropertyChanged
	{
		public string SourceRootFolderName
		{
			get { return _sourceRootFolderName; }
			set
			{
				_sourceRootFolderName = value;
				RaisePropertyChanged("SourceRootFolderName");
			}
		}

		public string DestRootFolderName
		{
			get { return _destRootFolderName; }
			set
			{
				_destRootFolderName = value;
				RaisePropertyChanged("SourceRootFolder");
			}
		}


		public Folder SourceRootFolder
		{
			get { return _sourceRootFolder; }
			set
			{
				_sourceRootFolder = value;
				RaisePropertyChanged("SourceRootFolder");
			}
		}

		public string SourceUrl
		{
			get { return _sourceUrl; }
			set
			{
				_sourceUrl = value;
				RaisePropertyChanged("SourceUrl");
			}
		}

		public string DestUrl
		{
			get { return _destUrl; }
			set
			{
				_destUrl = value;
				RaisePropertyChanged("DestUrl");
			}
		}

		public string DestDataSourceName
		{
			get { return _destDataSourceName; }
			set
			{
				_destDataSourceName = value;
				RaisePropertyChanged("DestDataSourceName");
			}
		}

		public string SourceUserName
		{
			get { return _sourceUserName; }
			set
			{
				_sourceUserName = value;
				RaisePropertyChanged("SourceUserName");
			}
		}

		public string SourcePassword
		{
			get { return _sourcePassword; }
			set
			{
				_sourcePassword = value;
				RaisePropertyChanged("SourcePassword");
			}
		}

		public bool UseIntegratedSecurity
		{
			get { return _useIntegratedSecurity; }
			set
			{
				_useIntegratedSecurity = value;
				RaisePropertyChanged("UseIntegratedSecurity");
			}
		}

		private RSCommunicator _communicator;
		private string _sourceRootFolderName;
		private string _destRootFolderName;
		private Folder _sourceRootFolder;
		private string _sourceUrl;
		private string _destUrl;
		private string _destDataSourceName;
		private bool _useIntegratedSecurity;
		private string _sourceUserName;
		private string _sourcePassword;

		public MainViewModel()
		{
			SourceUrl = DestUrl = "http://MACHINENAME/reportserver/reportservice2005.asmx";
			SourceRootFolderName = "";
			DestRootFolderName = "";
			DestDataSourceName = "/Data Sources/DATASOURCE";
			UseIntegratedSecurity = true;
		}

		public void GetSourceData()
		{

			try
			{
				_communicator = new RSCommunicator(SourceUrl, SourceUserName, SourcePassword);
				RSRepository repo = new RSRepository(_communicator, null);
				SourceRootFolder = repo.GetExistingItems(SourceRootFolderName);
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message, "Error");

			}
		}

		public void ClearDestination()
		{
			_communicator = new RSCommunicator(DestUrl);
			_communicator.DeleteAllReports(DestRootFolderName);
		}

		public void MoveReports()
		{
			try
			{
				RSRepository repo = new RSRepository(new RSCommunicator(SourceUrl, SourceUserName, SourcePassword), new RSCommunicator(DestUrl));
				repo.UploadReports(SourceRootFolder, DestRootFolderName, DestDataSourceName);
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message, "Error");
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		protected void RaisePropertyChanged(string propertyName)
		{
			if (string.IsNullOrEmpty(propertyName))
			{
				throw new ArgumentNullException("propertyName");
			}

			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

	}
}