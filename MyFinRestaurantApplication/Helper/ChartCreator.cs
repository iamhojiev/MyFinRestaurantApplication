using Guna.Charts.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using static Google.Protobuf.Reflection.UninterpretedOption.Types;

namespace ManagerApplication.Helper
{
    public class ChartCreator
    {
        public bool CheckEmpty(DataTable data)
        {
            return data.Rows.Count > 0;
        }

        public void ChartPie(GunaChart chart, Dictionary<string, double> data,  string label)
        {
            chart.Datasets.Clear();
            //chart.Title.Text = nameChart;

            var dataset = new GunaPieDataset()
            {
                Label = label
            };

            foreach (var kvp in data)
            {
                dataset.DataPoints.Add(kvp.Key, kvp.Value);
            }

            chart.Datasets.Add(dataset);
        }

        public void ChartLine(GunaChart chart, Dictionary<string, double> data, string label)
        {
            chart.Datasets.Clear();
            //chart.Title.Text = nameChart;

            //Chart configuration 
            chart.YAxes.GridLines.Display = false;
            chart.XAxes.GridLines.Display = true;
            //Create a new dataset 
            var dataset = new GunaLineDataset
            {
                Label = label,
                PointRadius = 10,
                PointStyle = PointStyle.Circle
            };


            foreach (var kvp in data)
            {
                dataset.DataPoints.Add(kvp.Key, kvp.Value);
            }

            chart.Datasets.Add(dataset);
        }

        public void ChartBar(GunaChart chart, Dictionary<string, double> data, string label)
        {
            chart.Datasets.Clear();
            //chart.Title.Text = nameChart;
            chart.YAxes.GridLines.Display = false;
            chart.XAxes.GridLines.Display = true;
            var dataset = new GunaBarDataset
            {
                Label = label
            };
            foreach (var kvp in data)
            {
                dataset.DataPoints.Add(kvp.Key, kvp.Value);
            }

            chart.Datasets.Add(dataset);
        }

        public void ChartHorizontalBar(GunaChart chart, Dictionary<string, double> data, string label)
        {
            chart.Datasets.Clear();
            chart.XAxes.GridLines.Display = false;
            chart.YAxes.GridLines.Display = true;
            //chart.Title.Text = nameChart;

            var dataset = new GunaHorizontalBarDataset
            {
                Label = label
            };
            foreach (var kvp in data)
            {
                dataset.DataPoints.Add(kvp.Key, kvp.Value);
            }

            chart.Datasets.Add(dataset);
        }

        public void ChartPolar(GunaChart chart, Dictionary<string, double> data, string label)
        {
            chart.Datasets.Clear();
            var dataset = new GunaPolarAreaDataset
            {
                Label = label
            };
            foreach (var kvp in data)
            {
                dataset.DataPoints.Add(kvp.Key, kvp.Value);
            }

            chart.Datasets.Add(dataset);
        }
    }
}
