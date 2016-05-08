using Extensions;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Turle
{
	public partial class MainWindow : Window
	{
		private readonly BackgroundWorker Worker = new BackgroundWorker();

		private readonly BackgroundWorker PrimesWorker = new BackgroundWorker();

		private readonly int n = 10000;

		private int i;

		private int i_for_primes_worker;

		public MainWindow()
		{
			this.InitializeComponent();
			this.Worker.DoWork += new DoWorkEventHandler(this.worker_DoWork);
			this.Worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.worker_RunWorkerCompleted);
			this.Worker.WorkerReportsProgress = true;
			this.Worker.ProgressChanged += new ProgressChangedEventHandler(this.worker_ProgressChanged);
			this.PrimesWorker.DoWork += new DoWorkEventHandler(this.PrimesWorker_DoWork);
			this.PrimesWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.PrimesWorker_RunWorkerCompleted);
			this.PrimesWorker.WorkerReportsProgress = true;
			this.PrimesWorker.ProgressChanged += new ProgressChangedEventHandler(this.PrimesWorker_ProgressChanged);
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if (!this.Worker.IsBusy)
			{
				this.GoButton.IsEnabled = false;
				BackgroundWorker worker = this.Worker;
				double[] actualWidth = new double[] { this.Ground.ActualWidth / 2, this.Ground.ActualHeight / 2 };
				worker.RunWorkerAsync(actualWidth);
			}
		}

		private void PrimesButtonClick(object sender, RoutedEventArgs e)
		{
			if (!this.PrimesWorker.IsBusy)
			{
				this.PrimesButton.IsEnabled = false;
				BackgroundWorker primesWorker = this.PrimesWorker;
				double[] actualWidth = new double[] { this.Ground.ActualWidth, this.Ground.ActualHeight };
				primesWorker.RunWorkerAsync(actualWidth);
			}
		}

		private void PrimesWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			IEnumerable<int> nums = Utilities.ErastotenesSieve(this.n);
			int num = nums.Count<int>();
			foreach (double[] numArray in nums.TurtleWalkOnDifferences(2, (double[])e.Argument))
			{
				this.PrimesWorker.ReportProgress((int)((float)this.i_for_primes_worker / (float)num * 100f), numArray);
				MainWindow iForPrimesWorker = this;
				iForPrimesWorker.i_for_primes_worker = iForPrimesWorker.i_for_primes_worker + 1;
				Thread.Sleep(1);
			}
		}

		private void PrimesWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			double[] userState = (double[])e.UserState;
			Line line = new Line();
			byte[] numArray = Utilities.HSVtoRGB(227 + (double)e.ProgressPercentage * 3.6, 1, 1);
			line.Stroke = new SolidColorBrush(Color.FromArgb(numArray[0], numArray[1], numArray[2], numArray[3]));
			line.StrokeThickness = 2;
			line.X1 = userState[0];
			line.Y1 = userState[1];
			line.X2 = userState[2];
			line.Y2 = userState[3];
			this.Ground.Children.Add(line);
			double num = 200;
			if (this.Ground.Width < line.X1 + num)
			{
				Canvas ground = this.Ground;
				ground.Width = ground.Width + num;
			}
			if (this.Ground.Height < line.Y1 + num)
			{
				Canvas height = this.Ground;
				height.Height = height.Height + num;
			}
		}

		private void PrimesWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this.i_for_primes_worker = 0;
			this.PrimesButton.IsEnabled = true;
		}

		private void worker_DoWork(object sender, DoWorkEventArgs e)
		{
			foreach (double[] numArray in Simulators.BrownianMotion(this.n, 10, (double[])e.Argument))
			{
				this.Worker.ReportProgress((int)((float)this.i / (float)this.n * 100f), numArray);
				Thread.Sleep(3);
				MainWindow mainWindow = this;
				mainWindow.i = mainWindow.i + 1;
			}
		}

		private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			double[] userState = (double[])e.UserState;
			Line line = new Line();
			byte[] numArray = Utilities.HSVtoRGB(227 + (double)e.ProgressPercentage * 3.6, 1, 1);
			line.Stroke = new SolidColorBrush(Color.FromArgb(numArray[0], numArray[1], numArray[2], numArray[3]));
			line.StrokeThickness = 2;
			line.X1 = userState[0];
			line.Y1 = userState[1];
			line.X2 = userState[2];
			line.Y2 = userState[3];
			this.Ground.Children.Add(line);
			double num = 200;
			if (this.Ground.Width < line.X1 + num)
			{
				Canvas ground = this.Ground;
				ground.Width = ground.Width + num;
			}
			if (this.Ground.Height < line.Y1 + num)
			{
				Canvas height = this.Ground;
				height.Height = height.Height + num;
			}
		}

		private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this.GoButton.IsEnabled = true;
			this.i = 0;
		}
	}
}