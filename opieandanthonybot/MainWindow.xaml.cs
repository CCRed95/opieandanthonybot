using System;
using System.Windows;
using System.Windows.Threading;
using opieandanthonybot.Data;
using static opieandanthonybot.Configuration.Config;

namespace opieandanthonybot
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private readonly DispatcherTimer dispatcher;

		public MainWindow()
		{
			InitializeComponent();

			dispatcher = new DispatcherTimer
			{
				Interval = AnalyzerDispatchTime
			};
			dispatcher.Tick += ExecuteAnalyzer;

			dispatcher.Start();
		}

		private void ExecuteAnalyzer(object Sender, EventArgs Args)
		{
			dispatcher.Stop();
			API.I.ScanRecentPosts();
			dispatcher.Start();
		}
	}
}
