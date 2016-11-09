using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;
namespace DirectorySearchProject.Histogram
{
    partial class ChartForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            this.chart1.AntiAliasing = System.Windows.Forms.DataVisualization.Charting.AntiAliasingStyles.None;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.IsSoftShadows = false;
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(-12, -3);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(1014, 417);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            this.chart1.TextAntiAliasingQuality = System.Windows.Forms.DataVisualization.Charting.TextAntiAliasingQuality.Normal;
            // 
            // ChartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1004, 411);
            this.Controls.Add(this.chart1);
            this.Name = "ChartForm";
            this.Text = "Histogram";
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        /// <summary>
        /// Adds information to the chart to visualize the data.
        /// </summary>
        /// <param name="histogram">Data Structure that contains the data to visualize</param>
        public void Visualize(Histogram<string> histogram)
        {
            chart1.Series.Clear();
            int counter = 1;
            foreach (KeyValuePair<string, int> kvp in histogram)
            {
                string key = kvp.Key.ToString();
                chart1.Series.Add(new Series(key));
                DataPoint dataPoint = new DataPoint();
                dataPoint.SetValueXY(counter, kvp.Value);


                chart1.Series[key].Points.Add(dataPoint);
                chart1.Series[key].Name = key;
                chart1.Series[key].ToolTip = key;
                chart1.ChartAreas[0].AxisX.CustomLabels.Add(dataPoint.XValue - .5, dataPoint.XValue + .5, key);
                counter++;
                Refresh();
            }
        }

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
    }
}