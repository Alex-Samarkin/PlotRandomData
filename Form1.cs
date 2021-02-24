using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MathNet;
using MathNet.Numerics;

using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using LiveCharts.WinForms;

namespace PlotRandomData
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // объем выборки
            int NSample = 1000;
            // число для генератора случайных чисел
            int Seed = 0;

            // параметр закона распределения
            double alpha = 0.12;

            // генератор случайных числел по экспоненциальному закону распределения
            // Exp... можно заменить на любой другой из имеющихся, но некоторым из них нужно больше / меньше параметров
            var exp = new MathNet.Numerics.Distributions.Exponential(alpha, new Random(Seed));

            // создаем массив
            double[] data = MathNet.Numerics.Generate.LinearRange(0, 1.0, NSample);
            // заполняем случайными числами
            exp.Samples(data);

            // убираем старый график
            cartesianChart1.Series.Clear();
            // создаем новый
            var scattter = new ScatterSeries()
            {
                // точки, поддерживающие анимацию, лучше NSamples уменьшить до 2000-1000
                Values = new ChartValues<ObservablePoint>(),
                // маркер - ромбик
                PointGeometry = DefaultGeometries.Diamond
            };
            // добавляем серию в график
            cartesianChart1.Series.Add(scattter);

            // насыщаем серию данными в цикле с анимацией каждого добавления
            int i = 0;
            foreach (var d in data)
            {
                scattter.Values.Add(new ObservablePoint(i, d));
                i++;
            }

            // cartesianChart1.Series.Add(scattter);
        }
    }
}
