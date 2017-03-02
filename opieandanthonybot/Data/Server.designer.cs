﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace opieandanthonybot.Data.Context
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="opieandanthonylive_db")]
	public partial class ServerDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertAnalyzedPost(opieandanthonybot.Data.Domain.AnalyzedPost instance);
    partial void UpdateAnalyzedPost(opieandanthonybot.Data.Domain.AnalyzedPost instance);
    partial void DeleteAnalyzedPost(opieandanthonybot.Data.Domain.AnalyzedPost instance);
    #endregion
		
		public ServerDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ServerDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ServerDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ServerDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<opieandanthonybot.Data.Domain.AnalyzedPost> AnalyzedPosts
		{
			get
			{
				return this.GetTable<opieandanthonybot.Data.Domain.AnalyzedPost>();
			}
		}
	}
}
namespace opieandanthonybot.Data.Domain
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.AnalyzedPosts")]
	public partial class AnalyzedPost : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ScanID;
		
		private string _PostID;
		
		private System.DateTime _AnalysisTimeUTC;
		
		private int _BotAction;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnScanIDChanging(int value);
    partial void OnScanIDChanged();
    partial void OnPostIDChanging(string value);
    partial void OnPostIDChanged();
    partial void OnAnalysisTimeUTCChanging(System.DateTime value);
    partial void OnAnalysisTimeUTCChanged();
    partial void OnBotActionChanging(int value);
    partial void OnBotActionChanged();
    #endregion
		
		public AnalyzedPost()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ScanID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int ScanID
		{
			get
			{
				return this._ScanID;
			}
			set
			{
				if ((this._ScanID != value))
				{
					this.OnScanIDChanging(value);
					this.SendPropertyChanging();
					this._ScanID = value;
					this.SendPropertyChanged("ScanID");
					this.OnScanIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PostID", DbType="VarChar(20) NOT NULL", CanBeNull=false)]
		public string PostID
		{
			get
			{
				return this._PostID;
			}
			set
			{
				if ((this._PostID != value))
				{
					this.OnPostIDChanging(value);
					this.SendPropertyChanging();
					this._PostID = value;
					this.SendPropertyChanged("PostID");
					this.OnPostIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AnalysisTimeUTC", DbType="DateTime NOT NULL")]
		public System.DateTime AnalysisTimeUTC
		{
			get
			{
				return this._AnalysisTimeUTC;
			}
			set
			{
				if ((this._AnalysisTimeUTC != value))
				{
					this.OnAnalysisTimeUTCChanging(value);
					this.SendPropertyChanging();
					this._AnalysisTimeUTC = value;
					this.SendPropertyChanged("AnalysisTimeUTC");
					this.OnAnalysisTimeUTCChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BotAction", DbType="Int NOT NULL")]
		public int BotAction
		{
			get
			{
				return this._BotAction;
			}
			set
			{
				if ((this._BotAction != value))
				{
					this.OnBotActionChanging(value);
					this.SendPropertyChanging();
					this._BotAction = value;
					this.SendPropertyChanged("BotAction");
					this.OnBotActionChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
